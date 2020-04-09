using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using Prism.Regions;
using BlankApp.Doamin.Contracts;
using Prism.Logging;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace BlankApp.ViewModels
{
    public class Menu
    {
        public string Header { get; set; }
        public UserControl View { get; set; }
    }
    public class MainWindowViewModel : BindableBase
    {
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IRegionManager _regionManager;
        private readonly IModuleManager _moduleManager;
        private readonly ILoggerFacade _logger;

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<Menu> _primaryMenus;
        public ObservableCollection<Menu> PrimaryMenu
        {
            get { return _primaryMenus ?? (_primaryMenus = new ObservableCollection<Menu>()); }
            set { SetProperty(ref _primaryMenus, value); }
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

                        PrimaryMenu.Clear();
                        var items = _regionManager.Regions[RegionContracts.TopContentRegion].Views.Cast<UserControl>();
                        foreach (var item in items)
                        {
                            PrimaryMenu.Add(new Menu
                            {
                                //此处到时候需要通过附加属性的方式来获取菜单名称
                                Header = Guid.NewGuid().ToString("N"),
                                View = item
                            });
                        }
                    });
                }
                return _loadCommand;
            }
        }

        private ICommand _switchMenuCommand;
        public ICommand SwitchMenuCommand
        {
            get
            {
                if (_switchMenuCommand == null)
                {
                    _switchMenuCommand = new DelegateCommand<Menu>(menu =>
                    {
                        _regionManager.Regions[RegionContracts.TopContentRegion].Activate(menu.View);
                    });
                }
                return _switchMenuCommand;
            }
        }
    }
}
