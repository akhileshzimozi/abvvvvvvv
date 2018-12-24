using Blabyap.Common.Model;
using BlabyApAzure.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Documents;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace BlabyApAzure.API.Controllers
{ 
 //   [Authorize]
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        [HttpGet]
        [Route("ChatInfo")]
        public async Task<IHttpActionResult> GetchatInfo(string CandidateEmail)
        { 
            //ResponseResultCVChat rsCht= new ResponseResultCVChat
            //{
            //    status = "Success",
            //    ErrorMsg = "",
            //    Data = null
            //};
            //IEnumerable<CVChat> chtmsgs =await DocumentDBRepository<CVChat>.GetDatasAsync(t => t.chatID.Contains(User.Identity.Name));
          
            //rsCht.Data = new List<CVChat>
            //{
            //    new CVChat()
            //    {
            //         fromUser 
            //        CandidateEmail = "test@gm.com",
            //        ChatMsg =" How are you doing",
            //        CreatedDate= DateTime.Now
            //    },
            //     new CVChat()
            //    {
            //        chatID = "dd.d@gm.com_Chat",
            //        CandidateEmail = "test@gm.com",
            //        ChatMsg ="Great. How about you?",
            //        CreatedDate= DateTime.Now
                   
            //    }
            //};

            return Ok();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("PostChat")]
        public async Task<IHttpActionResult> PostChat(CVChat cht)
        {
          ResponseResult rspCht = new ResponseResult();
          rspCht.ErrorMsg = "";
          rspCht.status = "Success";
           if (!ModelState.IsValid)
            {
                rspCht.status = "Fail";
                rspCht.ErrorMsg = "Model params improper";
                return Content(HttpStatusCode.BadRequest, rspCht);
            }
            //if (User.Identity.Name == null)
            //{
            //    rspCht.ErrorMsg = "User not authorized";
            //    rspCht.status = "Fail";
            //    return Content(HttpStatusCode.Unauthorized, rspCht);
            //}
            try
            {    
                Document newdoc = null;
                //cht.chatID = User.Identity.Name + "_Chat";
                newdoc = await DocumentDBRepository<CVChat>.CreateDataAsync(cht);
                if(newdoc==null)
                {
                rspCht.status = "Fail";
                return Content(HttpStatusCode.NoContent, rspCht);
                }
                return Content(HttpStatusCode.OK, rspCht);
            }
            catch (Exception e)
            {
                rspCht.status = "Fail";
                rspCht.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rspCht);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("ConnectChat")]
        public async Task<IHttpActionResult> AddChatUser(ChatUser chtUsr)
        {
            ResponseResult rspCht = new ResponseResult();
            rspCht.ErrorMsg = "";
            rspCht.status = "Success";

            if (!ModelState.IsValid)
            {
                rspCht.status = "Fail";
                rspCht.ErrorMsg = "Model params improper";
                return Content(HttpStatusCode.BadRequest, rspCht);
            }
            try
            {
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accUs = await usmngr.FindByNameAsync(chtUsr.UserMail);
                IdentityResult result = null;
                if (accUs == null)
                {
                    rspCht.status = "User is not found";
                    return Content(HttpStatusCode.NotFound, rspCht);
                }
                accUs.ChatConnectionID = chtUsr.connectionId;
                if (accUs.ChatConnectionID != null)
                    result = await usmngr.UpdateAsync(accUs);
                if (!result.Succeeded)
                {
                   rspCht.status = "Fail";
                   rspCht.ErrorMsg = "chat id is not updated";
                    return Content(HttpStatusCode.BadRequest, rspCht);
                }
                return Content(HttpStatusCode.OK, rspCht);
            }
            catch (Exception e)
            {
                rspCht.status = "Fail";
                rspCht.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rspCht);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("DisConnectChat")]
        public async Task<IHttpActionResult> RemoveChatUser(ChatUser chtUsr)
        {
            ResponseResult rspCht = new ResponseResult();
            rspCht.ErrorMsg = "";
            rspCht.status = "Success";
            if (!ModelState.IsValid)
            {
                rspCht.status = "Fail";
                rspCht.ErrorMsg = "Model params improper";
                return Content(HttpStatusCode.BadRequest, rspCht);
            }
            try
            {
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accUs = await usmngr.FindByNameAsync(chtUsr.UserMail);
                IdentityResult result = null;
                if (accUs == null)
                {
                    rspCht.status = "User is not found";
                    return Content(HttpStatusCode.NotFound, rspCht);
                }
                accUs.ChatConnectionID = "";
               result = await usmngr.UpdateAsync(accUs);
                if (!result.Succeeded)
                {
                    rspCht.status = "Fail";
                    rspCht.ErrorMsg = "chat id is not updated";
                    return Content(HttpStatusCode.BadRequest, rspCht);
                }
                return Content(HttpStatusCode.OK, rspCht);
            }
            catch (Exception e)
            {
                rspCht.status = "Fail";
                rspCht.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rspCht);
            }
        }
    }
}