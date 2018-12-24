using Blabyap.Common.Model;
using BlabyApAzure.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace BlabyApAzure.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Comments")]
    public class CommentsController : ApiController
    {
        /// <summary>
        /// This method is used to write comment on other CV's but User can't write
        /// any comment on his own CV.
        /// </summary>
        /// <param name="cmnt">CVComments view model is passed through this method.</param>
        /// <returns>Firsty the method will check the UserId is exists or not then the comment writer 
        /// Id is different from his own Id because you can't write any comment to yourself.
        /// So if UpdateAsync method is not succeed then it will sent a message "Comments counter is not updated" otherwise just update the comment.   
        /// </returns>
        [HttpPost]
        [Route("PostComment")]
        public async Task<IHttpActionResult> PostComment(CVComments cmnt)
        {
            ResponseResult rspCmnt = new ResponseResult();
            rspCmnt.ErrorMsg = "";
            rspCmnt.status = "Success";

            if (!ModelState.IsValid)
            {
                rspCmnt.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rspCmnt);

            }
            if (User.Identity.Name == null)
            {
                rspCmnt.ErrorMsg = "User not authorized";
                rspCmnt.status = "Fail";
                return Content(HttpStatusCode.Unauthorized, rspCmnt);
            }
            try
            {
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accUs = await usmngr.FindByNameAsync(User.Identity.Name);
                IdentityResult result = null;
                if (accUs == null)
                {
                    rspCmnt.status = "User is not found";
                    return Content(HttpStatusCode.NotFound, rspCmnt);

                }
                ApplicationUser accCandidate = await usmngr.FindByNameAsync(cmnt.CandidateEmail);

                if (accCandidate == null)
                {
                    rspCmnt.status = "Candidate mail does not exist";
                    return Content(HttpStatusCode.NotFound, rspCmnt);
                }
                if (accUs.Id == accCandidate.Id)
                {
                    rspCmnt.status = "You cannot comment yourself";
                    return Content(HttpStatusCode.BadRequest, rspCmnt);
                }
                accUs.NumComments += 1;
                result = await usmngr.UpdateAsync(accUs);

                if (!result.Succeeded)
                {
                    rspCmnt.status = "Fail";
                    rspCmnt.ErrorMsg = "Comments counter is not updated";
                    return Content(HttpStatusCode.BadRequest, rspCmnt);
                }

                Document newdoc = null;
                cmnt.CommentDate = DateTime.Now;
                cmnt.type = "Comments";
                cmnt.parentid = accUs.Id;
                newdoc = await DocumentDBRepository<CVComments>.CreateDataAsync(cmnt);
             
                if (newdoc == null)
                {
                    rspCmnt.status = "Fail";
                    return Content(HttpStatusCode.NoContent, rspCmnt);
                }

                CVCommentsCounter cmtsCtr = await DocumentDBRepository<CVCommentsCounter>.GetDataSqlAsync("select TOP 1 * from c where c.type='CommentsCounter'");// (u => u.UserMail == User.Identity.Name);
                if (cmtsCtr == null)
                {
                    CVCommentsCounter ctr = new CVCommentsCounter
                    {
                        type = "CommentsCounter",
                        parentID = accUs.Id,
                        mycomments = new List<string>
                        {
                           newdoc.Id
                        }
                    };
                    await DocumentDBRepository<CVCommentsCounter>.CreateDataAsync(ctr);
                }
                else
                {
                    cmtsCtr.mycomments.Add(newdoc.Id);
                    await DocumentDBRepository<CVCommentsCounter>.UpdateDataAsync(cmtsCtr.id, cmtsCtr);
                }
                return Content(HttpStatusCode.OK, rspCmnt);
            }

            catch (Exception e)
            {
                rspCmnt.status = "Fail";
                rspCmnt.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rspCmnt);
            }
        }
        /// <summary>
        /// This method is used by the user to see the comments and the person details who commented on CV. 
        /// </summary>
        /// <returns>Firstly this method will check if there is any comment on CV 
        /// then this method will return the details of the person who
        /// commented otherwise, it'll return a message "data not found".</returns>
        
        // GET: Comments
        [HttpGet]
        [Route("GetComments")]
        public async Task<IHttpActionResult> GetMyComments()
        {
            ResponseResultCVProfileComments rsCmnt = new ResponseResultCVProfileComments
            {
                status = "Success",
                ErrorMsg = "",
                Data = null
            };

            try
            {
                CVCommentsCounter cmtsCtr = await DocumentDBRepository<CVCommentsCounter>.GetDataSqlAsync("select TOP 1 * from c where c.type='CommentsCounter'");// (u => u.UserMail == User.Identity.Name);

                if (cmtsCtr == null)
                {
                    rsCmnt.status = "Fail";
                    rsCmnt.ErrorMsg = "No comments found";
                    return Content(HttpStatusCode.BadRequest, rsCmnt);
                }

                if (cmtsCtr.mycomments == null)
                {
                    rsCmnt.status = "Fail";
                    rsCmnt.ErrorMsg = "No comments found";
                    return Content(HttpStatusCode.BadRequest, rsCmnt);
                }
                if (cmtsCtr.mycomments.Count() <= 0)
                {
                    rsCmnt.status = "Fail";
                    rsCmnt.ErrorMsg = "No comments found";
                    return Content(HttpStatusCode.BadRequest, rsCmnt);
                }
                List <CVProfileComments> commentsdata= null;
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accCand = null; 

                foreach (string candCmtId in cmtsCtr.mycomments)
                {
                    CVComments cmt = await DocumentDBRepository<CVComments>.GetDataSqlAsync("select TOP 1 * from c where c.type='Comments' and c.id ='" + candCmtId +"'");// (u => u.UserMail == User.Identity.Name);
                    
                    if (cmt == null)
                        continue;
                    
                    accCand = await usmngr.FindByNameAsync(cmt.CandidateEmail);
                    if (accCand == null)
                        continue;
                   
                    CVProfileComments cmtsprofs = new CVProfileComments
                    {
                        CommentedBy = new BasicUserInfoModel
                        {
                            Birthday = accCand.Birthday,
                            DisplayName = accCand.DisplayName,
                            Email = accCand.Email,
                            FullName = accCand.FullName,
                            HideMyAge = accCand.HideMyAge,
                            ImageUrl = accCand.ImageUrl                           
                        },
                        mycomments = cmt
                    };

                    if (commentsdata == null)
                        commentsdata = new List<CVProfileComments>();
                    commentsdata.Add(cmtsprofs);
             }

                if(commentsdata ==null )
                {
                    rsCmnt.status = "Fail";
                    rsCmnt.ErrorMsg = "No comments found";
                    return Content(HttpStatusCode.NotFound, rsCmnt);
                }
                if(commentsdata.Count() ==0)
                {
                    rsCmnt.status = "Fail";
                    rsCmnt.ErrorMsg = "No comments found";
                    return Content(HttpStatusCode.NotFound, rsCmnt);
                }

                rsCmnt.Data = commentsdata;

                 return Ok(rsCmnt);

            }
            catch (Exception e)
            {
                rsCmnt.status = "Fail";
                rsCmnt.ErrorMsg = e.Message;
                return Content(HttpStatusCode.BadRequest, rsCmnt);
            }
        } 
    }
}