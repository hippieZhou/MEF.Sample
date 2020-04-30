using BlankApp.Dialogs;
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
                ProcessException(app.Container.Resolve(typeof(ILoggerFacade)) as ILoggerFacade, app.Container.Resolve(typeof(IDialogService)) as IDialogService, args.Exception);
                args.Handled = true;
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                ProcessException(app.Container.Resolve(typeof(ILoggerFacade)) as ILoggerFacade, app.Container.Resolve(typeof(IDialogService)) as IDialogService, args.ExceptionObject);
            };
        }

        private static void ProcessException(ILoggerFacade loggerFacade, IDialogService dialogService, object exception)
        {
            if (loggerFacade != null)
            {
                loggerFacade.Log(exception.ToString(), Category.Exception, Priority.High);
            }

            if (dialogService != null)
            {
                dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"message=异常提示"), r =>
                {
                    //Environment.Exit(0);
                });
            }
        }
    }
}
