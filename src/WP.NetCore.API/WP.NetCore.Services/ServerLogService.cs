using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;
using WP.NetCore.Repository.EFCore;

namespace WP.NetCore.Services
{
    public class ServerLogService : IServerLogService
    {
        private readonly WPDbContext dbContext;
        private readonly IMapper mapper;

        public ServerLogService(WPDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取API请求日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageModel<RequestLogViewModel>> GetRequestLogPageAsync(int pageIndex, int pageSize)
        {
            var list = dbContext.Set<RequestLog>().AsNoTracking();
            int count = await list.CountAsync();
            var pageList = await list.OrderByDescending(x => x.Timestamp)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PageModel<RequestLogViewModel>()
            {
                Data = mapper.Map<List<RequestLogViewModel>>(pageList),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = count
            };
        }

        /// <summary>
        /// 获取任务日志
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageModel<JobLogViewModel>> GetJobLogPageAsync(int pageIndex, int pageSize)
        {
            var list = dbContext.Set<JobLog>().AsNoTracking();
            int count = await list.CountAsync();
            var pageList = await list.OrderByDescending(x => x.Timestamp)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return new PageModel<JobLogViewModel>()
            {
                Data = mapper.Map<List<JobLogViewModel>>(pageList),
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = count
            };
        }


    }
}
