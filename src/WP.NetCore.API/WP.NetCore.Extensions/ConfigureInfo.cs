using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Extensions.Middleware;

namespace WP.NetCore.Extensions
{
    public static class ConfigureInfo
    {
        ///
        public static void UseConfigureMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerMiddleware(env);

            app.UseRequestLogMiddleware();

            //添加Cors跨域
            app.UseCors("CorsPolicy");
            //限流
            app.UseIpRateLimiting();
            // 使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();
            // Routing
            app.UseRouting();
            // 开启认证
            app.UseAuthentication();
            // 开启授权中间件
            app.UseAuthorization();
        }
    }
}
