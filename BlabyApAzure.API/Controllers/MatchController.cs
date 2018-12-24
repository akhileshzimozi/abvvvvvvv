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
    [RoutePrefix("api/Match")]
    public class MatchController : ApiController
    {
        // GET: Match

        [HttpGet]
        [Route("MatchInfo")]
        public async Task<IHttpActionResult> GetMatchInfo()
        {
            ResponseResultMatchProfile rsmp = new ResponseResultMatchProfile
            {
                status = "Success",
                ErrorMsg = "",
                Data = null

            };


            rsmp.Data = new List<CVMatchProfile>
            {
             new CVMatchProfile()
             {
                  userCV = new CVData()
                    {
                       // cvID = new Guid().ToString(),
                        CurrentCompany = "Zimozi",
                        JobTitle = "QA",
                        Industry = "Check",
                        Seniority = "Middle",
                        Organization1 = "Kent",
                        Nationality = "Indian",
                        Education = "BE",
                        Startups = "0",

                       MyExpertise = new List<CustomCaption>
                            {
                                new CustomCaption {Code ="Skill",Translation ="leadership",ISSelected=true},
                                new CustomCaption {Code ="Skill",Translation ="AI",ISSelected=true }
                            },
                        AboutYou = "The Best"

                     },
                    userdetails = new BasicUserInfoModel()
                    {
                           UserID = new Guid().ToString(),
                            Email = "a@a.com",
                            DisplayName ="TestUser",
                            FullName = "Bob Mathew",
                            Birthday = new DateTime(2000,11,11),
                            ImageUrl = "Image"
                    },

                    lstcomments = new List<CVProfileComments>
                    {

                        new CVProfileComments()
                        {
                            CommentedBy = new BasicUserInfoModel()
                            {
                                   UserID = new Guid().ToString(),
                                    Email = "a@a.com",
                                    DisplayName ="TestUser",
                                    FullName = "Bob Mathew",
                                    Birthday = new DateTime(2000,11,11),
                                    ImageUrl = "Image"
                            },
                            mycomments = new CVComments()
                            {

                                    CommentDate = new DateTime(2018,10,10),
                                     Description="The person is very professional and sincere",
                                    Ratings="5",
                                    CandidateEmail="james@as.com"
                             },
                         },
                        new CVProfileComments()
                         {
                            CommentedBy = new BasicUserInfoModel()
                            {
                                   UserID = new Guid().ToString(),
                                    Email = "a@a.com",
                                    DisplayName ="TestUser",
                                    FullName = "Bob Mathew",
                                    Birthday = new DateTime(2000,11,11),
                                    ImageUrl = "Image"
                            },
                            mycomments = new CVComments()
                            {

                                    CommentDate = new DateTime(2018,10,10), 
                                    Description="Very comimited and talented person.It was pleasure working with him",
                                    Ratings="4",
                                    CandidateEmail="james@as.com"
                            }
                     }
                    }
             }
        };
            //IEnumerable<CVMatch> matchedProfs = await DocumentDBRepository<CVMatch>.GetDatasAsync(t => t.MatchID.Contains(User.Identity.Name) == false);

            //if (matchedProfs == null)
            //{
            //    rsmp.status = "Fail";
            //    rsmp.ErrorMsg = "No profiles Found";
            //    return Content(HttpStatusCode.NotFound, rsmp);
            //}
            //if (matchedProfs.Count() == 0)
            //{
            //    rsmp.status = "Fail";
            //    rsmp.ErrorMsg = "No profiles Found";
            //    return Content(HttpStatusCode.NotFound, rsmp);
            //}

            //IEnumerable<BasicUserInfoModel> candidate = null;
            //foreach (CVMatch cv in matchedProfs)
            //{
            //    candidate = await DocumentDBRepository<BasicUserInfoModel>.GetDatasAsync(g => g.Email == cv.MatchID);

            //    if (candidate != null && candidate.Count() != 0)
            //    {
            //        CVMatchProfile swprof = new CVMatchProfile()
            //        {
            //            lstcomments
            //            userCV = cv,
            //            userdetails = candidate.First()
            //        };
            //        profiles.Add(swprof);
            //    }
            //}
            return Ok(rsmp);
        }

        [HttpPost]
        [Route("MatchCandidate")]
        public async Task<IHttpActionResult> PostMatchCandidate(CVMatch mtch)
        {
            ResponseResult rspMtch = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rspMtch.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rspMtch);

            }
            if (User.Identity.Name == null)
            {
                rspMtch.ErrorMsg = "User not authorized";
                rspMtch.status = "Fail";
                return Content(HttpStatusCode.Unauthorized, rspMtch);
            }
            try
            { 
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accUs = await usmngr.FindByNameAsync(User.Identity.Name);
                IdentityResult result = null;
                if (accUs == null)
                {
                    rspMtch.status = "User is not found";
                    return Content(HttpStatusCode.NotFound, rspMtch); 
                }
                ApplicationUser accCandidate = await usmngr.FindByNameAsync(mtch.CandidateEmail);

                if (accCandidate == null)
                {
                    rspMtch.status = "Candidate to match does not exist";
                    return Content(HttpStatusCode.NotFound, rspMtch);
                }
                if (accUs.Id == accCandidate.Id)
                {
                    rspMtch.status = "You cannot match yourself";
                    return Content(HttpStatusCode.BadRequest, rspMtch);
                }
                accUs.NumMatches += 1;
                result = await usmngr.UpdateAsync(accUs);

                if (!result.Succeeded)
                {
                    rspMtch.status = "Fail";
                    rspMtch.ErrorMsg = "Match counter is not updated";
                    return Content(HttpStatusCode.BadRequest, rspMtch);
                }
                await UpdateMatchCounter(accUs.Id,accCandidate);
                return Content(HttpStatusCode.OK, rspMtch);

            }

            catch (Exception e)
            {
                rspMtch.status = "Fail";
                rspMtch.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rspMtch);
            }
        }

        public async Task<ResponseResult> UpdateMatchCounter(string parID, ApplicationUser usrCandidate)
        {
            ResponseResult rspMtch = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };
            try
            {
                CVMatchCounter matchCtr = await DocumentDBRepository<CVMatchCounter>.GetDataSqlAsync("select TOP 1 * from c where c.type='MatchCounter' and c.parentID='" + parID+"'");// (u => u.UserMail == User.Identity.Name);

                if (matchCtr == null)
                {
                    CVMatchCounter ctr = new CVMatchCounter
                    {
                        type = "MatchCounter",
                        parentID = parID,
                        mymatches = new List<string>
                        {
                            usrCandidate.Id
                        }
                    };
                    await DocumentDBRepository<CVMatchCounter>.CreateDataAsync(ctr);
                }
                else
                {
                    if(matchCtr.mymatches.Contains(usrCandidate.Id))
                    {
                        rspMtch.status = "Fail";
                        rspMtch.ErrorMsg = "Candidate is already matched";
                    }
                    else
                    {
                        matchCtr.mymatches.Add(usrCandidate.Id);
                        Document d = await DocumentDBRepository<CVMatchCounter>.UpdateDataAsync(matchCtr.id, matchCtr);
                        if (d == null)
                        {
                            rspMtch.status = "Fail";
                            rspMtch.ErrorMsg = "Match Counter couldnt be set";
                        } 
                    }
                }
                return rspMtch;
            }
            catch (Exception e)
            {
                rspMtch.status = "Fail";
                rspMtch.ErrorMsg = e.Message;
                return rspMtch;
            }
           
        }
        [HttpPost]
        [Route("UnMatchCandidate")]
        public async Task<IHttpActionResult> UnMatchCandidate(CVUnmatch unmtch)
        {
            ResponseResult rspMtch = new ResponseResult
            {
                ErrorMsg = "",
                status = "Success"
            };

            if (!ModelState.IsValid)
            {
                rspMtch.status = "Fail";
                return Content(HttpStatusCode.BadRequest, rspMtch);

            }
            if (User.Identity.Name == null)
            {
                rspMtch.ErrorMsg = "User not authorized";
                rspMtch.status = "Fail";
                return Content(HttpStatusCode.Unauthorized, rspMtch);
            }
            try
            {
                ApplicationUserManager usmngr = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser accUs = await usmngr.FindByNameAsync(User.Identity.Name);
                IdentityResult result = null;
                if (accUs == null)
                {
                    rspMtch.status = "User is not found";
                    return Content(HttpStatusCode.NotFound, rspMtch);

                }
                ApplicationUser accCandidate = await usmngr.FindByNameAsync(unmtch.CandidateEmail);

                if (accCandidate == null)
                {
                    rspMtch.status = "Candidate to match does not exist";
                    return Content(HttpStatusCode.NotFound, rspMtch);
                }
                if (accUs.Id == accCandidate.Id)
                {
                    rspMtch.status = "You cannot unmatch yourself";
                    return Content(HttpStatusCode.BadRequest, rspMtch);
                }
                if (accUs.NumMatches <= 0)
                {
                    rspMtch.status = "NO matches found for the user";
                    return Content(HttpStatusCode.NotFound, rspMtch);
                }

                accUs.NumMatches -= 1;

                result = await usmngr.UpdateAsync(accUs);

                if (!result.Succeeded)
                {
                    rspMtch.status = "Fail";
                    rspMtch.ErrorMsg = "Match counter is not decremented";
                    return Content(HttpStatusCode.BadRequest, rspMtch);
                }

                CVMatchCounter matchCtr = await DocumentDBRepository<CVMatchCounter>.GetDataSqlAsync("select TOP 1 * from c where c.type='MatchCounter'");// (u => u.UserMail == User.Identity.Name);

                if (matchCtr == null)
                {
                    rspMtch.status = "Candidate to match does not exist";
                    return Content(HttpStatusCode.NotFound, rspMtch);
                }

                if (!matchCtr.mymatches.Contains(accCandidate.Id))
                {
                    rspMtch.status = "Fail";
                    rspMtch.ErrorMsg = "Candidate is not matched";
                    return Content(HttpStatusCode.BadRequest, rspMtch);
                }


                matchCtr.mymatches.Remove(accCandidate.Id);

                await DocumentDBRepository<CVMatchCounter>.UpdateDataAsync(matchCtr.id, matchCtr);

                CVUnMatchCounter unmatchCtr = await DocumentDBRepository<CVUnMatchCounter>.GetDataSqlAsync("select TOP 1 * from c where c.type='UnMatchCounter'");// (u => u.UserMail == User.Identity.Name);

                if (unmatchCtr == null)
                {
                    CVUnMatchCounter ctr = new CVUnMatchCounter
                    {
                        type = "UnMatchCounter",
                        parentID = accUs.Id,
                        UnmatchedCandidates = new List<UnmatchInfo>
                        {
                            new UnmatchInfo
                            {
                                candidateID = accCandidate.Id,
                                Reason =  unmtch.Reason
                            }

                        }
                    };
                    await DocumentDBRepository<CVUnMatchCounter>.CreateDataAsync(ctr);
                }
                else
                {
                    unmatchCtr.UnmatchedCandidates.Add(
                        new UnmatchInfo
                        {
                            candidateID = accCandidate.Id,
                            Reason = unmtch.Reason
                        });
                    await DocumentDBRepository<CVUnMatchCounter>.UpdateDataAsync(unmatchCtr.id, unmatchCtr);
                }

                return Content(HttpStatusCode.OK, rspMtch);

            }

            catch (Exception e)
            {
                rspMtch.status = "Fail";
                rspMtch.ErrorMsg = e.Message;
                return Content(HttpStatusCode.InternalServerError, rspMtch);
            }
        }
    }
}