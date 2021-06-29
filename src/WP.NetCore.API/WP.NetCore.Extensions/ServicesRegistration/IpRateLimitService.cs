using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class IpRateLimitService
    {
        public static void AddIpRateLimit(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddInMemoryRateLimiting();
        }
    }
}
