using BlankApp.Infrastructure.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleB.ViewModels
{
    /// <summary>
    /// 接收跳转参数需要继承 INavigationAware 接口
    /// </summary>
    public class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IEventAggregator _eventAggregator;
        private IRegionNavigationJournal _journal;

        private string _message = "我是来自 ModuleB 中的主界面";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(message =>
            {
                Message = message;
            });
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            //页面跳转成功之后获取参数
            Trace.WriteLine("页面进入");
            Message ="我是模块B，传给我的参数为：" +  navigationContext.Parameters.GetValue<string>(nameof(Message));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //是否允许跳转到此页面
            return navigationContext.Parameters.ContainsKey(nameof(Message));
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Trace.WriteLine("页面离开");
        }
    }
}