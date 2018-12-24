using Blabyap.Services;
using Blabyap.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class SwipePage : ContentPage
    {
        private ApiServices _apiServices = new ApiServices();
       
        public SwipePage()
        {
            InitializeComponent();
            
        }

        
        protected  override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as SwipePageViewModel)?.DemoData(DiscoveryPageViewModel.swipeProfile);

        }
    }
}
