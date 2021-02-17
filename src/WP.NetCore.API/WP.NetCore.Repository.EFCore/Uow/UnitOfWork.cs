using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Repository.EFCore
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly WPDbContext _dbContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(WPDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException("ef core dbcontext can not as null");
        }

        public WPDbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public void Begin()
        {
            if (_transaction == null)
            {
                _transaction = DbContext.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                Save();
                _transaction.Commit();
                Disposable();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                Disposable();
            }
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {

        }

        public void Disposable()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
        public async Task BeginAsync()
        {
            if (_transaction == null)
            {
                _transaction = await DbContext.Database.BeginTransactionAsync();
            }
        }
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await SaveAsync();
                await _transaction.CommitAsync();
                await DisposableAsync();
            }
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await DisposableAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        private async Task DisposableAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

      
    }

}
