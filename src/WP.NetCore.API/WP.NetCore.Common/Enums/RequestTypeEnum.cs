using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Common.Enums
{
    public enum RequestTypeEnum
    {
        [Description("Get")]
        Get = 1,
        [Description("Post")]
        Post = 2,
        [Description("Put")]
        Put = 3,
        [Description("Delete")]
        Delete = 4
    }

  
}
