using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleA.ViewModels
{
    public class SubViewModel : BindableBase
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private readonly IRegionManager _regionManager;

        public SubViewModel(IRegionManager regionManager)
        {
            Message = "我是来自 ModuleA 中的次界面";
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
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
                    });
                }
                return _backCommand;
            }
        }
    }
}