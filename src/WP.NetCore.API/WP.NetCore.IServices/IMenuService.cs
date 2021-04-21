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

        Task<List<MenuViewModel>> GetRoleMenuList();
    }
}
