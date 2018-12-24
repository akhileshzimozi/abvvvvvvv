using Blabyapp.API.DataModels;
using Blabyapp.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Blabyap.Common.Model;

namespace Blabyapp.API.Controllers
{

    public class SomeInput
    {
        public string TestInputData { get; set; }
    }
    public class PostStatus
    {
        public string Status { get; set; }
        public string FailReason { get; set; }
    }
    [RoutePrefix("api/DemoAPI")]
    public class DemoAPIController : ApiController
    {
        

        [HttpGet]
        [Route("UserCV")]
        public MyCV GetUserCV(string id)
        {
            return new MyCV();
        }
        
        //[HttpPost]
        //[Route("postuserprofile")]
        //public Task<IHttpActionResult> PostDemoUserProfile()
        //{
        //    UserProfile usrprof = new UserProfile
        //    {
               
        //        FullName = "rattana sharma",
        //        DisplayName = "Rattana",
        //        EmailAddress = "rattana.sharma@zimozi.co",
        //        Password = "P@ssw0rd",
        //        Birthday = new DateTime(1980, 06, 30),
        //        ImageUrl = "d.jpg",
        //        CreatedDate =DateTime.Now,
        //        UpdatedDate = DateTime.Now,
        //        DeletedDate =DateTime.Now
        //    };

        //    return new UserProfilesController().PostUserProfile(usrprof);

        //}

        //[HttpGet]
        //[Route("Getuserprofile")]
        //public Task<IHttpActionResult> GetDemoUserProfile(int id)
        //{
        //    Task<IHttpActionResult> res = new UserProfilesController().GetUserProfile(id);

        //    UserProfile contentResult = res.Result as UserProfile;

        //    return res;

        //}

        [HttpGet]
        [Route("Getuserprofile")]
        public UserProfile GetDemoUserProfile(int id)
        {

            return new UserProfile().GetDemoUserProfile(id);
                
        }

        [HttpPost]
        public PostStatus SomeInputPostExample(SomeInput model)
        {
            return new PostStatus { Status = "Success", FailReason = "" };
        } 
    }
}
