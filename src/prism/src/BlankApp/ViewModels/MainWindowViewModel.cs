using BlackApp.Application.Events;
using BlackApp.Application.Modularity;
using BlankApp.Dialogs;
using BlankApp.Infrastructure.Identity;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BlankApp.ViewModels
{
    public class Node
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public Node(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }

    public class MainWindowViewModel : BindableBase
    {
        private readonly IBusinessModuleLoader _moduleLoader;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
        private readonly IIdentityManager _identityManager;

        private string _title = "Prism Application For PHM";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _message = "日志区域";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private ObservableCollection<BusinessModuleEntity> _modules;
        public ObservableCollection<BusinessModuleEntity> Modules
        {
            get { return _modules; }
            set { SetProperty(ref _modules, value); }
        }

        private ObservableCollection<Node> _nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes ?? (_nodes = new ObservableCollection<Node>()); }
            set { SetProperty(ref _nodes, value); }
        }

        public MainWindowViewModel(
            IBusinessModuleLoader moduleLoader,
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IIdentityManager identityManager,
            ILoggerFacade logger)
        {
            _moduleLoader = moduleLoader ?? throw new ArgumentNullException(nameof(moduleLoader));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));

            _eventAggregator.GetEvent<RaisedExceptionEvent>().Subscribe(ex =>
            {
                _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"message=异常提示"), r =>
                {
                    //记录系统内部已知的错误日志
                });
            });
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                {
                    _loadedCommand = new DelegateCommand(() =>
                    {
                        Modules = new ObservableCollection<BusinessModuleEntity>(_moduleLoader.Modules);
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
                    _invokeNodeCommand = new DelegateCommand<string>(header =>
                    {
                        Nodes.Clear();
                        var items = Modules
                        .GroupBy(x => x.Header)
                        .Where(x => x.Key == header)
                        .SingleOrDefault().Select(x => new Node(x.Id, x.FriendlyName));
                        Nodes.AddRange(items);
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
                        if (business != null)
                        {
                            _moduleLoader.ActivateModuleView(business);
                        }
                    });
                }
                return _invokeModuleCommand;
            }
        }
    }
}
