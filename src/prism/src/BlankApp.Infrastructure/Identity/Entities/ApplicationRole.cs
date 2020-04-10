using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp.Infrastructure.Identity.Entities
{
    [Flags]
    public enum ApplicationRole
    {
        Administrator,
        User
    }
}
