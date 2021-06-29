using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class RedisCacheService
    {
        public static void AddRedisCache(this IServiceCollection services)
        {
            services.AddTransient<IRedisCacheManager, RedisCacheManager>();
            services.AddSingleton(sp =>
            {
                var conn = Appsettings.app(new string[] { "RedisConnection" });
                var configuration = ConfigurationOptions.Parse(conn, true);
                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });
        }
    }

}
