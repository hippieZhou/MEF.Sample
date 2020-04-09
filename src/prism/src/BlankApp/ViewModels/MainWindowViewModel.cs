using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using Prism.Regions;
using BlankApp.Doamin.Contracts;
using Prism.Logging;
using System.Collections.ObjectModel;

namespace BlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ILoggerFacade _logger;
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<IModuleInfo> _modules;
        public ObservableCollection<IModuleInfo> Modules
        {
            get { return _modules ?? (_modules = new ObservableCollection<IModuleInfo>()); }
            set { SetProperty(ref _modules, value); }
        }

        public MainWindowViewModel(
            IRegionManager regionManager,
            IModuleCatalog moduleCatalog,
            IModuleManager moduleManager,
            ILoggerFacade logger)
        {
            _moduleCatalog = moduleCatalog ?? throw new ArgumentNullException(nameof(moduleCatalog));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new DelegateCommand(() =>
                    {
                        Modules.Clear();

                        _moduleManager.LoadModuleCompleted += (sender, e) =>
                        {
                            Modules.Add(e.ModuleInfo);
                            _logger.Log(string.Format("{0} 加载完毕", e.ModuleInfo.ModuleName), Category.Debug, Priority.High);
                        };

                        #region 加载模块
                        foreach (var module in _moduleCatalog.Modules)
                        {
                            if (module == null)
                                new DllNotFoundException(nameof(module));

                            if (module.State != ModuleState.Initialized)
                            {
                                _moduleManager.LoadModule(module.ModuleName);
                            }
                        }
                        #endregion
                    });
                }
                return _loadCommand;
            }
        }

        private ICommand _switchModuleCommand;
        public ICommand SwitchModuleCommand
        {
            get
            {
                if (_switchModuleCommand == null)
                {
                    _switchModuleCommand = new DelegateCommand<string>(viewName =>
                    {
                        #region 页面导航
                        var parameters = new NavigationParameters();
                        parameters.Add("Message", "我是来自页面导航的参数");

                        _regionManager.RequestNavigate(RegionContracts.MainContentRegion, viewName, result =>
                        {
                            _logger.Log(string.Format("Navigation to {0}.{1}.{2} complete. ", viewName, result.Result, result.Context.Uri), Category.Debug, Priority.High);
                        }, parameters);
                        #endregion
                    });
                }
                return _switchModuleCommand;
            }
        }
    }
}
