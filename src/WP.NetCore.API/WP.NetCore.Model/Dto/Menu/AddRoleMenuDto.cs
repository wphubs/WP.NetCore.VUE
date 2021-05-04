using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.Dto.Menu
{
    public class AddRoleMenuDto
    {
        public List<long> MenuId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }

        public long CreateBy { get; set; }
    }
}
