using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model
{
    public interface IEntity
    {
        long Id { get; set; }
        DateTime? CreateTime { get; set; }

        DateTime? DeleteTime { get; set; }

        bool IsDelete { get; set; }

        long? CreateBy { get; set; }

        DateTime? ModifyTime { get; set; }
        long? ModifyBy { get; set; }


    }

    /// <summary>
    /// 实现接口IEntity,所有实体继承该这个基类
    /// </summary>
    public abstract class Entity : IEntity
    {

        public Entity()
        {
            CreateTime = DateTime.Now;
            IsDelete = false;
        }
     
        public long Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? CreateBy { get; set; }

        public DateTime? ModifyTime { get; set; }

        public long? ModifyBy { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
