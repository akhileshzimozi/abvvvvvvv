using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Prism.Mvvm;
using Xamarin.Forms;
using Blabyap.ViewModels.Match;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels
{
    public class MatchProfileItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Type1Template { get; set; }
        public DataTemplate Type2Template { get; set; }
        public DataTemplate Type3Template { get; set; }
        public DataTemplate Type4Template { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //this is to return dummy template

            var retunTemplate = DefaultTemplate;

            var selectedItem = (MatchProfileItem)item;

            if (selectedItem.Type == "Type1")
            {
                retunTemplate = Type1Template;
            }

            else if (selectedItem.Type == "Type2")
            {
                retunTemplate = Type2Template;
            }

            else if (selectedItem.Type == "Type3")
            {
                retunTemplate = Type3Template;
            }

            else if (selectedItem.Type == "Type4")
            {
                retunTemplate = Type4Template;
            }


            return retunTemplate;

        }

    }
    public class MPPageViewModel : ViewModelBase
    {
        public ObservableCollection<MatchProfileItem> MatchProfileItemList { get; set; }
        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand ChatCommand { get; set; }
        public DelegateCommand CommentsCommand { get; set; }


        public MPPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {

            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
            ChatCommand = new DelegateCommand(ChatCommandAction).ObservesCanExecute(() => CanNavigate);
            CommentsCommand = new DelegateCommand(CommentsCommandAction).ObservesCanExecute(() => CanNavigate);
            MatchProfileItemList = new ObservableCollection<MatchProfileItem>();

            DemoData();
        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;

        }

        async void ChatCommandAction()
        {
            CanNavigate = false;

            await _navigationService.NavigateAsync("ChatPage");
            CanNavigate = true;

        }

        async void CommentsCommandAction()
        {
            CanNavigate = false;

            await _navigationService.NavigateAsync("CommentsPage");
            CanNavigate = true;

        }

        public void DemoData()
        {
            if (MatchProfileItemList.Count > 0)
            {
                MatchProfileItemList.Clear();
            }

            {
                var group1 = new MatchProfileItem
                {
                    Type = "Type1",
                    PersonalViewModelItem = new PersonalViewModel
                    {
                        Name = "Amy Peng",
                        Designation = "Senior Partner",
                        Distance = "2.1 KM.",
                        ProfileImage = "matchprofilepic"
                    }
                };

                var group2 = new MatchProfileItem
                {
                    Type = "Type2",
                    ExperienceViewModelItem = new ExperienceViewModel
                    {
                        Consulting = "Consulting",
                        Coaching = "Coaching",
                        AI = "AI",
                        Technology = "Technology",
                        IoT = "IoT",
                        Blockchain = "Blockchain",
                        Enterpreneur = "Enterpreneur",
                        Startups = "Startups"
                    }
                };
                var group3 = new MatchProfileItem
                {
                    Type = "Type3",
                    MatchAboutYouViewModelItem = new MatchAboutYouViewModel
                    {
                        Description = "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                            "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                            "eam et aeque ullamcorper." +
                            "Ea tempor laoreet neglegentur duo.Vis ex fastidi suscipiantur," +
                            "eam et aeque ullamcorper."
                    }
                };

                OtherPersonalInfoViewModel otherPersonalInfoViewModel = new OtherPersonalInfoViewModel
                {
                    Industry = "Management Consulting",
                    Organization = "Delioette PA Consulting",
                    Education = "NUS"
                };
                var group4 = new MatchProfileItem
                {
                    Type = "Type4",
                    OtherPersonalInfoViewModelItem = otherPersonalInfoViewModel
                };

                MatchProfileItemList.Add(group1);
                MatchProfileItemList.Add(group2);
                MatchProfileItemList.Add(group3);
                MatchProfileItemList.Add(group4);

            }
        }
    }
}
