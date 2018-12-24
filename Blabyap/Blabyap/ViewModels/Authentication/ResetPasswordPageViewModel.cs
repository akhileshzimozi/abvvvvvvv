using Blabyap.Common.Model;
using Blabyap.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Blabyap.ViewModels.Authentication
{
	public class ResetPasswordPageViewModel : ViewModelBase
	{

		public string code { get; set; }
		public string confirmPassword { get; set; }
		public string email { get; set; }
		public string password { get; set; }

		ApiServices _apiServices;

		public DelegateCommand PreviousCommand { get; set; }
		public ResetPasswordPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService)
			: base(navigationService, pageDialogService, deviceService)
		{
			PreviousCommand = new DelegateCommand(PreviousCommandAction);
			_apiServices = new ApiServices();
		}
		public ICommand ResetPwdCommand
		{
			get
			{
				return new Command(async () =>
				{


					var reset = new ResetPassword
					{
					  Code=code,
					  Email=email,
					  Password=password,
						ConfirmPassword = confirmPassword
					};


					var result = await _apiServices.PostResetPassword(reset);

					if (result.status == "Fail")
					{
						await _pageDialogService.DisplayAlertAsync("Alert", "ResetPassword not done." + 
							"Please try again", "OK");
					}
					else
					{

						await _pageDialogService.DisplayAlertAsync("Alert", "Your password is reset Successfully.", "OK");
						await _navigationService.NavigateAsync("LoginPage");

					}



				});
			}
		}

		async void PreviousCommandAction()
		{
			await _navigationService.GoBackAsync();
		}

	}
}
