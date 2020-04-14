using BlankApp.Dialogs;
using Prism.Ioc;
using Prism.Logging;
using Prism.Services.Dialogs;
using Prism.Unity;
using System;

namespace BlankApp.Extensions
{
    public static class ApplicationExtension
    {
        public static void CatchGlobalException(this PrismApplication app)
        {
            app.DispatcherUnhandledException += (sender, args) =>
            {
                app.Container.Resolve<ILoggerFacade>()?.Log(args.Exception.ToString(), Category.Exception, Priority.High);
                args.Handled = true;
                app.Container.Resolve<IDialogService>()?.ShowDialog(nameof(NotificationDialog), new DialogParameters($"message=异常提示"), r => 
                {
                    Environment.Exit(0);
                });
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                app.Container.Resolve<ILoggerFacade>()?.Log(args.ExceptionObject.ToString(), Category.Exception, Priority.High);
                app.Container.Resolve<IDialogService>()?.ShowDialog(nameof(NotificationDialog), new DialogParameters($"message=异常提示"), r =>
                {
                    Environment.Exit(0);
                });
            };
        }
    }
}
