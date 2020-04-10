using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankApp.Doamin.Services
{
    public interface INavigationService
    {
        void NavigateTo(string regionName, string viewName);
    }
}
