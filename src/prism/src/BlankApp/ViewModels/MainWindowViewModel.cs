using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using BlankApp.Models;
using System.Linq;
using System.Diagnostics;
using BlankApp.Doamin.Services;
using BlankApp.Doamin.Contracts;
using Prism.Regions;
using System.Windows.Controls;
using BlankApp.Infrastructure.Identity;

namespace BlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IModuleService _moduleService;
        private readonly IModuleManager _moduleManager;
        private readonly IRegionManager _regionManager;
        private readonly IIdentityManager _identityManager;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<BusinessViewModel> _modules;
        public ObservableCollection<BusinessViewModel> Modules
        {
            get { return _modules ?? (_modules = new ObservableCollection<BusinessViewModel>()); }
            set { SetProperty(ref _modules, value); }
        }

        private ObservableCollection<Node> _nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes ?? (_nodes = new ObservableCollection<Node>()); }
            set { SetProperty(ref _nodes, value); }
        }

        public MainWindowViewModel(
            IModuleService moduleService,
            IModuleManager moduleManager,
            IRegionManager regionManager,
            IIdentityManager identityManager)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                {
                    _loadedCommand = new DelegateCommand(async() =>
                    {
                        #region 预加载模块

                        await _moduleService.Initialized();
                        var business = _moduleService.Modules.Select(x => x.ToBusiness());
                        Modules.Clear();
                        Modules.AddRange(business);
                        RaisePropertyChanged(nameof(Modules));

                        #endregion

                        Title = $"{_identityManager.CurrentUser.UserName}，欢迎回来";
                    });
                }
                return _loadedCommand;
            }
        }

        private ICommand _invokeNodeCommand;
        public ICommand InvokeNodeCommand
        {
            get
            {
                if (_invokeNodeCommand == null)
                {
                    _invokeNodeCommand = new DelegateCommand<string>(key =>
                    {
                        Nodes.Clear();
                        Nodes.AddRange(Modules.GetNodes(key));
                        RaisePropertyChanged(nameof(Nodes));
                    });
                }
                return _invokeNodeCommand;
            }
        }

        private ICommand _invokeModuleCommand;
        public ICommand InvokeModuleCommand
        {
            get
            {
                if (_invokeModuleCommand == null)
                {
                    _invokeModuleCommand = new DelegateCommand<Node>(node =>
                    {
                        if (node == null)
                            throw new ArgumentNullException(nameof(node));

                        var business = Modules.FirstOrDefault(x => x.Id == node.Id);
                        if (business.Module.State != ModuleState.Initialized)
                        {
                            _moduleManager.LoadModule(business.Module.ModuleName);
                        }

                        _regionManager.Regions[RegionContracts.SideContentRegion].ActiveViews.Cast<UserControl>().ToList().ForEach(p => 
                        {
                            _regionManager.Regions[RegionContracts.SideContentRegion].Deactivate(p);
                        });

                        var sideView = _regionManager.Regions[RegionContracts.SideContentRegion].Views
                        .FirstOrDefault(x => string.Equals(x.GetType().Assembly.CodeBase, business.Module.Ref, StringComparison.OrdinalIgnoreCase));
                        if (sideView != null)
                        {
                            _regionManager.Regions[RegionContracts.SideContentRegion].Activate(sideView);
                        }

                        _regionManager.RequestNavigate(RegionContracts.MainContentRegion, business.Module.ModuleName);
                    });
                }
                return _invokeModuleCommand;
            }
        }
    }
}
