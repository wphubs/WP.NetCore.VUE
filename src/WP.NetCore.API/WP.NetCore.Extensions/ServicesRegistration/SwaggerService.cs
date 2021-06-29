using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class SwaggerService
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            var APIName = Appsettings.app(new string[] { "APIName" });
            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{APIName}接口文档",
                        Description = $"{APIName} HTTP API {version}",
                    });
                    c.OrderActionsBy(o => o.RelativePath);
                });

                var xmlPath = Path.Combine(basePath, "WP.NetCore.API.xml");
                c.IncludeXmlComments(xmlPath, true);
                Console.WriteLine(xmlPath);
                var xmlModelPath = Path.Combine(basePath, "WP.NetCore.Model.xml");
                c.IncludeXmlComments(xmlModelPath);

                // 开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                #region Token绑定到ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权 直接在下框中输入Bearer {token}",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
        }
    }
}
