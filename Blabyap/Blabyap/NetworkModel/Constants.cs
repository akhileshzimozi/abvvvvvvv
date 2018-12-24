namespace Blabyap.Common.Model.NetworkModel
{
    public static class Constants
    {
        public static string AppName = "BlabyAp";

        public static string SyncfusionVersion = "16.3.0.21";
        public static string
        SyncfusionLicenseKey = "Mjc5NTRAMzEzNjJlMzMyZTMwWHNRUTA5ZXBFalQ0bXhqNGx5KzVuZjFMWWR0SVVCTllNM25MM3lpZTBhWT0=";


        #region SyncFusionOldLicenses
        //"Mjc5NTRAMzEzNjJlMzMyZTMwWHNRUTA5ZXBFalQ0bXhqNGx5KzVuZjFMWWR0SVVCTllNM25MM3lpZTBhWT0=");

        // Mjc5NjlAMzEzNjJlMzMyZTMwWHNRUTA5ZXBFalQ0bXhqNGx5KzVuZjFMWWR0SVVCTllNM25MM3lpZTBhWT0=
        //"Mjc5NzRAMzEzNjJlMzMyZTMwWHNRUTA5ZXBFalQ0bXhqNGx5KzVuZjFMWWR0SVVCTllNM25MM3lpZTBhWT0=";

        // copied taken 28th october 2018
        public static string SyncfusionLicenseKey_Old = "Mjc5NzRAMzEzNjJlMzMyZTMwWHNRUTA5ZXBFalQ0bXhqNGx5KzVuZjFMWWR0SVVCTllNM25MM3lpZTBhWT0=";

        public static string SyncfusionVersion16_2 = "16.2.0.46";
        public static string SyncfusionLicenseKey16_2 = "MjAxOTBAMzEzNjJlMzIyZTMwYzlLV1h2cExIOERjSElDTG9aZnc4R3k1N0YyRXhHQzd6WDZWL3dGVE42OD0=";
        #endregion



        public static string ServerIPPublic1 = "zimozitodo.azurewebsites.net";
        public static string ServerIPPublic2 = "genex.ddns.net:60024";

        public static bool ConnectAzure = true;


        // BaseURL
        public static string Protocol = "https";
        public static string BaseUrl = string.Format("{0}://{1}", Protocol, ConnectAzure==true ?ServerIPPublic1:ServerIPPublic2);
        // public static string BaseUrl = string.Format("{0}://{1}/BlabyApAzureDB", Protocol, ServerIPPublic);



        public static PostTokenResponse AuthorizationGetTokenResponse { get; set; }

        public static bool IsOnline = true;
        public static bool IsPendingRegistration = true;


        // public static PostTokenErrorResponse ErrorResponse { get; set; }


        public static string UserName = "";



 





        public static string Password = "";

        //Account
        public static class Accounts
        {
            //this code is for url generation for future use
            public static string LinkedinUrl = "https://www.linkedin.com/oauth/v2/authorization";

            public static string PostLinkedinAzure = string.Format("{0}/signin-linkedin", BaseUrl);

            public static string AccessToken = "https://www.linkedin.com/oauth/v2/accessToken";
            public static string PostTokenUrl = string.Format("{0}/token", BaseUrl);



            public static string PostRegisterEmailUrl = string.Format("{0}/api/Account/Register", BaseUrl);
            public static string PostRegisterPhoneUrl = string.Format("{0}/api/Account/RegisterPhone", BaseUrl);

            public static string PostForgetPasswordUrl(string email) => string.Format("{0}/api/Account/ForgetPassword?email={1}", BaseUrl, email);
            public static string PostConfirmOTPEmailCodeUrl(string emailOtpCode) => string.Format("{0}/api/Account/ConfirmOTPEmailCode?emailOtpCode={1}", BaseUrl, emailOtpCode);
            public static string PostConfirmOTPPhoneCodeUrl(string phoneOtpCode) => string.Format("{0}/api/Account/ConfirmOTPPhoneCode?phoneOtpCode={1}", BaseUrl, phoneOtpCode);



            public static string PostUserInfoUrl = string.Format("{0}/api/Account/UserInfo", BaseUrl);
            public static string PostChangePasswordUrl = string.Format("{0}/api/Account/ChangePassword", BaseUrl);
            public static string PostSetPasswordUrl = string.Format("{0}/api/Account/SetPassword", BaseUrl);
            public static string PostLogoutUrl = string.Format("{0}/api/Account/Logout", BaseUrl);
            public static string PostForgotPasswordUrl = string.Format("{0}/api/Account/ForgotPassword", BaseUrl);
        }

 


        public static class CV
        {
            public static string PostCVApiUrl = string.Format("{0}/api/cv/Postcv", BaseUrl);
            public static string PostDisCoveryApiUrl() => string.Format("{0}/DiscoverData", BaseUrl);
            public static string GetSwipeInfo() => string.Format("{0}/swipeprofiles", BaseUrl);
            public static string GetCVInfo() => string.Format("{0}/api/cv/GetCV", BaseUrl);
            public static string GetUserInfo() => string.Format("{0}/api/Account/BasicUserInfo?username=a@g.com", BaseUrl);
            public static string GetCommentInfo() => string.Format("{0}/api/Comments/GetComments", BaseUrl);
            public static string PostChatApiUrl = string.Format("{0}/PostChat", BaseUrl);


            public static string GetChatInfo() => string.Format("{0}/api/Chat/ChatInfo?CandidateEMail", BaseUrl);

            public static string GetMatchInfo() => string.Format("{0}/api/Match/MatchCandidate", BaseUrl);
            public static string GetIndustryInfo() => string.Format("{0}/api/Utility/Industryinfo", BaseUrl);
            public static string GetExpertiseInfo(string code) => string.Format("{0}/api/Utility/ExpertiseInfo?industrycode={1}", BaseUrl,code);
            public static string GetSeniorityInfo() => string.Format("{0}/api/Utility/SeniorityInfo", BaseUrl);
            public static string GetNationalityinfo() => string.Format("{0}/api/Utility/nationalityinfo", BaseUrl);
            public static string GetMatchProfileInfo() => string.Format("{0}/api/Match/MatchInfo", BaseUrl);
            public static string GetUNMatchInfo() => string.Format("{0}/api/Match/UnMatchCandidate", BaseUrl);

            public static string PostChaangePasswordUrl = string.Format("{0}/api/Account/ChangePassword", BaseUrl);
            public static string PostDeleteUrl = string.Format("{0}/api/Account/Delete", BaseUrl);
            public static string PostHideAgeUrl = string.Format("{0}/api/Account/HideAge", BaseUrl);
            public static string PostHideMeUrl = string.Format("{0}/api/Account/HideMe", BaseUrl);
            public static string PostContactUrl = string.Format("{0}/api/utility/Contactus", BaseUrl);
            public static string PostSuggestUrl = string.Format("{0}/api/utility/SuggestUs", BaseUrl);

            public static string PostLogoutUrl = string.Format("{0}/api/Account/Logout", BaseUrl);

            public static string PostResetPasswordUrl = string.Format("{0}/api/Account/ResetPassword", BaseUrl);
            public static string PostForgotPasswordUrl = string.Format("{0}/api/Account/ForgotPassword", BaseUrl);

            public static string PostPhotoUrl = string.Format("{0}/PostImage", BaseUrl);
            public static string PostLinkedinUrl = string.Format("{0}/api/Linkedin/SetLinkedinProfile", BaseUrl);
            public static string PostChatApiIDUrl1 = string.Format("https://blabychathub.azurewebsites.net/");

        }

    }
}