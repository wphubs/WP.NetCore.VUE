using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WP.NetCore.Common.Consts;
using WP.NetCore.Common.Helper;
using WP.NetCore.EventBus;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Services.Events;
using WP.NetCore.Services.EventSubscribers;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : BaseController
    {
        private readonly IWebHostEnvironment env;
        private readonly RabbitMQProducer rabbitMQProducer;
        private readonly IEventPublisher eventPublisher;
        private readonly ICapPublisher capPublisher;

        public HomeController(IWebHostEnvironment env, 
            RabbitMQProducer rabbitMQProducer, 
            IEventPublisher eventPublisher,
            ICapPublisher capPublisher
         )
        {
            this.env = env;
            this.rabbitMQProducer = rabbitMQProducer;
            this.eventPublisher = eventPublisher;
            this.capPublisher = capPublisher;
        }


        //[HttpGet]
        //public async Task Get()
        //{
        //    var id = new Snowflake().NextId();
        //    var tData = new TestCapEvent.EventData() { UserId = 11111, UserName = "管理员" };
        //    var source = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
        //    await eventPublisher.PublishAsync(new TestCapEvent(id, tData, source));
        //    //rabbitMQProducer.BasicPublish(MQExchange.Logs, MQRoutingKeys.Logs, new User() { UserName="测试测试",Id=11111111});
        //}


        [Authorize]
        [HttpGet("GetServerInfo")]
        public ActionResult GetServerInfo()
        {
            var objSysData =new  {
                Platform = RuntimeInformation.OSDescription.ToString(),
                EnvironmentName = env.EnvironmentName,
                OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                MemoryFootprint = (Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + " MB",
                WorkingTime = DateHelper.TimeSubTract(DateTime.Now, Process.GetCurrentProcess().StartTime)
            };
            return Ok(objSysData);
        }

    }
}
