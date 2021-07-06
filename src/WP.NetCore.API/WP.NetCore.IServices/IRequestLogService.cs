using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.IServices
{
    public interface IServerLogService
    {
         Task<PageModel<RequestLogViewModel>> GetRequestLogPageAsync(int pageIndex, int pageSize);

        Task<PageModel<JobLogViewModel>> GetJobLogPageAsync(int pageIndex, int pageSize);
    }
}
