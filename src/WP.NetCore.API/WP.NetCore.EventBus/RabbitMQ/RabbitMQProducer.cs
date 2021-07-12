using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WP.NetCore.EventBus
{
    public class RabbitMQProducer : IDisposable
    {
        private readonly IModel _channel;


        public RabbitMQProducer(IOptionsSnapshot<RabbitMQConfig> options )
        {
            _channel = RabbitMQConnection.GetInstance(options).Connection.CreateModel();
        }


        public virtual void BasicPublish<TMessage>(string exchange, string routingKey, TMessage message, IBasicProperties properties = null, bool mandatory = false)
        {
            Policy.Handle<Exception>()
                  .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(1), (ex, time, retryCount, content) =>
                  {
                      //_logger.LogError(ex, string.Format("{0}:{1}", retryCount, ex.Message));
                  })
                  .Execute(() =>
                  {
                      var content = message as string;
                      if (content == null)
                          content = JsonSerializer.Serialize(message);

                      var body = Encoding.UTF8.GetBytes(content);
                      //当mandatory标志位设置为true时，如果exchange根据自身类型和消息routingKey无法找到一个合适的queue存储消息
                      //那么broker会调用basic.return方法将消息返还给生产者;
                      //当mandatory设置为false时，出现上述情况broker会直接将消息丢弃

                      _channel.BasicPublish(exchange, routingKey, mandatory, basicProperties: properties, body);

                      //开启确认模式
                      //_channel.ConfirmSelect();
                      //消息是否到达服务器
                      //bool publishStatus = _channel.WaitForConfirms();
                  });
        }

        public virtual IBasicProperties CreateBasicProperties()
        {
            return _channel.CreateBasicProperties();
        }

        public void Dispose()
        {
            if (_channel != null)
                _channel.Dispose();
        }
    }
}
