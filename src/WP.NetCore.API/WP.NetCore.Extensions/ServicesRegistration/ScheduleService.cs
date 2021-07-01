using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.SchedulerJob;
using WP.NetCore.SchedulerJob.Job;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class ScheduleService
    {
        public static void AddScheduleJob(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, SchedulerJobFactory>();
            services.AddTransient<HttpJob>();//Job使用瞬时依赖注入
            services.AddSingleton<ISchedulerCenter, SchedulerCenter>();
        }
    }
  
}
