using BlankApp.Doamin.Context;
using BlankApp.Doamin.Entities;
using BlankApp.Infrastructure.Event;
using BlankApp.Modules.ModuleB.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleB.ViewModels
{
    /// <summary>
    /// 接收跳转参数需要继承 INavigationAware 接口
    /// </summary>
    public class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IAsyncRepository<Person> _asyncRepository;
        private readonly IEventAggregator _eventAggregator;
        private IRegionNavigationJournal _journal;

        public MainViewModel(
            IAsyncRepository<Person> asyncRepository,
            IEventAggregator eventAggregator)
        {
            _asyncRepository = asyncRepository ?? throw new ArgumentNullException(nameof(asyncRepository));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<MessageSentEvent>().Subscribe(message =>
            {
                QueryString = message;
            });
        }

        private string _queryString = "我是来自 ModuleB 中的主界面";
        public string QueryString
        {
            get { return _queryString; }
            set { SetProperty(ref _queryString, value); }
        }

        private ObservableCollection<PersonDto> _persons;
        public ObservableCollection<PersonDto> Persons
        {
            get { return _persons ?? (_persons = new ObservableCollection<PersonDto>()); }
            set { SetProperty(ref _persons, value); }
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

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new DelegateCommand(() =>
                    {
                        var persons = _asyncRepository.Table.Where(x => x.Name.Contains(QueryString));
                        if (persons.Any())
                        {
                            Persons.Clear();
                            foreach (var person in persons)
                            {
                                Persons.Add(new PersonDto { Name = person.Name });
                            }
                        }
                    });
                }
                return _searchCommand;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            //页面跳转成功之后获取参数
            Trace.WriteLine("页面进入");
            QueryString ="我是模块B，传给我的参数为：" +  navigationContext.Parameters.GetValue<string>(nameof(QueryString));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //是否允许跳转到此页面
            return navigationContext.Parameters.ContainsKey(nameof(QueryString));
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Trace.WriteLine("页面离开");
        }
    }
}