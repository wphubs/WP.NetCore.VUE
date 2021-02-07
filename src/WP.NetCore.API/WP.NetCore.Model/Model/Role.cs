using System;
using System.Collections.Generic;
using System.Text;

namespace WP.NetCore.Model.Model
{
    public class Role: Entity
    {
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
