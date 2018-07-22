using Sample.Core;
using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Sample.Plugin2
{
    [Export(typeof(IView))]
    [CustomExportMetadata(1, "Plugin2")]
    public partial class MainView : UserControl, IView
    {
        [ImportMany(typeof(IWeight))]
        public Lazy<IWeight, IMetadata>[] Weights { get; private set; }
        public MainView()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void Loading_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.lv.Items.Clear();
            foreach (var weight in Weights)
            {
                var expander = new Expander()
                {
                    Header = weight.Metadata.Description
                };
                expander.Content = weight.Value;
                this.lv.Items.Add(expander);
            }
        }
    }
}
