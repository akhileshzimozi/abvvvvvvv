using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Blabyap.Views;
using Rg.Plugins.Popup.Services;
using Blabyap.Common.Model.NetworkModel;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;
using System.Windows.Input;
using Blabyap.Services;
using Blabyap.Common.Model;

namespace Blabyap.ViewModels
{


    public class SignUpPageViewModel : ViewModelBase
    {
       
        

        ApiServices _apiServices = new ApiServices();


        private RegisterInfo registerInfo;
        public RegisterInfo RegisterInfo
        {
            get { return this.registerInfo; }
            set { this.registerInfo = value; }
        }


       


        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand AddPhotoCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }


        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            Title = "Sign Up";
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
            AddPhotoCommand = new DelegateCommand(AddPhotoCommandAction).ObservesCanExecute(() => CanNavigate);
            SignUpCommand = new DelegateCommand(SignUpCommandAction).ObservesCanExecute(() => CanNavigate);

            this.registerInfo = new RegisterInfo();
        }

        private async void SignUpCommandAction()
        {

            CanNavigate = false;
            IsBusy = true;


            var isSuccess = await _apiServices.RegisterAsync(registerInfo.FullName, registerInfo.DisplayName, registerInfo.Birthday, registerInfo.ImageUrl, registerInfo.Email, registerInfo.Password, registerInfo.ConfirmPassword);
            if (isSuccess)
            {
                Constants.UserName = registerInfo.Email;
                Constants.Password = registerInfo.Password;
                await _pageDialogService.DisplayAlertAsync("Message", "Registered Successfully", "OK");
                Application.Current.MainPage = new NavigationPage(new SignUpGreetingPage());
            }

            else
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Please Enter Valid Email/Password.  " +
                    "Note : Password must contain atleast One Capital Letter, Small Alphabet, Special Symbol and Numeric Character", "OK");
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

        async void AddPhotoCommandAction()
        {
            CanNavigate = false;
            await PopupNavigation.Instance.PushAsync(new AddPhotoPopupPage());
            CanNavigate = true;
        }

    }

}
