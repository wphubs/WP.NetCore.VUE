using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.SchedulerJob;

namespace WP.NetCore.Extensions.Middleware
{
    public static class ScheduleJobMiddleware
    {
        public static void UseScheduleJob(this IApplicationBuilder app, IScheduleJobService scheduleJobService, ISchedulerCenter schedulerCenter)
        {
            var listJob = scheduleJobService.GetAllAsync().Result;
            listJob.ToList().ForEach(item => 
            {
                if (item.IsStart)
                {
                    try
                    {
                        schedulerCenter.AddScheduleJobAsync(item);
                        Console.WriteLine($"ScheduleJob[{item.JobName}] Start");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ScheduleJob[{item.JobName}] Fail ,Error:{ex.ToString()}");
                    }
                }
            });
        }
    }
}
