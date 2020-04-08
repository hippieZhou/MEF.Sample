using BlankApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Windows;
using BlankApp.Infrastructure;
using BlankApp.Infrastructure.CrossCutting;

namespace BlankApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 使用面向接口编程的编程思想，请将服务的抽象和实现分隔开，划分每个服务的作用域
        /// 如果是全局服务的话建议在主程序中进行注册，各个模块都可以使用该服务的示例
        /// 模块级别的服务在相应模块内部注册即可，
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            #region 登录窗口
            var login = new LoginDialog();
            if (!login.ShowDialog().GetValueOrDefault())
            {
                Environment.Exit(0);
            }
            #endregion

            base.InitializeShell(shell);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册基础设施
            containerRegistry.AddInfrastructure();
            //注册横切面
            containerRegistry.AddCrossCutting();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //使用路径扫描的方式来进行模块发现
            return new DirectoryModuleCatalog() { ModulePath = AppDomain.CurrentDomain.BaseDirectory };
        }
    }
}
