using Blabyap.Services;
using Blabyap.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;

namespace Blabyap.Views.Home
{
    public partial class BlabyMasterDetailPage : MasterDetailPage, 
    IMasterDetailPageOptions
    {
        ApiServices apiServices;
        public BlabyMasterDetailPage()
        {
            InitializeComponent();
            apiServices = new ApiServices();
        }

        public bool IsPresentedAfterNavigation
        {
            get { return Device.Idiom != TargetIdiom.Phone; }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await apiServices.PostGetAuthorizationTokenResponse();
            (BindingContext as BlabyMasterDetailPageViewModel)?.DemoData(ApiServices.obj, LoginPageViewModel.User);

        }

    }
}
