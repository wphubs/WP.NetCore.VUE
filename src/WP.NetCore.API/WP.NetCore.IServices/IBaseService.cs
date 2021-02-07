using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model;

namespace WP.NetCore.IServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<PageModel<TEntity>> GetPageListAsync(int pageIndex, int pageSize);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(Guid Id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
