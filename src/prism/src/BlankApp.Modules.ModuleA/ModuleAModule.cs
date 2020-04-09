using BlankApp.Doamin.Contracts;
using BlankApp.Modules.ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BlankApp.Modules.ModuleA
{
    /// <summary>
    /// 启用按需加载模式
    /// </summary>
    [Module(OnDemand = true)]
    public class ModuleAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 如果页面不需要导航的话可以使用如下方式进行页面注册
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionContracts.ChildContentRegion, typeof(ViewA));
            regionManager.RegisterViewWithRegion(RegionContracts.ChildContentRegion, typeof(ViewB));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //如果页面需要导航的话可以使用如下方式进行页面注册（参数用于标识页面名称，需要做到全局唯一，不能重复）
            containerRegistry.RegisterForNavigation<MainView>(nameof(ModuleAModule));
            containerRegistry.RegisterForNavigation<MainView>(nameof(SubView));
        }
    }
}