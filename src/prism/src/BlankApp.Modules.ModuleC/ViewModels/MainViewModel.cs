using Prism.Mvvm;

namespace BlankApp.Modules.ModuleC.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainViewModel()
        {
            Message = "View A from your Prism ModuleC";
        }
    }
}
