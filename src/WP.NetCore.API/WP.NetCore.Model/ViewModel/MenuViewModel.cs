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

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }


        /// <summary>
        /// 页面路径
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Component { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MetaData Meta { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuViewModel> Children { get; set; }


    }

    public class MetaData
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
