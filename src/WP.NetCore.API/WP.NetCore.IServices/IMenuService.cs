using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.IServices
{
    public interface IMenuService : IBaseService<Menu>
    {


        /// <summary>
        /// 获取所有页面菜单
        /// </summary>
        /// <returns></returns>
        Task<List<PageMenuViewModel>> GetPageMenuListAsync();

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuViewModel>> GetMenuListAsync();

        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <returns></returns>
        Task<List<RouterViewModel>> GetRoleMenuListAsync();
    }
}
