using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.EventBus
{
    public class RabbitMQConnection
    {
        private static volatile RabbitMQConnection _uniqueInstance;
        private static readonly object _lockObject = new object();

        public IConnection Connection { get; private set; }

        public static RabbitMQConnection GetInstance(IOptionsSnapshot<RabbitMQConfig> options)
        {
            if (_uniqueInstance == null || _uniqueInstance.Connection == null || _uniqueInstance.Connection.IsOpen == false)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance == null || _uniqueInstance.Connection == null || _uniqueInstance.Connection.IsOpen == false)
                    {
                        _uniqueInstance = new RabbitMQConnection(options.Value);
                    }
                }
            }
            return _uniqueInstance;
        }
        private RabbitMQConnection(RabbitMQConfig mqOption)
        {
            var factory = new ConnectionFactory()
            {
                HostName = mqOption.HostName,
                //VirtualHost = mqOption.VirtualHost,
                UserName = mqOption.UserName,
                Password = mqOption.Password,
                Port = mqOption.Port
            };

            Policy.Handle<SocketException>()
                  .Or<BrokerUnreachableException>()
                  .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(1), (ex, time, retryCount, content) =>
                  {
                      if (3 == retryCount)
                          throw ex;
                      //_logger.LogError(ex, string.Format("{0}:{1}", retryCount, ex.Message));
                  })
                  .Execute(() =>
                  {
                      Connection = factory.CreateConnection();
                  });
        }
    }
}
