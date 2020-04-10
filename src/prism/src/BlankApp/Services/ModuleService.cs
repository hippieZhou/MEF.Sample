using BlankApp.Doamin.Services;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlankApp.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IList<IModuleInfo> _modules;

        private readonly IModuleCatalog _moduleCatalog;
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;

        public ModuleService(
        IRegionManager regionManager,
        IModuleCatalog moduleCatalog,
        IModuleManager moduleManager)
        {
            _moduleCatalog = moduleCatalog ?? throw new ArgumentNullException(nameof(moduleCatalog));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
            _modules = new List<IModuleInfo>();
        }

        public IEnumerable<IModuleInfo> Modules => _modules;

        public async Task Initialized()
        {
            foreach (var module in _moduleCatalog.Modules)
            {
                if (module == null)
                    new DllNotFoundException(nameof(module));

                _modules.Add(module);
            }
            await Task.Yield();
        }
    }
}
