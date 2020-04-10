using BlankApp.Models;
using Prism.Modularity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlankApp.Services
{
    public interface IModuleService
    {
        IEnumerable<IModuleInfo> Modules { get; }
        Task Initialized();
    }
}
