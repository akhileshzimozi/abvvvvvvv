using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Blabyap.Common.Model;
using BlabyApAzure.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure.Documents;
namespace BlabyApAzure.API.Controllers
{
    public class DiscoverController : ApiController
    {
    /// <summary>
    /// This method is used for data are selected the 
    /// </summary>
    /// <param name="discInfo"></param>
    /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("DiscoverData")]
        public async Task<IHttpActionResult> GetDiscoverData(PageDiscoveryInfo discInfo)
        {     
           var headers = Request.Headers;
            ResponseResultSwipeProfile resSwp = new ResponseResultSwipeProfile
            {
                Data = null,
                status = "Success", 
                ErrorMsg = ""
            };
            try
            {
            List<CVDataSwipeProfile> profiles = new List<CVDataSwipeProfile>();
                IEnumerable<ApplicationUser> discUsers = null;
       if(discInfo != null && discInfo.discovery!= null)
       {
        if (discInfo.discovery.Expertise != null && discInfo.discovery.Expertise.Count()!= 0)
           {
     //  'Computer' and array_contains(c.CVInfo.MyExpertise, { Translation: "leadership" }, true)
            if (discInfo.discovery.Industry!="")
          {
                if ( discInfo.discovery.Seniority != null && discInfo.discovery.Seniority.Count() > 0)
                {
                    discUsers = await DocumentDBRepository<ApplicationUser>.QueryAndContinue(discInfo.curPage, discInfo.PageSize, f => f.CVInfo.MyExpertise
                                    .Where(skill => discInfo.discovery.Expertise.Contains(skill.Translation) && f.UserName != User.Identity.Name && f.HideMe == false && f.CVInfo.Industry == discInfo.discovery.Industry && discInfo.discovery.Seniority.Contains(f.CVInfo.Seniority))
                                    .Select(skill => f));
                }
                else
                 {
                    discUsers = await DocumentDBRepository<ApplicationUser>.QueryAndContinue(discInfo.curPage, discInfo.PageSize, f => f.CVInfo.MyExpertise
                                                .Where(skill => discInfo.discovery.Expertise.Contains(skill.Translation) && f.UserName != User.Identity.Name && f.HideMe == false && f.CVInfo.Industry == discInfo.discovery.Industry)
                                                .Select(skill => f));
                 }
           }
            else
                        {
                            if (discInfo.discovery.Seniority != null && discInfo.discovery.Seniority.Count() > 0)
                            {
                                discUsers = await DocumentDBRepository<ApplicationUser>.QueryAndContinue(discInfo.curPage, discInfo.PageSize, f => f.CVInfo.MyExpertise
                                                .Where(skill => discInfo.discovery.Expertise.Contains(skill.Translation) && f.UserName != User.Identity.Name && f.HideMe == false && discInfo.discovery.Seniority.Contains(f.CVInfo.Seniority))
                                                .Select(skill => f));
                            }
                            else
                            {
                                discUsers = await DocumentDBRepository<ApplicationUser>.QueryAndContinue(discInfo.curPage, discInfo.PageSize, f => f.CVInfo.MyExpertise
                                                            .Where(skill => discInfo.discovery.Expertise.Contains(skill.Translation) && f.UserName != User.Identity.Name && f.HideMe == false )
                                                            .Select(skill => f));
                            }
                        } 
           }                    
 
           }
                if (discUsers == null)
                {
                    resSwp.status = "Fail";
                    resSwp.ErrorMsg ="No profiles Found";
                    return Content(HttpStatusCode.NotFound, resSwp);
                }
                if(discUsers.Count() == 0)
                {
                    resSwp.status = "Fail";
                    resSwp.ErrorMsg = "No profiles Found";
                    return Content(HttpStatusCode.NotFound, resSwp);
                }

        ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        ApplicationUser accUs = await usmngr.FindByNameAsync(User.Identity.Name);
        IEnumerable<CVMatchCounter> matchProfs = await DocumentDBRepository<CVMatchCounter>.GetDatasAsync(g=> g.parentID == accUs.Id);
        CVMatchCounter matchProf = matchProfs.FirstOrDefault();
        foreach (ApplicationUser cv in discUsers)
                {
                  BasicUserInfoModel bs = new BasicUserInfoModel
                    {
                        HideMyAge= cv.HideMyAge,
                        DisplayName = cv.DisplayName,
                        FullName = cv.FullName,
                        Email = cv.UserName,
                        ImageUrl = cv.ImageUrl, 
                        Birthday = cv.Birthday 
                    };
                    {
                    CVDataSwipeProfile swprof = new CVDataSwipeProfile()
                        {
                            userCV = cv.CVInfo,
                            userdetails = bs,
                            cardColor = "White"
                        };
                        if (matchProf != null )
                        {
                            if( matchProf.mymatches.Contains(cv.Id))
                                 swprof.cardColor = "Grey";
                        }
                        profiles.Add(swprof);
                    }
                }
                // if (profiles != null && profiles.Count()!= 0 && discInfo.swipeDirection != "")
                {
                    SwipeInfo swInfo = null;
                    accUs.DiscoverFilter = discInfo.discovery;
                    if(discInfo.swipeDirection!=null && discInfo.swipeDirection!="")
                    {
                    if (discInfo.swipeDirection.ToLower() == "right")
                    {
                        swInfo = new SwipeInfo
                        {
                            type = "swipes",
                            swipeDir = discInfo.swipeDirection.ToLower(),
                            createdDate = DateTime.Now,
                            parentId = accUs.Id
                        };
                        accUs.NumRightSwipes += 1;

                        if (discInfo.matchCandidate != null && discInfo.matchCandidate.Trim() != "")
                        {
                            ApplicationUser accCandidate = await usmngr.FindByNameAsync(discInfo.matchCandidate);

                            if (accCandidate == null)
                            {
                                resSwp.status = "Candidate to match does not exist";
                                return Content(HttpStatusCode.NotFound, resSwp);
                            }

                            if (accUs.Id == accCandidate.Id)
                            {
                                resSwp.status = "You cannot match yourself";
                                return Content(HttpStatusCode.BadRequest, resSwp);
                            }

                            ResponseResult mtch = await new MatchController().UpdateMatchCounter(accUs.Id, accCandidate);

                            if (mtch.status == "Success")
                            {
                                accUs.NumMatches += 1;
                                //resSwp.status = "Fail";
                                //resSwp.ErrorMsg = mtch.ErrorMsg;
                                //return Content(HttpStatusCode.BadRequest, resSwp);
                            }
                        }

                        //accUs.Id
                    }
                    else if (discInfo.swipeDirection.ToLower() == "left")
                    {
                        swInfo = new SwipeInfo
                        {
                            swipeDir = discInfo.swipeDirection.ToLower(),
                            createdDate = DateTime.Now,
                            parentId = accUs.Id
                        };
                        accUs.NumLeftSwipes += 1;
                    }
                }
                    IdentityResult result = await usmngr.UpdateAsync(accUs);
                    if (!result.Succeeded)
                    {
                        resSwp.status = "Fail";
                        resSwp.ErrorMsg = "user swipes not updated";
                        return Content(HttpStatusCode.BadRequest, resSwp);
                    }  
                    else
                    {
                        Document resSwipe = await DocumentDBRepository<SwipeInfo>.CreateDataAsync(swInfo);
                    }
                }
          
               
                  resSwp.Data = profiles;
                return Content(HttpStatusCode.OK, resSwp);
                //CVData entries = discData.Last();

                //CALL DISCOVER ALGO
            }
            catch (Exception e)
            {
                resSwp.status = "Fail";
                resSwp.ErrorMsg = e.Message;
                return Content(HttpStatusCode.BadRequest,resSwp);
            }
        } 
        // GET: Discover
    }
}