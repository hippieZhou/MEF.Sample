using BlankApp.Infrastructure.Identity;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System;

namespace BlankApp.ViewModels
{
	public class LoginDialogViewModel: BindableBase
    {
		private readonly IIdentityManager _identityManager;

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


		public LoginDialogViewModel(IIdentityManager identityManager)
		{
			_identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
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
