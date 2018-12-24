using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Blabyap.ViewModels.Profile;
using Blabyap.Services;
using System.Windows.Input;
using Blabyap.Common.Model;
using System.Diagnostics;
using System;
using Blabyap.Views;
using System.Collections.Generic;
using System.Collections;
using Blabyap.Controls;
using Prism.Commands;

namespace Blabyap.ViewModels
{
    //public class MyCVItemTemplateSelector : DataTemplateSelector
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

    //        var selectedItem = (MyCVItem)item;

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


    public class IndusModel
    {
        private string industry;
        public string Industry
        {
            get { return industry; }
            set { industry = value; }
        }
    }


   
    public class NationalityModel
    {
        private string nationality;
        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
    }


    public class ExpertiseModel
    {
        private string expertise;
        public string Expertise
        {
            get { return expertise; }
            set { expertise = value; }
        }
    }

    public class SeniorModel
    {
        private string seniority;
        public string Seniority
        {
            get { return seniority; }
            set { seniority = value; }
        }
    }
   
    public class MyCVPageViewModel : ViewModelBase
    {
       

        private string _currentCompany;
        public string currentCompany
        {
            get { return _currentCompany; }
            set { SetProperty(ref _currentCompany, value); }
        }

        private string _aboutYou;
        public string aboutYou
        {
            get { return _aboutYou; }
            set { SetProperty(ref _aboutYou, value); }
        }

        private string _education;
        public string education
        {
            get { return _education; }
            set { SetProperty(ref _education, value); }
        }
        private object _MyExpertise;
        public object  myExpertise
        {
            get { return _MyExpertise; }
            set { SetProperty(ref _MyExpertise, value); }
        }
        private string _organization2;
        public string organization2
        {
            get { return _organization2; }
            set { SetProperty(ref _organization2, value); }
        }
        private string _organization3;
        public string organization3
        {
            get { return _organization3; }
            set { SetProperty(ref _organization3, value); }
        }
        private string _nationality;
        public string nationality
        {
            get { return _nationality; }
            set { SetProperty(ref _nationality, value); }
        }
        private string _organization1;
        public string organization1
        {
            get { return _organization1; }
            set { SetProperty(ref _organization1, value); }
        }
        private string _seniority;
        public string seniority
        {
            get { return _seniority; }
            set { SetProperty(ref _seniority, value); }
        }
        private string _industry;
        public string industry
        {
            get { return _industry; }
            set { SetProperty(ref _industry, value); }
        }
        private string _jobTitle;
        public string jobTitle
        {
            get { return _jobTitle; }
            set { SetProperty(ref _jobTitle, value); }
        }

       
      


      

        ApiServices _apiServices { get; set; }
      
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

        private List<FamilyData> nationalityCollection;
        public List<FamilyData> NationalityCollection
        {
            get { return nationalityCollection; }
            set { SetProperty(ref nationalityCollection, value); }

        }


        private List<FamilyData> expertiseCollection;
        public List<FamilyData> ExpertiseCollection
        {
            get { return expertiseCollection; }
            set { SetProperty(ref expertiseCollection, value); }

        }


        private List<CustomCaption> _exp;
        public List<CustomCaption> exp
        {
            get { return _exp; }
            set { SetProperty(ref _exp, value); }

        }
      




       
        EditorLengthValidatorBehavior editorLengthValidatorBehavior;

        public DelegateCommand GotoDiscoveryCommand { get; set; }
        public MyCVPageViewModel(
                                INavigationService navigationService,
                                 IPageDialogService pageDialogService,
                             IDeviceService deviceService 
                                )
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = "My CV";
            GotoDiscoveryCommand = new DelegateCommand(GotoDiscoveryCommandAction).ObservesCanExecute(() => CanNavigate);
            editorLengthValidatorBehavior = new EditorLengthValidatorBehavior();

           


            _apiServices = new ApiServices();


            exp = new List<CustomCaption>();


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
        }

        private async void GotoDiscoveryCommandAction()
        {
            CanNavigate = false;
            IsBusy = true;

            try
            {

                var result = (IEnumerable)myExpertise;


                //  _e = myExpertise as List<FamilyData>;

                foreach (FamilyData ab in result)
                {
                    CustomCaption cc = new CustomCaption
                    {
                        Code = "Skill",
                        Translation = ab.Name,
                        ISSelected = true
                    };

                    exp.Add(cc);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = true;
            var idea = new CVData
            {
                CurrentCompany = currentCompany,
                JobTitle = jobTitle,
                Industry = industry,
                Seniority = seniority,
                Organization1 = organization1,
                Organization2 = organization2,
                Organization3 = organization3,
                Nationality = nationality,
                MyExpertise = exp,
                Education = education,
                AboutYou = aboutYou
            };
            await _apiServices.PostCVApiIDUrl(idea);
            await _navigationService.NavigateAsync("DiscoveryPage");

            IsBusy = false;

            CanNavigate = true;
        }

        private List<CustomCaption> _e;
        public List<CustomCaption> e
        {
            get { return _e; }
            set { SetProperty(ref _e, value); }
        }

        public  void DemoData(ResponseResultCV cVData, ResponseResultCVLinkedIn cVLinkedIn, ResponseResultLookUp natinalityData, ResponseResultLookUp industryData, ResponseResultLookUp expertise)
        {

           

            try
            {
               

                if (cVData != null )
                {
                    currentCompany = cVData.Data.CurrentCompany;
                    jobTitle = cVData.Data.JobTitle;
                    seniority = cVData.Data.Seniority;
                    industry = cVData.Data.Industry;
                    organization1 = cVData.Data.Organization1;
                    organization2 = cVData.Data.Organization2;
                    organization3 = cVData.Data.Organization3;
                    nationality = cVData.Data.Nationality;
                    education = cVData.Data.Education;
                    aboutYou = cVData.Data.AboutYou;
                    exp = cVData.Data.MyExpertise;
                    NationalityCollection = natinalityData.Data;
                    IndustryCollection = industryData.Data;

                    if (expertise != null)
                        ExpertiseCollection = expertise.Data;






                }

                else if (cVLinkedIn != null )

                {
                    currentCompany = cVLinkedIn.Data.userCV.CurrentCompany;
                    jobTitle = cVLinkedIn.Data.userCV.JobTitle;
                    nationality = cVLinkedIn.Data.userCV.Nationality;
                    aboutYou = cVLinkedIn.Data.userCV.AboutYou;
                    industry = cVLinkedIn.Data.userCV.Industry;
                    NationalityCollection = natinalityData.Data;
                    IndustryCollection = industryData.Data;
                    if (expertise != null)
                        ExpertiseCollection = expertise.Data;

                }

                else
                {
                    NationalityCollection = natinalityData.Data;
                    IndustryCollection = industryData.Data;
                    if (expertise != null)
                        ExpertiseCollection = expertise.Data;
                }
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }




        }
    }
}
