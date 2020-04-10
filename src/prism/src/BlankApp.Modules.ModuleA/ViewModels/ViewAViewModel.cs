using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using System;
using BlankApp.Doamin.Events;
using Prism.Events;

namespace BlankApp.Modules.ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private IRegionNavigationJournal _journal;
        public ViewAViewModel(
             ILoggerFacade logger,
             IEventAggregator eventAggregator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(logger));
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(message =>
            {
                QueryString = message;
            });
        }

        private string _queryString = "我是来自 ModuleA 中的 A 界面";
        public string QueryString
        {
            get { return _queryString; }
            set { SetProperty(ref _queryString, value); }
        }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new DelegateCommand(() =>
                    {
                        if (_journal.CanGoBack)
                        {
                            _journal.GoBack();
                        }
                    });
                }
                return _backCommand;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return navigationContext.Parameters.ContainsKey(nameof(QueryString));
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            //页面跳转成功之后获取参数
            _logger.Log("页面进入", Category.Debug, Priority.None);
            QueryString = "我是模块A，传给我的参数为：" + navigationContext.Parameters.GetValue<string>(nameof(QueryString));
        }
    }
}
