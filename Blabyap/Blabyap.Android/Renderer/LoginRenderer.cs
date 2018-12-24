using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Blabyap.Droid.Renderer;
using Blabyap.Services;
using Blabyap.Views.Authentication;
using Xamarin.Auth;
using Prism.Navigation;

using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ProviderLoginPage), typeof(LoginRenderer))]

namespace Blabyap.Droid.Renderer
{
       
  public  class LoginRenderer : PageRenderer
    {

     static INavigationService navigation;
       LinkeDindata data = new LinkeDindata(navigation);

        public LoginRenderer() {

           

        }
        private bool showLogin = true;

        private OAuth2Authenticator GetAuthenticator()
        {
            OAuth2Authenticator auth = null;

            auth = new OAuth2Authenticator(
                clientId: "77oba6ddrqi37w",
               clientSecret: "vb7paAa7jO0AwFzx",
                 scope: "r_basicprofile,r_emailaddress",
               authorizeUrl: new Uri("https://www.linkedin.com/oauth/v2/authorization"),
                   redirectUrl: new Uri("http://blabyap.azurewebsites.net/Signin-linkedin"),
                 accessTokenUrl: new Uri("https://www.linkedin.com/oauth/v2/accessToken")
                 
             );

            return auth;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            var activity = this.Context as Activity;
            if (showLogin && LinkeDindata.User == null)
            {
                showLogin = false;
                var loginPage = Element as ProviderLoginPage;
                  var auth = GetAuthenticator();

                auth.Completed += async (sender, eventArgs) =>
                {
                    if (eventArgs.IsAuthenticated)

                    {
                        var values = eventArgs.Account.Properties;
                        var access_token = values["access_token"];
                        data.SetUser(access_token);
                    }
                    else
                    {
                       // await Navigation.PushModalAsync(new ProviderLoginPage());

                        //await navigation.GoBackAsync();
                    }
                };
                activity.StartActivity(auth.GetUI(activity));
            }
        }
    }

}
