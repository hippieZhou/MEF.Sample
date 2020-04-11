using BlankApp.Doamin.Events;
using BlankApp.Infrastructure.Identity;
using BlankApp.Infrastructure.Identity.Entities;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleA.ViewModels
{
    public class SideViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IIdentityManager _identityManager;
        private readonly ILoggerFacade _logger;

        public SideViewModel(
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IIdentityManager identityManager,
            ILoggerFacade logger)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        private ICommand _authCommand;
        public ICommand AuthCommand
        {
            get
            {
                if (_authCommand == null)
                {
                    _authCommand = new DelegateCommand(() =>
                    {
                        var message = _identityManager.CurrentUser.Role == ApplicationRole.Administrator ? "是" : "不是";
                        message = $"当前用户{message}管理员";

                        _dialogService.ShowDialog("NotificationDialog", new DialogParameters($"message={message}"), r =>
                        {
                            if (r.Result == ButtonResult.None)
                                Title = "Result is None";
                            else if (r.Result == ButtonResult.OK)
                                Title = "Result is OK";
                            else if (r.Result == ButtonResult.Cancel)
                                Title = "Result is Cancel";
                            else
                                Title = "I Don't know what you did!?";
                        });

                        _eventAggregator.GetEvent<MessageSentEvent>().Publish(message);
                        _logger.Log(message, Category.Debug, Priority.High);
                    });
                }
                return _authCommand;
            }
        }
    }
}
