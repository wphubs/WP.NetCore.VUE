using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.IServices;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.Dto.Menu;
using WP.NetCore.Common.Helper;

namespace WP.NetCore.Services
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        private readonly IBaseRepository<Role> baseRepository;
        private readonly IBaseRepository<Menu> menuRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public RoleService(IBaseRepository<Role> baseRepository, IBaseRepository<Menu> menuRepository, IMapper mapper, IUnitOfWork uow)
        {

            this.baseDal = baseRepository;
            this.baseRepository = baseRepository;
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.uow = uow;
        }


        public async Task SetRoleMenu(AddRoleMenuDto dto)
        {
            try
            {
                await uow.BeginAsync();
                var role = await baseRepository.FirstAsync(dto.RoleId);
                List<MenuRole> listAdd = new List<MenuRole>();

                dto.MenuId.ForEach(async item =>
                {
                    var menu = await menuRepository.FirstAsync(item);
                    listAdd.Add(new MenuRole() { RoleId= dto.RoleId, MenuId= item, Id = new Snowflake().GetId(),CreateBy=dto.CreateBy });
                    //.Add(new MenuRole() { Menu=menu});
                });
                await uow.DbContext.AddRangeAsync(listAdd.AsEnumerable());
                await uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                throw;
            }
        }
    }
}
