using AspNetCoreRateLimit;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Text;
using WP.NetCore.Common;
using WP.NetCore.Common.Helper;
using WP.NetCore.EventBus;
using WP.NetCore.Extensions.ServicesRegistration;
using WP.NetCore.SchedulerJob.HostedService;
using WP.NetCore.Services.EventSubscribers;

namespace WP.NetCore.Extensions
{

    public static class ServiceInfo
    {
        public static void AddConfigureServices(this IServiceCollection services, IConfiguration Configuration, IWebHostEnvironment Env)
        {
            var basePath = AppContext.BaseDirectory;
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            services.AddRedisCache();
            services.AddSingleton<HttpHelper>();
            services.AddIpRateLimit(Configuration);
            services.AddEFCore();
            services.AddSqlSugar();
            services.AddAutoMapper(typeof(ServiceInfo));
            services.AddSwagger();
            services.AddScheduleJob();
            services.AddCorsPolicy();
            services.AddJwtAuthentication();
            services.AddHealthChecks().AddMySql(Appsettings.app(new string[] { "DBConnection" })); ;
            services.AddSignalRService();
            services.Configure<RabbitMQConfig>(Configuration.GetSection("RabbitMq"));
            services.AddEventBus(Configuration);
       
            services.AddControllers(o =>
            {
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            })
            .AddNewtonsoftJson(options =>
            {
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ContractResolver = new CustomContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
            });
            services.AddHostedService<TestHostedService>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        }
    }
}
