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
    public class RequestLogService : IRequestLogService
    {
        private readonly WPDbContext dbContext;
        private readonly IMapper mapper;

        public RequestLogService(WPDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PageModel<RequestLogViewModel>> GetPageAsync(int pageIndex, int pageSize)
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

    }
}
