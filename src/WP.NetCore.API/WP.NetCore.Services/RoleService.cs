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
using Microsoft.EntityFrameworkCore;
using WP.NetCore.Common;

namespace WP.NetCore.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IBaseRepository<Role> baseRepository;
        private readonly WPDbContext dbContext;
        private readonly IBaseRepository<Menu> menuRepository;
        private readonly IBaseRepository<MenuRole> menuRoleRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;
        private readonly IRedisCacheManager cache;
        public RoleService(IBaseRepository<Role> baseRepository,
            WPDbContext dbContext,
            IBaseRepository<Menu> menuRepository, 
            IBaseRepository<MenuRole> menuRoleRepository,
            IRedisCacheManager cache,
            IMapper mapper,
            IUnitOfWork uow)
        {
            this.cache = cache;
            this.baseDal = baseRepository;
            this.baseRepository = baseRepository;
            this.dbContext = dbContext;
            this.menuRepository = menuRepository;
            this.menuRoleRepository = menuRoleRepository;
            this.mapper = mapper;
            this.uow = uow;
        }


        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10,PrefixKey ="Role")]
        public async Task<List<long>> GetRoleMenu(long roleId)
        {
            var objRole = await baseRepository.LoadNoTrackingAsync(x => x.Id == roleId && x.IsDelete == false);
            var role = await objRole.Include(x => x.MenuRoles).FirstAsync();
            var listUserMenu = role.MenuRoles.Where(x => x.IsDelete == false);
            var listMenu = from menu in listUserMenu select menu.MenuId;
            return listMenu.ToList();
        }

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task SetRoleMenu(AddRoleMenuDto dto)
        {
            try
            {
                await cache.RemovePattern("Role");
                await uow.BeginAsync();
                var objRole = await baseRepository.LoadNoTrackingAsync(x => x.Id == dto.RoleId);
                var role = await objRole.Include(x => x.MenuRoles).FirstAsync();
                List<MenuRole> listAdd = new List<MenuRole>();
                var idwork = new Snowflake();
                dto.MenuId.ForEach(async item =>
                {
                    var menu = await menuRepository.FirstNoTrackingAsync(item);
                    listAdd.Add(new MenuRole() { RoleId = dto.RoleId, MenuId = item, Id = idwork.NextId(), CreateBy = dto.CreateBy });
                });
                await menuRoleRepository.DeleteRangeAsync(role.MenuRoles);
                await uow.DbContext.AddRangeAsync(listAdd.AsEnumerable());
                await uow.CommitAsync();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                throw;
            }
        }


        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10, PrefixKey = "Role")]
        public async Task<List<string>> GetRolePermission(long roleId)
        {

            if (roleId == 999999999)
            {
                var objList = await menuRepository.GetAllAsync(x=>x.Url!=null && x.IsDelete == false);
                var list = from menu in objList select "/api/"+menu.Url.ToLower();
                return list.ToList();
            }
            else
            {
                var objList = await dbContext.MenuRole.Join(dbContext.Menu, menuRole => menuRole.MenuId,
                          menu => menu.Id, (menuRole, menu) => new { Url = menu.Url, Role = menuRole.RoleId, IsDelete = menuRole.IsDelete })
                          .Where(x => x.Role == roleId && x.IsDelete == false&&x.Url != null).ToListAsync();
                var list = from menu in objList select "/api/" + menu.Url.ToLower();
                return list.ToList();
            }


        }
    }
}
