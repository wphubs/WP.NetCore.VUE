using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class CorsPolicyService
    {
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // 配置策略
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .WithOrigins(Appsettings.app(new string[] { "Cors" }).Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
                //无限制 所有客户端都能访问
                //options.AddPolicy("LimitRequests", policy => 
                //policy.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
        }
    }
}
