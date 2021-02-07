using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.IServices;
using WP.NetCore.Model.Model;

namespace WP.NetCore.Services
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        private readonly IBaseRepository<Role> baseRepository;
        private readonly IMapper mapper;

        public RoleService(IBaseRepository<Role> baseRepository, IMapper mapper)
        {

            this.baseDal = baseRepository;
            this.baseRepository = baseRepository;
            this.mapper = mapper;
        }
    }
}
