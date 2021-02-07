using System;
using System.Collections.Generic;
using System.Text;

namespace WP.NetCore.Common
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : Attribute
    {
        //缓存绝对过期时间
        public int AbsoluteExpiration { get; set; } = 30;

    }
}
