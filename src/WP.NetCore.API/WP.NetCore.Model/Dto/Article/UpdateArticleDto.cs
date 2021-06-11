using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.Dto.Article
{
    public class UpdateArticleDto
    {


        [Required(ErrorMessage = "ID不能为空")]
        public long Id { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }


        /// <summary>
        /// 类别ID
        /// </summary>
        [Required(ErrorMessage = "类别不能为空")]
        public long ClassId { get; set; }
    }
}
