using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.ViewModel
{
    public class MenuViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 页面路径
        /// </summary>
        public string Component { get; set; }


        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否为按钮
        /// </summary>
        public bool IsButton { get; set; } = false;


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuViewModel> children { get; set; }


        public long? ParentId { get; set; } = 0;

        public DateTime? CreateTime { get; set; }
    }
}
