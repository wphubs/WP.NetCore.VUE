using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Repository.EFCore;

namespace WP.NetCore.Services
{
    public class NothingService : BaseService<Nothing>, INothingService
    {
        private readonly IBaseRepository<Nothing> baseRepository;
        private readonly WPDbContext dbContext;
        private readonly IRedisCacheManager redisCacheManager;

        public NothingService(IBaseRepository<Nothing> baseRepository, WPDbContext dbContext, IRedisCacheManager redisCacheManager)
        {
            this.baseRepository = baseRepository;
            this.dbContext = dbContext;
            this.redisCacheManager = redisCacheManager;
        }


        public async Task<Nothing> GetTodayNothingData()
        {
            var cache = await redisCacheManager.Get<Nothing>(nameof(GetTodayNothingData));
            if (cache != null)
            {
                return cache;
            }
            else
            {
                var list = await dbContext.Nothing.FromSqlRaw("SELECT * FROM Nothing  ORDER BY RAND() LIMIT 1;").ToListAsync();
                DateTime currentTime = DateTime.Now;  //获取当前时间
                TimeSpan ts = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,23,59,59) - currentTime;	//计算时间差
                await redisCacheManager.Set(nameof(GetTodayNothingData), list[0], ts);
                return list[0];
            }

        

        }

        
    }
}
