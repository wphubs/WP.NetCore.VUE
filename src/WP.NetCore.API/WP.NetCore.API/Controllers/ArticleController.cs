using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Common.Helper;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto.Article;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

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
        /// 获取文章列表（无权限）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetArticleList")]
        public async Task<ActionResult<PageModel<Article>>> GetArticleList([FromRoute] int pageIndex, [FromRoute] int pageSize)
        {
            var listArticle = await articleService.GetArticleListAsync(pageIndex, pageSize);
            listArticle.Data.ForEach(item => 
            {
                item.Content = HtmlHelper.ReplaceHtmlTag(MarkdownHelper.MarkdownToHtml(item.Content));
            });
            return Ok(listArticle);
        }

        /// <summary>
        /// 获取文章详情（无权限）
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [HttpGet("GetArticleInfo")]
        public async Task<ActionResult<ArticleViewModel>> GetArticleInfo([FromRoute]long articleId)
        {
            var objArticle = await articleService.GetArticleInfo(articleId);
            return Ok(objArticle);
        }



        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize("Permission")]
        public async Task<ActionResult<PageModel<ArticleViewModel>>> Get([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var listArticle = await articleService.GetArticleListAsync(pageIndex, pageSize);
            return Ok(listArticle);
        }



        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="articleDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize("Permission")]
        public async Task<ActionResult> Post([FromBody] AddArticleDto articleDto)
        {
            var objClass = await articleClassService.FirstAsync(articleDto.ClassId);
            if (objClass == null)
            {
                return NotFound("ID不存在");
            }
            var objArticle = mapper.Map<Article>(articleDto);
            objArticle.Class = objClass;
            objArticle.CreateBy = GetToken().Id;
            await articleService.AddAsync(objArticle);
            return Ok();

        }


        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="articleDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize("Permission")]
        public async Task<ActionResult> Put([FromBody] UpdateArticleDto articleDto)
        {
            if (await articleClassService.FirstNoTrackingAsync(articleDto.ClassId) == null)
            {
                return NotFound("ID不存在");
            }
            var objArticle = mapper.Map<Article>(articleDto);
            objArticle.ModifyBy = GetToken().Id;
            await articleService.UpdateAsync(objArticle);
            return NoContent();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize("Permission")]
        public async Task<ActionResult> Delete([FromQuery] long Id)
        {
            if (default(long) == Id)
            {
                return BadRequest("ID不能为空");
            }
            if (await articleService.FirstNoTrackingAsync(Id) == null)
            {
                return NotFound("ID不存在");
            }
            await articleService.DeleteAsync(Id);
            return NoContent();
        }
    }
}
