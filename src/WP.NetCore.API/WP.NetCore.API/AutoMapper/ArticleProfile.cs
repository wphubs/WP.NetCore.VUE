using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model.Dto.Article;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.AutoMapper
{
    public class ArticleProfile : Profile
    {

        public ArticleProfile()
        {
            CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(s => s.Class.ClassName))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(s => s.Class.Id));
            CreateMap<AddArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();
        }
    }
}
