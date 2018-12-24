using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth; 
using BlabyApAzure.API.Models;
using BlabyApAzure.API.Providers;
using BlabyApAzure.API.Results;
using Blabyap.Common.Model;
using Newtonsoft.Json.Linq;

using System.Net;
using System.Xml.Linq;
using Microsoft.Azure.Documents;

namespace BlabyApAzure.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        // private const string LinkedinUserInfoParameters = "id,first-name,last-name,headline,picture-url,industry,summary,specialties,positions:(id, title, summary, start-date,end-date,is-current,company:(id, name, type, size, industry, ticker)),educations:(id, school-name,field-of-study,start-date,end-date,degree,activities,notes),associations,interests,num-recommenders,date-of-birth,publications:(id, title, publisher:(name),authors:(id, name),date,url,summary),patents:(id, title, summary, number, status:(id, name),office:(name),inventors:(id, name),date,url),languages:(id, language:(name),proficiency:(level, name)),skills:(id, skill:(name)),certifications:(id, name, authority:(name),number,start-date,end-date),courses:(id, name, number),recommendations-received:(id, recommendation-type,recommendation-text,recommender),honors-awards,three-current-positions,three-past-positions,volunteer";
        private const string LinkedinUserInfoParameters = "id,first-name,last-name,maiden-name,headline,location,picture-url,industry,summary,positions,site-standard-profile-request,api-standard-profile-request,public-profile-url,email-address";

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }
        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        //[Route("ManageInfo")]
        //public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        //{
        //    IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

        //    if (user == null)
        //    {
        
        //        return null;
        //    }
        //    List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

        //    foreach (IdentityUserLogin linkedAccount in user.Logins)
        //    {
        //        logins.Add(new UserLoginInfoViewModel
        //        {
        //            LoginProvider = linkedAccount.LoginProvider,
        //            ProviderKey = linkedAccount.ProviderKey
        //        });
        //    }

        //    if (user.PasswordHash != null)
        //    {
        //        logins.Add(new UserLoginInfoViewModel
        //        {
        //            LoginProvider = LocalLoginProvider,
        //            ProviderKey = user.UserName,
        //        });
        //    }

        //    return new ManageInfoViewModel
        //    {
        //        LocalLoginProvider = LocalLoginProvider,
        //        Email = user.UserName,
        //        Logins = logins,
        //        ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
        //    };
        //}

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            ResponseResult res = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            if (!ModelState.IsValid)
            {
                res.ErrorMsg = "Modelstate Invalid";
                res.status = "Fail";
                return Content(HttpStatusCode.BadRequest, res);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                res.ErrorMsg = "Not succeeded";
                res.status = "Fail";
                return Content(HttpStatusCode.NotAcceptable, res);
               // return GetErrorResult(result);
            }

            return Ok(res);
        }

        /// <summary>
        /// This method is used when User Forgets the password and want to regenerate it.
        /// </summary>
        /// <param name="model">ForgotPassword model is passing as parameter to this method.</param>
        /// <returns>It will send a mail on the user's Email ID having Password code if succeed then return OK method
        /// otherwise give a message "Forgot password code not sent"</returns>

        [HttpPost]
        [AllowAnonymous ]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPassword model)
        {

            ResponseResult rspFgt = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            if (ModelState.IsValid)
            {
                if (model.Email == null)
                {
                    rspFgt.ErrorMsg = "Email Empty";
                    rspFgt.status = "Fail";
                    return Content(HttpStatusCode.BadRequest, rspFgt);
                }
                var user = await UserManager.FindByNameAsync(model.Email);
                
                if (user == null)
                {
                    rspFgt.ErrorMsg = "User not found";
                    rspFgt.status = "Fail";
                    return Content(HttpStatusCode.BadRequest,rspFgt);
                }

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                string callbackUrl = Url.Link("Default", new { controller = "User/ManageAccount/reset-password", userId = user.Id,Email=user.Email, code = code });
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                
                return Content(HttpStatusCode.OK, rspFgt);
            }
            rspFgt.status = "Fail";
            rspFgt.ErrorMsg = "Forgot password code not sent";
            return Content(HttpStatusCode.BadRequest, rspFgt);
        }

        /// <summary>
        /// This method is used when User want to change the password.
        /// </summary>
        /// <param name="model">ResetPasswordBindingModel model is passing as parameter to this method.</param>
        /// <returns>It will send user Id, Code and Password as parameters to ResetPasswordAsync method, if reset succeed then it will
        /// give a message "Password Reset done" otherwise "Password could not be reset" </returns>

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordBindingModel model)
        {
            ResponseResult rspRst = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rspRst.status = "Fail";
                rspRst.ErrorMsg = "Model state invalid";
                return Content(HttpStatusCode.BadRequest, rspRst);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {

                rspRst.status = "Fail";
                rspRst.ErrorMsg = "Reset not done";
                return Content(HttpStatusCode.BadRequest, rspRst);
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                rspRst.status = "Success";
                rspRst.ErrorMsg = "Password Reset done";
                return Content(HttpStatusCode.OK, rspRst);
              
            }
            rspRst.status = "Fail";
            rspRst.ErrorMsg = "Password could not be reset";
            return Content(HttpStatusCode.BadRequest, rspRst);
        }
        /// <summary>
        /// This method is used when User set the password.
        /// </summary>
        /// <param name="model">SetPasswordBindingModel model is passing as parameter to this method.</param>
        /// <returns>It will send UserId and NewPassword as parameters to AddPasswordAsync method, on succeed it 
        /// it will return OK method. </returns>

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        /// <summary>
        /// This method is used when User want use LinkedIN account for Login .
        /// </summary>
        /// <param name="model">AddExternalLoginBindingModel model is passing as parameter to this method.</param>
        /// <returns>It will send UserId and NewPassword as parameters to AddPasswordAsync method, on succeed it 
        /// it will return OK method. </returns>

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

     

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
               
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
      //  [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                
                 ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                BasicUserInfoModel basic = new BasicUserInfoModel
                {
                    Birthday = user.Birthday,
                    DisplayName = user.DisplayName,
                    FullName = user.FullName,
                    ImageUrl = user.ImageUrl,
                    Email = user.Email
                };
               
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName, basic);

                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ValidUser")]
        //public async Task<IHttpActionResult> ValidUser(UserCheck usr)
        public async Task<IHttpActionResult> ValidUser(string usrMail)
        {
            ResponseResult res = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            try
            {
                if (!ModelState.IsValid)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "Model state invalid";
                    return Content(HttpStatusCode.BadRequest, res);
                }

                ApplicationUser accUs = await UserManager.FindByNameAsync(usrMail);

                if (accUs == null)
                {
                    res.ErrorMsg = "User not found";
                    res.status = "Fail";
                    return Content(HttpStatusCode.NotFound, res);
                }
                return Content(HttpStatusCode.OK, res);
            }

            catch (Exception e)
            {
                res.ErrorMsg = e.Message;
                res.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, res);
            }
        }

        [HttpPost]
        [Route("HideMe")]
        public async Task<IHttpActionResult> HideMe(HideUserData hidmdl)
        {
            ResponseResult res = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            try
            {
                if (!ModelState.IsValid)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "Model state invalid";
                    return Content(HttpStatusCode.BadRequest, res);
                }

                ApplicationUser accUs = await UserManager.FindByNameAsync(User.Identity.Name);

                if (accUs == null)
                {
                    res.ErrorMsg = "User not found";
                    res.status = "Fail";
                    return Content(HttpStatusCode.NotFound, res);
                }
                accUs.HideMe = hidmdl.HideFlag;

                IdentityResult result = await UserManager.UpdateAsync(accUs);

                if (!result.Succeeded)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "user profile not updated";
                    return Content(HttpStatusCode.BadRequest, res);
                }
                return Content(HttpStatusCode.OK, res);
            }

            catch (Exception e)
            {
                res.ErrorMsg = e.Message;
                res.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, res);
            }

        }

        [HttpPost]
        [Route("HideAge")]
        public async Task<IHttpActionResult> HideAge(HideUserData hidmdl)
        {
            ResponseResult res = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            try
            {
                if (!ModelState.IsValid)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "Model state invalid";
                    return Content(HttpStatusCode.BadRequest, res);
                }

                ApplicationUser accUs = await UserManager.FindByNameAsync(User.Identity.Name);

                if (accUs == null)
                {
                    res.ErrorMsg = "User profile not found";
                    res.status = "Fail";
                    return Content(HttpStatusCode.NotFound, res);
                }
                accUs.HideMyAge = hidmdl.HideFlag;

                IdentityResult result = await UserManager.UpdateAsync(accUs);

                if (!result.Succeeded)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "user profile not updated";
                    return Content(HttpStatusCode.BadRequest, res);
                }
                return Content(HttpStatusCode.OK, res);
            }

            catch (Exception e)
            {
                res.ErrorMsg = e.Message;
                res.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, res);
            }

        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IHttpActionResult> Delete()
        {
            ResponseResult res = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            try
            {
                if (!ModelState.IsValid)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "Model state invalid";
                    return Content(HttpStatusCode.BadRequest, res);
                }

                ApplicationUser accUs = await UserManager.FindByNameAsync(User.Identity.Name);

                if (accUs == null)
                {
                    res.ErrorMsg = "User not found";
                    res.status = "Fail";
                    return Content(HttpStatusCode.NotFound, res);
                }
                accUs.Deleted = true;
                accUs.DeletedDate = System.DateTime.Now;

                IdentityResult result = await UserManager.UpdateAsync(accUs);

                if(!result.Succeeded)
                {
                    res.status = "Fail";
                    res.ErrorMsg = "user not updated";
                    return Content(HttpStatusCode.BadRequest, res);
                } 
                return Content(HttpStatusCode.OK, res);
            }

            catch (Exception e)
            {
                res.ErrorMsg = e.Message;
                res.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, res);
            }

        }
        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
      
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            ResponseResult rgstr = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rgstr.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rgstr);
               
            }

            var user = new ApplicationUser() { Deleted = false, UserName = model.Email, Email = model.Email, Birthday = model.Birthday, FullName = model.FullName, DisplayName = model.DisplayName, CreatedDate = DateTime.Now, ImageUrl = model.ImageUrl };


            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        if(rgstr.ErrorMsg=="")
                            rgstr.ErrorMsg = error;
                        else
                            rgstr.ErrorMsg += ","+error;
                    }
                }
                

                rgstr.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, rgstr);
            }

            //string locationName = "";
            //string url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", latitude, longitude);
            //XElement xml = XElement.Load(url);
            //if (xml.Element("status").Value == "OK")
            //{
            //    locationName = string.Format("{0}",
            //        xml.Element("result").Element("formatted_address").Value);
            //}
            //locationName;



            return Ok(rgstr);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ObtainLocalAccessToken")]
        public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
        {

            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                return BadRequest("Provider or external access token is not sent");
            }

            var verifiedAccessToken = await VerifyExternalAccessToken(provider, externalAccessToken);
            if (verifiedAccessToken == null)
            {
                return BadRequest("Invalid Provider or External Access Token");
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(provider, verifiedAccessToken.user_id));

            bool hasRegistered = user != null;

            if (!hasRegistered)
            {
                return BadRequest("External user is not registered");
            }

            //generate access token response
            var accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName);

            return Ok(accessTokenResponse);

        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result); 
            }
            return Ok();
        }

        // POST api/Account/Location
        
        //public async Task<IHttpActionResult> GetAddress(string latitude, string longitude)
        //{
        //    string locationName = "";
        //    string url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", latitude, longitude);
        //    XElement xml = XElement.Load(url);
        //    if (xml.Element("status").Value == "OK")
        //    {
        //        locationName = string.Format("{0}",
        //            xml.Element("result").Element("formatted_address").Value);
        //    }
        //    return locationName;
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private JObject GenerateLocalAccessTokenResponse(string userName)
        {

            var tokenExpiration = TimeSpan.FromDays(1);

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", userName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString())
        );

            return tokenResponse;
        }
        private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        {
            ParsedExternalAccessToken parsedToken = null;

            var verifyTokenEndPoint = "";

            if (provider == "LinkedIn")
            {
                //You can get it from here: https://developers.facebook.com/tools/accesstoken/
                //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook

                var appToken = "xxxxx";
                verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
            }
            else
            {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "LinkedIn")
                {
                    parsedToken.user_id = jObj["data"]["user_id"];
                    parsedToken.app_id = jObj["data"]["app_id"];

                    if (!string.Equals(Startup.LinkedInAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }


            }

            return parsedToken;
        }
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken")
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
