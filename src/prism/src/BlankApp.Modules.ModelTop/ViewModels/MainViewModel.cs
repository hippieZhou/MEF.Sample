using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using BlankApp.Infrastructure.Event;

namespace BlankApp.Modules.ModelTop.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private string _message = "我来自 ModelTop 模块，位于 TopContentRegion 区域";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        private ICommand _sendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (_sendCommand == null)
                {
                    _sendCommand = new DelegateCommand(() =>
                    {
                        _eventAggregator.GetEvent<MessageSentEvent>().Publish($"我来自 ModelTop 模块，正在广播当前时间：{DateTime.Now}");
                    });
                }
                return _sendCommand;
            }
        }
    }
}