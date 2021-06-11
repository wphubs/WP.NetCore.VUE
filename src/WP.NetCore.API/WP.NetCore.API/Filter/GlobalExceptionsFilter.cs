using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WP.NetCore.API.Filter
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        public GlobalExceptionsFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            //var json = new JsonErrorResponse();
            //json.Message = context.Exception.Message;//错误信息
            //if (_env.IsDevelopment())
            //{
            //    json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            //}
            //context.Result = new InternalServerErrorObjectResult(json);

            string aa = context.Exception.Message;
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
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
  
    }
}
