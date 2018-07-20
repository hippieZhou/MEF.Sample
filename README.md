# 在 WPF 中实现插件式开发 (MEF + MvvmLightLibsStd10)

> 之前一直使用 MvvmLight 进行相关的 WPF 开发工作，插件式开发还是在 2 年前接触到 Prism 框架后才真正实践过，由于 Prism 本身太重，不太适合新人入门，我当初也是理解了好久一段时间。
这两天又看了一下 MEF 的相关东西，发现理解起来顺畅多了，于是写了一个简易版的 Demo 练一下手


## 示例描述

* 通过 IOC 和 DI 方式将服务和模块到处和动态加载
* 通过使用 MvvmLight 的 MessengerInstance 实现模块间通信

## 关键示例代码

* 主程序：Sample

```C#
    public partial class MainView : Window
    {
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
```

* 强类型元数据接口及相关实现

```C#
    public interface IMetadata
    {
        [DefaultValue(0)]
        int Priority { get; }
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CustomExportMetadata : ExportAttribute, IMetadata
    {
        public int Priority { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Author{ get; private set; }
        public string Version { get; private set; }

        public CustomExportMetadata() : base(typeof(IMetadata))
        {
        }
        public CustomExportMetadata(int priority):this()
        {
            this.Priority = priority;
        }
        public CustomExportMetadata(int priority, string name) : this(priority)
        {
            this.Name = name;
        }
        public CustomExportMetadata(int priority, string name, string description) : this(priority, name)
        {
            this.Description = description;
        }
        public CustomExportMetadata(int priority, string name, string description, string author) : this(priority, name, description)
        {
            this.Author = author;
        }
        public CustomExportMetadata(int priority, string name, string description, string author, string version) : this(priority, name, description, author)
        {
            this.Version = version;
        }
    }
```

* 子模块 1：Sample.Plugin1

```C#
    [Export(typeof(IView))]
    [CustomExportMetadata(0, "Plugin1")]
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
    [CustomExportMetadata(1, "Plugin2")]
    public partial class MainView : UserControl, IView
    {
        public MainView()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
```

# 涉及内容

* EMF
	* 延迟加载
	* 元数据（强类型）导出
* MVVM
* 模块通信
* 样式共享

# 参考：

* [Building Hello MEF – Part II – Metadata and why being Lazy is a good thing.](https://blogs.msdn.microsoft.com/gblock/2009/12/04/building-hello-mef-part-ii-metadata-and-why-being-lazy-is-a-good-thing/)
* [【WPF】运用MEF实现窗口的动态扩展](https://www.cnblogs.com/tcjiaan/p/5844619.html)
* [实战MEF（5）：导出元数据](https://www.cnblogs.com/tcjiaan/p/3324552.html)
* [一个基于MEF的可拓展的WPF Host程序](https://blog.csdn.net/zhaowei303/article/details/38071751)
* [MEF 编程指南（六）：导出和元数据](https://www.cnblogs.com/JavCof/p/3679224.html)
