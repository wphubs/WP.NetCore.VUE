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
using WP.NetCore.Common.Helper;
using WP.NetCore.Model;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IWebHostEnvironment env;

        public HomeController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet("GetServerInfo")]
        public ResponseResult GetServerInfo()
        {
            var objSysData =new  {
                Platform = RuntimeInformation.OSDescription.ToString(),
                EnvironmentName = env.EnvironmentName,
                OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                MemoryFootprint = (Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + " MB",
                WorkingTime = DateHelper.TimeSubTract(DateTime.Now, Process.GetCurrentProcess().StartTime)
            };
            return new ResponseResult().Success(objSysData);
        }

    }
}
