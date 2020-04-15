using BlankApp.Doamin.Contracts;
using BlankApp.Doamin.Services;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace BlankApp.Services
{
    public class ModuleService : IModuleService
    {
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
        }

        public IEnumerable<IModuleInfo> Modules => _moduleCatalog.Modules;

        public void ActivateSideView(IModuleInfo module)
        {
            LoadModule(module);

            _regionManager.Regions[RegionContracts.SideContentRegion].ActiveViews.Cast<UserControl>().ToList().ForEach(p =>
            {
                _regionManager.Regions[RegionContracts.SideContentRegion].Deactivate(p);
            });

            var sideView = _regionManager.Regions[RegionContracts.SideContentRegion].Views
                .FirstOrDefault(x => string.Equals(x.GetType().Assembly.CodeBase, module.Ref, StringComparison.OrdinalIgnoreCase));
            if (sideView != null)
            {
                _regionManager.Regions[RegionContracts.SideContentRegion].Activate(sideView);
            }
        }

        public void ActivateMainView(IModuleInfo module)
        {
            LoadModule(module);
            _regionManager.RequestNavigate(RegionContracts.MainContentRegion, module.ModuleName);
        }

        private void LoadModule(IModuleInfo module)
        {
            if (module == null)
                throw new ArgumentNullException(nameof(module));

            if (module.State != ModuleState.Initialized)
            {
                _moduleManager.LoadModule(module.ModuleName);
            }
        }
    }
}
