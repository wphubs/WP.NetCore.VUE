using DotNetCore.CAP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using WP.NetCore.EventBus;
using WP.NetCore.Services.Events;

namespace WP.NetCore.Services.EventSubscribers
{

    public class TestCapSubscriber: ICapSubscribe
    {
        [CapSubscribe("WP.NetCore.Services.Events.TestCapEvent")]
        public async Task TestMethod(string strJson) 
        {
            var eventData = JsonConvert.DeserializeObject<BaseEvent<TestCapEvent.EventData>>(strJson);
            await Task.CompletedTask;
        }
    }
}
