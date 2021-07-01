using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WP.NetCore.SchedulerJob
{
    public class SchedulerJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SchedulerJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var serviceScope = _serviceProvider.CreateScope();
            return serviceScope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;

        }
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
