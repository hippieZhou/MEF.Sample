using BlankApp.Doamin.Contracts;
using BlankApp.Modules.ModuleA.Views;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleA.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string _message  = "我是来自 ModuleA 中的主界面";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private readonly IRegionManager _regionManager;
        private readonly ILoggerFacade _logger;

        public MainViewModel(
            IRegionManager regionManager,
            ILoggerFacade logger)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private ICommand _navCommand;
        public ICommand NavCommand
        {
            get
            {
                if (_navCommand == null)
                {
                    _navCommand = new DelegateCommand(() =>
                    {
                        var parameters = new NavigationParameters();
                        parameters.Add("QueryString", DateTime.Now);
                        _regionManager.RequestNavigate(RegionContracts.MainContentRegion, nameof(ViewA), result =>
                         {
                             _logger.Log(string.Format("Navigation to {0} complete. ", result.Context.Uri), Category.Debug, Priority.High);
                         }, parameters);
                    });
                }
                return _navCommand;
            }
        }
    }
}