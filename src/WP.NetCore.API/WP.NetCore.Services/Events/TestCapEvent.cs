using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.EventBus;

namespace WP.NetCore.Services.Events
{

    public sealed class TestUserCapEvent : BaseEvent<TestUserCapEvent.EventData>
    {
        public TestUserCapEvent(long id, EventData eventData, string source)
            : base(id, eventData, source)
        {
        }

        public class EventData
        {
            public long UserId { get; set; }

            public string UserName { get; set; }
        }
    }
}
