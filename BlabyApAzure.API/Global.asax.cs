using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Blabyap.Common.Model;
using BlabyApAzure.API.Models;

namespace BlabyApAzure.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DocumentDBRepository<BasicUserInfoModel>.Initialize();
          //  DocumentDBRepository<CVData>.Initialize();
            DocumentDBRepository<LinkedInProfile>.Initialize();
            DocumentDBRepository<LookupInfo>.Initialize();
			DocumentDBRepository<DiscoverInfo>.Initialize();
            DocumentDBRepository<ApplicationUser>.Initialize(); 
            DocumentDBRepository<CVMatch>.Initialize();
            DocumentDBRepository<CVChat>.Initialize();
            DocumentDBRepository<CVComments>.Initialize();
            DocumentDBRepository<SwipeInfo>.Initialize();
            DocumentDBRepository<CVMatchCounter>.Initialize();
            DocumentDBRepository<CVUnMatchCounter>.Initialize();
            DocumentDBRepository<CVCommentsCounter>.Initialize();
            DocumentDBRepository<ContactSuggest>.Initialize();
        }
    }
}
