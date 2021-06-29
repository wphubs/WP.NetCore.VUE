using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model.Dto.Role;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.Extensions.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<AddRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

        }
    }

}
