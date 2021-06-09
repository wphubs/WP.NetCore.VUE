using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.ViewModel
{
    public class ArticleViewModel
    {

        public long Id { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }


        public string ClassName { get; set; }

        public long ClassId { get; set; }
    }
}
