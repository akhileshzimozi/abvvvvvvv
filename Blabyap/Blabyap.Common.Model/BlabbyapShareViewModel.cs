using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Blabyap.Common.Model
{
    public class ResponseResult
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
    }
    public class ResponseResultImage
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public Uri Data { get; set; }
    }
    public class ResponseResultLookUp
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public List<FamilyData> Data { get; set; }
    }
    public class ResponseResultLocation
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
    public class ResponseResultCV
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public CVData Data { get; set; }
    }
    public class ResponseResultCVLinkedIn
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public DataLinkedIn Data { get; set; }
    }
    public class ResponseResultSwipeProfile
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public string ContinuousToken { get; set; }
        public List<CVDataSwipeProfile> Data { get; set; }
    }
    public class ResponseResultCVChat
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public List<CVChat> Data { get; set; }
    }

    public class ResponseResultCVProfileComments
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public List<CVProfileComments> Data { get; set; }
    }
    public class ResponseResultMatchProfile
    {
        public string status { get; set; }
        public string ErrorMsg { get; set; }
        public List<CVMatchProfile> Data { get; set; }
    }

    public class CustomCaption
    {
        public string Code { get; set; }
        public string Translation { get; set; }
        public string name { get; set; }
        public bool ISSelected { get; set; }
    }
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPassword
    {
        public string Email { get; set; }
    }
    public class LinkedInAccess
    {
        public string Token { get; set; }
    }

    public class HideUserData
    {
        public bool HideFlag { get; set; }
    }
    public class ResetPassword
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class ImageUpload
    {
        public byte[] Image { get; set; }
        public int count { get; set; }
        //  public Stream Image { get; set; }
    }

    public class FamilyData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
    }

    public class LookupInfo
    {
        public string Family { get; set; }
        public List<FamilyData> FamilyInfo { get; set; }
        public LookupInfo()
        {
            FamilyInfo = new List<FamilyData>();
        }
    }

    public class ContactSuggest
    {
        public string type { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
    }
    public class PageSwipeInfo
    {
        public int curPage { get; set; }
        public int PageSize { get; set; }
        public string swipeDirection { get; set; }
        public string matchCandidate { get; set; }
    }

    public class PageDiscoveryInfo
    {
        public int curPage { get; set; }
        public int PageSize { get; set; }
        public string swipeDirection { get; set; }
        public string matchCandidate { get; set; }
        public DiscoverInfo discovery { get; set; }
        public PageDiscoveryInfo()
        {
            // discovery = new DiscoverInfo();
        }
    }

    public class DiscoverInfo
    {
        public List<string> Expertise { get; set; }
        public List<string> Seniority { get; set; }

        public string Industry { get; set; }
        public string distanceKM { get; set; }

        public DiscoverInfo()
        {
            //Expertise = new List<string>();
            //Seniority = new List<string>();

        }
    }

    public partial class RegisterInfoModel
    {
        [JsonProperty("FullName")]

        public string FullName { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("Birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        // [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
    public class SwipeInfo
    {
        public string id { get; set; }
        public string type { get; set; }
        public string parentId { get; set; }
        public string swipeDir { get; set; }
        public DateTime createdDate { get; set; }
    }
    public class BasicUserInfoModel
    {
        public string id { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public bool HideMyAge { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CVData
    {
        //  public string id { get; set; }  
        public string type { get; set; }
        public string CurrentCompany { get; set; }
        public string CVEmail { get; set; }
        public string JobTitle { get; set; }
        public string Industry { get; set; }
        public string Seniority { get; set; }
        public string Organization1 { get; set; }
        public string Organization2 { get; set; }
        public string Organization3 { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public string Startups { get; set; }
        public List<CustomCaption> MyExpertise { get; set; }
        public string AboutYou { get; set; }
        public CVData()
        {
            MyExpertise = new List<CustomCaption>();

        }

    }
    public partial class DataLinkedIn
    {
        public BasicUserInfoModel userdetails { get; set; }
        public CVData userCV { get; set; }
    }

    public partial class CVDataSwipeProfile
    {
        public BasicUserInfoModel userdetails { get; set; }
        public CVData userCV { get; set; }
        public string cardColor { get; set; }
    }
    public class CVCommentsCounter
    {
        public string id { get; set; }
        public string type { get; set; }
        public string parentID { get; set; }
        public List<string> mycomments { get; set; }
    }

    public class CVMatchCounter
    {
        public string id { get; set; }
        public string type { get; set; }
        public string parentID { get; set; }
        public List<string> mymatches { get; set; }
    }
    public class CVUnMatchCounter
    {
        public string id { get; set; }
        public string type { get; set; }
        public string parentID { get; set; }
        public List<UnmatchInfo> UnmatchedCandidates { get; set; }
    }

    public partial class CVMatch
    {
        public string CandidateEmail { get; set; }

    }
    public partial class UnmatchInfo
    {
        public string candidateID { get; set; }
        public string Reason { get; set; }
    }
    public partial class CVUnmatch
    {
        public string CandidateEmail { get; set; }
        public string Reason { get; set; }
    }
    public partial class CVMatchProfile
    {
        public BasicUserInfoModel userdetails { get; set; }
        public CVData userCV { get; set; }
        public List<CVProfileComments> lstcomments { get; set; }
    }
    public partial class CVProfileComments
    {
        public BasicUserInfoModel CommentedBy { get; set; }
        public CVComments mycomments { get; set; }
    }
    public partial class CVComments
    {
        public string id { get; set; }
        public string type { get; set; }
        public string parentid { get; set; }
        public string CandidateEmail { get; set; }
        public string Description { get; set; }
        public string Ratings { get; set; }
        public DateTime? CommentDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
    public class ChatUser
    {
        public string UserMail { get; set; }
        public string connectionId { get; set; }
    };

    public class UserCheck
    {
        public string userMail { get; set; }
    }
    public partial class CVChat
    {
        public string type { get; set; }
        public string fromUser { get; set; }
        public string toUser { get; set; }
        public string ChatMsg { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

}
