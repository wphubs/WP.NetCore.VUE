using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.IServices;

namespace WP.NetCore.SchedulerJob.Job
{
    public abstract class JobBase
    {
        private readonly IScheduleJobService scheduleJobService;

        public JobBase(IScheduleJobService scheduleJobService)
        {
            this.scheduleJobService = scheduleJobService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var template = "JobId:{@JobId},JobGroup:{@JobGroup},JobName:{@JobName},Time:{@Time},Result:{@Result},Message:{@Message}";
            string jobId = string.Empty;
            string jobName = string.Empty;
            string jobGroup = string.Empty;
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                jobId = context.JobDetail.JobDataMap.GetString(JobConstant.JobId);
                jobName = context.JobDetail.Key.Name;
                jobGroup = context.JobDetail.Key.Group;
                var objJob = await scheduleJobService.FirstNoTrackingAsync(Convert.ToInt64(jobId));
                var result = await ExecuteJob(context);
                if (objJob != null)
                {
                    objJob.ExecTimes++;
                    await scheduleJobService.UpdateAsync(objJob);
                    stopwatch.Stop();
                    Log.ForContext<JobBase>().Information(template, jobId, jobGroup, jobName, stopwatch.Elapsed.TotalMilliseconds.ToString("0.00"), result.Result, result.Msg);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Log.ForContext<JobBase>().Error(ex,template, jobId, jobGroup, jobName, stopwatch.Elapsed.TotalMilliseconds.ToString("0.00"), false, ex.Message);
            }
    
        }

        public abstract Task<(bool Result, string Msg)> ExecuteJob(IJobExecutionContext context);

     
    }
}
