using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Stopwatch stopwatch = new Stopwatch();
            var jobName = context.JobDetail.Key.Name;
            var jobGroup = context.JobDetail.Key.Group;

            await ExecuteJob(context);
        }

        public abstract Task ExecuteJob(IJobExecutionContext context);

     
    }
}
