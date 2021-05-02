using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MenuService(IBaseRepository<Menu> baseRepository, IMapper mapper)
        {
            this.baseDal = baseRepository;
            this.baseRepository = baseRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// 获取所有页面菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<PageMenuViewModel>> GetPageMenuListAsync()
        {
            var list = await baseRepository.GetAllAsync(x => !x.IsButton,x=>x.Sort, true);
            return mapper.Map<List<PageMenuViewModel>>(list);
        }


        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
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
                    objMenu.children= GetMenuChildren(menu, item.Id);
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
                    item.children = GetMenuChildren(listMenu, item.Id);
                });
            }
            return listSelect;
        }



        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<RouterViewModel>> GetRoleMenuListAsync()
        {
            var listMenu = await baseRepository.GetAllAsync(x => x.IsDelete == false, x => x.Sort, true);
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
                var objSelect = GetChildrenMenu(listMenu, item.Id);
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
