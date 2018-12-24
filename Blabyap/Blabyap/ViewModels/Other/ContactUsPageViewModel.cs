using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Blabyap.Common.Model.NetworkModel;
using Blabyap.Services;
using Blabyap.Common.Model;
using System.Diagnostics;

namespace Blabyap.ViewModels
{
    public class ContactUsPageViewModel : ViewModelBase
    {


        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        private string mobile;
        public string Mobile
        {
            get { return mobile; }
            set { SetProperty(ref mobile, value); }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }

        ApiServices apiServices;

        public ContactUsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
            SendCommand = new DelegateCommand(SendCommandAction).ObservesCanExecute(() => CanNavigate);

            apiServices = new ApiServices();
        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;

        }

        public void DemoData(BasicUserInfoModel userInfoModel, ResponseResultCVLinkedIn cVLinkedIn)
        {
            try
            {
                if (cVLinkedIn == null)
                {
                    FullName = userInfoModel.FullName;
                    Email = userInfoModel.Email;

                }
                else
                {
                    FullName = cVLinkedIn.Data.userdetails.FullName;
                    Email = cVLinkedIn.Data.userdetails.Email;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private async void SendCommandAction()
        {
            CanNavigate = false;
            IsBusy = true;
            try { var postContacts = new ContactSuggest {

                MobileNumber = Mobile,
                FullName = FullName,
                Message = Message
            };

            var result = await apiServices.PostContactUs(postContacts);

            if(result.status == "Success")
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Our team will contact you in next 24 hours.", "Ok");
                await _navigationService.GoBackAsync();
            } else
                await _pageDialogService.DisplayAlertAsync("Alert", "Try again", "Ok");

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
            CanNavigate = true;



        }
    }
}
