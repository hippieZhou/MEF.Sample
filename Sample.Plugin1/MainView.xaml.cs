using Sample.Core;
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Sample.Plugin1
{
    [Export(typeof(IView))]
    [ExportMetadata("name", "Plugin1")]
    public partial class MainView : UserControl, IView
    {
        [ImportingConstructor]
        public MainView([Import("DataService")]IService service)
        {
            this.DataContext = new MainViewModel(service);
            InitializeComponent();
        }
    }
}
