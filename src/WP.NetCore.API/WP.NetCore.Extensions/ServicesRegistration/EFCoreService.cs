using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Repository.EFCore;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class EFCoreService
    {
        public static void AddEFCore(this IServiceCollection services)
        {
            var connection = Appsettings.app(new string[] { "DBConnection" });
            services.AddDbContext<WPDbContext>(options => options.UseMySQL(connection));
        }
    }
}
