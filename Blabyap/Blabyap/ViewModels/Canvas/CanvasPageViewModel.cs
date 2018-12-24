using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Blabyap.Views.Other;

namespace Blabyap.ViewModels
{
    public class CanvasPageViewModel : ViewModelBase
    {
        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand PolicyCommand { get; set; }
        public DelegateCommand TermsCondCommand { get; set; }
        public DelegateCommand ContactUsCommand { get; set; }
        public DelegateCommand SuggestionsCommand { get; set; }

        public CanvasPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(() => CanNavigate);
            PolicyCommand = new DelegateCommand(PolicyCommandAction).ObservesCanExecute(() => CanNavigate);
            TermsCondCommand = new DelegateCommand(TermsCondCommandAction).ObservesCanExecute(() => CanNavigate);
            ContactUsCommand = new DelegateCommand(ContactUsCommandAction).ObservesCanExecute(() => CanNavigate);
            SuggestionsCommand = new DelegateCommand(SuggestionsCommandAction).ObservesCanExecute(() => CanNavigate);


        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync("BlabyMasterDetailPage/NavigationPage");
            CanNavigate = true;
        }

        async void PolicyCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync("PrivacyPolicyPage");
            CanNavigate = true;
        }

        async void TermsCondCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync("PremiumPage");
            CanNavigate = true;
        }

        async void ContactUsCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync("ContactUsPage");
            CanNavigate = true;

        }

        async void SuggestionsCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync(nameof(SuggestionPage));
            CanNavigate = true;

        }
    }
}
