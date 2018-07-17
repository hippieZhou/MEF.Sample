using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core
{
    public interface IService
    {
        void QueryData(int numuber, Action<int> action);
    }
}
