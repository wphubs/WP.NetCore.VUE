
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WP.NetCore.Extensions
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class VersionRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        public VersionRouteAttribute(string actionName = "[action]") : base("/api/{version}/[controller]/" + actionName)
        {
        }

        public string GroupName { get; set; }

     
        public VersionRouteAttribute(ApiVersions version, string actionName = "[action]") : base($"/api/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }

        public VersionRouteAttribute(ApiVersions version) : base($"/api/{version.ToString()}/[controller]")
        {
            GroupName = version.ToString();
        }

    }
}
