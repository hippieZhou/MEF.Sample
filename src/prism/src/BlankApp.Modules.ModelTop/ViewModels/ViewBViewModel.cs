using Prism.Commands;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using System;
using BlankApp.Doamin.Contracts;

namespace BlankApp.Modules.ModelTop.ViewModels
{
    public class ViewBViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;
        private readonly ILoggerFacade _logger;

        public ViewBViewModel(
            IRegionManager regionManager,
             IModuleManager moduleManager,
             ILoggerFacade logger)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(moduleManager));
        }

        private ICommand _navigationCommand;
        public ICommand NavigationCommand
        {
            get
            {
                if (_navigationCommand == null)
                {
                    _navigationCommand = new DelegateCommand(() =>
                    {
                        #region 页面导航
                        var parameters = new NavigationParameters
                        {
                            { "Message", "我是来自页面导航的参数" }
                        };

                        var viewName = "ModuleBModule";

                        _regionManager.RequestNavigate(RegionContracts.MainContentRegion, viewName, result =>
                        {
                            _logger.Log(string.Format("Navigation to {0}.{1}.{2} complete. ", viewName, result.Result, result.Context.Uri), Category.Debug, Priority.High);
                        }, parameters);
                        #endregion
                    });
                }
                return _navigationCommand;
            }
        }
    }
}
