using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Owin.Security.Providers.LinkedIn;

namespace BlabyApAzure.API.Providers
{
    public class LinkedinAuthProvider : LinkedInAuthenticationProvider
    {
        public class GoogleAuthProvider
        {
            public void ApplyRedirect(LinkedInApplyRedirectContext context)
            {
                context.Response.Redirect(context.RedirectUri);
            }

            public Task Authenticated(LinkedInAuthenticatedContext context)
            {
                context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
                return Task.FromResult<object>(null);
            }

            public Task ReturnEndpoint(LinkedInReturnEndpointContext context)
            {
                return Task.FromResult<object>(null);
            }
        }
    }
}