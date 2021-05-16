using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WP.NetCore.API.AuthHelper
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor contextAccessor;

        public PermissionHandler(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var questUrl = contextAccessor.HttpContext.Request.Path.Value.ToLower();
            var quertType=contextAccessor.HttpContext.Request.Method.ToLower();
            foreach (var t in context.User.Identities)
            {
                foreach (var claim in t.Claims)
                {
                    //通过Type可以判断声明的类型，这里处理role的声明获取角色信息
                    if (claim.Type == ClaimTypes.Role)
                    {
                        var role = claim.Value;
                        //if (roles.Exists(x => x.Urls.Exists(role => role == claim.Value) && x.Url == resource.RawText.ToLower()))
                        //{
                          context.Succeed(requirement);
                           return;
                        //}
                    }
                }
            }
            context.Fail();
            return;
        }
    }
}
