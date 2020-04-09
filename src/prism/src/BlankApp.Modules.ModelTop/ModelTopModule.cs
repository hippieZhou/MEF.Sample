using BlankApp.Doamin.Contracts;
using BlankApp.Modules.ModelTop.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace BlankApp.Modules.ModelTop
{
    [Module(OnDemand = true)]
    public class ModelTopModule : IModule
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// 注入区域管理器
        /// </summary>
        /// <param name="regionManager"></param>
        public ModelTopModule(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 如果页面不需要导航的话可以使用如下方式进行页面注册
            //_regionManager.RegisterViewWithRegion(RegionContracts.TopContentRegion, typeof(MainView));
            _regionManager.RegisterViewWithRegion(RegionContracts.TopContentRegion, typeof(ViewA));
            _regionManager.RegisterViewWithRegion(RegionContracts.TopContentRegion, typeof(ViewB));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}