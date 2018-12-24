using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Blabyap.Views;
using Blabyap.Services;

namespace Blabyap.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {

        private bool hideAgeFlag;
        public bool HideAgeFlag
        {
            get { return hideAgeFlag; }
            set { SetProperty(ref hideAgeFlag, value); }
        }
        public DelegateCommand VIPAccountCommand { get; set; }
        public DelegateCommand UnlockFeatuesCommand { get; set; }
        public DelegateCommand MeetNowCommand { get; set; }
        public DelegateCommand SuperMatchCommand { get; set; }
        public DelegateCommand SkinChangeCommand { get; set; }
        public DelegateCommand DeleteAccountCommand { get; set; }
        public DelegateCommand HideAgeCommand { get; set; }

        ApiServices apiServices;
        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            Title = "Settings";
            apiServices = new ApiServices();
            VIPAccountCommand = new DelegateCommand(VIPAccountCommandAction).ObservesCanExecute(()=> CanNavigate);
            UnlockFeatuesCommand = new DelegateCommand(UnlockFeatuesCommandAction).ObservesCanExecute(() => CanNavigate);
            MeetNowCommand = new DelegateCommand(MeetNowCommandAction).ObservesCanExecute(() => CanNavigate);
            SuperMatchCommand = new DelegateCommand(SuperMatchCommandAction).ObservesCanExecute(() => CanNavigate);
            SkinChangeCommand = new DelegateCommand(SkinChangeCommandAction).ObservesCanExecute(() => CanNavigate);
            DeleteAccountCommand = new DelegateCommand(DeleteAccountCommandAction).ObservesCanExecute(() => CanNavigate);
            // HideAgeCommand = new DelegateCommand(HideAgeCommandAction);

        }

       

        //private async void HideAgeCommandAction()
        //{

        //    if (HideAgeFlag == true)
        //    { 
        //    var hideFlag = new HideUserData
        //    {
        //        HideFlag = true
        //    };
        //    await apiServices.PostHideAge(hideFlag);

        //    }
        //    else
        //    {
        //        var hideFlag = new HideUserData
        //        {
        //            HideFlag = false
        //        };
        //        await apiServices.PostHideAge(hideFlag);

        //    }
        //}

        private void VIPAccountCommandAction()
        {
            CanNavigate = false;
            _pageDialogService.DisplayAlertAsync("VIP Account Coming Soon", "VIP Account Clicked", "Ok");
            CanNavigate = true;

        }

        private void UnlockFeatuesCommandAction()
        {
            CanNavigate = false;

            _pageDialogService.DisplayAlertAsync("Unlock Features Coming Soon", "Unlock Features Clicked", "Ok");
            CanNavigate = true;

        }

        async void MeetNowCommandAction()
        {
            CanNavigate = false;

            await _pageDialogService.DisplayAlertAsync("Meet Now Features Coming Soon", "Meet Now Features Clicked", "Ok");
            CanNavigate = true;

        }

        async void SuperMatchCommandAction()
        {
            CanNavigate = false;

            await _pageDialogService.DisplayAlertAsync("Super Match Features Coming Soon", "Super Match Features Clicked", "Ok");
            CanNavigate = false;

        }

        async void SkinChangeCommandAction()
        {
            CanNavigate = false;

            await _navigationService.NavigateAsync("ChooseSkinPage");
            CanNavigate = true;

        }

        async void DeleteAccountCommandAction()
        {
            CanNavigate = false;

            await _pageDialogService.DisplayAlertAsync("Alert", "Are you sure to delete your Accont", "Ok", "Cancel");
            IsBusy = true;
            var isValid = await apiServices.PostDelete();

            if(isValid.status == "Success")
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Your Account is deleted Successfully", "Ok");
                await _navigationService.NavigateAsync(nameof(LoginPage));

            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Some issue occured while deleting your Account.Please try again.", "Ok");
            }
            IsBusy = false;
            CanNavigate = true;

        }
    }
}
