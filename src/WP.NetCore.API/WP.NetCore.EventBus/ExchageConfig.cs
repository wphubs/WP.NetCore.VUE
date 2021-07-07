using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.EventBus
{
    public class ExchageConfig
    {
        public string Name { get; set; }
        public ExchangeType Type { get; set; }
        public string DeadExchangeName { get; set; }
    }
}
