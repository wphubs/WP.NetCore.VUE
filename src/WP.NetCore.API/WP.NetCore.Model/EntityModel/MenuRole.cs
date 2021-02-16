using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.EntityModel
{
    public class MenuRole : EntityBase
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }

        public Menu Menu { get; set; }

        public Role Role { get; set; }
    }
}
