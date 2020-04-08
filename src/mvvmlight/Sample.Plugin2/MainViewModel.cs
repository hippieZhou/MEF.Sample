using GalaSoft.MvvmLight;

namespace Sample.Plugin2
{
    public class MainViewModel : ViewModelBase
    {
        private int _number;
        public int Number
        {
            get { return _number; }
            set { Set(ref _number, value); }
        }
        public MainViewModel()
        {
            MessengerInstance.Register<int>(this, "Plugin", obj => 
            {
                this.Number = -obj;
            });
        }
    }
}
