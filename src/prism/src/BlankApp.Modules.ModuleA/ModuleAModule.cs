using BlankApp.Doamin.Bus;
using BlankApp.Doamin.Contracts;
using BlankApp.Doamin.Events;
using BlankApp.Doamin.Modularity;
using BlankApp.Modules.ModuleA.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace BlankApp.Modules.ModuleA
{
    /// <summary>
    /// 启用按需加载模式
    /// </summary>
    [Module(OnDemand = true)]
    [BusinessModule(MainMenu = MainMenuContracts.A, FriendlyName = "模块A")]
    public class ModuleAModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public ModuleAModule(
            IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ShellSendEvent>().Subscribe(modelName =>
            {
                if (modelName == nameof(ModuleAModule))
                {
                    _regionManager.RequestNavigate(RegionContracts.SideContentRegion, typeof(SideView).FullName);
                    _regionManager.RequestNavigate(RegionContracts.MainContentRegion, typeof(MainView).FullName);
                }
            });
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 如果页面不需要导航的话可以使用如下方式进行页面注册
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //如果页面需要导航的话可以使用如下方式进行页面注册（参数用于标识页面名称，需要做到全局唯一，不能重复）
            containerRegistry.RegisterForNavigation<SideView>(typeof(SideView).FullName);
            containerRegistry.RegisterForNavigation<MainView>(typeof(MainView).FullName);
            containerRegistry.RegisterForNavigation<ViewA>(typeof(ViewA).FullName);
        }
    }
}