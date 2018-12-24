using Blabyap.Common.Model;
using Blabyap.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Blabyap.Services
{
   public  class LinkeDindata
    {
         ApiServices _apiServices;
        INavigationService _navigationServic;

        public LinkeDindata( INavigationService navigationServic)
        {
            _navigationServic = navigationServic;
            _apiServices = new ApiServices();
        }
        public static ResponseResultCVLinkedIn User;
        public async  void SetUser(string response)
        {

            var linkedIn = new LinkedInAccess
            {
                Token = response
            };
            User = await _apiServices.PostLinkedin(linkedIn);
            Application.Current.MainPage = new MyCVPage();

            //  await _navigationServic.NavigateAsync("/BlabyMasterDetailPage/NavigationPage/MyCVPage");
        }
    }
}
