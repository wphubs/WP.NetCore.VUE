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
        public class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status200OK;
            }
        }
  
    }
}
