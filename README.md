# 在 WPF 中实现插件式开发 (MEF + MvvmLightLibsStd10)

> 之前一直使用 MvvmLight 进行相关的 WPF 开发工作，插件式开发还是在 2 年前接触到 Prism 框架后才真正实践过，由于 Prism 本身太重，不太适合新人入门，我当初也是理解了好久一段时间。
这两天又看了一下 MEF 的相关东西，发现理解起来顺畅多了，于是写了一个简易版的 Demo 练一下手


## 示例描述

* 通过 IOC 和 DI 方式将服务和模块到处和动态加载
* 通过使用 MvvmLight 的 MessengerInstance 实现模块间通信

## 关键示例代码

* 主程序：Sample

```C#
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
```

* 子模块 1：Sample.Plugin1

```C#
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
```


* 子模块 2：Sample.Plugin2

```C#
    [Export(typeof(IView))]
    [ExportMetadata("name", "Plugin2")]
    public partial class MainView : UserControl, IView
    {
        public MainView()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
```


# 参考：

* [Building Hello MEF – Part II – Metadata and why being Lazy is a good thing.](https://blogs.msdn.microsoft.com/gblock/2009/12/04/building-hello-mef-part-ii-metadata-and-why-being-lazy-is-a-good-thing/)
* [【WPF】运用MEF实现窗口的动态扩展](https://www.cnblogs.com/tcjiaan/p/5844619.html)