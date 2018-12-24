using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Prism.Mvvm;
using Blabyap.Views;
using Blabyap.Services;

namespace Blabyap.ViewModels
{
    public class AppTour1PageViewModel : ViewModelBase
    {
        public DelegateCommand GetStartedCommand { get; set; }

        ApiServices api;
             

        public AppTour1PageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            GetStartedCommand = new DelegateCommand(GetStartedCommandAction).ObservesCanExecute(()=> CanNavigate);
            api = new ApiServices();

        }

        async void GetStartedCommandAction()
        {

            CanNavigate = false;
            var a = await api.GetComments();
            var b = await api.GetCVApiUrl();
            var c = await api.GetExpertise("4");
            var d = await api.GetIndustry();
            var e = await api.GetMatch();
            var f = await api.GetMatchProfile();
            var g = await api.GetNationality();
            var h = await api.GetProfile();
            var i = await api.GetSeniority();
            var j = await api.GetSwipeInfoApiUrl();
            var k = await api.GetUNMatch();
          //  var l = await api.PostChangePassword();
          //  var m = await api.PostChatApiIDUrl();
          //  var n = await api.PostChatApiIDUrl1();
         //   var o = await api.PostComments();
         //   var p = await api.PostContactSuggest();
            //var q = await api.PostCVApiIDUrl();
            //var r = await api.PostDelete();
            //var s = await api.PostDisCoveryApi();
            //var t = await api.PostForgotPassword();
            var u = await api.PostGetAuthorizationTokenResponse();
            //var w = await api.PostHideAge();
            //var x = await api.PostHideMe();
            //var y = await api.PostImage();
            //var z = await api.PostLinkedin();
            var aa = await api.PostLogOut();
            //var ab = await api.PostResetPassword();
            //var ac = await api.RegisterAsync();
            var ad = await api.UserInfo();


            CanNavigate = true;

            await _navigationService.NavigateAsync("BlabyMasterDetailPage/NavigationPage/MyCVPage");
        }
    }
}
