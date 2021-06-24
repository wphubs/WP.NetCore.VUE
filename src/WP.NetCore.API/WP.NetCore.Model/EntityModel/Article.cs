using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WP.NetCore.Model;

namespace WP.NetCore.Model.EntityModel
{
    public class Article : EntityBase
    {
       
        public ArticleClass Class { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int Browse { get; set; } = 0;

        /// <summary>
        /// 评论数量（暂时没用，预留字段）
        /// </summary>
        public int Comment { get; set; } = 0;


    }


}
