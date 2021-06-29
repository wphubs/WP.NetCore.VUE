using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class SignalRService
    {

        public static void AddSignalRService(this IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = Int64.MaxValue;
                options.AddFilter<SignalRExceptionFilter>();

            }).AddNewtonsoftJsonProtocol(configure =>
            {
                var setting = configure.PayloadSerializerSettings;
                setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                setting.NullValueHandling = NullValueHandling.Ignore;
                setting.ContractResolver = new DefaultContractResolver();
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }

    }
}
