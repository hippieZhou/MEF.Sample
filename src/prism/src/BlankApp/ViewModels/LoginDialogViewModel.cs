using BlackApp.Application.Framework;
using BlankApp.Infrastructure.Context;
using BlankApp.Infrastructure.Identity;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace BlankApp.ViewModels
{
	public class LoginDialogViewModel : BindableBase, IDialogAware
	{
		private readonly IIdentityManager _identityManager;
		private readonly ILoggerFacade _logger;

		public string Title => "用户登录";

		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		public LoginDialogViewModel(
			IIdentityManager identityManager,
			ILoggerFacade logger)
		{
			_identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private ICommand _loadedCommand;
		public ICommand LoadedCommand
		{
			get
			{
				if (_loadedCommand == null)
				{
					_loadedCommand = new DelegateCommand(async () =>
					{
						var identityDbContext = EnginContext.Current.Resolve<ApplicationIdentityDbContext>();
						await ApplicationDbInitializer.SeedIdentityAsync(identityDbContext);
					});
				}
				return _loadedCommand;
			}
		}

		private ICommand _adminloginCommand;
		public ICommand AdminLoginCommand
		{
			get
			{
				if (_adminloginCommand == null)
				{
					_adminloginCommand = new DelegateCommand(() =>
					{
						Login("管理员", "admin");
					});
				}
				return _adminloginCommand;
			}
		}

		private ICommand _userloginCommand;
		public ICommand UserLoginCommand
		{
			get
			{
				if (_userloginCommand == null)
				{
					_userloginCommand = new DelegateCommand(() =>
					{
						Login("普通用户", "user");
					});
				}
				return _userloginCommand;
			}
		}

		private void Login(string userName, string password)
		{
			var ok = _identityManager.Login(userName, password);
			if (ok)
			{
				_logger.Log("登录成功", Category.Debug, Priority.High);
				RaiseRequestClose(new DialogResult(ButtonResult.OK));
			}
			else
			{
				Message = "用户名和密码不匹配, 登录失败";
			}
		}

		private ICommand _exitCommand;
		public ICommand ExitCommand
		{
			get
			{
				if (_exitCommand == null)
				{
					_exitCommand = new DelegateCommand(() =>
					{
						RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
					});
				}
				return _exitCommand;
			}
		}

		#region Services
		public event Action<IDialogResult> RequestClose;
		public virtual void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}

		public virtual bool CanCloseDialog()
		{
			return true;
		}
		public virtual void OnDialogClosed()
		{
		}

		public virtual void OnDialogOpened(IDialogParameters parameters)
		{
			Message = parameters.GetValue<string>("message");
		}
		#endregion
	}
}
