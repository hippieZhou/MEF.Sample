using BlankApp.Doamin.Services;
using Prism.Regions;
using System;

namespace BlankApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;

        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
        }
        public void NavigateTo(string regionName, string viewName)
        {
            _regionManager.RequestNavigate(regionName, viewName);
        }
    }
}
