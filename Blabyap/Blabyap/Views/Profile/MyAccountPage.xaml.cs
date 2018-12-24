using Blabyap.Services;
using Blabyap.ViewModels;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class MyAccountPage : ContentPage
    {

        ApiServices apiServices;
        public MyAccountPage()
        {
            InitializeComponent();
            apiServices = new ApiServices();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await apiServices.PostGetAuthorizationTokenResponse();
            (BindingContext as MyAccountPageViewModel)?.DemoData(ApiServices.obj, LoginPageViewModel.User);

        }
    }
}
