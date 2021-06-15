using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("Permission")]
        public async Task<ResponseResult> Post([FromBody] AddArticleDto articleDto)
        {
            var objClass = await articleClassService.FirstAsync(articleDto.ClassId);
            if (objClass == null)
            {
                throw new Exception("ID不存在");
            }
            var objArticle = mapper.Map<Article>(articleDto);
            objArticle.Class = objClass;
            objArticle.CreateBy = GetToken().Id;
            await articleService.AddAsync(objArticle);
            return new ResponseResult().Success();
        }


        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="articleDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize("Permission")]
        public async Task<ResponseResult> Put([FromBody] UpdateArticleDto articleDto)
        {
            if (await articleClassService.FirstNoTrackingAsync(articleDto.ClassId) == null)
            {
                throw new Exception("ID不存在");
            }
            var objArticle = mapper.Map<Article>(articleDto);
            objArticle.ModifyBy = GetToken().Id;
            await articleService.UpdateAsync(objArticle);
            return new ResponseResult().Success();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize("Permission")]
        public async Task<ResponseResult> Delete(long Id)
        {
            if (default(long) == Id)
            {
                return new ResponseResult().Error("ID不能为空");
            }
            if (await articleService.FirstNoTrackingAsync(Id) == null)
            {
                throw new Exception("ID不存在");
            }
            await articleService.DeleteAsync(Id);
            return new ResponseResult().Success();
        }
    }
}
