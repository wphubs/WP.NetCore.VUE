using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto.Article;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;
using WP.NetCore.Repository.EFCore;

namespace WP.NetCore.Services
{
    public class ArticleService : BaseService<Article>, IArticleService
    {
        private readonly IBaseRepository<Article> baseRepository;
        private readonly WPDbContext dbContext;
        private readonly IBaseRepository<ArticleClass> articleClassRepository;
        private readonly IMapper mapper;

        public ArticleService(IBaseRepository<Article> baseRepository,WPDbContext dbContext, IBaseRepository<ArticleClass> articleClassRepository, IMapper mapper)
        {
            this.baseDal = baseRepository;
            this.baseRepository = baseRepository;
            this.dbContext = dbContext;
            this.articleClassRepository = articleClassRepository;
            this.mapper = mapper;
        }



        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageModel<ArticleViewModel>> GetArticleListAsync(int pageIndex, int pageSize)
        {
            var article = await baseDal.GetPageAsync(x => x.IsDelete == false, x => x.CreateTime, pageIndex, pageSize);
            var listArticle = article.Data.Include(x => x.Class);
            var listViewModel = mapper.Map<List<ArticleViewModel>>(listArticle.ToList());
            var objReturn = new PageModel<ArticleViewModel>()
            {
                Data = listViewModel,
                Total = article.Total,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return objReturn;
        }



    }
}
