using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using BlankApp.Models;
using System.Linq;
using BlankApp.Doamin.Services;
using BlankApp.Infrastructure.Identity;
using Prism.Events;
using BlankApp.Doamin.Events;
using Prism.Services.Dialogs;
using BlankApp.Dialogs;

namespace BlankApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IModuleService _moduleService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogService _dialogService;
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
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IIdentityManager identityManager)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));

            _eventAggregator.GetEvent<RaisedExceptionEvent>().Subscribe(ex => 
            {
                _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"message=异常提示"), r =>
                {
                   //todo
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
                        #region 预加载模块
                        Modules.Clear();
                        Modules.AddRange(_moduleService.Modules.Select(x => x.ToBusiness()));
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
                        if (business != null)
                        {
                            _moduleService.ActivateSideView(business.Module);
                            _moduleService.ActivateMainView(business.Module);
                        }
                    });
                }
                return _invokeModuleCommand;
            }
        }
    }
}
