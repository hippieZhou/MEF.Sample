using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Sample.Core;

namespace Sample.Plugin1
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IService service;

        private int _number;
        public int Number
        {
            get { return _number; }
            set { Set(ref _number, value); }
        }

        private RelayCommand _todoCommand;

        public MainViewModel(IService service)
        {
            this.service = service;
        }

        public RelayCommand TodoCommand
        {
            get
            {
                return _todoCommand ?? (_todoCommand = new RelayCommand(() =>
                    {
                        service?.QueryData(Number, sum => { this.Number = sum; });
                        MessengerInstance.Send(this.Number, "Plugin");
                    }));
            }
        }
    }
}
