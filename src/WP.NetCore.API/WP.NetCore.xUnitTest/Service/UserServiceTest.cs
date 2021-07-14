using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Repository.EFCore;
using Xunit;

namespace WP.NetCore.xUnitTest.Service
{
    public class UserServiceTest
    {
        ServiceProvider provider = new ServiceProvider();

        public readonly IUserService userService;

        public UserServiceTest()
        {
            var container = provider.Init();
            var aa = container.Resolve<WPDbContext>();
            userService = container.Resolve<IUserService>();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [Fact]
        public async void GetUserListAsync()
        {
            var list=await userService.GetUserListAsync(1, 10);
            Assert.True(list.Data.Count>0);
        }
    }
}
