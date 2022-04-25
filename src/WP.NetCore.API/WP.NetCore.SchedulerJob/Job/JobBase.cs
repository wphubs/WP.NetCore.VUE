using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Common.Helper;
using WP.NetCore.IServices;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.SchedulerJob.Job
{
    public abstract class JobBase
    {
        private readonly IScheduleJobService scheduleJobService;

        public JobBase(IScheduleJobService scheduleJobService)
        {
            this.scheduleJobService = scheduleJobService;
        }

        private const string EmailTitle = "[Error]WP.SchedulerJob";

        public async Task Execute(IJobExecutionContext context)
        {
            var template = "JobId:{@JobId},JobGroup:{@JobGroup},JobName:{@JobName},Time:{@Time},Result:{@Result},Message:{@Message}";
            string jobId = string.Empty;
            string jobName = string.Empty;
            string jobGroup = string.Empty;
            Stopwatch stopwatch = new Stopwatch();
            ScheduleJob scheduleJob = null; 
            try
            {
                stopwatch.Start();
                jobId = context.JobDetail.JobDataMap.GetString(JobConstant.JobId);
                jobName = context.JobDetail.Key.Name;
                jobGroup = context.JobDetail.Key.Group;
                scheduleJob = await scheduleJobService.FirstNoTrackingAsync(Convert.ToInt64(jobId));
                var result = await ExecuteJob(context);
                if (scheduleJob != null)
                {
                    scheduleJob.ExecTimes++;
                    await scheduleJobService.UpdateAsync(scheduleJob);
                    stopwatch.Stop();
                    Log.ForContext<JobBase>().Information(template, jobId, jobGroup, jobName, stopwatch.Elapsed.TotalMilliseconds.ToString("0.00"), result.Result, result.Msg);
                }
                if (!result.Result)
                {
                    EmailHelper.SendEmail(EmailTitle, result.Msg);
                }
            }
            catch (Exception ex)
            {

                stopwatch.Stop();
                EmailHelper.SendEmail(EmailTitle, GetExceptionDetail(scheduleJob, ex));
                Log.ForContext<JobBase>().Error(ex,template, jobId, jobGroup, jobName, stopwatch.Elapsed.TotalMilliseconds.ToString("0.00"), false, ex.Message);
            }
    
        }
        public abstract Task<(bool Result, string Msg)> ExecuteJob(IJobExecutionContext context);

        private string GetExceptionDetail(ScheduleJob job, Exception exception)
        {
            var detail = new StringBuilder();
            if (job != null)
            {
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 任务ID： {0}  </p>", job.Id);
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 任务名： {0}  </p>", job.JobName);
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 任务组： {0}  </p>", job.JobGroup);
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 任务类型： {0}  </p>", job.JobType);
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 请求类型： {0}  </p>", job.RequestType);
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 请求地址： {0}  </p>", job.RequestUrl);
            }
            detail.AppendFormat(@"<p> 异常时间： {0} </p>", DateTime.Now);
            detail.AppendFormat(@"<p> 异常类型： {0}  </p>", exception.HResult);
            detail.AppendFormat(@"<p> Exception实例： {0}  </p>", exception.InnerException);
            detail.AppendFormat(@"<p> 对象名称： {0} </p>", exception.Source);
            detail.AppendFormat(@"<p> 异常方法： {0}  </p>", exception.TargetSite);
            detail.AppendFormat(@"<p> 堆栈信息： {0}  </p>", exception.StackTrace);
            detail.AppendFormat(@"<p> 异常消息： {0}  </p>", exception.Message);
            return detail.ToString();
        }

    }
}
