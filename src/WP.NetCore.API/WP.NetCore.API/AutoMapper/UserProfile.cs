using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model;
using WP.NetCore.Model.Dto;
using WP.NetCore.Model.Model;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.AutoMapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            //CreateMap<PageModel<User>, PageModel<UserViewModel>>();
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.SexText, opt => opt.MapFrom(s => s.Sex == 1 ? "男" : "女"))
                .ForMember(dest=>dest.Roles,opt=>opt.MapFrom(s=>s.UserRoles.Select(x=>x.RoleId)))
               .ForMember(dest => dest.RolesName, opt => opt.MapFrom(s => s.UserRoles.Select(x => x.Role.RoleName)));
        }
     
    }
}
