using Blabyap.Common.Model.NetworkModel;
using Blabyap.Views.Authentication;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class LoginPage : ContentPage
    {
        Account account;
        AccountStore store;
        public LoginPage()
        {
            InitializeComponent();
            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

        }

        //private async void Button_Clicked(object sender, System.EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new ProviderLoginPage());

        //}



    }
}
