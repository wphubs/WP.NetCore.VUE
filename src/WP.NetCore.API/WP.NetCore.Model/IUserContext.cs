using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model
{
    public interface IUserContext
    {
        long Id { get; set; }

        string Name { get; set; }
    }
}
