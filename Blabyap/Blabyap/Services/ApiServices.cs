using Blabyap.Common.Model;
using Blabyap.Common.Model.NetworkModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Blabyap.Services
{
    public  class ApiServices : IRestServices
    {
        public static string EmailCong  { get; set; }
        public static string PasswordCong { get; set; }
        public static string LoginUName { get; set; }
        public static string LoginPass { get; set; }

        public bool SignUpResult { get; set; }
        public bool LoginResult { get; set; }
        public bool LoginResultCOng { get; set; }
        public static BasicUserInfoModel obj { get; set; }

        /// <summary>
        /// Clear UserName and Password
        /// This function will called in all the places where ever we need to clear the username and Password.
        /// </summary>
        /// <returns></returns>
        public bool ClearUserNameNPassword() {

            bool rslt = false;

            try
            {

                Constants.UserName = string.Empty;
                Constants.Password = string.Empty;
                rslt = true;
            }
            catch (Exception ex)
            {

                // todo need to log error 
                // later we will link to app center cash,error,events track analytics 
            }

            return rslt;
        }





        


        /// <summary>
        /// Store Username and Password
        /// will be called in all the places where we need to store the password.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_password"></param>
        /// <returns></returns>
        public bool StoreUserNameNPassword(string _username, string _password) {

            bool rslt = false;

            try
            {

                Constants.UserName = _username;
                Constants.Password = _password;
                rslt = true;
            }
            catch (Exception ex)
            {

              // todo need to log error 
              // later we will link to app center cash,error,events track analytics 
            }

            return rslt;
        }




        /// <summary>
        /// RegisterAsync -> For registration 
        /// 
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="displayName1"></param>
        /// <param name="birthDay1"></param>
        /// <param name="image"></param>
        /// <param name="email1"></param>
        /// <param name="password1"></param>
        /// <param name="confirmPassword1"></param>
        /// <returns></returns>
        public async Task<bool> RegisterAsync(string fullName, string displayName1, DateTime birthDay1, string image, string email1, string password1, string confirmPassword1)
        {
            var client = new HttpClient();
            //string s = "2018/11/10 12:00:00 AM";
            //birthDay1 = DateTime.ParseExact(s, "yyyy/M/dd hh:mm:ss tt",
            //                                   CultureInfo.InvariantCulture);
            EmailCong = email1;
            PasswordCong = password1;

            var model = new RegisterInfoModel
            {
              FullName = fullName,
              DisplayName = displayName1,
              Birthday = birthDay1,
              ImageUrl = image,
              Email = email1,
              Password = password1,
              ConfirmPassword = confirmPassword1
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            try
            {
               var response = await client.PostAsync(Constants.Accounts.PostRegisterEmailUrl, content);


                SignUpResult =  response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return SignUpResult;

        }


        public async Task<bool> LoginAsync(string userName, string password)
        {
           

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, Constants.Accounts.PostTokenUrl);

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);



            var content = await response.Content.ReadAsStringAsync();



            Debug.WriteLine(content);

            return response.IsSuccessStatusCode;
        }



        /// <summary>
        /// For getting token
        /// </summary>
        /// <returns></returns>
        public async Task<PostTokenResponse> PostGetAuthorizationTokenResponse()
        {
            try
            {
                PostRequestBody postrequestBodystructure = new PostRequestBody
                {
                    UserName = Constants.UserName,
                    Password = Constants.Password
                };

                //  check is token exist
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                //  is empty (token not exist) do network operation and get the token
                if (string.IsNullOrEmpty(AuthorizationKey))
                {
                    List<KeyValuePair<string, string>> urlEncodedList = postrequestBodystructure.GetFormUrlEncodedContent();
                    using (var httpClient = new HttpClient())
                    {
                        using (var content = new FormUrlEncodedContent(urlEncodedList))
                        {
                            content.Headers.Clear();
                            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            HttpResponseMessage response = await httpClient.PostAsync(Constants.Accounts.PostTokenUrl, content);//.ConfigureAwait(false); ;
                            var responsecontent = await response.Content.ReadAsStringAsync();//.ConfigureAwait(false);;
                            if (response.IsSuccessStatusCode)
                            {
                                Constants.AuthorizationGetTokenResponse = JsonConvert.DeserializeObject<PostTokenResponse>(responsecontent);
                               obj = JsonConvert.DeserializeObject<BasicUserInfoModel>(Constants.AuthorizationGetTokenResponse.profileInfo);

                            }
                            else
                            {
                                PostTokenErrorResponse rslt = JsonConvert.DeserializeObject<PostTokenErrorResponse>(responsecontent);
                                Constants.AuthorizationGetTokenResponse = new PostTokenResponse
                                {
                                    ErrorResponse = rslt
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Constants.AuthorizationGetTokenResponse;
        }

        public async Task<RegisterInfo> GetProfile()
        {


            RegisterInfo JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.Accounts.PostUserInfoUrl;
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<RegisterInfo>(json);

                    }
                    else
                    {
                        //   throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<PostTokenResponse>PostCVApiIDUrl(CVData myCVsModel)
        {


            PostTokenResponse JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(myCVsModel);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostCVApiUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);



                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<PostTokenResponse>(json);

                    }
                    else
                    {
                        //   throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }






        public async Task<ResponseResultSwipeProfile> PostDisCoveryApi(PageDiscoveryInfo pageDiscovery)
        {
            ResponseResultSwipeProfile JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(pageDiscovery);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostDisCoveryApiUrl(), string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);



                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResultSwipeProfile>(json);

                    }
                    else
                    {
                        //   throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

    



        public async Task<List<CVDataSwipeProfile>> GetSwipeInfoApiUrl()
        {

            List<CVDataSwipeProfile> JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetSwipeInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<List<CVDataSwipeProfile>>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<ResponseResultCV> GetCVApiUrl()
        {

            ResponseResultCV JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetCVInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResultCV>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<BasicUserInfoModel> UserInfo()
        {

            BasicUserInfoModel JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetUserInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<BasicUserInfoModel>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<List<CVChat> >GetChat()
        {

            List<CVChat> JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetChatInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<List<CVChat>>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<List<CVComments>> GetComments()
        {

            List<CVComments> JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetCommentInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<List<CVComments>>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<PostTokenResponse> PostComments(CVComments comments)
        {


            PostTokenResponse JSONObj = null;

            try
            {




                var requestJson = JsonConvert.SerializeObject(comments);


                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                //var postrequestbodystructure = new UserInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    //string requesturl = Constants.CartsAPI.PostCartApiUrl;
                    var uri = new Uri(string.Format(Constants.CV.PostCVApiUrl, string.Empty));

                    //var content = new StringContent(requestJson);
                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = await httpClient.PostAsync(Constants.CartsAPI.PostCartApiUrl, content);

                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);

                    //HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<PostTokenResponse>(json);

                    }
                    else
                    {
                        //   throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }


        public async Task<CVMatch> GetMatch()
        {

            CVMatch JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                { 
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetMatchInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<CVMatch>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<ResponseResultLookUp> GetIndustry()
        {

            ResponseResultLookUp  JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetIndustryInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResultLookUp>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }
       

        public async Task<ResponseResultLookUp> GetExpertise(string code)
        {

            ResponseResultLookUp JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetExpertiseInfo(code);
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResultLookUp>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<List<FamilyData>> GetSeniority()
        {

            List<FamilyData> JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetSeniorityInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<List<FamilyData>>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<ResponseResultLookUp> GetNationality()
        {

            ResponseResultLookUp JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetNationalityinfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResultLookUp>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<PostTokenResponse> PostChatApiIDUrl(CVChat myChat)
        {


            PostTokenResponse JSONObj = null;

            try
            {



                var requestJson = JsonConvert.SerializeObject(myChat);


                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                //var postrequestbodystructure = new UserInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    //string requesturl = Constants.CartsAPI.PostCartApiUrl;
                    var uri = new Uri(string.Format(Constants.CV.PostChatApiUrl, string.Empty));

                    //var content = new StringContent(requestJson);
                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = await httpClient.PostAsync(Constants.CartsAPI.PostCartApiUrl, content);

                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);

                    //HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<PostTokenResponse>(json);

                    }
                    else
                    {
                        //   throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<List<CVMatchProfile>> GetMatchProfile()
        {

            List<CVMatchProfile> JSONObj = null;

            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetNationalityinfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<List<CVMatchProfile>>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }



        public async Task<CVUnmatch> GetUNMatch()
        {
            CVUnmatch JSONObj = null;
            try
            {

                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                var postrequestbodystructure = new RegisterInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    string requesturl = Constants.CV.GetUNMatchInfo();
                    HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<CVUnmatch>(json);

                    }
                    else
                    {
                        //throw new WebException("Error in processing your request.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }


        /// <summary>
        /// We need to add model here.
        /// </summary>
        /// <param name="myChat"></param>
        /// <returns></returns>
        public async Task<ResponseResult> PostChangePassword(ChangePassword changePassword)
        {


            ResponseResult JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(changePassword);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }

                //var postrequestbodystructure = new UserInfo();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    //string requesturl = Constants.CartsAPI.PostCartApiUrl;
                    var uri = new Uri(string.Format(Constants.CV.PostChaangePasswordUrl, string.Empty));

                    //var content = new StringContent(requestJson);
                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = await httpClient.PostAsync(Constants.CartsAPI.PostCartApiUrl, content);

                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);

                    //HttpResponseMessage response = await httpClient.GetAsync(requesturl);//.Result;


                    //if (response.IsSuccessStatusCode)
                    //{
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                    //}
                    //else
                    //{
                       
                    //}
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }


        public async Task<bool> PostLogOut()
        {

            bool JSONObj = false; ;

            try
            {
                var requestJson = JsonConvert.SerializeObject(null);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.Accounts.PostLogoutUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);

                    JSONObj = response.IsSuccessStatusCode;

                    /// todo 
                    /// check is the status code is success...
                    /// if success
                    /// clear the constants user name and password.
                    /// 
                    if(JSONObj == true)
                    {
                        ClearUserNameNPassword();
                        Constants.AuthorizationGetTokenResponse.Access_Token = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;

        }

        public async Task<ResponseResult> PostForgotPassword(ForgotPassword forgotPassword)
        {


            ResponseResult JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(forgotPassword);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostForgotPasswordUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);
                    string json = await response.Content.ReadAsStringAsync();
                    JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                  
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
         }
        public async Task<ResponseResultCVChat> PostChatApiIDUrl1(CVChat cvChat)
        {


            ResponseResultCVChat JSONObj = null;

            try
            {
               
                var requestJson = JsonConvert.SerializeObject(cvChat);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostChatApiIDUrl1, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);
                    string json = await response.Content.ReadAsStringAsync();
                    JSONObj = JsonConvert.DeserializeObject<ResponseResultCVChat>(json);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }
        public async Task<ResponseResult> PostResetPassword(ResetPassword resetPassword)
        {


            ResponseResult JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(resetPassword);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostResetPasswordUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);
                    string json = await response.Content.ReadAsStringAsync();
                    JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }



        public async Task<ResponseResultImage> PostImage(ImageUpload resetPassword)
        {


            ResponseResultImage JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(resetPassword);
                //string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                //if (string.IsNullOrEmpty(AuthorizationKey) == true)
                //{
                //    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                //}
                using (var httpClient = new HttpClient())
                {
                   // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostPhotoUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);
                    string json = await response.Content.ReadAsStringAsync();
                    JSONObj = JsonConvert.DeserializeObject<ResponseResultImage>(json);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }


        public async Task<ResponseResultCVLinkedIn> PostLinkedin(LinkedInAccess forgotPassword)
        {


            ResponseResultCVLinkedIn JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(forgotPassword);


                using (var httpClient = new HttpClient())
                {
                    
                       var uri = new Uri(string.Format(Constants.CV.PostLinkedinUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);
                    string json = await response.Content.ReadAsStringAsync();
                    JSONObj = JsonConvert.DeserializeObject<ResponseResultCVLinkedIn>(json);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<ResponseResult> PostDelete()
        {
            ResponseResult JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(null);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostDeleteUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);

                   
                    if (response.IsSuccessStatusCode)
                    {
                    string json = await response.Content.ReadAsStringAsync();//.Result;
                    JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<ResponseResult> PostHideAge(HideUserData hideUserData)
        {
            ResponseResult JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(hideUserData);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostHideAgeUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

        public async Task<ResponseResult> PostHideMe(HideUserData hideUserData)
        {
            ResponseResult JSONObj = null;

            try
            {
                var requestJson = JsonConvert.SerializeObject(hideUserData);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostHideMeUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }






        public async Task<ResponseResult> PostContactUs(ContactSuggest contactSuggest)
        {
            ResponseResult JSONObj = null;
            try
            {
                var requestJson = JsonConvert.SerializeObject(contactSuggest);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostContactUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }


        public async Task<ResponseResult> PostSuggestUs(ContactSuggest contactSuggest)
        {
            ResponseResult JSONObj = null;
            try
            {
                var requestJson = JsonConvert.SerializeObject(contactSuggest);
                string AuthorizationKey = Constants.AuthorizationGetTokenResponse?.Access_Token;
                if (string.IsNullOrEmpty(AuthorizationKey) == true)
                {
                    Constants.AuthorizationGetTokenResponse = await PostGetAuthorizationTokenResponse();
                }


                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Constants.AuthorizationGetTokenResponse.Access_Token);
                    var uri = new Uri(string.Format(Constants.CV.PostSuggestUrl, string.Empty));

                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = null;
                    response = await httpClient.PostAsync(uri, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();//.Result;
                        JSONObj = JsonConvert.DeserializeObject<ResponseResult>(json);

                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return JSONObj;
        }

    }
}
