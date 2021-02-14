using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model
{
    public class ViewModelBase
    {
        public long Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? CreateBy { get; set; }

        public DateTime? ModifyTime { get; set; }

        public long? ModifyBy { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDelete { get; set; }
    }
}
