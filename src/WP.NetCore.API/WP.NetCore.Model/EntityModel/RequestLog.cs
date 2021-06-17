using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.EntityModel
{
    public class RequestLog 
    {
        public int id { get; set; }

        [StringLength(100)]
        public string Timestamp { get; set; }

        [StringLength(15)]
        public string Level { get; set; }
        public string Message { get; set; }
        public string Properties { get; set; }


        public string Exception { get; set; }


        public DateTimeOffset _ts { get; set; }

     
    }
}
