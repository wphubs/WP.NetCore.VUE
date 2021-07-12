using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.EventBus.Cap;
using WP.NetCore.Services.Events;

namespace WP.NetCore.Services.EventSubscribers
{
    public class TestCapSubscriber : CapSubscriber
    {
        private readonly UserService userService;

        public TestCapSubscriber(UserService userService)
        {
            this.userService = userService;
        }

        [CapSubscribe(nameof(TestCapEvent))]
        public async Task Process(Events.TestCapEvent eto)
        {
            string aa="";
            await Task.CompletedTask;
        }
    }
}
