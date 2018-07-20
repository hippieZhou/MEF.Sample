using Sample.Core;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sample
{
    public partial class MainView : Window
    {
        /// <summary>
        /// 如果使用 [ImportMany(typeof(IView))] 的方式，
        /// 可以省略 Plugins = container.GetExportedValues<IView>();
        /// </summary>
        [ImportMany(typeof(IView))]
        public Lazy<IView, IMetadata>[] Plugins { get; private set; }

        private CompositionContainer container = null;

        public MainView()
        {
            InitializeComponent();
            var dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Plugins"));
            if (dir.Exists)
            {
                var catalog = new DirectoryCatalog(dir.FullName, "Sample.*.dll");
                container = new CompositionContainer(catalog);
                container.ComposeParts(this);

                Plugins.OrderBy(p => p.Metadata.Priority);

                foreach (var item in Plugins)
                {
                    this.tab.Items.Add(new TabItem()
                    {
                        Header = item.Metadata.Name,
                        Content = item.Value
                    });
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
