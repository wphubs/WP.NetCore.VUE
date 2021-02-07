using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.Dto
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "ID不能为空")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>

        public bool IsEnable { get; set; } = true;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; } = 1;
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }


        public List<Guid> Roles { get; set; }
    }
}
