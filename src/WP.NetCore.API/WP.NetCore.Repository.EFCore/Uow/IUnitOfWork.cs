using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Repository.EFCore
{
    public interface IUnitOfWork : IDisposable
    {
        WPDbContext DbContext { get; }
        #region Nomal
        void Begin();
        void Commit();
        void Rollback();
        void Save();
        #endregion

        #region Asynchronous
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveAsync();
        #endregion

    }
}
