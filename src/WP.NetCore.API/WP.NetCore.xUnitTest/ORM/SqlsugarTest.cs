using Autofac;
using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Repository.SqlSugar.Repository;
using Xunit;

namespace WP.NetCore.xUnitTest.ORM
{
    public class SqlsugarTest
    {
        ServiceProvider provider = new ServiceProvider();

        readonly ISqlSugarRepository sqlSugarRepository;
        public SqlsugarTest()
        {
            var container = provider.Init();
            sqlSugarRepository = container.Resolve<ISqlSugarRepository>();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [Fact]
        public async void GetUserListAsync()
        {
            var list = await sqlSugarRepository.Context.Queryable<UserTest>().ToListAsync();
            Assert.True(list.Count > 0);
        }
    }

    [SqlSugar.SugarTable("User")]
    public class UserTest
    {
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
