using Blabyap.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BlabyApAzure.API.Models;
using Microsoft.Azure.Documents;
using System.Security.Claims;
using System.Web.Http.Description;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
namespace BlabyApAzure.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/CV")]
    public class CVController : ApiController
    {
    /// <summary>
    /// This method is used by the Candidate to upload the CV on his own UserID.
    /// </summary>
    /// <param name="mycv">CvData View Model passed through this method</param>
    /// <returns>
    /// Firstly the candidate will check the userId if the Id will not found then it will send a message "User Not Found",
    /// otherwise it will update the CV. 
    /// </returns>
        [HttpPost]
        [Route("PostCV")]
        public async Task<IHttpActionResult> PostCandidateCV(CVData mycv)
        {
            ResponseResultCV rgcv = new ResponseResultCV
            {
                ErrorMsg = "",
                status = "Success",
                Data = null
            };
            if (!ModelState.IsValid)
            {
                rgcv.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rgcv); 
            }
            try
            {
               mycv.type = "CVData";
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accUs = await usmngr.FindByNameAsync(User.Identity.Name);
                IdentityResult result = null;
                if (accUs == null)
                {     
                    rgcv.status = "User is not found";
                    return Content(HttpStatusCode.NotFound, rgcv);
                }
                if (accUs.CVInfo == null)
                    accUs.CVInfo = mycv;

                if (accUs.CVInfo != null)
                    result = await usmngr.UpdateAsync(accUs);
                
                if (!result.Succeeded)
                {
                    rgcv.status = "Fail";
                    rgcv.ErrorMsg = "CV not updated";
                    return Content(HttpStatusCode.BadRequest, rgcv);
                }
                rgcv.Data = mycv;
                return Content(HttpStatusCode.OK, rgcv);
               // return Ok(mycv);// CreatedAtRoute("DefaultApi", new { id = mycv.cvID }, mycv);
            }
            catch(Exception e)
            {
                rgcv.status = "Fail";
                rgcv.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rgcv);
            }
        }
        /// <summary>
        /// This is used for getting this CvData.
        /// </summary>
        /// <returns> Firstly this method will check the userId is null or not if not null 
        /// then it'll check the CVData is null or not. If the CVData is not
        /// null then it'll return OK method.
        /// </returns>
        // GET: api/MyCVs/5
        [Route("GetCV")]
       [HttpGet]
        [ResponseType(typeof(ResponseResultCV))]
        public async Task<IHttpActionResult> GetMyCV()
        {
            ResponseResultCV rgcv = new ResponseResultCV
            {
                ErrorMsg = "",
                status = "Success",
                Data = null
            };
            if(User.Identity.Name==null)
            {
                rgcv.status = "Fail";
                return Content(HttpStatusCode.Unauthorized, rgcv);
            }
            ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser accUs = await usmngr.FindByNameAsync(User.Identity.Name);
            if (accUs==null)
            {
                rgcv.status = "User is not found";
                return Content(HttpStatusCode.NotFound, rgcv);
            }
            if (accUs.CVInfo == null)
            {
                rgcv.status = "CV is not posted";
                return Content(HttpStatusCode.NotFound, rgcv);
            }
            rgcv.Data = accUs.CVInfo;
            return Content(HttpStatusCode.OK, rgcv);
        } 
    }

}