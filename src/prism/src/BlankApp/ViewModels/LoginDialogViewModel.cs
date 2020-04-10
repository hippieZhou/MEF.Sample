using BlankApp.Infrastructure.Identity;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;
using Prism.Logging;
using BlankApp.Infrastructure.Context;

namespace BlankApp.ViewModels
{
	public class LoginDialogViewModel: BindableBase
    {
		private readonly ApplicationDbContext _dbContext;
		private readonly IIdentityManager _identityManager;
		private readonly ILoggerFacade _logger;

		private bool? _dialogResult;
		public bool? DialogResult
		{
			get { return _dialogResult; }
			set { SetProperty(ref _dialogResult, value); }
		}

		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		public LoginDialogViewModel(
			ApplicationDbContext dbContext,
			IIdentityManager identityManager,
			ILoggerFacade logger)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
						await ApplicationDbInitializer.SeedAsync(_dbContext);
					});
				}
				return _loadedCommand;
			}
		}

		private ICommand _loginCommand;
		public ICommand LoginCommand
		{
			get
			{
				if (_loginCommand == null)
				{
					_loginCommand = new DelegateCommand(() =>
					{
						var ok = _identityManager.Login("admin", "password");
						if (ok)
						{
							_logger.Log("登录成功", Category.Debug, Priority.High);
							DialogResult = true;
						}
						else
						{
							Message = "用户名和密码不匹配, 登录失败";
						}
					});
				}
				return _loginCommand;
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
						DialogResult = false;
					});
				}
				return _exitCommand;
			}
		}
	}
}
