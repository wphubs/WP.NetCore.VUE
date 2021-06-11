using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.ViewModel
{
    public class RequestLogViewModel
    {
        public int id { get; set; }

        public string Timestamp { get; set; }

        public string Level { get; set; }
        public string Message { get; set; }
        public RequestLogProperties Properties { get; set; }

        public DateTime _ts { get; set; }
    }

    public class RequestLogProperties
    {
            public string RequestHost { get; set; }
            public string RequestScheme { get; set; }
            public string Agent { get; set; }
            public string IP { get; set; }
            public string RequestMethod { get; set; }
            public string RequestPath { get; set; }
            public int StatusCode { get; set; }
            public float Elapsed { get; set; }
            public string SourceContext { get; set; }
            public string RequestId { get; set; }
            public string ConnectionId { get; set; }
  
    }

}
