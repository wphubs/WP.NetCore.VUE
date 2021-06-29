using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {

            CreateMap<RequestLog, RequestLogViewModel>()
              .ForMember(dest => dest.Properties, opt => opt.MapFrom(s => JsonConvert.DeserializeObject<RequestLogProperties>(s.Properties)))
             .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(s =>Convert.ToDateTime(s.Timestamp)));
        }
    }
}
