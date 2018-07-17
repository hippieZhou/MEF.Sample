using Sample.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Sample
{
    public partial class MainWindow : Window
    {
        private CompositionContainer container = null;

        public MainWindow()
        {
            InitializeComponent();
            var dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Plugins"));
            if (dir.Exists)
            {
                var catalog = new DirectoryCatalog(dir.FullName, "Sample.*.dll");
                container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                var list = container.GetExportedValues<IView>();
                foreach (var item in list)
                {
                    var attr = item.GetType().GetCustomAttribute<ExportMetadataAttribute>();
                    this.tab.Items.Add(new TabItem() { Header = attr.Value, Content = item });
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            container?.Dispose();
            base.OnClosing(e);
        }
    }
}
