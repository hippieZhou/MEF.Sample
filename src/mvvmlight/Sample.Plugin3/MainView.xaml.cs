using Sample.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sample.Plugin3
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [Export(typeof(IWeight))]
    [CustomExportMetadata(2, "SubMenu", "这是一个字模块")]
    public partial class MainView : UserControl, IWeight
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
