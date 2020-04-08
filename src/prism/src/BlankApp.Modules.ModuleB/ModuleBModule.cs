using BlankApp.Modules.ModuleB.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace BlankApp.Modules.ModuleB
{
    [Module(OnDemand = true)]
    public class ModuleBModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleBModule(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RegisterViewWithRegion(RegionContracts.ContentRegion, typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>(nameof(ModuleBModule));
        }
    }
}