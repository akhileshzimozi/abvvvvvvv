using Blabyap.Common.Model;
using Blabyap.Services;
using Blabyap.Views.Authentication;
using Plugin.Connectivity;
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
	public class ForgotPasswordPageViewModel : ViewModelBase
	{
		public string email { get; set; }

		ApiServices _apiServices;

		public DelegateCommand PreviousCommand { get; set; }

		public DelegateCommand ForgotPwdCommand { get; set; }

		public ForgotPasswordPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService)
			: base(navigationService, pageDialogService, deviceService)
		{
			PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=>CanNavigate);
			ForgotPwdCommand = new DelegateCommand(ForgotPwdCommandAction).ObservesCanExecute(() => CanNavigate);
			_apiServices = new ApiServices();
		}

		private async void ForgotPwdCommandAction()
		{

			CanNavigate = false;
			IsBusy = true;
			var forgot = new ForgotPassword
			{
				Email = email
			};


			var result = await _apiServices.PostForgotPassword(forgot);

			if (result.status == "Fail")
			{
				await _pageDialogService.DisplayAlertAsync("Alert", "Email is not valid." +
					 "Please SignUp or Try again.", "OK");
			}
			else
			{

				await _pageDialogService.DisplayAlertAsync("Alert", "Email sent you Successfully.", "OK");
				await _navigationService.NavigateAsync("LoginPage");

			}

			IsBusy = false;
			CanNavigate = true;
		}

	
		public  async void CheckConnection()
		{
			if (!CrossConnectivity.Current.IsConnected)
				await _pageDialogService.DisplayAlertAsync("Alert", "No Internet", "ok");
			// await _navigationService.NavigateAsync("ProviderLoginPage");


			else
				return;
		}
		async void PreviousCommandAction()
		{
			CanNavigate = false;
			await _navigationService.GoBackAsync();
			CanNavigate = true;
		}

	}
}
