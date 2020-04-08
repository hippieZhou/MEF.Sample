using BlankApp.Doamin.Contracts;
using BlankApp.Infrastructure.Event;
using Prism.Commands;
using Prism.Events;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;

        public MainViewModel(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            ILoggerFacade logger)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(message => 
            {
                Message = message;
            });
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
                        _regionManager.RequestNavigate(RegionContracts.MainContentRegion, "SubView", result =>
                         {
                             _logger.Log(string.Format("Navigation to {0} complete. ", result.Context.Uri), Category.Debug, Priority.High);
                         });
                    });
                }
                return _navCommand;
            }
        }
    }
}