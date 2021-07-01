using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WP.NetCore.SchedulerJob.HostedService
{
    public class TestHostedService : IHostedService
    {
        private Timer timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("TestHostedService Start");
            timer = new Timer((state)=> 
            {
                Console.WriteLine($"{DateTime.Now}:TestHostedService Timer");
            }, null, TimeSpan.Zero,
              TimeSpan.FromSeconds(600));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
