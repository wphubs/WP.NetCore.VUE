using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using WP.NetCore.API.AOP;
using WP.NetCore.API.Filter;
using WP.NetCore.API.SwaggerHelper;
using WP.NetCore.Common;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.IServices;
using WP.NetCore.Services;
using WP.NetCore.API.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WP.NetCore.Common.Helper;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using AspNetCoreRateLimit;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using StackExchange.Redis;
//using WP.NetCore.IServices;
//using WP.NetCore.Services;

namespace WP.NetCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public string APIName { get; set; } = "NetCore.API";

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var basePath = AppContext.BaseDirectory;

            #region Appsettings
            services.AddSingleton(new Appsettings(Env.ContentRootPath));
            #endregion


            #region Redis

            services.AddTransient<IRedisCacheManager, RedisCacheManager>();

            // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {

                var conn = Appsettings.app(new string[] { "RedisConnection" });
                var configuration = ConfigurationOptions.Parse(conn, true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });


            #endregion

            #region IpRateLimit限流

            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            services.AddInMemoryRateLimiting();

            #endregion

            #region EFCORE
            var connection = Appsettings.app(new string[] { "DBConnection" });
            services.AddDbContext<WPDbContext>(options => options.UseMySQL(connection));
            //services.AddDbContext<WPDbContext>(options =>
            //{
            //    options.UseMySQL(connection);
            //    options.AddInterceptors(new CustomCommandInterceptor());
            //});
            #endregion

            #region AddAutoMapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region Swagger
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
            #endregion

            #region Cors
            services.AddCors(c =>
            {
                // 配置策略
                c.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins(Appsettings.app(new string[] { "Cors"}).Split(','))
                    .AllowAnyHeader()//允许任意头
                    .AllowAnyMethod();//允许任意方法
                });
            });
            #endregion

            #region JWT
            var symmetricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = Appsettings.app(new string[] { "Audience", "Issuer" });
            var Audience = Appsettings.app(new string[] { "Audience", "Audience" });
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = Issuer,//发行人
                ValidateAudience = true,
                ValidAudience = Audience,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };
            PermissionRequirement permissionRequirement = new PermissionRequirement();

            //listPermission.Add(n);
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Permission", policy => policy.AddRequirements(permissionRequirement));
            });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ResponseResultHandler);
                o.DefaultForbidScheme = nameof(ResponseResultHandler);
            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };
             })
             .AddScheme<AuthenticationSchemeOptions, ResponseResultHandler>(nameof(ResponseResultHandler), o => { }); ;


            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region HealthChecks

            services.AddHealthChecks();

            #endregion

            #region AddControllers
            services.AddControllers(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionsFilter));
            
            })
            //全局配置Json序列化处理
            .AddNewtonsoftJson(options =>
              {

                  //忽略循环引用
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  //不使用驼峰样式的key
                  //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
 
                  options.SerializerSettings.ContractResolver = new CustomContractResolver();
                  //设置时间格式
                  options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                  options.SerializerSettings.StringEscapeHandling= StringEscapeHandling.EscapeNonAscii;
              });
            #endregion

            #region IpRateLimit限流

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            #endregion
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();

            builder.RegisterType<APICacheAOP>();
            //builder.RegisterType<APITranAOP>();
            //builder.RegisterType<APILogAOP>();
            builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerDependency();//注册仓储

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储

            var assemblysServices = Assembly.Load("WP.NetCore.Services");
            builder.RegisterAssemblyTypes(assemblysServices)
                      .AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors()//对目标类型启用接口拦截
                      .InterceptedBy(typeof(APICacheAOP));// typeof(APICacheAOP),typeof(APILogAOP) typeof(APITranAOP), 
            var assemblyRepository = Assembly.Load("WP.NetCore.Repository.EFCore");
            builder.RegisterAssemblyTypes(assemblyRepository).AsImplementedInterfaces()
                      .InstancePerDependency()
                      .EnableInterfaceInterceptors();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
    
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    if (env.IsDevelopment())
                    {
                        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{APIName} {version}");
                    }
                    else {
                        c.SwaggerEndpoint($"/netcore/swagger/{version}/swagger.json", $"{APIName} {version}");
                    }
                    
                });
                c.RoutePrefix = "";
            });
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "Handled {RequestPath}";
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    var request = httpContext.Request;
                    diagnosticContext.Set("RequestHost", request.Host.Value);
                    diagnosticContext.Set("RequestScheme", request.Scheme);
                    diagnosticContext.Set("Agent", request.Headers["User-Agent"].ToString());
                    var client = request.Headers["X-Forwarded-For"].ToString();
                    if (string.IsNullOrEmpty(client))
                    {
                        client = httpContext.Connection.RemoteIpAddress.ToString();
                    }
                    diagnosticContext.Set("IP", client);

                };
            });
            //添加Cors跨域
            app.UseCors("LimitRequests");
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

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
