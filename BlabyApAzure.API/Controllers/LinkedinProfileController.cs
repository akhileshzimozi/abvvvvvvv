using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BlabyApAzure.API.Models;
using Microsoft.Azure.Documents;
using System.Security.Claims;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Blabyap.Common.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;
using System.Net;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace BlabyApAzure.API.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Linkedin")]
    public class LinkedinProfileController : ApiController
    {
        private const string LinkedinUserInfoParameters = "id,first-name,last-name,maiden-name,headline,location,picture-url,industry,summary,positions,site-standard-profile-request,api-standard-profile-request,public-profile-url,email-address";

        [HttpPost]
        [Route("SaveLinkedinProfile")]
        public async Task<IHttpActionResult> SaveLinkedinProfile(string code)
        {
            ResponseResultCVLinkedIn rsp = new ResponseResultCVLinkedIn
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };
            

            if (string.IsNullOrEmpty(code))
            {
                rsp.ErrorMsg = "Not found";
                rsp.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rsp);
                
            }
            try
            {

                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://www.linkedin.com/")
                };
                var requestUrl = $"oauth/v2/accessToken?grant_type=authorization_code&code={code}&redirect_uri={ConfigurationManager.AppSettings.Get("Linkedin_RedirectUrl")}&client_id={ConfigurationManager.AppSettings.Get("Linkedin_ClientID")}&client_secret={ConfigurationManager.AppSettings.Get("Linkedin_ClientSecret")}&scope={"r_basicprofile,r_emailaddress"}";
                var response = await httpClient.GetAsync(requestUrl);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    rsp.ErrorMsg = "Response invalid " + response.StatusCode;
                    rsp.status = "Fail";
                    return Content(HttpStatusCode.NotFound, rsp);
                }
                var token = JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
                if (token.Access_token == null)
                {
                    rsp.ErrorMsg = "Token is null";
                    rsp.status = "Fail";
                    return Content(HttpStatusCode.NotFound, rsp);
                }
                LinkedInProfile usr = await GetUserFromAccessTokenAsync(token.Access_token);
                ApplicationUser regInfo = new ApplicationUser()
                {
                    UserName = usr.EmailAddress,
                    Email = usr.EmailAddress,
                    DisplayName = usr.FirstName, 
                    FullName = usr.FirstName + " " + usr.LastName, 
                    ImageUrl = usr.PictureUrl,
                    CreatedDate = DateTime.Now,
                    CVInfo = new CVData()
                    {
                        type ="CVData",
                        CVEmail = usr.EmailAddress,
                        Nationality = usr.Location.Country.Code,
                        //  Position = usr.Positions,
                        JobTitle = usr.Headline,
                        Industry = usr.Industry,
                        AboutYou = usr.Summary
                    }
            };

                ResponseResult rspuser = await PostLinkedInUserProfile(regInfo);

                if (rspuser.status == "Success")
                {
                    BasicUserInfoModel bs = new BasicUserInfoModel
                    {
                        HideMyAge = regInfo.HideMyAge,
                        DisplayName = regInfo.DisplayName,
                        FullName = regInfo.FullName,
                        Email = regInfo.UserName,
                        ImageUrl = regInfo.ImageUrl,
                        Birthday = regInfo.Birthday
                    };
                    rsp.Data = new DataLinkedIn
                    {
                        userCV = regInfo.CVInfo,
                        userdetails = bs
                    };
                   
                    return Ok(rsp);
                }
                return Content(HttpStatusCode.NotFound, rsp);
            }
            catch(Exception e)
            {
                rsp.ErrorMsg = e.Message;
                rsp.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, rsp);
            }
        }

        [HttpPost]
        [Route("SetLinkedinProfile")]
        public async Task<IHttpActionResult> SetLinkedinProfile(LinkedInAccess linktok)
        {
            ResponseResultCVLinkedIn rsp = new ResponseResultCVLinkedIn
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };
            if (!ModelState.IsValid)
            {
                rsp.ErrorMsg = "Not found";
                rsp.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rsp);

            }

            if (string.IsNullOrEmpty(linktok.Token))
            {
                rsp.ErrorMsg = "Token Empty";
                rsp.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rsp);

            }
            try
            {

                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://www.linkedin.com/")
                };
                LinkedInProfile usr = await GetUserFromAccessTokenAsync(linktok.Token);

                BasicUserInfoModel regInfo = new BasicUserInfoModel()
                {
                    UserID = usr.EmailAddress,
                    DisplayName = usr.FirstName,
                    FullName = usr.FirstName + " " + usr.LastName,
                    Email = usr.EmailAddress,
                    ImageUrl = usr.PictureUrl

                };

                CVData cv = new CVData()
                {
                    CVEmail = usr.EmailAddress,
                    Nationality = usr.Location.Country.Code,
                //  Position = usr.Positions,
                    JobTitle = usr.Headline,
                    Industry = usr.Industry,
                    AboutYou = usr.Summary
                };

                ResponseResult rspuser = await PostLinkedInUserProfile(regInfo);

                if (rspuser.status == "Success")
                {
                   // IHttpActionResult rspcv = await new CVController().PostCandidateCV(cv);//.PostCandidateProfile(usr);
                    rsp.Data = new DataLinkedIn
                    {
                        userCV = cv,
                        userdetails = regInfo
                    };

                    return Ok(rsp);
                }
                return Content(HttpStatusCode.NotFound, rsp);
            }
            catch (Exception e)
            {
                rsp.ErrorMsg = e.Message;
                rsp.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, rsp);
            }
        }

        [Authorize]
        private async Task<LinkedInProfile> GetUserFromAccessTokenAsync(string token)
        {
            var apiClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.linkedin.com")
            };

            var url = $"/v1/people/~:({LinkedinUserInfoParameters})?oauth2_access_token={token}&format=json&scope=r_basicprofile,r_emailaddress";
            // var url = $"/v2/seniorities?oauth2_access_token={token}&format=json";
            var response = await apiClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LinkedInProfile>(jsonResponse);
        }


        [Authorize]
        public async Task<IHttpActionResult> PostCandidateProfile(LinkedInProfile myprof)
        {
            ResponseResultCV rsp = new ResponseResultCV
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rsp.ErrorMsg = "Bad Request";
                rsp.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rsp);
               
            }
            try
            {
                if (User.Identity.Name != null)
                    myprof.EmailAddress = User.Identity.Name;//.FindFirstValue(ClaimTypes.NameIdentifier);
                else
                {
                    rsp.ErrorMsg = "User not found";
                    rsp.status = "Fail";
                    return Content(HttpStatusCode.NotFound, rsp);
                }
              
                myprof.linkid = myprof.EmailAddress + "_CV";

                IEnumerable<LinkedInProfile> Linkprofiles = await DocumentDBRepository<LinkedInProfile>.GetDatasAsync(u => u.linkid == myprof.linkid);//.FirstOrDefault();// (u => u.UserMail == User.Identity.Name);
                Document newdoc = null;

                if (Linkprofiles == null)
                {
                    newdoc = await DocumentDBRepository<LinkedInProfile>.CreateDataAsync(myprof);
                }
                else if (Linkprofiles.Count() == 0)
                { 
                  newdoc = await DocumentDBRepository<LinkedInProfile>.CreateDataAsync(myprof);
                }
                else
                {
                    LinkedInProfile scv = Linkprofiles.FirstOrDefault();
                    myprof.Id = scv.Id;
                    //Document exPrf = (dynamic)scv;
                    
                    newdoc = await DocumentDBRepository<LinkedInProfile>.UpdateDataAsync(scv.Id, myprof);
                }
                return Ok(myprof);// CreatedAtRoute("DefaultApi", new { id = myprof.linkid }, myprof);
            }
            catch (Exception e)
            {
                rsp.ErrorMsg = e.Message;
                rsp.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsp);
            }
        }
        [HttpGet]
        [ResponseType(typeof(LinkedInProfile))]
        public async Task<IHttpActionResult> GetMyCV(string usermail)
        {
            IEnumerable<LinkedInProfile> userCVs = await DocumentDBRepository<LinkedInProfile>.GetDatasAsync(u => u.linkid == usermail + "_CV");// (u => u.UserMail == User.Identity.Name);

            if (userCVs == null)
            {
                return NotFound();
            }

            LinkedInProfile RetData = userCVs.First<LinkedInProfile>();
            return Ok(RetData);
        }
        public async Task<ResponseResult> PostLinkedInUserProfile(BasicUserInfoModel myprof)
        {
            ResponseResult rsp = new ResponseResult
            {

                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rsp.ErrorMsg = "Bad Request";
                rsp.status = "Fail";
                return rsp;

            }
            try
            {
                IEnumerable<BasicUserInfoModel> Linkprofile = await DocumentDBRepository<BasicUserInfoModel>.GetDatasAsync(u => u.UserID == myprof.UserID);//.FirstOrDefault();// (u => u.UserMail == User.Identity.Name);
                Document newdoc = null;

                if (Linkprofile == null)
                {
                    newdoc = await DocumentDBRepository<BasicUserInfoModel>.CreateDataAsync(myprof);
                }
                else if (Linkprofile.Count() == 0)
                {

                    newdoc = await DocumentDBRepository<BasicUserInfoModel>.CreateDataAsync(myprof);
                }
                else
                {
                    BasicUserInfoModel scv = Linkprofile.FirstOrDefault();
                    myprof.id = scv.id;
                    //Document exPrf = (dynamic)scv;

                    newdoc = await DocumentDBRepository<BasicUserInfoModel>.UpdateDataAsync(scv.id, myprof);
                }
                return rsp;// CreatedAtRoute("DefaultApi", new { id = myprof.linkid }, myprof);
            }
            catch (Exception e)
            {
                rsp.ErrorMsg = e.Message;
                rsp.status = "Fail";

                return rsp;
            }
        }

        public async Task<ResponseResult> PostLinkedInUserProfile(ApplicationUser  myprof)
        {
            ResponseResult rsp = new ResponseResult
            {
             
                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rsp.ErrorMsg = "Bad Request";
                rsp.status = "Fail";
                return rsp;

            }
            try
            { 
                 IEnumerable<ApplicationUser> Linkprofile = await DocumentDBRepository<ApplicationUser>.GetDatasAsync(u => u.UserName == myprof.UserName);//.FirstOrDefault();// (u => u.UserMail == User.Identity.Name);
                Document newdoc = null;

                if (Linkprofile == null)
                {
                    newdoc = await DocumentDBRepository<ApplicationUser>.CreateDataAsync(myprof);
                }
                else if (Linkprofile.Count() == 0)
                {

                    newdoc = await DocumentDBRepository<ApplicationUser>.CreateDataAsync(myprof);
                }
                else
                {
                    

                    ApplicationUser scv = Linkprofile.FirstOrDefault();
                   // myprof.id = scv.id;
                    //Document exPrf = (dynamic)scv;
                
                //    newdoc = await DocumentDBRepository<ApplicationUser>.UpdateDataAsync(scv.id, myprof);
                }
                return rsp;// CreatedAtRoute("DefaultApi", new { id = myprof.linkid }, myprof);
            }
            catch (Exception e)
            {
                rsp.ErrorMsg = e.Message;
                rsp.status = "Fail";
                return rsp;
            }
        }
    }
}