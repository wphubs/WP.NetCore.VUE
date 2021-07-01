using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Common.Enums;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.SchedulerJob.Job;

namespace WP.NetCore.SchedulerJob
{
    public class SchedulerCenter : ISchedulerCenter
    {
        private IScheduler scheduler;
        private readonly IJobFactory jobFactory;
        public SchedulerCenter(IJobFactory jobFactory)
        {
            this.jobFactory = jobFactory;
            scheduler = GetSchedulerAsync().Result;
        }


        private async Task<IScheduler> GetSchedulerAsync()
        {
            if (scheduler != null)
                return this.scheduler;
            else
            {
                NameValueCollection collection = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" },
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(collection);
                return scheduler = await factory.GetScheduler();
            }
        }


        /// <summary>
        /// 开启任务调度
        /// </summary>
        /// <returns></returns>
        public async Task StartScheduleAsync()
        {
            this.scheduler.JobFactory = this.jobFactory;
            if (!this.scheduler.IsStarted)
            {
                await this.scheduler.Start();
                await Console.Out.WriteLineAsync("任务调度已开启");
            }
        }


        /// <summary>
        /// 停止任务调度 
        /// </summary>
        /// <returns></returns>
        public async Task StopScheduleAsync()
        {
            if (!this.scheduler.IsShutdown)
            {
                await this.scheduler.Shutdown();
                await Console.Out.WriteLineAsync("任务调度已停止");
            }
        }

      


        /// <summary>
        /// 创建任务
        /// </summary>
        /// <param name="scheduleJob"></param>
        /// <returns></returns>
        public async Task AddScheduleJobAsync(ScheduleJob scheduleJob)
        {
            JobKey jobKey = new JobKey(scheduleJob.JobName, scheduleJob.JobGroup);
            if (await scheduler.CheckExists(jobKey))
            {
                throw new Exception ("任务已存在");
            }
            DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(scheduleJob.BeginTime, 1);//设置开始时间
            if (scheduleJob.EndTime == null)
            {
                scheduleJob.EndTime = DateTime.MaxValue.AddDays(-1);
            }
            DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(scheduleJob.EndTime, 1);//设置暂停时间
            //判断任务调度是否开启
            if (!scheduler.IsStarted)
            {
                await StartScheduleAsync();
            }
            var httpDir = new Dictionary<string, string>()
                {
                    { Constant.EndAt, scheduleJob.EndTime.ToString()},
                    { Constant.JobTypeEnum, ((int)scheduleJob.JobType).ToString()},
                };
            IJobConfigurator jobConfigurator = null;
            if (scheduleJob.JobType == JobTypeEnum.Url)
            {
                jobConfigurator = JobBuilder.Create<HttpJob>();
                httpDir.Add(Constant.REQUESTURL, scheduleJob.RequestUrl);
                httpDir.Add(Constant.HEADERS, scheduleJob.Headers);
                httpDir.Add(Constant.REQUESTPARAMETERS, scheduleJob.RequestParameters);
                httpDir.Add(Constant.REQUESTTYPE, ((int)scheduleJob.RequestType).ToString());
            }
            IJobDetail job = jobConfigurator
                .SetJobData(new JobDataMap(httpDir))
                .WithDescription(scheduleJob.Description)
                .WithIdentity(scheduleJob.JobName, scheduleJob.JobGroup)
                .Build();// 创建触发器
            ITrigger trigger;
            if (scheduleJob.TriggerType == TriggerTypeEnum.Cron)//CronExpression.IsValidExpression(entity.Cron))
            {
                trigger = CreateCronTrigger(scheduleJob);
            }
            else
            {
                trigger = CreateSimpleTrigger(scheduleJob);
            }
            await scheduler.ScheduleJob(job, trigger);
        }


        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="scheduleJob"></param>
        /// <returns></returns>
        public async Task PauseJobAsync(string jobGroup, string jobName)
        {
            await scheduler.PauseJob(new JobKey(jobName,jobGroup));
        }


        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public async Task DeleteJobAsync(string jobGroup, string jobName)
        {
            await scheduler.DeleteJob(new JobKey(jobName, jobGroup));
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public async Task TriggerJobAsync(string jobGroup, string jobName)
        {
            await scheduler.TriggerJob(new JobKey(jobName, jobGroup));
        }



        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="scheduleJob"></param>
        /// <returns></returns>
        public async Task ResumeJobAsync(string jobGroup, string jobName)
        {
            var jobKey = new JobKey(jobName, jobGroup);
            if (await scheduler.CheckExists(jobKey))
            {
                var jobDetail = await scheduler.GetJobDetail(jobKey);
                var endTime = jobDetail.JobDataMap.GetString("EndAt");
                if (!string.IsNullOrWhiteSpace(endTime) && DateTime.Parse(endTime) <= DateTime.Now)
                {
                    throw new Exception("任务已过期");
                }
                else
                {
                    await scheduler.ResumeJob(jobKey);
                }
            }
            else
            {
                throw new Exception("任务不存在");
            }
        }




        /// <summary>
        /// 创建类型Simple的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateSimpleTrigger(ScheduleJob entity)
        {
            //作业触发器
            if (entity.SimpleTimes.HasValue && entity.SimpleTimes > 0)
            {
                return TriggerBuilder.Create()
               .WithIdentity(entity.JobName, entity.JobGroup)
               .StartAt(entity.BeginTime)//开始时间
               .EndAt(entity.EndTime)//结束数据
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInSeconds(entity.IntervalSecond.Value)//执行时间间隔，单位秒
                        .WithRepeatCount(entity.SimpleTimes.Value)//执行次数、默认从0开始
                        .WithMisfireHandlingInstructionFireNow();
               })
               .ForJob(entity.JobName, entity.JobGroup)//作业名称
               .Build();
            }
            else
            {
                return TriggerBuilder.Create()
               .WithIdentity(entity.JobName, entity.JobGroup)
               .StartAt(entity.BeginTime)//开始时间
               .EndAt(entity.EndTime)//结束数据
               .WithSimpleSchedule(x =>
               {
                   x.WithIntervalInSeconds(entity.IntervalSecond.Value)//执行时间间隔，单位秒
                        .RepeatForever()//无限循环
                        .WithMisfireHandlingInstructionFireNow();
               })
               .ForJob(entity.JobName, entity.JobGroup)//作业名称
               .Build();
            }

        }

        /// <summary>
        /// 创建类型Cron的触发器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ITrigger CreateCronTrigger(ScheduleJob entity)
        {
            // 作业触发器
            return TriggerBuilder.Create()

                   .WithIdentity(entity.JobName, entity.JobGroup)
                   .StartAt(entity.BeginTime)//开始时间
                    .EndAt(entity.EndTime)//结束时间
                   .WithCronSchedule(entity.Cron, cronScheduleBuilder => cronScheduleBuilder.WithMisfireHandlingInstructionFireAndProceed())//指定cron表达式
                   .ForJob(entity.JobName, entity.JobGroup)//作业名称
                   .Build();
        }
    }
}
