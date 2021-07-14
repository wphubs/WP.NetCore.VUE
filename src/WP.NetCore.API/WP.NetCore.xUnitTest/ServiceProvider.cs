using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.API;
using WP.NetCore.Common;
using WP.NetCore.Common.Helper;
using WP.NetCore.EventBus;
using WP.NetCore.EventBus.Cap;
using WP.NetCore.Extensions;
using WP.NetCore.Extensions.ServicesRegistration;
using WP.NetCore.IServices;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.SchedulerJob.HostedService;
using WP.NetCore.Services;

namespace WP.NetCore.xUnitTest
{
    public class ServiceProvider
    {
        public  IConfiguration Configuration { get; } = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

        public IContainer Init()
        {
            var basePath = AppContext.BaseDirectory;

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(new Appsettings(basePath));
            services.AddRedisCache();
            services.AddSingleton<HttpHelper>();

            services.AddEFCore();
            services.AddAutoMapper(typeof(ServiceInfo));
            services.AddSwagger();
            services.AddScheduleJob();
            services.AddCorsPolicy();
            services.AddJwtAuthentication();
            services.AddHealthChecks().AddMySql(Appsettings.app(new string[] { "DBConnection" })); ;
            services.AddSignalRService();
            services.Configure<RabbitMQConfig>(Configuration.GetSection("RabbitMq"));
            services.AddEventBus(Configuration);
            services.AddHostedService<TestHostedService>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            var builder = new ContainerBuilder();
            builder.RegisterType<APICacheAOP>();
            //builder.RegisterType<APITranAOP>();
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerDependency();//注册仓储
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储
            var assemblysServices = Assembly.Load("WP.NetCore.Services");
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors()
                      .InterceptedBy(typeof(APICacheAOP));// typeof(APICacheAOP),typeof(APILogAOP) typeof(APITranAOP), 
            var assemblyRepository = Assembly.Load("WP.NetCore.Repository.EFCore");
            builder.RegisterAssemblyTypes(assemblyRepository).AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors();

            ////注册事件发布者
            //builder.RegisterType<CapPublisher>()
            //       .As<IEventPublisher>()
            //       .SingleInstance();
            ////注册Rabbitmq生产者
            //builder.RegisterType<RabbitMQProducer>()
            //       .InstancePerLifetimeScope();

            ////注册Rabbitmq消费者
            //builder.RegisterAssemblyTypes(assemblysServices)
            //       .Where(t => t.IsAssignableTo<IHostedService>() && t.IsAssignableTo<BaseRabbitMQConsumer>() && !t.IsAbstract)
            //       .AsImplementedInterfaces()
            //       .SingleInstance();

            builder.Populate(services);

            var ApplicationContainer = builder.Build();
            return ApplicationContainer;

        }
    }
}
