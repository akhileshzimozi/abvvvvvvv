using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Blabyap.Views;
using System.Threading.Tasks;
using Prism;
using Prism.Ioc;
using DryIoc;
using Prism.DryIoc;
using Prism.Logging;
using Plugin.Multilingual;
using Blabyap.Resources;

using DebugLogger = Blabyap.Services.DebugLogger;
using Blabyap.Views.Home;
using Blabyap.ViewModels;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Prism.Navigation;
using Blabyap.Common.Model;
using Blabyap.Helpers;
using System;
using Blabyap.Services;
using Blabyap.Views.Profile;
using Blabyap.Views.Authentication;
using Blabyap.ViewModels.Authentication;
using Plugin.Connectivity;
using Prism.Services;
using Blabyap.SplashScreen;
using Blabyap.Views.Other;
using Blabyap.Common.Model.NetworkModel;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Blabyap
{
    //public partial class App : Application
    //{
    //    //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure


    //    public App()
    //    {
    //        InitializeComponent();

    //        if (UseMockDataStore)
    //            DependencyService.Register<MockDataStore>();
    //        else
    //            DependencyService.Register<AzureDataStore>();

    //        MainPage = new MainPage();
    //    }

    //    protected override void OnStart()
    //    {
    //        // Handle when your app starts
    //    }

    //    protected override void OnSleep()
    //    {
    //        // Handle when your app sleeps
    //    }

    //    protected override void OnResume()
    //    {
    //        // Handle when your app resumes
    //    }
    //}
    public partial class App : PrismApplication
    {
        public static string SyncfusionVersion = "16.3.0.21";
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;
      



        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App()
            : this(null)
        {

        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {

        }
        //private void SetMainPage()
        //{
        //    if (!string.IsNullOrEmpty(Settings.AccessToken))
        //    {
        //        if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
        //        {
        //           // LoginPageViewModel loginViewModel = new LoginPageViewModel();
        //            loginViewModel.LoginCommand.Execute(null);
        //        }
        //        MainPage = new NavigationPage(new MyCVPage());
        //    }
        //    else if (!string.IsNullOrEmpty(Settings.Username)
        //          && !string.IsNullOrEmpty(Settings.Password))
        //    {
        //        MainPage = new NavigationPage(new LoginPage());
        //    }
        //    else
        //    {
        //        MainPage = new NavigationPage(new SignUpPage());
        //    }
        //

       

      
        protected async override  void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc5NzZAMzEzNjJlMzMyZTMwRUlmalptZEpXdFRQcGlVNERDNUVDWVNKTnM5US9ZQ0FETVVCWWRoc3p4RT0=");
            InitializeComponent();

           
            LogUnobservedTaskExceptions();
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;


           //  await NavigationService.NavigateAsync(nameof(Splash));

           MainPage = new NavigationPage(new Splash());
           // SetMainPage();

            //  await NavigationService.NavigateAsync(nameof(MPPage));
            //  await NavigationService.NavigateAsync(nameof(SwipePage));
            //  var commonmodel = new TestViewModel();
            //await NavigationService.NavigateAsync("NavigationPage/BlabyMasterDetailPage");
            // await NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(BlabyMasterDetailPage));
            // await NavigationService.NavigateAsync(nameof(BlabyMasterDetailPage));


        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry
                .RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);


            //containerRegistry.Register<INavigationService,
            //PopupNavigationService>(PrismApplicationBase.NavigationServiceName);

            containerRegistry.Register<ILoggerFacade, Services.DebugLogger>();

            // Register the Popup Plugin Navigation Service
            //   containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterInstance(CreateLogger());


            // Navigating to "TabbedPage?createTab=ViewA&createTab=ViewB&createTab=ViewC will generate a TabbedPage
            // with three tabs for ViewA, ViewB, & ViewC
            // Adding `selectedTab=ViewB` will set the current tab to ViewB
            containerRegistry.RegisterForNavigation<TabbedPage>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Blabyap.Views.MainPage>();

           // containerRegistry.RegisterForNavigation<SplashScreenPage>();
            containerRegistry.RegisterForNavigation<LoginPage,LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage,SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpGreetingPage, SignUpGreetingPageViewModel>();
            containerRegistry.RegisterForNavigation<MyCVPage, MyCVPageViewModel>();
            containerRegistry.RegisterForNavigation<DiscoveryPage,DiscoveryPageViewModel>();
            containerRegistry.RegisterForNavigation<SwipePage,SwipePageViewModel>();
            containerRegistry.RegisterForNavigation<MatchPage,MatchPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatPage,ChatPageViewModel>();
            containerRegistry.RegisterForNavigation<MatchProfilePage, MatchProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<ChatListPage,ChatListPageViewModel>();
            containerRegistry.RegisterForNavigation<MyAccountPage,MyAccountPageViewModel>();
            containerRegistry.RegisterForNavigation<CommentsPage,CommentsPageViewModel>();
            containerRegistry.RegisterForNavigation<MeetNowPage,MeetNowPageViewModel>();
            containerRegistry.RegisterForNavigation<CanvasPage,CanvasPageViewModel>();
            containerRegistry.RegisterForNavigation<PrivacyPolicyPage,PrivacyPolicyPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactUsPage,ContactUsPageViewModel>();
            containerRegistry.RegisterForNavigation<ChooseSkinPage,ChooseSkinPageViewModel>();
            containerRegistry.RegisterForNavigation<BlabyMasterDetailPage, BlabyMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutUsPage,AboutUsPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage,SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<PremiumPage,PremiumPageViewModel>();
            containerRegistry.RegisterForNavigation<AppTour1Page,AppTour1PageViewModel>();
            containerRegistry.RegisterForNavigation<AppTour2Page,AppTour2PageViewModel>();
            containerRegistry.RegisterForNavigation<PassportPage,PassportPageViewModel>();
            containerRegistry.RegisterForNavigation<MPPage,MPPageViewModel>();
            containerRegistry.RegisterForNavigation<TourCarouselPage,TourCarouselPageViewModel>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ForgotPasswordPage, ForgotPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ResetPasswordPage, ResetPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<SuggestionPage, SuggestionPageViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle IApplicationLifecycle
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle IApplicationLifecycle
            base.OnResume();

            // Handle when your app resumes
        }

        private ILoggerFacade CreateLogger() =>
            new DebugLogger();

        private void LogUnobservedTaskExceptions()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                // Container.Resolve<ILoggerFacade>().Log(e.Exception);
            };
        }
    }
}
