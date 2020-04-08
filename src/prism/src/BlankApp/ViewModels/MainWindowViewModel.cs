using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using Prism.Regions;
using BlankApp.Doamin.Contracts;
using System.Diagnostics;

namespace BlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(
            IRegionManager regionManager,
            IModuleCatalog moduleCatalog,
            IModuleManager moduleManager)
        {
            _moduleCatalog = moduleCatalog ?? throw new ArgumentNullException(nameof(moduleCatalog));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
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

        private ICommand _menuCommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menuCommand == null)
                {
                    _menuCommand = new DelegateCommand<string>(viewName =>
                    {
                        #region 页面导航
                        var parameters = new NavigationParameters();
                        parameters.Add("Message", "我是来自页面导航的参数");

                        _regionManager.RequestNavigate(RegionContracts.MainContentRegion, viewName, result =>
                        {
                            Trace.WriteLine(string.Format("Navigation to {0}.{1}.{2} complete. ", viewName, result.Result, result.Context.Uri));
                        }, parameters);
                        #endregion
                    });
                }
                return _menuCommand;
            }
        }
    }
}
