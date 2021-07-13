using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WP.NetCore.EventBus.Cap
{
    public class CapPublisher : IEventPublisher
    {
        private readonly ICapPublisher _eventBus;

        public CapPublisher(ICapPublisher capPublisher)
        {
            _eventBus = capPublisher;
        }

        public virtual async Task PublishAsync<T>(T eventObj, string callbackName = null, CancellationToken cancellationToken = default(CancellationToken))
            where T : IEvent
        {
            await _eventBus.PublishAsync(typeof(T).FullName, JsonSerializer.Serialize(eventObj), callbackName, cancellationToken);
        }

        public virtual async Task PublishAsync<T>(T eventObj, IDictionary<string, string> headers, CancellationToken cancellationToken = default(CancellationToken))
            where T : IEvent
        {
            await _eventBus.PublishAsync<T>(typeof(T).Name, eventObj, headers, cancellationToken);
        }

        public virtual void Publish<T>(T eventObj, string callbackName = null)
            where T : IEvent
        {
            _eventBus.Publish(typeof(T).Name, eventObj, callbackName);
        }

        public virtual void Publish<T>(T eventObj, IDictionary<string, string> headers)
            where T : IEvent
        {
            _eventBus.Publish(typeof(T).Name, eventObj, headers);
        }
    }
}
