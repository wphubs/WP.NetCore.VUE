using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Common.Consts
{
  



    public class MQExchange
    {
        public const string Logs = "exchange-logs";

        public const string Dead = "exchange-dead-letter";
    }

    public class MQRoutingKeys
    {
        public const string Logs = "LogRouting";
    }
}
