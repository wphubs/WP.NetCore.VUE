using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WP.NetCore.Model;

namespace WP.NetCore.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public ISqlSugarClient Db { get;  }
    }
}
