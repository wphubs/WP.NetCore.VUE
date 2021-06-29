using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WP.NetCore.IServices;

namespace WP.NetCore.Extensions
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IRoleService roleService;

        public PermissionHandler(IHttpContextAccessor contextAccessor, IRoleService roleService)
        {
            this.contextAccessor = contextAccessor;
            this.roleService = roleService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var questUrl = contextAccessor.HttpContext.Request.Path.Value.ToLower();
            var quertType=contextAccessor.HttpContext.Request.Method.ToLower();
            var currentUserRoles = (from item in contextAccessor.HttpContext.User.Claims select item.Value).ToList();
            foreach (var t in context.User.Identities)
            {
                foreach (var claim in t.Claims)
                {
                    if (claim.Type == "Role")
                    {
                        var role = claim.Value;
                        var listPermission = await roleService.GetRolePermission(Convert.ToInt64(role));
                        if (listPermission.Contains($"{questUrl}/{quertType}"))
                        {
                            context.Succeed(requirement);
                           return;
                        }
                    }
                }
            }
            context.Fail();
            return;
        }
    }
}
