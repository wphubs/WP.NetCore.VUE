using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WP.NetCore.Extensions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public long Role { get; set; }

   
        public List<string> Urls { get; set; }


    }
}
