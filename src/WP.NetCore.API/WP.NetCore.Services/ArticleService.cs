using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
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
        /// 获取文章内容
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10, PrefixKey = Constant.ArticleKey)]
        public async Task<ArticleViewModel> GetArticleInfo(long articleId)
        {
            var article = await baseDal.LoadAsync(x => x.IsDelete == false&&x.Id==articleId );
            var obj = await article.Include(x => x.Class).FirstOrDefaultAsync();
            obj.Browse++;
            await baseDal.SaveAsync();
            return mapper.Map<ArticleViewModel>(obj);
        }


        /// <summary>
        /// 获取热门文章（目前按浏览量排序）
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10, PrefixKey = Constant.ArticleKey)]
        public async Task<PageModel<ArticleViewModel>> GetHotArticleListAsync()
        {
            var article = await baseDal.GetPageAsync(x => x.IsDelete == false, x => x.Browse, 1, 10);
            var listArticle = article.Data.Include(x => x.Class);
            var listViewModel = mapper.Map<List<ArticleViewModel>>(listArticle.ToList());
            listViewModel.ForEach(item =>item.Content=null);
            var objReturn = new PageModel<ArticleViewModel>()
            {
                Data = listViewModel,
                Total = article.Total,
            };
            return objReturn;
        }





        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10, PrefixKey = Constant.ArticleKey)]
        public async Task<PageModel<ArticleViewModel>> GetArticleListAsync(long? classId,int pageIndex, int pageSize)
        {
            Expression<Func<Article, bool>> expression = x => x.IsDelete==false;
            if (classId != null)
            {
                expression = x => x.IsDelete == false && x.Class.Id == classId;
            }
            var article = await baseDal.GetPageAsync(expression, x => x.CreateTime, pageIndex, pageSize);
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
