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

        public MenuService(IBaseRepository<Menu> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        
        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuViewModel>> GetRoleMenuList()
        {
            var listMenu = await baseRepository.GetAllAsync(x => x.IsDelete == false);
            var list = GetRootMenu(listMenu, 0);
            return list;
        }

        /// <summary>
        /// 获取跟菜单
        /// </summary>
        /// <param name="listMenu"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<MenuViewModel> GetRootMenu(List<Menu> listMenu, long parentId)
        {
            List<MenuViewModel> listModel = new List<MenuViewModel>();
            var objMenu = listMenu.FindAll(x => x.ParentId == parentId);
            if (objMenu.Count != 0)
            {
                objMenu.ForEach(item =>
                {
                    MenuViewModel model = new MenuViewModel();
                    model.path = item.Path;
                    model.component = "Layout";
                    var objSelect = GetChildrenMenu(listMenu, item.Id);
                    if (objSelect.Count == 0)
                    {
                        model.children = new List<MenuViewModel>()
                        {
                            new MenuViewModel()
                            {
                                name= item.Name,path = item.Path,component=item.Component,
                                meta= new MetaData(){icon = item.Icon, title = item.Title}
                            }
                        };
                    }
                    else
                    {
                        model.name = item.Name;
                        model.meta = new MetaData() { icon = item.Icon, title = item.Title };
                        model.children = objSelect;
                    }
                    listModel.Add(model);
                });
            }
            return listModel;
        }

        /// <summary>
        /// 递归获取子菜单
        /// </summary>
        /// <param name="listMenu"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<MenuViewModel> GetChildrenMenu(List<Menu> listMenu, long parentId)
        {
            List<MenuViewModel> listModel = new List<MenuViewModel>();
            var objMenu = listMenu.FindAll(x => x.ParentId == parentId);
            objMenu.ForEach(item =>
            {
                MenuViewModel model = new MenuViewModel();
                model.path = item.Path;
                model.component = item.Component;
                model.name = item.Name;
                model.meta = new MetaData() { icon = item.Icon, title = item.Title };
                var objSelect = GetChildrenMenu(listMenu, item.Id);
                listModel.Add(model);
            });
            return listModel;
        }



    }
}
