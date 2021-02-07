using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.Model;
using System.Linq;
namespace WP.NetCore.Services
{
    public class BaseService<TEntity>: IBaseService<TEntity> where TEntity : class, IEntity, new()
    {
        public IBaseRepository<TEntity> baseDal;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await baseDal.AddAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid Id)
        {
            await baseDal.DeleteAsync(Id);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageModel<TEntity>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return null;
            //return await baseDal.GetPageAsync(x => x.IsDelete == false, x => x.CreateTime, pageIndex, pageSize);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await baseDal.GetAllAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            await baseDal.UpdateAsync(entity);
        }

    }
}
