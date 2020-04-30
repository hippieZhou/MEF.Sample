using Prism.Ioc;
using BlankApp.Views;
using System.Windows;
using BlackApp.Application.Framework;
using BlankApp.Extensions;
using Serilog;
using System.Text;
using System.IO;
using Serilog.Events;
using BlankApp.ViewModels;
using BlankApp.Dialogs;
using Prism.Modularity;
using BlackApp.Application;
using BlankApp.Infrastructure;
using BlankApp.Infrastructure.CrossCutting;
using System;
using Prism.Services.Dialogs;
using BlackApp.Application.Modularity;
using BlackApp.Application.Contracts;

namespace BlankApp
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.CatchGlobalException();

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.File(
                path: Path.Combine("Logs", "log.txt"),
                encoding: Encoding.UTF8,
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();

            Log.Information("系统已启动。");

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();
            base.OnExit(e);
        }

        protected override void InitializeShell(Window shell)
        {
            //#region 登录窗口
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog(nameof(LoginDialog), new DialogParameters($"message=用户登录"), r =>
            {
                if (r.Result == ButtonResult.Cancel)
                {
                    Environment.Exit(0);
                }
            });
            //#endregion


            base.InitializeShell(shell);
        }

        protected override Window CreateShell()
        {
            EnginContext.Initialize(new GeneralEngine(Container));
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IBusinessModuleLoader, BusinessModuleLoader>();

            containerRegistry.RegisterDialogWindow<MainDialog>();
            containerRegistry.RegisterDialog<LoginDialog, LoginDialogViewModel>(nameof(LoginDialog));
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>(DialogContracts.Notification);

            //注册日志系统
            containerRegistry.RegisterSerilog();
            //注册应用服务
            containerRegistry.RegisterApplication(Container);
            //注册基础设施
            containerRegistry.RegisterInfrastructure(Container);
            //注册横切面
            containerRegistry.RegisterCrossCutting();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = AppDomain.CurrentDomain.BaseDirectory };
        }
    }
}
