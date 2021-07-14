using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using Xunit;

namespace WP.NetCore.xUnitTest.Service
{
    public class RoleServiceTest
    {
        ServiceProvider provider = new ServiceProvider();

        public readonly IRoleService roleService;

        public RoleServiceTest()
        {
            var container = provider.Init();
            roleService = container.Resolve<IRoleService>();
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        [Fact]
        public async void GetRoleListAsync()
        {
            var list = await roleService.GetAllAsync();
            Assert.True(list.ToList().Count > 0);
        }


    }
}
