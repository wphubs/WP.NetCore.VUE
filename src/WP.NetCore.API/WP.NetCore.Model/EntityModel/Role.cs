using System;
using System.Collections.Generic;
using System.Text;

namespace WP.NetCore.Model.EntityModel
{
    public class Role: EntityBase
    {
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<MenuRole> MenuRoles { get; set; }
    }
}
