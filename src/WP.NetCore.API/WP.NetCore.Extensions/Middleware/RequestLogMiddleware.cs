using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model;
using Microsoft.Extensions.DependencyInjection;

namespace WP.NetCore.Extensions.Middleware
{
    public static class RequestLogMiddleware
    {
        public static void UseRequestLogMiddleware(this IApplicationBuilder app)
        {
            
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "Handled {RequestPath}";
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var request = httpContext.Request;
                    diagnosticContext.Set("RequestHost", request.Host.Value);
                    diagnosticContext.Set("RequestScheme", request.Scheme);
                    diagnosticContext.Set("Agent", request.Headers["User-Agent"].ToString());
                    var client = request.Headers["X-Forwarded-For"].FirstOrDefault();
                    if (string.IsNullOrEmpty(client))
                    {
                        client = httpContext.Connection.RemoteIpAddress.ToString();
                    }
                    diagnosticContext.Set("IP", client);
                    var userContext = httpContext.Request.HttpContext.RequestServices.GetService<IUserContext>();
                    diagnosticContext.Set("UserId", userContext.Id);
                    diagnosticContext.Set("UserName", userContext.Name);
                };
            });
        }
    }
}
