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

namespace Blabyap.ViewModels 
{
	public class ChangePasswordPageViewModel : ViewModelBase
	{

		public string oldPassword { get; set; }
		public string newPassword { get; set; }
		public string confirmPassword { get; set; }


		ApiServices _apiServices;

		public DelegateCommand PreviousCommand { get; set; }
		public DelegateCommand ChangePwdCommand { get; set; }

		public ChangePasswordPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) 
			: base(navigationService, pageDialogService, deviceService)
		{
			PreviousCommand = new DelegateCommand(PreviousCommandAction);
			ChangePwdCommand = new DelegateCommand(ChangePwdCommandAction);

			_apiServices = new ApiServices();
		}

		private async void ChangePwdCommandAction()
		{

			CanNavigate = false;
			IsBusy = true;
			var changePassword = new ChangePassword
			{
				OldPassword = oldPassword,
				NewPassword = newPassword,
				ConfirmPassword = confirmPassword
			};


			var result = await _apiServices.PostChangePassword(changePassword);

			if (result.status == "Fail")
			{
				await _pageDialogService.DisplayAlertAsync("Alert", "Please Enter Valid Email/Password.  " +
							"Note : Password must contain atleast One Capital Letter, Small Alphabet, Special Symbol and Numeric Character", "OK");
			}
			else
			{

				await _pageDialogService.DisplayAlertAsync("Alert", "Your password is changed Successfully.", "OK");
				await _navigationService.NavigateAsync("LoginPage");

			}
			IsBusy = false;
			CanNavigate = true;
		}





		async void PreviousCommandAction()
		{
			CanNavigate = false;
			await _navigationService.GoBackAsync();
			CanNavigate = true;
		}


	}
}
