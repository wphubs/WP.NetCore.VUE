using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WP.NetCore.Model.EntityModel
{
    public class User: EntityBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>

        [DefaultValue(true)]
        public bool IsEnable { get; set; } = true;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
