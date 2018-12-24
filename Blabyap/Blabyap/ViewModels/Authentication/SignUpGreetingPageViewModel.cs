using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Blabyap.Views;
using System.Windows.Input;
using Xamarin.Forms;
using Blabyap.Services;
using Blabyap.Common.Model.NetworkModel;

namespace Blabyap.ViewModels
{
    public class SignUpGreetingPageViewModel : ViewModelBase
    {
        ApiServices _apiServices;
        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand CompleteCVCommand { get; set; }
        public DelegateCommand TakeTourCommand { get; set; }

        public SignUpGreetingPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
              CompleteCVCommand = new DelegateCommand(CompleteCVCommandAction).ObservesCanExecute(() => CanNavigate);
            TakeTourCommand = new DelegateCommand(TakeTourCommandAction).ObservesCanExecute(() => CanNavigate);
            _apiServices = new ApiServices();
        }

        private async void CompleteCVCommandAction()
        {

            CanNavigate = false;
            IsBusy = true;
           
            var tokenresponse = await _apiServices.PostGetAuthorizationTokenResponse();


            if (string.IsNullOrEmpty(tokenresponse.Access_Token) != true)
                await _navigationService.NavigateAsync("/BlabyMasterDetailPage/NavigationPage/MyCVPage");


            IsBusy = false;
            CanNavigate = true;
        }

      
        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;
        }

       
        async void TakeTourCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync(nameof(TourCarouselPage));
            CanNavigate = true;
        }
    }
}
