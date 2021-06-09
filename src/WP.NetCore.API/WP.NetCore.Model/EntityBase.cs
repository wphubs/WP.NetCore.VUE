using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model
{
    /// <summary>
    /// 实现接口IEntity,所有实体继承该这个基类
    /// </summary>
    public abstract class EntityBase 
    {

        public EntityBase()
        {
            CreateTime = DateTime.Now;
            IsDelete = false;
        }
     
        public long Id { get; set; }
        public DateTime? CreateTime { get; set; } = new DateTime();
        public long? CreateBy { get; set; }

        public DateTime? ModifyTime { get; set; }

        public long? ModifyBy { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
