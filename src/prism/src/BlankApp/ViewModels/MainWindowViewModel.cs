using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using BlankApp.Models;
using System.Linq;
using BlankApp.Doamin.Events;
using BlankApp.Doamin.Bus;
using Prism.Events;
using System.Diagnostics;
using BlankApp.Doamin.Services;

namespace BlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventBus _bus;
        private readonly IModuleService _moduleService;
        private readonly IModuleManager _moduleManager;
        private readonly IEventAggregator _eventAggregator;
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
            IEventBus bus,
            IModuleService moduleService,
            IModuleManager moduleManager,
            IEventAggregator eventAggregator)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
            _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
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
                        Trace.WriteLine(DateTime.Now);
                        _eventAggregator.GetEvent<ShellSendEvent>().Publish(business.Module.ModuleName);
                    });
                }
                return _invokeModuleCommand;
            }
        }
    }
}
