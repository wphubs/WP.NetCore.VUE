using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.IServices
{
    public interface INothingService : IBaseService<Nothing>
    {
        Task<Nothing> GetTodayNothingData();
    }
}
