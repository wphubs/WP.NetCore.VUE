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

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : BaseController
    {
        private readonly IWebHostEnvironment env;
        private readonly RabbitMQProducer rabbitMQProducer;

        public HomeController(IWebHostEnvironment env, RabbitMQProducer rabbitMQProducer)
        {
            this.env = env;
            this.rabbitMQProducer = rabbitMQProducer;
        }


        [HttpGet]
        public void Get()
        {
            rabbitMQProducer.BasicPublish(MQExchange.Logs, MQRoutingKeys.Logs, new User() { UserName="测试测试",Id=11111111});
        }

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
