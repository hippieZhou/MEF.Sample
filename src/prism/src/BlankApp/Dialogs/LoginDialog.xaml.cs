using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BlankApp.Dialogs
{
    public partial class LoginDialog : UserControl
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                Window.GetWindow(this).DragMove();
            }
        }
    }
}
