using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blabyapp.API.ViewModels;
using Blabyapp.API.DataModels;
using Blabyap.Common.Model;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Description;
using Blabyapp.API.Helpers;
using System.IO;

namespace Blabyapp.API.Controllers
{
    [RoutePrefix("api/Utility")]
    public class UtilityController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [AllowAnonymous]
        [Route("ExternalLogin")]
        public IHttpActionResult ExternalLogin()
        {

            //    return new ChallengeResult("LinkedIn", "http://localhost:58582/Api/Account/SaveLinkedinUser", this.Request);
            //ChallengeResult res = new ChallengeResult("LinkedIn", "http://localhost:58582/api/Account/SaveLinkedinUser", this.Request);
            //res.ExecuteAsync(new System.Threading.CancellationToken());

            return new ChallengeResult("LinkedIn", "http://localhost:58582/Api/Account/SaveLinkedinUser", this.Request);
        }
        [HttpGet]
        [Route("SwipeInfo")]
        public List<CVDataSwipeProfile> GetSwipeProfiles()
        {
            return new List<CVDataSwipeProfile>
            {
                new CVDataSwipeProfile()
                {
                    userCV = new CVData()
                    {
                        cvID = new Guid().ToString(),
                        CurrentCompany = "Zimozi",
                        JobTitle = "QA",
                        Industry = "Check",
                        Seniority = "Middle",
                        Organization1 = "Kent",
                        Nationality = "Indian",
                        Education = "BE",
                        Startups = "0",

                       //MyExpertise = new List<string>
                       //     {
                       //        "leadership","AI"
                       //     },
                        AboutYou = "The Best"

                     },
                    userdetails = new RegisterInfoModel()
                    {
                            //UserID = new Guid().ToString(),
                            Email = "a@a.com",
                            DisplayName ="TestUser",
                            FullName = "Bob Mathew",
                            Birthday = new DateTime(2000,11,11),
                            ImageUrl = "Image"
                    }

                },
                new CVDataSwipeProfile()
                {
                    userCV = new CVData()
                    {
                        cvID = new Guid().ToString(),
                        CurrentCompany = "Zimozi",
                        JobTitle = "QA",
                        Industry = "Check",
                        Seniority = "Middle",
                        Organization1 = "Kent",
                        Nationality = "Indian",
                        Education = "BE",
                        Startups = "0",

                       //MyExpertise = new List<CustomCaption>
                       //     {
                       //         new CustomCaption {Code ="Skill",Translation ="leadership",ISSelected=true},
                       //         new CustomCaption {Code ="Skill",Translation ="AI",ISSelected=true }
                       //     },
                        AboutYou = "The Best"

                     },
                    userdetails = new RegisterInfoModel()
                    {
                          // UserID = new Guid().ToString(),
                            Email = "david@gmail.com",
                            DisplayName ="David",
                            FullName = "David",
                            Birthday = new DateTime(1998,11,11),
                            ImageUrl = "Image"
                    }
            }
        };


            //return new Comments().GetDemoComments(usermail, displayname);
        }

        //[HttpPost]
        //[Route("DiscoverData")] 
        //[ResponseType(typeof(SwipeProfile))]
        //public async Task<IHttpActionResult> GetDiscoverData(DiscoverInfo filter)
        //{
        //    string seniority = "";

        //    foreach (CustomCaption exp in filter.Seniority)
        //    {
        //        if (seniority != "")
        //            seniority += ",";

        //        seniority = seniority + exp.Translation;
        //    }


        //    IQueryable<MyCV> cvs = from p in db.MyCVs
        //                where p.Industry == filter.Industry
        //                select p;
            

        //    int chk = cvs.Count();
        //    return Ok(new SwipeProfile());//.GetDemoSwipeProfileList());
            
          
        //    //return new Comments().GetDemoComments(usermail, displayname);
        //}

        [HttpGet]
        [Route("MatchInfo")]
        public List<MatchProfile> GetMatchInfo()
        {
            return new List<MatchProfile>
            {
             new MatchProfile()
             {
                  userCV = new CVData()
                    {
                        cvID = new Guid().ToString(),
                        CurrentCompany = "Zimozi",
                        JobTitle = "QA",
                        Industry = "Check",
                        Seniority = "Middle",
                        Organization1 = "Kent",
                        Nationality = "Indian",
                        Education = "BE",
                        Startups = "0",

                       //MyExpertise = new List<CustomCaption>
                       //     {
                       //         new CustomCaption {Code ="Skill",Translation ="leadership",ISSelected=true},
                       //         new CustomCaption {Code ="Skill",Translation ="AI",ISSelected=true }
                       //     },
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

                    lstcomments = new List<ProfileComments>
                    {
                        new ProfileComments()
                        {
                        CommentBy ="James",
                        CommentDate = new DateTime(2018,10,10),
                        CommentID = new Guid().ToString(),
                        Description="The person is very professional and sincere",
                        Ratings="5",
                        UserEmail="james@as.com"
                        },
                        new ProfileComments()
                        {
                        CommentBy ="Renie",
                        CommentDate = new DateTime(2018,10,10),
                        CommentID = new Guid().ToString(),
                        Description="Very comimited and talented person.It was pleasure working with him",
                        Ratings="4",
                        UserEmail="james@as.com"
                        }
                    }

             }
        };
        }
        [HttpPost]
        [Route("PostCV")]
        public async Task<IHttpActionResult> PostDemoCV()
        {
            CVData ncv = new CVData
            {
                cvID = new Guid().ToString(),
                CurrentCompany = "zimzimzim",
                JobTitle = "QA",
                Industry = "Medical",
                Seniority = "Medium",
                Organization1 = "Sasd",
                Nationality = "Indian",
                Education = "BE",
                Startups = "0",
                // MyExpertise="Leadership",
                //MyExpertise = new List<CustomCaption>
                //    {
                //        new CustomCaption {Code ="Skill",Translation ="leadership",ISSelected=true},
                //        new CustomCaption {Code ="Skill",Translation ="leadership",ISSelected=true }
                //    },
                AboutYou = "The Best"
            };

            return await new MyCVsController().PostMyCV(ncv);
        }

        [HttpGet]
        [Route("GetComments")]
        public List<ProfileComments> GetMyComments(string usermail, string displayname)
        {
           return new List<ProfileComments>
            {
            new ProfileComments
            {
                CommentID = "1",
                UserEmail = "akhil.singh@gmail.com",
                Description = "This is the best recruiter and very good pay master",
                Ratings = "5",
                CommentDate = DateTime.Now,
                CommentBy = "akhl"
            },
            new ProfileComments
            {
                CommentID = "2",
                UserEmail = "Pr@s.com",
                Description = "This is very good recruiter and employee centric company",
                Ratings = "5",
                CommentDate = DateTime.Now,
                CommentBy = "nicky"
            }

            };
        }

        [HttpGet]
        [Route("ForgotPassword")]
        public Task<IHttpActionResult> ForgotPassword(string email)
        {
            SetPasswordBindingModel usrpwd = new SetPasswordBindingModel
            {
                Email = email,
                NewPassword = "P@1234567yu",
                ConfirmPassword = "P@1234567yu"
            };
            return new AccountController().SetPassword(usrpwd);
        }

        [HttpGet]
        [Route("ChatInfo")]
        public List<ChatInfo> GetchatInfo()
        {
            return new List<ChatInfo>
            {
                new ChatInfo()
                {
                    chatID = new Guid().ToString(),
                    CandidateEmail = "test@gm.com",
                    ChatMsg =" How are you doing",
                    CreatedDate= DateTime.Now,
                    RecruiterEmail ="rtn@ab.com"
                }
            };
        }
    }
}