using Blabyap.Common.Model;
using Blabyap.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class SettingsPage : ContentPage
    {

        ApiServices apiServices;
        public SettingsPage()
        {
            InitializeComponent();
            apiServices = new ApiServices();
        }

       

        private async void HideAgeCommand(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                var hideFlag = new HideUserData
                {
                    HideFlag = true
                };
                await apiServices.PostHideAge(hideFlag);

            }
            else
            {
                var hideFlag = new HideUserData
                {
                    HideFlag = false
                };
                await apiServices.PostHideAge(hideFlag);

            }
        }

        private async void HideMeCommand(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                var hideFlag = new HideUserData
                {
                    HideFlag = true
                };
                await apiServices.PostHideMe(hideFlag);

            }
            else
            {
                var hideFlag = new HideUserData
                {
                    HideFlag = false
                };
                await apiServices.PostHideMe(hideFlag);

            }
        }
    }
}
