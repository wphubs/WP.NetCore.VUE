using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Model;

namespace WP.NetCore.Extensions.ServicesRegistration
{
    public static class JwtService
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
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
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Permission", policy => policy.AddRequirements(permissionRequirement));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(ResponseResultHandler);
                options.DefaultForbidScheme = nameof(ResponseResultHandler);
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];
                         if (!string.IsNullOrEmpty(accessToken) && (context.HttpContext.Request.Path.StartsWithSegments("/welcomehub")))
                         {
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
                         var claims = context.Principal.Claims;
                         userContext.Id = long.Parse(claims.First(x => x.Type == "Id").Value);
                         userContext.Name = claims.First(x => x.Type == "Name").Value;
                         return Task.CompletedTask;
                     },
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
        }
    }
}
