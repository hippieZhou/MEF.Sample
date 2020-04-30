using BlackApp.Application.Contracts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace BlankApp.Modules.ModuleB.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            Message = "View A from your Prism ModuleB";
        }

        private ICommand _sayCommand;
        public ICommand SayCommand
        {
            get
            {
                if (_sayCommand == null)
                {
                    _sayCommand = new DelegateCommand(() =>
                    {

                        _dialogService.ShowDialog(DialogContracts.Notification, new DialogParameters($"message={DateTime.Now}"), r =>
                        {
                            //Environment.Exit(0);
                        });
                    });
                }
                return _sayCommand;
            }
        }
    }
}
