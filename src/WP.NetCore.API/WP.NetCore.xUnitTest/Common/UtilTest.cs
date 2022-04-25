using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common.Helper;
using Xunit;

namespace WP.NetCore.xUnitTest.Common
{
    public class UtilTest
    {
        ServiceProvider provider = new ServiceProvider();

   
        public UtilTest()
        {
            var container = provider.Init();
        }

        /// <summary>
        /// 测试缓存
        /// </summary>
        [Fact]
        public void SendTestMail()
        {
            EmailHelper.SendEmail("测试", "asdf");
            Assert.True(true);
        }
    }
}
