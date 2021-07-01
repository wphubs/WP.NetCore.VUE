using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;

namespace WP.NetCore.Extensions.Middleware
{
    public static class SwaggerMiddleware
    {
        public static void UseSwaggerMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var APIName = Appsettings.app(new string[] { "APIName" });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                typeof(ApiVersions).GetEnumNames().OrderBy(e => e).ToList().ForEach(version =>
                {
                    if (env.IsDevelopment())
                    {
                        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{APIName} {version}");
                    }
                    else
                    {
                        c.SwaggerEndpoint($"/netcore/swagger/{version}/swagger.json", $"{APIName} {version}");
                    }

                });
                c.RoutePrefix = "";
            });
        }
    }
}
