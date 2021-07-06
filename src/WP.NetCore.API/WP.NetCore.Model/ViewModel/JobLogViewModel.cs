using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.ViewModel
{
    public class JobLogViewModel
    {
        public int id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Level { get; set; }
        public string Message { get; set; }
        public JobLogProperties Properties { get; set; }

        public DateTime? _ts { get; set; }
    }

    public class JobLogProperties
    {
     
            public string JobId { get; set; }
            public string JobGroup { get; set; }
            public string JobName { get; set; }
            public float Time { get; set; }
            public bool Result { get; set; }
            public string Message { get; set; }
            public string LogType { get; set; }

    }

}
