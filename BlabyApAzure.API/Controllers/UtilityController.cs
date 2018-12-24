using Blabyap.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BlabyApAzure.API.Models;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.Azure.Documents;

namespace BlabyApAzure.API.Controllers
{
    public class UtilityController : ApiController
    {
        [HttpGet]
        [Route("IndustryInfo")]
        public async Task<IHttpActionResult> GetIndustryInfo()
        {
            ResponseResultLookUp rsplookup = new ResponseResultLookUp()
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };
            IEnumerable<LookupInfo> Industries = await DocumentDBRepository<LookupInfo>.GetDatasAsync(u => u.Family == "Industry");// (u => u.UserMail == User.Identity.Name);

            if (Industries == null)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
            if (Industries.Count() == 0)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
            LookupInfo entries = Industries.Last();
            try
            {
                List<FamilyData> lstInds = new List<FamilyData>(entries.FamilyInfo);
                rsplookup.Data = lstInds;
                return Content(HttpStatusCode.OK, rsplookup);
            }
            catch (Exception e)
            {
                rsplookup.ErrorMsg = e.Message;
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
        }
        [HttpGet]
        [Route("NationalityInfo")]
        public async Task<IHttpActionResult> GetNationalityInfo()
        {
            ResponseResultLookUp rsplookup = new ResponseResultLookUp()
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };
            IEnumerable<LookupInfo> Countries = await DocumentDBRepository<LookupInfo>.GetDatasAsync(u => u.Family == "Nationality");// (u => u.UserMail == User.Identity.Name);

            if (Countries == null)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
            if (Countries.Count() == 0)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
            LookupInfo cntries = Countries.Last();

            try
            {
                List<FamilyData> lstCntry = new List<FamilyData>(cntries.FamilyInfo);
                rsplookup.Data = lstCntry;
                return Content(HttpStatusCode.OK, rsplookup);
            }
            catch (Exception e)
            {
                rsplookup.ErrorMsg = e.Message;
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
        }

        [HttpGet]
        [Route("SeniorityInfo")]
        public async Task<IHttpActionResult> GetSeniorityInfo()
        {
            ResponseResultLookUp rsplookup = new ResponseResultLookUp()
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };
            IEnumerable<LookupInfo> Seniority = await DocumentDBRepository<LookupInfo>.GetDatasAsync(u => u.Family == "Seniority");// (u => u.UserMail == User.Identity.Name);

            if (Seniority == null)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
            if (Seniority.Count() == 0)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }

            LookupInfo entries = Seniority.Last();

            try
            {
                List<FamilyData> lstSeniority = new List<FamilyData>(entries.FamilyInfo);


                rsplookup.Data = lstSeniority;
                return Content(HttpStatusCode.OK, rsplookup);
            }
            catch (Exception e)
            {
                rsplookup.ErrorMsg = e.Message;
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
        }
        [HttpGet]
        [Route("ExpertiseInfo")]
        public async Task<IHttpActionResult> GetExpertiseInfo(string industrycode = "")
        {
            ResponseResultLookUp rsplookup = new ResponseResultLookUp()
            {
                Data = null,
                ErrorMsg = "",
                status = "Success"
            };
            IEnumerable<LookupInfo> Expertise = await DocumentDBRepository<LookupInfo>.GetDatasAsync(u => u.Family == "Expertise");// (u => u.UserMail == User.Identity.Name);

            if (Expertise == null)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
            if (Expertise.Count() == 0)
            {
                rsplookup.ErrorMsg = "Not found";
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }

            LookupInfo entries = Expertise.Last();

            try
            {
                List<FamilyData> lstExpertise = new List<FamilyData>(entries.FamilyInfo);
                List<FamilyData> retLst = new List<FamilyData>();
                if (industrycode == "")
                {
                    retLst = lstExpertise;
                }
                else
                {
                    foreach (FamilyData fmd in lstExpertise)
                    {
                        if (fmd.Code == industrycode)
                            retLst.Add(fmd);
                    }
                }

                rsplookup.Data = retLst;
                return Content(HttpStatusCode.OK, rsplookup);
            }
            catch (Exception e)
            {
                rsplookup.ErrorMsg = e.Message;
                rsplookup.status = "Fail";
                return Content(HttpStatusCode.NotFound, rsplookup);
            }
        }
        [HttpPost]
        [Route("SwipeProfiles")]
        public async Task<IHttpActionResult> GetSwipeProfiles(/*DiscoverInfo discInfo=null*/)
        {
            ResponseResultSwipeProfile rg = new ResponseResultSwipeProfile();
            rg.status = "Success";
            rg.ErrorMsg = "";
            rg.Data = new List<CVDataSwipeProfile>
            {
                new CVDataSwipeProfile()
                {
                    userCV = new CVData()
                    {
                        //id = new Guid().ToString(),
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
                    userdetails = new BasicUserInfoModel()
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
                        //id = new Guid().ToString(),
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
                          // UserID = new Guid().ToString(),
                            Email = "david@gmail.com",
                            DisplayName ="David",
                            FullName = "David",
                            Birthday = new DateTime(1998,11,11),
                            ImageUrl = "Image"
                    }
            }
        };
            return Ok(rg);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("PostImage")]
        public async Task<IHttpActionResult> AddProfileImage(ImageUpload imgload)
        {
            ResponseResultImage res = new ResponseResultImage
            {
                ErrorMsg = "",
                status = "Success",
                Data = null
            };
         

            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }
            try
            {
                var newImageName = string.Empty;
                var path = System.Web.Hosting.HostingEnvironment.MapPath("~");
                var guid = Guid.NewGuid().ToString();
                    newImageName = guid;

                    var storageAccount = new CloudStorageAccount(new StorageCredentials("blabyapstorage", "YFkGt/WLwMZay8+DWiDjUGVltYKDDpW+Kcm6AuYPsGn/bkh7M0rOT1UEf2SS3KfjcSaiyi2W5x6z9Ti8C9a0nQ=="), true);

                    // Create the blob client.
                    var blobClient = storageAccount.CreateCloudBlobClient();
                    // Retrieve reference to a previously created container.
                    var container = blobClient.GetContainerReference("imagecontainer");
                if(container ==null)
                {
                    res.ErrorMsg = "Image container not found";
                    res.status = "Fail";
                    return Content(HttpStatusCode.NotFound, res);
                }
                    // Retrieve reference to a blob named "myblob".
                    var blockBlob = container.GetBlockBlobReference(newImageName);
                if (blockBlob == null)
                {
                    res.ErrorMsg = "Image reference could not be created";
                    res.status = "Fail";
                    return Content(HttpStatusCode.NotFound, res);
                }
                await blockBlob.UploadFromByteArrayAsync(imgload.Image, 0,imgload.count);

                //   blockBlob.UploadFile(Fname);
               // blockBlob.UploadFromByteArray(imgload.Image);//.UploadFromStream(filestream);//after this point it doesn't work
                    res.Data = blockBlob.Uri;
                
                return Ok(res);
            }

            catch (Exception e)
            {
                res.ErrorMsg = e.Message;
                res.status = "Fail";
                return Content(HttpStatusCode.InternalServerError, res);
            }
        }

        [HttpPost]
        [Route("ContactSuggest")]
        public async Task<IHttpActionResult> ContactUs(ContactSuggest cntus)
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
            try
            {
                Document newdoc = null;
                cntus.type = "ContactUs";

                newdoc = await DocumentDBRepository<ContactSuggest>.CreateDataAsync(cntus);

                if (newdoc == null)
                {
                    res.status = "Fail";
                    return Content(HttpStatusCode.NoContent, res);
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
        [Route("ContactSuggest")]
        public async Task<IHttpActionResult> Suggestion(ContactSuggest cntus)
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
            try
            {
                Document newdoc = null;
                cntus.type = "Suggestion";

                newdoc = await DocumentDBRepository<ContactSuggest>.CreateDataAsync(cntus);

                if (newdoc == null)
                {
                    res.status = "Fail";
                    return Content(HttpStatusCode.NoContent, res);
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
    }

   
}