using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WP.NetCore.API.Hubs;
using WP.NetCore.Extensions;
using WP.NetCore.Extensions.ModuleRegistration;

namespace WP.NetCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfigureServices(Configuration, Env);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseConfigureMiddleware(env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<WelcomeHub>("/welcomehub");
                endpoints.MapControllers();
            });
        }
    }
}
