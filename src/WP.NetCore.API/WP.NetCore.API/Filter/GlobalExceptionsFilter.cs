using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model;

namespace WP.NetCore.API.Filter
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDiagnosticContext diagnosticContext;

        public GlobalExceptionsFilter(IWebHostEnvironment env, IDiagnosticContext diagnosticContext)
        {
            _env = env;
            this.diagnosticContext = diagnosticContext;
        }
        public void OnException(ExceptionContext context)
        {
            diagnosticContext.Set("ExceptionFilter", context.Exception.Message);
            context.Result = new InternalServerErrorObjectResult(new ResponseResult().Error($"服务器异常:{context.Exception.Message}"));

       
        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }

        public class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status200OK;
            }
        }
  
    }
}
