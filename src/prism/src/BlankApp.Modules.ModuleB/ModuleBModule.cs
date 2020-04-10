using BlankApp.Doamin.Contracts;
using BlankApp.Doamin.Events;
using BlankApp.Doamin.Modularity;
using BlankApp.Modules.ModuleB.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace BlankApp.Modules.ModuleB
{
    [Module(OnDemand = true)]
    [BusinessModule(MainMenu = MainMenuContracts.B, FriendlyName = "模块B")]
    public class ModuleBModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public ModuleBModule(
            IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ShellSendEvent>().Subscribe(modelName =>
            {
                if (modelName == nameof(ModuleBModule))
                {
                    _regionManager.RequestNavigate(RegionContracts.MainContentRegion, typeof(MainView).FullName);
                }
            });
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>(typeof(MainView).FullName);
        }
    }
}