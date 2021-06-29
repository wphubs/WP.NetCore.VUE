using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.Services;

namespace WP.NetCore.Extensions.ModuleRegistration
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
        }
    }
}
