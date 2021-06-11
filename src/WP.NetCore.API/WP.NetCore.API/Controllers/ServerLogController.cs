using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerLogController : BaseController
    {
        private readonly IRequestLogService requestLogService;

        public ServerLogController(IRequestLogService requestLogService)
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
        public async Task<ResponseResult> GetRequestLog(int pageIndex,int pageSize) 
        {
            var log = await requestLogService.GetPageListAsync(pageIndex,pageSize);
            return new ResponseResult().Success(log);
        }
    }
}
