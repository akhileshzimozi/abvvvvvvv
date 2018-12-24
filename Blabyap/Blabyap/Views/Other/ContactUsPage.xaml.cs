using Blabyap.Services;
using Blabyap.ViewModels;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class ContactUsPage : ContentPage
    {

        ApiServices _apiServices;
        public ContactUsPage()
        {
            InitializeComponent();
            _apiServices = new ApiServices();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
          await _apiServices.PostGetAuthorizationTokenResponse();
            (BindingContext as ContactUsPageViewModel)?.DemoData(ApiServices.obj, LoginPageViewModel.User);


        }

    }
}
