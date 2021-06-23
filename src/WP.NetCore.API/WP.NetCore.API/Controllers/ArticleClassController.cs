using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleClassController :  BaseController
    {
  
        private readonly IArticleClassService articleClassService;

        public ArticleClassController( IArticleClassService articleClassService)
        {
            this.articleClassService = articleClassService;
        }

        /// <summary>
        /// 获取文章分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleClass>>> Get()
        {
            var listUser = await articleClassService.GetAllAsync();
            return Ok(listUser);
        }

    }
}
