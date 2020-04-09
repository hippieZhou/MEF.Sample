using BlankApp.Infrastructure.Event;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace BlankApp.Modules.ModelTop.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        public ViewAViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            //_eventAggregator.GetEvent<MessageSentEvent>().Publish($"我来自 ModelTop 模块，正在广播当前时间：{DateTime.Now}");
        }
    }
}
