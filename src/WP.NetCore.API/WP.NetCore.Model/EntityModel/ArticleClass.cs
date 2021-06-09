using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WP.NetCore.Model;
namespace WP.NetCore.Model.EntityModel
{
    public class ArticleClass : EntityBase
    {
        [MaxLength(50)]
        public string ClassName { get; set; }

        public List<Article> Articles { get; set; }
    }
}
