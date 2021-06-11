using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.IServices;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Repository.EFCore;

namespace WP.NetCore.Services
{
    public class RequestLogService: BaseService<RequestLog>, IRequestLogService
    {
        public RequestLogService(IBaseRepository<RequestLog> baseRepository)
        {
            this.baseDal = baseRepository;
        }
    }
}
