using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using System.Collections.ObjectModel;
using Blabyap.Views;
using Xamarin.Forms;
using Blabyap.Services;
using System.Diagnostics;
using Blabyap.Common.Model;

namespace Blabyap.ViewModels
{
    public class BlabyMasterDetailPageViewModel : ViewModelBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public ObservableCollection<MyMenuItem> MenuItems { get; set; }

        MyMenuItem selectedMenuItem;
        public MyMenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        public DelegateCommand NavigateCommand { get;  set; }
        ApiServices apiServices;


        public BlabyMasterDetailPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {

            apiServices = new ApiServices();
            MenuItems = new ObservableCollection<MyMenuItem>
            {
                new MyMenuItem()
                {
                    Id =1,
                    Icon = "MycvIcon",
                    PageName = nameof(MyCVPage),
                    Title = "My CV"
                },

                new MyMenuItem()
                {
                      Id =2,
                    Icon = "discoveryIcon",
                    PageName = nameof(DiscoveryPage),
                    Title = "Discovery"
                },

                new MyMenuItem()
                {
                      Id =3,
                    Icon = "meetnowIcon",
                    PageName = nameof(MeetNowPage),
                    Title = "Meet Now"
                },

                new MyMenuItem()
                {
                      Id =4,
                    Icon = "passportIcon",
                    PageName = nameof(PassportPage),
                    Title = "Passport"
                },

                new MyMenuItem()
                {
                      Id =5,
                    Icon = "premiumIcon",
                    PageName = nameof(PremiumPage),
                    Title = "Premium & more"
                },

                new MyMenuItem()
                {
                      Id =6,
                    Icon = "settingsIcon",
                    PageName = nameof(SettingsPage),
                    Title = "Settings"
                },

                new MyMenuItem()
                {
                      Id =7,
                    Icon = "aboutusIcon",
                    PageName = nameof(CanvasPage),
                    Title = "About Us"
                }
                ,
                
                 new MyMenuItem()
                {
                      Id =8,
                    Icon = "aboutusIcon",
                   PageName = nameof(MyAccountPage),
                    Title = "My Profile"
                },
                new MyMenuItem()
                {
                      Id =9,
                    Icon = "aboutusIcon",
                   PageName = nameof(LoginPage),
                    Title = "Sign Out"
                }
            };

            NavigateCommand = new DelegateCommand(Navigate).ObservesCanExecute(()=> CanNavigate);
        }


      public  void DemoData(BasicUserInfoModel userInfoModel, ResponseResultCVLinkedIn cVLinkedIn)
        {
            try { 
                if(cVLinkedIn ==null)
            Name = userInfoModel.FullName;
                else
                    Name = cVLinkedIn.Data.userdetails.FullName;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
            


        async void Navigate()
        {

            CanNavigate = false;
            if (SelectedMenuItem.Id == 9)
            {
                //    await _navigationService.ClearPopupStackAsync(null, true);
                try {
                var result = await apiServices.PostLogOut();
                if (result == true)
                {
                    await _navigationService.NavigateAsync("/LoginPage");
                //  (App.Current.MainPage as MasterDetailPage).IsPresented = true;

                }
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
            else
            {
                await _navigationService.NavigateAsync(nameof(NavigationPage) + "/" + SelectedMenuItem.PageName);
                (App.Current.MainPage as MasterDetailPage).IsPresented = true;


            }
            CanNavigate = true;
        }
    }


    public class MyMenuItem
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }
    }
}
