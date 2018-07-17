using Sample.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Service
{
    [Export(nameof(DataService), typeof(IService))]
    public class DataService : IService
    {
        public void QueryData(int numuber, Action<int> action)
        {
            action(numuber + 1);
        }
    }
}
