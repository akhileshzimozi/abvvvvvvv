using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Blabyapp.API.Providers;
using Blabyapp.API.ViewModels;
using Blabyapp.API.DataModels;
using System.Web.Http;
using Owin.Security.Providers.LinkedIn;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Security.Claims;
using Blabyapp.API.Controllers;
using Blabyap.Common.Model;
//using System.IdentityModel.Claims;

namespace Blabyapp.API
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
          
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            var linkedinoptions = new LinkedInAuthenticationOptions
            {
                ClientId = "77oba6ddrqi37w",
                ClientSecret = "vb7paAa7jO0AwFzx",
                Provider = new LinkedInAuthenticationProvider()
                {  
                    OnAuthenticated = (context) =>
                    {
                        RegisterInfoModel regInfo = new RegisterInfoModel()
                        {
                            DisplayName = context.Name,
                            FullName = context.FamilyName,
                            Email = context.Email,
                            ImageUrl =""//picture-url

                        };
                        
                        MyCV cv = new MyCV()
                        {
                            Expertise = context.Positions,
                            JobTitle = context.Headline,
                            Industry = context.Industry
                            

                        };
                        foreach (var x in context.User)
                        {
                            var claimType = string.Format("urn:linkedin:{0}", x.Key);
                            string claimValue = x.Value.ToString();
                            if (!context.Identity.HasClaim(claim => claim.Value == claimValue))
                                context.Identity.AddClaim(new Claim(claimType, claimValue, ""));
                        }

                        RegisterExternalBindingModel mdl = new RegisterExternalBindingModel();
                        mdl.Email = context.Email;

                        //return new AccountController().RegisterExternal(mdl);
                        return Task.FromResult(0);
                    }
                    
                   
                }
                //CallbackPath = new PathString("/signin-linkedin")

            };
            List<string> profileFieldsToGet = new List<string>() { "location","industry" };

            foreach (var field in profileFieldsToGet)
            {
                linkedinoptions.ProfileFields.Add(field);
            }
            app.UseLinkedInAuthentication(linkedinoptions);


            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
