using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using Xunit;

namespace WP.NetCore.xUnitTest.Redis
{
    public class RedisCahceTest
    {
        ServiceProvider provider = new ServiceProvider();

        public readonly IRedisCacheManager cache;

        public RedisCahceTest()
        {
            var container = provider.Init();
            cache = container.Resolve<IRedisCacheManager>();
        }

        /// <summary>
        /// 测试缓存
        /// </summary>
        [Fact]
        public async void GetRedisCache()
        {
            var key = "UnitTest";
            var content = "Test";
            await cache.Set(key, content, TimeSpan.FromMinutes(1));
            var result = await cache.Get<string>(key);
            Assert.Equal(result, content);
        }
    }
}
