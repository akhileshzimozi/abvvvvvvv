using System;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Blabyap.Services;
using Blabyap.Common.Model.NetworkModel;
using System.Diagnostics;
using Xamarin.Auth;
using Blabyap.NetworkModel;
using Blabyap.Common.Model;

namespace Blabyap.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    { 
        //   readonly IPageDialogService pageDialogService;
        public string UserName { get; set; }
        public string Password { get; set; }
        Account account;
        AccountStore store;
        private ApiServices _apiServices;


        public DelegateCommand ForgotCommand { get; set; }
        public DelegateCommand SignUpCommand { get; set; }
        public DelegateCommand LinkedInCommand { get; set; }

        public DelegateCommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) 
            : base(navigationService, pageDialogService, deviceService)
        {

           Title = "Login";

            SignUpCommand = new DelegateCommand(SignUpCommandAction).ObservesCanExecute(() => CanNavigate);
            LinkedInCommand = new DelegateCommand(LinkedInCommandAction).ObservesCanExecute(() => CanNavigate);
            ForgotCommand = new DelegateCommand(ForgotCommandAction).ObservesCanExecute(() => CanNavigate);
            LoginCommand = new DelegateCommand(LoginCommandAction).ObservesCanExecute(() => CanNavigate);
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            _apiServices = new ApiServices();
        }

        private async void LoginCommandAction()
        {
            CanNavigate = false;
            try
            {
                IsBusy = true;
                _apiServices.StoreUserNameNPassword(UserName, Password);

                var tokenresponse = await _apiServices.PostGetAuthorizationTokenResponse();


                if (string.IsNullOrEmpty(tokenresponse.Access_Token) != true)

                {

                    //Application.Current.MainPage = new NavigationPage(new MyCVPage());
                    await _navigationService.NavigateAsync("/BlabyMasterDetailPage/NavigationPage/MyCVPage");



                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Alert", "Please Enter Valid Email/Password.  " +
                        "Note : Password must contain atleast One Capital Letter, Small Alphabet, Special Symbol and Numeric Character", "OK");

                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            CanNavigate = true;
        }

        public void LinkedInCommandAction()
        {
            CanNavigate = false;
            IsBusy = true;

            var authenticator = new OAuth2Authenticator(
                  clientId: "77oba6ddrqi37w",
               clientSecret: "vb7paAa7jO0AwFzx",
                 scope: "r_basicprofile,r_emailaddress",
               authorizeUrl: new Uri(Constants.Accounts.LinkedinUrl),
                   redirectUrl: new Uri(Constants.Accounts.PostLinkedinAzure),
                 accessTokenUrl: new Uri(Constants.Accounts.AccessToken)
);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
            IsBusy = false;
            CanNavigate = true;

        }

        public static ResponseResultCVLinkedIn User;

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {

            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            if (e.IsAuthenticated)
            {

                await _pageDialogService.DisplayAlertAsync("Alert", "Congrats Linkedin Login is completed.", "ok");
                CanNavigate = false;
                IsBusy = true;
                var values = e.Account.Properties;
                var access_token = values["access_token"];

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }

                await store.SaveAsync(account = e.Account, Constants.AppName);
                var linkedIn = new LinkedInAccess
                {
                    Token = access_token
                };
                User = await _apiServices.PostLinkedin(linkedIn);

                await _navigationService.NavigateAsync("/BlabyMasterDetailPage/NavigationPage/MyCVPage");

                IsBusy = false;
                CanNavigate = true;


            }
        }

        async void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            else
            {
                try
                {
                    //	Debug.WriteLine("Authentication error: " + e.Message);
                    // 

                 //   await Navigation.PushModalAsync(new OAuthNativeFlowPage());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }






        async void ForgotCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync("ForgotPasswordPage");
            CanNavigate = true;
        }

  

        async   void SignUpCommandAction()
        {
            CanNavigate = false;
           await   _navigationService.NavigateAsync("SignUpPage");
            CanNavigate = true;
        }

        
    }

}
