using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.IServices;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;
using System.Linq;
using WP.NetCore.Common.Helper;

namespace WP.NetCore.Services
{
    public class BaseService<TEntity>: IBaseService<TEntity> where TEntity : EntityBase, new()
    {
        public IBaseRepository<TEntity> baseDal;


        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstAsync(long Id)
        {
            return await baseDal.FirstAsync(Id);
        }

        public async Task<TEntity> FirstNoTrackingAsync(long Id)
        {
            return await baseDal.FirstNoTrackingAsync(Id);
        }



        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Id = new Snowflake().NextId();
            return await baseDal.AddAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(long Id)
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
            var list = await baseDal.GetPageAsync(x => x.IsDelete == false, x => x.CreateTime, pageIndex, pageSize);
            return new PageModel<TEntity>() {Data= list.Data.ToList(),PageIndex=pageIndex,PageSize=pageSize,Total=list.Total};
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
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
