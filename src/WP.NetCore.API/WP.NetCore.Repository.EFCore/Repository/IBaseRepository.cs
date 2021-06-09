﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.Repository.EFCore
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase, new()
    {
        /// <summary>
        /// IQueryable to Entity
        /// </summary>
        /// <returns>IQueryable<Entity></returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns>返回所有数据</returns>
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// 根据条件获取全部数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回所有数据</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件以及排序获取数据
        /// </summary>
        /// <typeparam name="TProperty">排序字段</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isAsc">是否正序（默认倒叙）</param>
        /// <returns>返回所有数据</returns>
        Task<List<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, bool isAsc = false);


        IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="TProperty">排序字段</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="isAsc">是否正序（默认倒叙）</param>
        /// <returns>返回数据总数量和数据</returns>
        Task<(int Total, IQueryable<TEntity> Data)> GetPageAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, int pageIndex, int pageSize, bool isAsc = false);
        /// <summary>
        /// 根据主键id获取实体数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>返回实体或空</returns>
        Task<TEntity> FirstNoTrackingAsync(long id);
        /// <summary>
        /// 根据查询条件获取实体数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回实体或空</returns>
        Task<TEntity> FirstNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);



        /// <summary>
        /// 根据主键id获取实体数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>返回实体或空</returns>
        Task<TEntity> FirstAsync(long id);
        /// <summary>
        /// 根据查询条件获取实体数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回实体或空</returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);



        /// <summary>
        /// 根据条件判断数据是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回新增后的实体</returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据主键id删除数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task DeleteAsync(long id);
        /// <summary>
        /// 根据实体删除数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        Task RemoveAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns></returns>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
