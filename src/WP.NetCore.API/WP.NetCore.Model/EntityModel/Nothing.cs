using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.EntityModel
{
    public class Nothing : EntityBase
    {
        public string Content { get; set; }

        public string Source { get; set; }

        public string Author { get; set; }

        public int Weight { get; set; }
    }
}
