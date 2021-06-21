using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model
{
    public class UserContext: IUserContext
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
