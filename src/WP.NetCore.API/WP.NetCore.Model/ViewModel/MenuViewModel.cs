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
        public string title { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string path { get; set; }


        /// <summary>
        /// 页面路径
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string component { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MetaData meta { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MenuViewModel> children { get; set; }


    }

    public class MetaData
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public List<string> permission { get; set; }
    }
}
