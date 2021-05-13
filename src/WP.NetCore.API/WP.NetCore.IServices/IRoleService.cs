using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model.Dto.Menu;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.IServices
{
    public interface IRoleService:IBaseService<Role>
    {

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<long>> GetRoleMenu(long roleId);

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task SetRoleMenu(AddRoleMenuDto dto);
    }
}
