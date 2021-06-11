using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.EntityModel
{
    public class RequestLog : EntityBase
    {
        public string Timestamp { get; set; }

        public string Level { get; set; }
        public string Message { get; set; }

        public string text { get; set; }
        public string Properties { get; set; }

        public DateTime _ts { get; set; }

     
    }
}
