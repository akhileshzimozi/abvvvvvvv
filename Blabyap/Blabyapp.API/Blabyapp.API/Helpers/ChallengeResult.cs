using Microsoft.Owin.Security;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Blabyapp.API.ViewModels;
using Blabyapp.API.Providers;
using Blabyapp.API.Results;
using Blabyapp.API.DataModels;
using Blabyap.Common.Model;
using System.Linq;
using Newtonsoft.Json;

namespace Blabyapp.API.Helpers
{
    public class ChallengeResult : IHttpActionResult
    {
        private const string XsrfKey = "XsrfId&amp";
 
  public ChallengeResult(string provider, string redirectUri, HttpRequestMessage request)
    : this(provider, redirectUri, null, request)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId, HttpRequestMessage request)
        {
            AuthenticationProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
            MessageRequest = request;
        }

        public string AuthenticationProvider { get; private set; }

        public string RedirectUri { get; private set; }

        public string UserId { get; private set; }

        public HttpRequestMessage MessageRequest { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var properties = new AuthenticationProperties() { RedirectUri = this.RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }

            MessageRequest.GetOwinContext().Authentication.Challenge(properties, AuthenticationProvider);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = MessageRequest;

            return  Task.FromResult(response);
        }
    }
}