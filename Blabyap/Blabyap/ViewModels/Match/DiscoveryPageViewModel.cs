using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Blabyap.ViewModels.Match;
using Blabyap.ViewModels.Profile;
using System.Windows.Input;
using Blabyap.Services;
using Blabyap.Views;
using System.Diagnostics;
using Blabyap.Common.Model;
using System.Collections;

namespace Blabyap.ViewModels
{
    //public class DiscoveryItemTemplateSelector : DataTemplateSelector
    //{
    //    public DataTemplate Type1Template { get; set; }
    //    public DataTemplate Type2Template { get; set; }
    //    public DataTemplate Type3Template { get; set; }
    //    public DataTemplate Type4Template { get; set; }
    //    public DataTemplate Type5Template { get; set; }
    //    public DataTemplate DefaultTemplate { get; set; }

    //    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    //    {
    //        //this is to return dummy template

    //        var retunTemplate = DefaultTemplate;

    //        var selectedItem = (DiscoveryItem)item;

    //        if (selectedItem.Type == "Type1")
    //        {
    //            retunTemplate = Type1Template;
    //        }

    //        else if (selectedItem.Type == "Type2")
    //        {
    //            retunTemplate = Type2Template;
    //        }

    //        else if (selectedItem.Type == "Type3")
    //        {
    //            retunTemplate = Type3Template;
    //        }

    //        else if (selectedItem.Type == "Type4")
    //        {
    //            retunTemplate = Type4Template;
    //        }

    //        else if (selectedItem.Type == "Type5")
    //        {
    //            retunTemplate = Type5Template;
    //        }

    //        return retunTemplate;

    //    }

    //}
    public class DiscoveryPageViewModel : ViewModelBase
    {
      

        private object _myExpertise;
        public object myExpertise
        {
            get { return _myExpertise; }
            set { SetProperty(ref _myExpertise, value); }
        }

        public static List<string> _exp;
        public List<string> exp
        {
            get { return _exp; }
            set { SetProperty(ref _exp, value); }
        }

        public static List<string> _sen;
        public List<string> sen
        {
            get { return _sen; }
            set { SetProperty(ref _sen, value); }
        }

        private object _siniority;
        public object siniority
        {
            get { return _siniority; }
            set { SetProperty(ref _siniority, value); }
        }
       
        public static string _industry;
        public string industry
        {
            get { return _industry; }
            set { SetProperty(ref _industry, value); }
        }

        private List<FamilyData> industryCollection;
        public List<FamilyData> IndustryCollection
        {
            get { return industryCollection; }
            set { SetProperty(ref industryCollection, value); }

        }
        private ObservableCollection<SeniorModel> seniorityCollection;
        public ObservableCollection<SeniorModel> SeniorityCollection
        {
            get { return seniorityCollection; }
            set { seniorityCollection = value; }
        }




        private List<FamilyData> expertiseCollection;
        public List<FamilyData> ExpertiseCollection
        {
            get { return expertiseCollection; }
            set { SetProperty(ref expertiseCollection, value); }

        }



        public DelegateCommand SwipingCommand { get; set; }

        public DiscoveryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {

            Title = "Discovery";

            sen = new List<string>();
            exp = new List<string>();

            /// Seniority comes from here
            seniorityCollection = new ObservableCollection<SeniorModel>();
            seniorityCollection.Add(new SeniorModel() { Seniority = "Student" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "MiddleLevel" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "Fresher" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "GM" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "AGM" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "Director" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "Manager" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "Lead" });
            seniorityCollection.Add(new SeniorModel() { Seniority = "HR Manager" });

            SwipingCommand = new DelegateCommand(SwipingCommandAction).ObservesCanExecute(() => CanNavigate);


        }

        private async void SwipingCommandAction()
        {
            CanNavigate = false;
            try
            {

                var result = (IEnumerable)myExpertise;
                var result1 = (IEnumerable)siniority;


                //  _e = myExpertise as List<FamilyData>;
                try {
                foreach (SeniorModel ab in result)
                {
                    var s = ab.Seniority;

                    sen.Add(s);

                }
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                foreach (FamilyData ab in result1)
                {
                    var e = ab.Name;

                    exp.Add(e);

                }
            }
            catch (Exception ex) { }
            IsBusy = true;
            var postdata = new PageDiscoveryInfo
            {
                PageSize = 1,
                curPage = 1,
                //swipeDirection = "",
                discovery = new DiscoverInfo
                {
                    Expertise = exp,
                    Industry = industry,
                    Seniority = sen,
                    distanceKM = null
                }


            };


            swipeProfile = await _apiServices.PostDisCoveryApi(postdata);

            //var navigationParams = new NavigationParameters();
            //navigationParams.Add("e",exp);
            //navigationParams.Add("s", sen);
            //navigationParams.Add("i", industry);


            await _navigationService.NavigateAsync("SwipePage");

            IsBusy = false;

            CanNavigate = true;
        }

        private ApiServices _apiServices = new ApiServices();
        public static ResponseResultSwipeProfile swipeProfile;

       
       

        public void DemoData()
        {
            try
            {
                IndustryCollection = DiscoveryPage.industry.Data;
                if (DiscoveryPage.expertise != null)
                    ExpertiseCollection = DiscoveryPage.expertise.Data;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }

}