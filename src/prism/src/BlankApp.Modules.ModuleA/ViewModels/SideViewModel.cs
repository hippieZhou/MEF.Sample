using BlankApp.Doamin.Events;
using BlankApp.Infrastructure.Identity;
using BlankApp.Infrastructure.Identity.Entities;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleA.ViewModels
{
    public class SideViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IIdentityManager _identityManager;
        private readonly ILoggerFacade _logger;

        public SideViewModel(
            IEventAggregator eventAggregator,
            IIdentityManager identityManager,
            ILoggerFacade logger)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private Visibility _visibility;

        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
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
                        Visibility = _identityManager.CurrentUser.Role == ApplicationRole.Administrator ? Visibility.Visible : Visibility.Collapsed;
                        var message = _identityManager.CurrentUser.Role == ApplicationRole.Administrator ? "是" : "不是";
                        message = $"当前用户{message}管理员";
                        _eventAggregator.GetEvent<MessageSentEvent>().Publish(message);
                        _logger.Log(message, Category.Debug, Priority.High);
                    });
                }
                return _authCommand;
            }
        }
    }
}
