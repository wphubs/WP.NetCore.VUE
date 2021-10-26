using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Repository.SqlSugar.Uow
{
    public interface ISqlSugarUnitOfWork
    {
        SqlSugarClient sqlSugarClient { get; }

        void Begin();
        void Commit();
        void Rollback();

   
        //Task BeginAsync();
        //Task CommitAsync();
        //Task RollbackAsync();

    
    }
}
