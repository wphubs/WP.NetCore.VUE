using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto.Article;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        private readonly IArticleClassService articleClassService;

        public ArticleController(IArticleService articleService, IMapper mapper, IArticleClassService articleClassService)
        {
            this.articleService = articleService;
            this.mapper = mapper;
            this.articleClassService = articleClassService;
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> Get(int pageIndex, int pageSize)
        {
            var listArticle = await articleService.GetArticleListAsync(pageIndex, pageSize);
            return new ResponseResult().Success(listArticle);
        }



        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="articleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> Post([FromBody] AddArticleDto articleDto)
        {
            var objClass = await articleClassService.FirstAsync(articleDto.ClassId);
            if (objClass == null)
            {
                throw new Exception("ID不存在");
            }
            var objArticle = mapper.Map<Article>(articleDto);
            objArticle.Class = objClass;
            articleDto.CreateBy = GetToken().Id;
            await articleService.AddAsync(objArticle);
            return new ResponseResult().Success();
        }
    }
}
