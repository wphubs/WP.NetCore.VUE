using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.ViewModel
{
    public class PageMenuViewModel
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

    }
}
