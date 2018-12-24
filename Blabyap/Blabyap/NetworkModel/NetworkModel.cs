using Akavache;
using Blabyap.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reactive.Linq;
//using Xamarin.Forms;


namespace Blabyap.Common.Model.NetworkModel
{

    public partial class RegisterInfo : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        // public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;



        private string fullName;
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name should not be empty")]
        public string FullName
        {
            get { return this.fullName; }
            set
            {
                this.fullName = value;
            }
        }

    string displayName;

        [DataType(DataType.Text)]
        [DisplayOptions(ValidMessage = "Your Name looks Awesome.")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayName should not be empty")]
        [StringLength(10, ErrorMessage = "DisplayName should not exceed 10 characters")]
        public string DisplayName
        {
            get { return this.displayName; }
            set
            {
                this.displayName = value;
            }
        }

       DateTime birthday;



        [DateRange(MaxYear  = 2000, ErrorMessage = "Sorry, You are not 18 year Old.")]
        public DateTime Birthday
        {
            get { return this.birthday; }
            set
            {
                this.birthday = value;
            }
        }

       string imageUrl;

        [Display(AutoGenerateField = false)]
        public string ImageUrl
        {
            get { return this.imageUrl; }
            set
            {
                this.imageUrl = value;
            }
        }

        string email;


        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email should not be empty")]
        public string Email
        {
            get { return this.email; }
            set
            {
                this.email = value;
            }
        }

        string password;


        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password should Minimum 8 characters.")]
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
            }
        }

         string confirmPassword;



        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ConfirmPassword should not be empty")]
        public string ConfirmPassword
        {
            get { return this.confirmPassword; }
            set
            {
                this.confirmPassword = value;
            }
        }










        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    }





    public partial class MyCVsModel
    {
       

        //[JsonProperty("UserMail")]
        //public string UserMail { get; set; }

        [JsonProperty("CurrentCompany")]
        public string CurrentCompany { get; set; }

        [JsonProperty("JobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("Industry")]
        public string Industry { get; set; }

        [JsonProperty("Seniority")]
        public string Seniority { get; set; }

        [JsonProperty("Organization1")]
        public string Organization1 { get; set; }

        [JsonProperty("Organization2")]
        public string Organization2 { get; set; }

        [JsonProperty("Organization3")]
        public string Organization3 { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("Education")]
        public string Education { get; set; }

        [JsonProperty("Startups")]
        public string Startups { get; set; }

        [JsonProperty("MyExpertise")]
        public MyExpertise MyExpertise { get; set; }

        [JsonProperty("AboutYou")]
        public string AboutYou { get; set; }
    }

    public partial class MyExpertise
    {
        [JsonProperty("ExpertInfo")]
        public string ExpertInfo { get; set; }

        [JsonProperty("ExpertType")]
        public string ExpertType { get; set; }
    }



    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string EmployeeAge { get; set; }
    }

    #region Global Comman Clases
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var postScCreateProfileRequest = PostScCreateProfileRequest.FromJson(jsonString);

    public static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public partial class SimpleResponse
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static SimpleResponse FromJson(string json) => JsonConvert.DeserializeObject<SimpleResponse>(json, Converter.Settings);

        public static string ToJson(SimpleResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
    #endregion

    /// <summary>
    /// Model used in PostGetAuthorizationTokenResponse network call
    /// </summary>
    #region  Post Get Authorization Token Response

    public partial class PostTokenErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
    public partial class PostTokenResponse
    {
        [JsonProperty("access_token")]
        public string Access_Token { get; set; }
        [JsonProperty("token_type")]
        public string Token_Type { get; set; }
        [JsonProperty("expires_in")]
        public string Expires_In { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public string Issued { get; set; }
        [JsonProperty(".expires")]
        public string Expires { get; set; }

        [JsonProperty("ProfileInfo")]
        public string profileInfo { get; set; }


        //public partial class ProfileInfo
        //{
        //    [JsonProperty("id")]
        //    public object Id { get; set; }

        //    [JsonProperty("UserID")]
        //    public object UserId { get; set; }

        //    [JsonProperty("Email")]
        //    public string Email { get; set; }

        //    [JsonProperty("DisplayName")]
        //    public string DisplayName { get; set; }

        //    [JsonProperty("FullName")]
        //    public string FullName { get; set; }

        //    [JsonProperty("Birthday")]
        //    public string Birthday { get; set; }

        //    [JsonProperty("HideMyAge")]
        //    public bool HideMyAge { get; set; }

        //    [JsonProperty("ImageUrl")]
        //    public object ImageUrl { get; set; }
        //}


        public PostTokenErrorResponse ErrorResponse { get; set; }
        /*
         * // Response .
         {
         "access_token":"euQWliwIF16QRJJNJe6jZtigPKzZVGCtZ0I91n67j5jnVD4ivQdO1gifafPDFVQTRLrF-vyBishRC8rIcBv8qtXYVWxL1qFXcFw534TqAEYvKc3SGzwaHXGU_IPvuhk7zRpXz-YECegRTAYbIKDZUsltAktiF5xjuL4Eh55g7ypBTwsk35lg2BbEysM4G4OmZZOO3rdE3rymue29FvF1fTRrZOHep-leI1U_BesyXM2pSmOAK2mbURcpvg4DXqmRX2188mPHe5vUXpFWOhSlh_Z0HaPhKAJiIE0jgdo0245GXmHLU_81RClYImIyVY2DUn1cLabFvW2fLdM4R9eM_CLICE4sFZrIk7-fbs4Ue0vLtS9iHQw38dhOAFkFJ39eKiqSitibFcktHwOpDlbe-z2C_fxB-GjYgcLyiHVOh-0qUr1OA6chdK3PCnl6Gt5NZGZO8VygCfqDLloWApm3hxldVYYsYIaH1-5Buhy3dkdGHm70-XCifr9jLqosWKZ5tdBkpcBJmBj04iQYlTEXGfyxS2JSBRRK4ezr7DLO49A",
         "token_type":"bearer",
         "expires_in":3599,
         "userName":"technicianL05@Besam.com",
         ".issued":"Wed, 20 Jun 2018 06:44:00 GMT",
         ".expires":"Wed, 20 Jun 2018 07:44:00 GMT"}
         */

        public PostTokenResponse()
        {
            ErrorResponse = new PostTokenErrorResponse();
        }
    }

    public partial class PostRequestBody
    {
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("grant_type")]
        public string Grant_Type { get { return "password"; } }


        //DeviceUniqueID


        public string DeviceUniqueID
        {
            get
            {
                string TOKEN = "";
                try
                {
                    var FirebasePNTOKEN = BlobCache.LocalMachine.GetObject<string>("FirebasePNTOKEN");
                    TOKEN = FirebasePNTOKEN.SingleOrDefaultAsync().Wait();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                return TOKEN;
            }
        }
        public string DevicePlatform { get { return "Android"; } }

        public List<KeyValuePair<string, string>> GetFormUrlEncodedContent()
        {



            return new List<KeyValuePair<string, string>>()
                    {
                            new KeyValuePair<string, string>("grant_type",Grant_Type),
                            new KeyValuePair<string, string>("username", UserName),
                            new KeyValuePair<string, string>("password", Password)
                    };
        }
    }

    #endregion


    #region Post RegisterEmail 

    public partial class RegisterEmailRequest
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("RegisterType")]
        public string RegisterType { get; set; }

        [JsonProperty("UserType")]
        public string UserType { get; set; }

        [JsonProperty("DeviceUID")]
        public string DeviceUid { get; set; }

        [JsonProperty("DevicePlatform")]
        public string DevicePlatform
        {
            get
            {
                string platform = "";
                //switch (Device.RuntimePlatform)
                //{
                //    case Device.iOS:
                //        platform = "iOS";
                //        break;
                //    case Device.Android:
                //        platform = "Android";
                //        break;
                //    case Device.UWP:
                //        platform = "UWP";
                //        break;
                //}

                return platform;


            }
        }

        [JsonProperty("DeviceIsOnline")]
        public bool DeviceIsOnline { get; set; }

        public static RegisterEmailRequest FromJson(string json) => JsonConvert.DeserializeObject<RegisterEmailRequest>(json, Converter.Settings);

        public static string ToJson(RegisterEmailRequest registerEmailRequest) => JsonConvert.SerializeObject(registerEmailRequest, Converter.Settings);

    }




    #endregion

    #region Error Response
    //public partial class ErrorResponse
    //{
    //    [JsonProperty("Message")]
    //    public string Message { get; set; }

    //    [JsonProperty("ModelState")]
    //    public ModelState ModelState { get; set; }
    //}

    //public partial class ModelState
    //{
    //    [JsonProperty("model.ConfirmPassword")]
    //    public string[] ModelConfirmPassword { get; set; }
    //}

    //public partial class ErrorResponse
    //{
    //    public static ErrorResponse FromJson(string json) => JsonConvert.DeserializeObject<ErrorResponse>(json, Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this ErrorResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    //}
    #endregion

    #region Success Response
    public partial class ModelState
    {
        [JsonProperty("Errors")]
        public string[] Errors { get; set; }

        [JsonProperty("model.ConfirmPassword")]
        public string[] ModelConfirmPassword { get; set; }
    }

    public partial class StatusResponse
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public ModelState ModelState { get; set; }


        public static StatusResponse FromJson(string json) => JsonConvert.DeserializeObject<StatusResponse>(json, Converter.Settings);
        public static string ToJson(StatusResponse statusResponse) => JsonConvert.SerializeObject(statusResponse, Converter.Settings);

    }


    #endregion


    public partial class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }


    #region Post Change Password
    public partial class PostChangePasswordResponse
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

    }

    public partial class PostChangePasswordRequest
    {
        [JsonProperty("OldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty("NewPassword")]
        public string NewPassword { get; set; }

        [JsonProperty("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public PostChangePasswordRequest(string oldpassword, string newpassword, string confirmpassword)
        {
            OldPassword = oldpassword;
            NewPassword = newpassword;
            ConfirmPassword = confirmpassword;
        }

        public List<KeyValuePair<string, string>> GetFormUrlEncodedContent()
        {
            return new List<KeyValuePair<string, string>>()
                    {
                            new KeyValuePair<string, string>("oldpassword",OldPassword.ToString()),
                            new KeyValuePair<string, string>("newpassword", NewPassword.ToString()),
                            new KeyValuePair<string, string>("confirmpassword", ConfirmPassword),



                    };
        }
    }
    #endregion








    #region Post SC CreateProfile
    public partial class PostScCreateProfileRequest
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        public static PostScCreateProfileRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScCreateProfileRequest>(json, Converter.Settings);
        public static string ToJson(PostScCreateProfileRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public partial class PostScCreateProfileResponse : SimpleResponse
    {
    }

    #endregion

    #region Post Sc CreateCompanyProfile
    public partial class PostScCreateCompanyProfileRequest
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        public static PostScCreateCompanyProfileRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScCreateCompanyProfileRequest>(json, Converter.Settings);
        public static string ToJson(PostScCreateCompanyProfileRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public partial class PostScCreateCompanyProfileResponse : SimpleResponse
    {
    }
    #endregion

    #region Post SC PostScCarDetails
    public partial class PostScCarDetailsRequest
    {
        [JsonProperty("CarMake")]
        public string CarMake { get; set; }

        [JsonProperty("CarModel")]
        public string CarModel { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Mileage")]
        public string Mileage { get; set; }

        [JsonProperty("CarRegistrationNumber")]
        public string CarRegistrationNumber { get; set; }

        public static PostScCarDetailsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScCarDetailsRequest>(json, Converter.Settings);
        public static string ToJson(PostScCarDetailsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScCarDetailsResponse : SimpleResponse
    {
    }

    #endregion

    #region Post Sc CreditCardDetails
    public partial class PostScCreditCardDetailsRequest
    {
        [JsonProperty("CardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("Expires")]
        public string Expires { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CVC")]
        public string Cvc { get; set; }

        public static PostScCreditCardDetailsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScCreditCardDetailsRequest>(json, Converter.Settings);
        public static string ToJson(PostScCreditCardDetailsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScCreditCardDetailsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp CreateProfile
    public partial class PostSpCreateProfileRequest
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        public static PostSpCreateProfileRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpCreateProfileRequest>(json, Converter.Settings);
        public static string ToJson(PostSpCreateProfileRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpCreateProfileResponse : SimpleResponse
    {

      //  public ServiceProvider ResponseServiceProvider { get; set; }
          
    }
    #endregion

    #region Post Sp Create CompanyProfile
    public partial class PostSpCreateCompanyProfileRequest
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        public static PostSpCreateCompanyProfileRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpCreateCompanyProfileRequest>(json, Converter.Settings);
        public static string ToJson(PostSpCreateCompanyProfileRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpCreateCompanyProfileResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp CarDetails
    public partial class PostSpCarDetailsRequest
    {
        [JsonProperty("CarMake")]
        public string CarMake { get; set; }

        [JsonProperty("CarModel")]
        public string CarModel { get; set; }

        [JsonProperty("Year")]
        public string Year { get; set; }

        [JsonProperty("Mileage")]
        public string Mileage { get; set; }

        [JsonProperty("CarRegistrationNumber")]
        public string CarRegistrationNumber { get; set; }

        public static PostSpCarDetailsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpCarDetailsRequest>(json, Converter.Settings);
        public static string ToJson(PostSpCarDetailsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpCarDetailsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp EmployeeDetails
    public partial class PostSpEmployeeDetailsRequest
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("UniquieIdentification")]
        public string UniquieIdentification { get; set; }

        public static PostSpEmployeeDetailsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpEmployeeDetailsRequest>(json, Converter.Settings);
        public static string ToJson(PostSpEmployeeDetailsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpEmployeeDetailsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp AddExpertise
    public partial class PostSpAddExpertiseRequest
    {
        [JsonProperty("ExpertiseInfo")]
        public string ExpertiseInfo { get; set; }

        [JsonProperty("ExpertiseType")]
        public string ExpertiseType { get; set; }

        public static PostSpAddExpertiseRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpAddExpertiseRequest>(json, Converter.Settings);
        public static string ToJson(PostSpAddExpertiseRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpAddExpertiseResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp CreditCardDetails
    public partial class PostSpCreditCardDetailsRequest
    {
        [JsonProperty("CardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("Expires")]
        public string Expires { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CVC")]
        public string Cvc { get; set; }

        public static PostSpCreditCardDetailsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpCreditCardDetailsRequest>(json, Converter.Settings);
        public static string ToJson(PostSpCreditCardDetailsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpCreditCardDetailsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp CreditCardDetails
    public partial class PostScViewProfileRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostScViewProfileRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScViewProfileRequest>(json, Converter.Settings);
        public static string ToJson(PostScViewProfileRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScViewProfileResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sc Call
    public partial class PostScCallRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostScCallRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScCallRequest>(json, Converter.Settings);
        public static string ToJson(PostScCallRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScCallResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sc ViewCars
    public partial class PostScViewCarsRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostScViewCarsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScViewCarsRequest>(json, Converter.Settings);
        public static string ToJson(PostScViewCarsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScViewCarsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sc Location
    public partial class PostScLocationRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostScLocationRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScLocationRequest>(json, Converter.Settings);
        public static string ToJson(PostScLocationRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScLocationResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sc NewSMS

    public partial class PostScNewSmsRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostScNewSmsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostScNewSmsRequest>(json, Converter.Settings);
        public static string ToJson(PostScNewSmsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostScNewSmsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Search Sp Services
    public partial class PostSearchSPServicesRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSearchSPServicesRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSearchSPServicesRequest>(json, Converter.Settings);
        public static string ToJson(PostSearchSPServicesRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSearchSPServicesResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp MM MapViewInfo
    public partial class PostSpMMMapViewInfoRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpMMMapViewInfoRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpMMMapViewInfoRequest>(json, Converter.Settings);
        public static string ToJson(PostSpMMMapViewInfoRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpMMMapViewInfoResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp TT MapViewInfo
    public partial class PostSpTTMapViewInfoRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpTTMapViewInfoRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpTTMapViewInfoRequest>(json, Converter.Settings);
        public static string ToJson(PostSpTTMapViewInfoRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpTTMapViewInfoResponse : SimpleResponse
    {
    }
    #endregion


    #region Sp Idont Know Issue RequiredAssistance
    public partial class PostSpIdontKnowIssueRequiredAssistanceRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpIdontKnowIssueRequiredAssistanceRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpIdontKnowIssueRequiredAssistanceRequest>(json, Converter.Settings);
        public static string ToJson(PostSpIdontKnowIssueRequiredAssistanceRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostSpIdontKnowIssueRequiredAssistanceResponse : SimpleResponse
    {
    }
    #endregion

    #region PostInviteFriendEmail
    public partial class PostInviteFriendEmailRequest
    {
        [JsonProperty("EmailAddress")]
        public string[] EmailAddress { get; set; }

        public static PostInviteFriendEmailRequest FromJson(string json) => JsonConvert.DeserializeObject<PostInviteFriendEmailRequest>(json, Converter.Settings);
        public static string ToJson(PostInviteFriendEmailRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostInviteFriendEmailResponse : SimpleResponse
    {
    }
    #endregion


    #region PostInviteFriendEmail
    public partial class PostInviteFriendSMSRequest
    {
        [JsonProperty("EmailAddress")]
        public string[] EmailAddress { get; set; }

        public static PostInviteFriendSMSRequest FromJson(string json) => JsonConvert.DeserializeObject<PostInviteFriendSMSRequest>(json, Converter.Settings);
        public static string ToJson(PostInviteFriendSMSRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class PostInviteFriendSMSResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Service ProposalsList

    public partial class PostSpServiceProposalsListRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpServiceProposalsListRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpServiceProposalsListRequest>(json, Converter.Settings);
        public static string ToJson(PostSpServiceProposalsListRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpServiceProposalsListResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Service Proposal Info

    public partial class PostSpServiceProposalInfoRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpServiceProposalInfoRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpServiceProposalInfoRequest>(json, Converter.Settings);
        public static string ToJson(PostSpServiceProposalInfoRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpServiceProposalInfoResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Service Proposal Accept

    public partial class PostSpServiceProposalAcceptRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpServiceProposalAcceptRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpServiceProposalAcceptRequest>(json, Converter.Settings);
        public static string ToJson(PostSpServiceProposalAcceptRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpServiceProposalAcceptResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Service Proposal Reject

    public partial class PostSpServiceProposalRejectRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpServiceProposalRejectRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpServiceProposalRejectRequest>(json, Converter.Settings);
        public static string ToJson(PostSpServiceProposalRejectRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpServiceProposalRejectResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp GetDocs

    public partial class PostSpGetDocsRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpGetDocsRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpGetDocsRequest>(json, Converter.Settings);
        public static string ToJson(PostSpGetDocsRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpGetDocsResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp GetLocation

    public partial class PostSpGetLocationRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpGetLocationRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpGetLocationRequest>(json, Converter.Settings);
        public static string ToJson(PostSpGetLocationRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpGetLocationResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp GetExpertise

    public partial class PostSpGetExpertiseRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpGetExpertiseRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpGetExpertiseRequest>(json, Converter.Settings);
        public static string ToJson(PostSpGetExpertiseRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpGetExpertiseResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp GetEmployees

    public partial class PostSpGetEmployeesRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpGetEmployeesRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpGetEmployeesRequest>(json, Converter.Settings);
        public static string ToJson(PostSpGetEmployeesRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpGetEmployeesResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp GetServices

    public partial class PostSpGetServicesRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpGetServicesRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpGetServicesRequest>(json, Converter.Settings);
        public static string ToJson(PostSpGetServicesRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpGetServicesResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp HireMe

    public partial class PostSpHireMeRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpHireMeRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpHireMeRequest>(json, Converter.Settings);
        public static string ToJson(PostSpHireMeRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpHireMeResponse : SimpleResponse
    {
    }
    #endregion

    #region Post S Schedule On DateService

    public partial class PostSpScheduleOnDateServiceRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpScheduleOnDateServiceRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpScheduleOnDateServiceRequest>(json, Converter.Settings);
        public static string ToJson(PostSpScheduleOnDateServiceRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpScheduleOnDateServiceResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Schedule Imediate Service

    public partial class PostSpScheduleImediateServiceRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpScheduleImediateServiceRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpScheduleImediateServiceRequest>(json, Converter.Settings);
        public static string ToJson(PostSpScheduleImediateServiceRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpScheduleImediateServiceResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Schedule SetLocation

    public partial class PostSpScheduleSetLocationRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpScheduleSetLocationRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpScheduleSetLocationRequest>(json, Converter.Settings);
        public static string ToJson(PostSpScheduleSetLocationRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpScheduleSetLocationResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp Start MapNavigation

    public partial class PostSpStartMapNavigationRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpStartMapNavigationRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpStartMapNavigationRequest>(json, Converter.Settings);
        public static string ToJson(PostSpStartMapNavigationRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpStartMapNavigationResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp InProgress Cancel

    public partial class PostSpInProgressCancelRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpInProgressCancelRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpInProgressCancelRequest>(json, Converter.Settings);
        public static string ToJson(PostSpInProgressCancelRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpInProgressCancelResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp InProgress NewChat

    public partial class PostSpInProgressNewChatRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpInProgressNewChatRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpInProgressNewChatRequest>(json, Converter.Settings);
        public static string ToJson(PostSpInProgressNewChatRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpInProgressNewChatResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp InProgress ChatList

    public partial class PostSpInProgressChatListRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpInProgressChatListRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpInProgressChatListRequest>(json, Converter.Settings);
        public static string ToJson(PostSpInProgressChatListRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpInProgressChatListResponse : SimpleResponse
    {
    }
    #endregion

    #region Post Sp InProgress VoiceCall

    public partial class PostSpInProgressVoiceCallRequest
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        public static PostSpInProgressVoiceCallRequest FromJson(string json) => JsonConvert.DeserializeObject<PostSpInProgressVoiceCallRequest>(json, Converter.Settings);
        public static string ToJson(PostSpInProgressVoiceCallRequest self) => JsonConvert.SerializeObject(self, Converter.Settings);


    }

    public partial class PostSpInProgressVoiceCallResponse : SimpleResponse
    {
    }
    #endregion

    #region Get SC Notification ListResponse
    public partial class GetSCNotificationListResponse
    {
        [JsonProperty("ProfilePicURL")]
        public string ProfilePicUrl { get; set; }

        [JsonProperty("NotificationDateTime")]
        public string NotificationDateTime { get; set; }

        [JsonProperty("NotificationHeader")]
        public string NotificationHeader { get; set; }

        [JsonProperty("NotificationDetails")]
        public string NotificationDetails { get; set; }

        public static GetSCNotificationListResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetSCNotificationListResponse[]>(json, Converter.Settings);
        public static string ToJson(GetSCNotificationListResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetSpHomeHighlightsRequest
    {
    }
    #endregion

    #region Get SC Notification ListResponse
    public partial class GetSPNotificationListResponse
    {
        [JsonProperty("ProfilePicURL")]
        public string ProfilePicUrl { get; set; }

        [JsonProperty("NotificationDateTime")]
        public string NotificationDateTime { get; set; }

        [JsonProperty("NotificationHeader")]
        public string NotificationHeader { get; set; }

        [JsonProperty("NotificationDetails")]
        public string NotificationDetails { get; set; }

        public static GetSPNotificationListResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetSPNotificationListResponse[]>(json, Converter.Settings);
        public static string ToJson(GetSPNotificationListResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetSpHomeHighlightsRequest
    {
    }
    #endregion


    #region Get Sp Home Highlights
    public partial class GetSpHomeHighlightsResponse
    {
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("FailReason")]
        public string FailReason { get; set; }

        //[JsonProperty("News")]
        //public News[] News { get; set; }

        //[JsonProperty("Promotions")]
        //public Promotion[] Promotions { get; set; }

        public static GetSpHomeHighlightsResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetSpHomeHighlightsResponse[]>(json, Converter.Settings);
        public static string ToJson(GetSpHomeHighlightsResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetSpHomeHighlightsRequest
    {
    }
    #endregion


    #region Get Services Response
    public partial class GetServicesResponse
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Translation")]
        public string Translation { get; set; }

        public static GetServicesResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetServicesResponse[]>(json, Converter.Settings);
        public static string ToJson(GetServicesResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetServicesRequest
    {
    }
    #endregion

    #region Get Engine
    public partial class GetEngineResponse
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Translation")]
        public string Translation { get; set; }

        public static GetEngineResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetEngineResponse[]>(json, Converter.Settings);
        public static string ToJson(GetEngineResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetEngineRequest
    {
    }
    #endregion

    #region Get City
    public partial class GetCityResponse
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Translation")]
        public string Translation { get; set; }

        public static GetCityResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetCityResponse[]>(json, Converter.Settings);
        public static string ToJson(GetCityResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetCityRequest
    {
    }
    #endregion

    #region Get Company
    public partial class GetCompanyResponse
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Translation")]
        public string Translation { get; set; }

        public static GetCompanyResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetCompanyResponse[]>(json, Converter.Settings);
        public static string ToJson(GetCompanyResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetCompanyRequest
    {
    }
    #endregion

    #region Get Search SP Services
    public partial class GetSearchSPServicesResponse
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Translation")]
        public string Translation { get; set; }

        public static GetSearchSPServicesResponse[] FromJson(string json) => JsonConvert.DeserializeObject<GetSearchSPServicesResponse[]>(json, Converter.Settings);
        public static string ToJson(GetSearchSPServicesResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);

    }

    public partial class GetSearchSPServicesRequest
    {
    }
    #endregion


    public class PostSpAddFewMoreDetailsResponse :SimpleResponse
    {
      //  public ServiceProvider ResponseServiceProvider { get; set; }
    }
}