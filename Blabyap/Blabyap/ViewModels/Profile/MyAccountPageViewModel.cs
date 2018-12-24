using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Blabyap.Services;
using Blabyap.Common.Model;
using System.Diagnostics;

namespace Blabyap.ViewModels
{
    public class MyAccountPageViewModel : ViewModelBase
    {
            private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }
            private string dispalyName;
        public string DispalyName
        {
            get { return dispalyName; }
            set { SetProperty(ref dispalyName, value); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        private string mobileNumber;
        public string MobileNumber
        {
            get { return mobileNumber; }
            set { SetProperty(ref mobileNumber, value); }
        }
        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set { SetProperty(ref birthday, value); }
        }
       
      

        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand ChangePwdCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        ApiServices apiServices;

        public MyAccountPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) 
            : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
            ChangePwdCommand = new DelegateCommand(ChangePwdCommandAction).ObservesCanExecute(() => CanNavigate);
            LogoutCommand = new DelegateCommand(LogoutCommandAction).ObservesCanExecute(() => CanNavigate);

            apiServices = new ApiServices();
        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;

        }

        private async void ChangePwdCommandAction()
        {
            CanNavigate = false;

            await _navigationService.NavigateAsync("ChangePasswordPage");
            CanNavigate = true;

        }
        public void DemoData(BasicUserInfoModel userInfoModel, ResponseResultCVLinkedIn cVLinkedIn)
        {
            try
            {
                if (cVLinkedIn == null)
                { 
                    FullName = userInfoModel.FullName;
                    Birthday = userInfoModel.Birthday;
                    DispalyName = userInfoModel.DisplayName;
                    Email = userInfoModel.Email;

                }
                else { 
                    FullName = cVLinkedIn.Data.userdetails.FullName;
                    DispalyName = cVLinkedIn.Data.userdetails.DisplayName;
                    Email = cVLinkedIn.Data.userdetails.Email;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async  void LogoutCommandAction()
        {
            CanNavigate = false;
            IsBusy = true;
            var result = await apiServices.PostLogOut();
            if (result == true)
            {
                await _navigationService.NavigateAsync("LoginPage");

            }
            IsBusy = false;
            CanNavigate = true;
        }
    }
}
