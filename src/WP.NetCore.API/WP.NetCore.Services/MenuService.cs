using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.IServices;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;
using WP.NetCore.Repository.EFCore;

namespace WP.NetCore.Services
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        private readonly IBaseRepository<Menu> baseRepository;
        private readonly IMapper mapper;
        private readonly WPDbContext dbContext;

        public MenuService(IBaseRepository<Menu> baseRepository, IMapper mapper, WPDbContext dbContext)
        {
            this.baseDal = baseRepository;
            this.baseRepository = baseRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }


        /// <summary>
        /// 获取所有页面菜单
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10, PrefixKey = Constant.RoleKey)]
        public async Task<List<PageMenuViewModel>> GetPageMenuListAsync()
        {
            var list = await baseRepository.GetAllAsync(x => !x.IsButton, x => x.Sort, true);
            return mapper.Map<List<PageMenuViewModel>>(list);
        }


        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10, PrefixKey = Constant.RoleKey)]
        public async Task<List<MenuViewModel>> GetMenuListAsync()
        {
            var list = await baseRepository.GetAllAsync(x => x.IsDelete == false, x => x.Sort, true);
            var menu = mapper.Map<List<MenuViewModel>>(list);
            List<MenuViewModel> listMenu = new List<MenuViewModel>();
            list.ForEach(item =>
            {
                if (item.ParentId == 0)
                {
                    var objMenu = mapper.Map<MenuViewModel>(item);
                    objMenu.children = GetMenuChildren(menu, item.Id);
                    listMenu.Add(objMenu);
                }

            });
            return listMenu;

        }

        /// <summary>
        /// 获取子级菜单
        /// </summary>
        /// <param name="listMenu"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<MenuViewModel> GetMenuChildren(List<MenuViewModel> listMenu, long parentId)
        {
            List<MenuViewModel> listModel = new List<MenuViewModel>();
            var listSelect = listMenu.FindAll(x => x.ParentId == parentId);
            if (listSelect.Count != 0)
            {
                listSelect.ForEach(item =>
                {
                    item.children = GetMenuChildren(listMenu, Convert.ToInt64(item.Id));
                });
            }
            return listSelect;
        }



        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10,PrefixKey = Constant.RoleKey)]
        public async Task<List<RouterViewModel>> GetRoleMenuListAsync(List<long> listRole)
        {
            List<Menu> listMenu = new List<Menu>();
            if (listRole.Contains(999999999))
            {
                listMenu = await baseRepository.GetAllAsync(x => x.IsDelete == false, x => x.Sort, true);
            }
            else
            {
                listMenu = await dbContext.MenuRole.Join(dbContext.Menu, menuRole => menuRole.MenuId,
                menu => menu.Id, (menuRole, menu) => new { menuRole, menu })
               .Where(x => x.menuRole.IsDelete == false && x.menu.IsDelete == false && listRole.Contains(x.menuRole.RoleId))
               .Select(x => x.menu)
               .ToListAsync();
            }
            var list = GetRootMenu(listMenu, 0);
            return list;
        }

        /// <summary>
        /// 获取跟菜单
        /// </summary>
        /// <param name="listMenu"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<RouterViewModel> GetRootMenu(List<Menu> listMenu, long parentId)
        {
            List<RouterViewModel> listModel = new List<RouterViewModel>();
            var objMenu = listMenu.FindAll(x => x.ParentId == parentId);
            if (objMenu.Count != 0)
            {
                objMenu.ForEach(item =>
                {
                    RouterViewModel model = new RouterViewModel();
                    model.path = $"/{item.Component}";
                    model.component = "Layout";
                    var objSelect = GetChildrenMenu(listMenu, item.Id);
                    if (objSelect.Count == 0)
                    {
                        model.meta = null;
                        model.children = new List<RouterViewModel>()
                        {
                            new RouterViewModel()
                            {
                                name= item.Component,
                                path =  $"/{item.Component}",
                                component=item.Component,
                                meta= new MetaData(){icon = item.Icon, title = item.Title,permission= GetMenuPermission(item.Id,listMenu)}
                            }
                        };
                    }
                    else
                    {
                        model.name = item.Component;
                        model.meta = new MetaData() { icon = item.Icon, title = item.Title, permission = GetMenuPermission(item.Id, listMenu) };
                        model.children = objSelect;
                    }
                    listModel.Add(model);
                });
            }
            return listModel;
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="listMenu"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<RouterViewModel> GetChildrenMenu(List<Menu> listMenu, long parentId)
        {
            List<RouterViewModel> listModel = new List<RouterViewModel>();
            var objMenu = listMenu.FindAll(x => x.ParentId == parentId && !x.IsButton);
            objMenu.ForEach(item =>
            {
                RouterViewModel model = new RouterViewModel();
                model.path = $"/{item.Component}";
                model.component = item.Component;
                model.name = item.Component;
                model.meta = new MetaData() { icon = item.Icon, title = item.Title, permission = GetMenuPermission(parentId, listMenu) };
                //var objSelect = GetChildrenMenu(listMenu, item.Id);
                model.children = GetChildrenMenu(listMenu, item.Id);
                listModel.Add(model);
            });
            return listModel;
        }

        /// <summary>
        /// 获取菜单按钮
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="listRouter"></param>
        /// <returns></returns>
        private List<string> GetMenuPermission(long parentId, List<Menu> listRouter)
        {
            var listButton = listRouter.FindAll(p => p.ParentId == parentId && p.IsButton);
            List<string> buttonInfo = new List<string>();
            listButton.ForEach(it => buttonInfo.Add(it.Component));
            return buttonInfo;
        }


    }
}
