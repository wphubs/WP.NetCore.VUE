using System;
using System.Collections.Generic;
using System.Text;
using WP.NetCore.Model.EntityModel;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WP.NetCore.Model;

namespace WP.NetCore.Repository.EFCore
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase, new()
    {
        private readonly WPDbContext _dbContext;
        private readonly IUserContext userContext;

        public BaseRepository(WPDbContext _DbContext, IUserContext userContext)
        {
            _dbContext = _DbContext;
            this.userContext = userContext;
        }
        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().Where(x => x.IsDelete == false).AsNoTracking();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Query().ToListAsync();
        }
  

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Query().Where(predicate).ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, bool isAsc = false)
        {
            var list = isAsc == true ? Query().OrderBy(order) : Query().OrderByDescending(order);
            return await list.Where(predicate).ToListAsync();
        }

        public async Task<IQueryable<TEntity>> LoadNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await Task.Run(() =>  Query().Where(predicate));
        }
      

        public async Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await Task.Run(()=> _dbContext.Set<TEntity>().Where(predicate));
        }




        public async Task<(int Total,IQueryable<TEntity> Data)> GetPageAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, int pageIndex, int pageSize, bool isAsc = false)
        {
            var list = Query().Where(predicate);

            int count = await list.CountAsync();
            var orderList = isAsc == true ? list.OrderBy(order) : list.OrderByDescending(order);

            var pageList =  orderList
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
            return (count, pageList);
        }



        public async Task<TEntity> FirstNoTrackingAsync(long id)
        {
            return await Query().Where(w => w.Id == id && w.IsDelete == false).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstAsync(long id)
        {
            return await _dbContext.Set<TEntity>().Where(w => w.Id == id&&w.IsDelete==false).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Query().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Query().AnyAsync(predicate);
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreateBy = userContext.Id;
            var addEntitiy = await _dbContext.AddAsync(entity);
            await SaveAsync();
            return addEntitiy.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            await SaveAsync();
        }



        public async Task UpdateAsync(TEntity entity)
        {
            var info = await _dbContext.FindAsync<TEntity>(entity.Id);
            if (info != null)
            {
                //database  property
                var types = info.GetType();
                var property = types.GetProperties();
                //entity property
                var entityType = entity.GetType();
                var entityTypeProperty = entityType.GetProperties();
                foreach (var item in property)
                {
                    switch (item.Name)
                    {
                        case nameof(info.CreateTime):
                            continue;
                        case nameof(info.DeleteTime):
                            continue;
                        case nameof(info.IsDelete):
                            continue;
                        case nameof(info.Id):
                            continue;
                        case nameof(info.ModifyBy):
                            item.SetValue(info, userContext.Id);
                            continue;
                        case nameof(info.ModifyTime):
                            item.SetValue(info, DateTime.Now);
                            continue;
                        default:
                            break;
                    }
                    foreach (var entityItem in entityTypeProperty)
                    {
                        if (entityItem.Name == item.Name)
                        {
                            item.SetValue(info, entityItem.GetValue(entity));
                        }
                    }
                }
                await SaveAsync();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities.Count() > 0)
            {
                foreach (var entity in entities)
                {
                    var info = await _dbContext.FindAsync<TEntity>(entity.Id);
                    if (info != null)
                    {
                        //database  property
                        var types = info.GetType();
                        var property = types.GetProperties();
                        //entity property
                        var entityType = entity.GetType();
                        var entityTypeProperty = entityType.GetProperties();
                        foreach (var item in property)
                        {
                            switch (item.Name)
                            {
                                case nameof(info.CreateTime):
                                    continue;
                                case nameof(info.DeleteTime):
                                    continue;
                                case nameof(info.IsDelete):
                                    continue;
                                case nameof(info.Id):
                                    continue;
                                case nameof(info.ModifyBy):
                                    item.SetValue(info, userContext.Id);
                                    continue;
                                case nameof(info.ModifyTime):
                                    item.SetValue(info, DateTime.Now);
                                    continue;
                                default:
                                    break;
                            }
                            foreach (var entityItem in entityTypeProperty)
                            {
                                if (entityItem.Name == item.Name)
                                {
                                    item.SetValue(info, entityItem.Name);
                                }
                            }
                        }

                    }
                }
                await SaveAsync();
            }
        }

        public async Task  RemoveAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var info = await _dbContext.FindAsync<TEntity>(id);
            if (info != null)
            {
                info.IsDelete = true;
                info.DeleteTime = DateTime.Now;
                info.ModifyBy = userContext.Id;
                await SaveAsync();
            }
        }


        public async Task DeleteAsync(TEntity entity)
        {
            await DeleteAsync(entity.Id);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities.Count() > 0)
            {
                foreach (var entity in entities)
                {
                    var info = await _dbContext.FindAsync<TEntity>(entity.Id);
                    if (info != null)
                    {
                        info.IsDelete = true;
                        info.DeleteTime = DateTime.Now;
                        info.ModifyBy = userContext.Id;

                    }
                }
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


    }
}
