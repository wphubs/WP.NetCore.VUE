using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common.Consts;
using WP.NetCore.EventBus;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.Services.Subscriber
{
    public sealed class TestMQConsumer:BaseRabbitMQConsumer
    {
        private readonly IServiceProvider _services;

        public TestMQConsumer(IOptionsSnapshot<RabbitMQConfig> options, IServiceProvider services)
            : base(options)
        {
            _services = services;
        }


        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="exchage">交换机</param>
        /// <param name="routingKey">路由Key</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        protected async override Task<bool> Process(string exchage, string routingKey, string message)
        {
            bool result = false;
            try
            {
                using (var scope = _services.CreateScope())
                {
                    await Task.Run(() => { Console.WriteLine("收到消息：" + message); });
                    //var repository = scope.ServiceProvider.GetRequiredService<IMongoRepository<SysLoginLog>>();
                    var entity = JsonConvert.DeserializeObject<User>(message);
                    //await repository.AddAsync(entity);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
            }
            return result;
        }




        /// <summary>
        /// 配置Exchange
        /// </summary>
        /// <returns></returns>
        protected override ExchageConfig GetExchageConfig()
        {
            return new ExchageConfig()
            {
                Name = MQExchange.Logs
                ,
                Type = ExchangeType.Direct
                ,
                DeadExchangeName = MQExchange.Dead
            };
        }

        /// <summary>
        /// 设置路由Key
        /// </summary>
        /// <returns></returns>
        protected override string[] GetRoutingKeys()
        {
            return new[] { MQRoutingKeys.Logs };
        }

        /// <summary>
        /// 配置队列
        /// </summary>
        /// <returns></returns>
        protected override QueueConfig GetQueueConfig()
        {
            var config = GetCommonQueueConfig();

            config.Name = "q-adnc-maint-loginlog";
            config.AutoAck = false;
            config.PrefetchCount = 5;
            config.Arguments = new Dictionary<string, object>()
                  {
                     //设置当前队列的DLX
                    { "x-dead-letter-exchange",MQExchange.Dead} 
                    //设置DLX的路由key，DLX会根据该值去找到死信消息存放的队列
                    ,{ "x-dead-letter-routing-key",MQRoutingKeys.Logs}
                    //设置消息的存活时间，即过期时间(毫秒)
                    ,{ "x-message-ttl",1000*60}
                  };
            return config;
        }

    }
}
