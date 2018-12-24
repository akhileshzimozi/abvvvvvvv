using Blabyap.Services;
using Blabyap.ViewModels;
using Xamarin.Forms;

namespace Blabyap.Views.Other
{
    public partial class SuggestionPage : ContentPage
    {

        ApiServices _apiServices;
        public SuggestionPage()
        {
            InitializeComponent();
            _apiServices = new ApiServices();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _apiServices.PostGetAuthorizationTokenResponse();
            (BindingContext as SuggestionPageViewModel)?.DemoData(ApiServices.obj, LoginPageViewModel.User);


        }

    }
}
