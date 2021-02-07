using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.Dto.Role
{
    public class AddRoleDto
    {
        [Required(ErrorMessage = "角色名称不能为空")]
        public string RoleName { get; set; }
    }
}
