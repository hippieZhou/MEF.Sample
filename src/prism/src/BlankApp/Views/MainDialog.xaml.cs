using Prism.Services.Dialogs;
using System.Windows;

namespace BlankApp.Views
{
    public partial class MainDialog : Window, IDialogWindow
    {
        public IDialogResult Result { get; set; }
        public MainDialog()
        {
            InitializeComponent();
        }
    }
}
