using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Repository.SqlSugar.Uow
{
    public class SqlSugarUnitOfWork: ISqlSugarUnitOfWork
    {

        /// <summary>
        /// SqlSugar 对象
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlSugarClient"></param>

        public SqlSugarUnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = (SqlSugarClient)sqlSugarClient;
        }

        public SqlSugarClient sqlSugarClient
        {
            get
            {
                return _sqlSugarClient;
            }
        }

        public void Begin()
        {
            sqlSugarClient.Ado.BeginTran(IsolationLevel.ReadCommitted);
        }

      
        public void Commit()
        {
            sqlSugarClient.CommitTran();
        }


        public void Rollback()
        {
            sqlSugarClient.RollbackTran();
        }

   
    }
}
