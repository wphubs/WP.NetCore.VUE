using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServerLogController : BaseController
    {
        private readonly IServerLogService requestLogService;

        public ServerLogController(IServerLogService requestLogService)
        {
            this.requestLogService = requestLogService;
        }

        /// <summary>
        /// 获取请求日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetRequestLog")]
        public async Task<ActionResult<PageModel<RequestLogViewModel>>> GetRequestLog(int pageIndex,int pageSize) 
        {
            ;   //TODO：改为本地文件方式
            var log = await requestLogService.GetRequestLogPageAsync(pageIndex,pageSize);
            return Ok(log);
        }


        [HttpGet("GetJobLog")]
        public async Task<ActionResult<PageModel<JobLogViewModel>>> GetJobLog(int pageIndex, int pageSize)
        {
            //TODO：改为本地文件方式
            var log = await requestLogService.GetJobLogPageAsync(pageIndex, pageSize);
            return Ok(log);
        }

    }
}
