using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model
{
    public class ViewModelBase
    {
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid? CreateBy { get; set; }

        public DateTime? ModifyTime { get; set; }

        public Guid? ModifyBy { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
