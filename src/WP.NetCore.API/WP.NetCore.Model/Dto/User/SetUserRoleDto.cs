using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.Dto.User
{
    public class SetUserRoleDto
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public long UserId { get; set; }

        [MinLength(1, ErrorMessage = "用户角色不能为空")]
        public List<long> RoleId { get; set; }
    }
}
