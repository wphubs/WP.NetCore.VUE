using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model;

namespace WP.NetCore.Extensions
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly IDiagnosticContext diagnosticContext;

        public GlobalExceptionsFilter(IWebHostEnvironment env, IDiagnosticContext diagnosticContext)
        {
            this.env = env;
            this.diagnosticContext = diagnosticContext;
        }
        public void OnException(ExceptionContext context)
        {
            diagnosticContext.Set("ExceptionFilter", $"【异常类型】：{context.Exception.GetType().Name}<br/>【异常信息】：{context.Exception.Message} <br/>【堆栈调用】：{context.Exception.StackTrace }");
            if (env.IsDevelopment())
            {
                context.Result = new InternalServerErrorObjectResult($"{context.Exception.Message}");
            }
            else 
            {
                context.Result = new InternalServerErrorObjectResult($"服务器异常");
            }

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
