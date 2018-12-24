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
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Blabyap.Views.Match;
using Blabyap.ViewModels.Match;
using Blabyap.Common.Model;
using Blabyap.Services;
using System.Collections;

namespace Blabyap.ViewModels
{
	//public class SwipeItemTemplateSelector : DataTemplateSelector
	//{
	//	public DataTemplate Type1Template { get; set; }
	//	public DataTemplate Type2Template { get; set; }
	//	public DataTemplate Type3Template { get; set; }
	//	public DataTemplate Type4Template { get; set; }
	//	public DataTemplate DefaultTemplate { get; set; }

	//	protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
	//	{
	//		//this is to return dummy template

	//		var retunTemplate = DefaultTemplate;

	//		var selectedItem = (MatchProfileItem)item;

	//		if (selectedItem.Type == "Type1")
	//		{
	//			retunTemplate = Type1Template;
	//		}

	//		else if (selectedItem.Type == "Type2")
	//		{
	//			retunTemplate = Type2Template;
	//		}

	//		else if (selectedItem.Type == "Type3")
	//		{
	//			retunTemplate = Type3Template;
	//		}

	//		else if (selectedItem.Type == "Type4")
	//		{
	//			retunTemplate = Type4Template;
	//		}

	//		return retunTemplate;
	//	}
	//}

	public class SwipePageViewModel : ViewModelBase 
	{
		private ObservableCollection<MatchProfileItem> swipePageItemList;
		public ObservableCollection<MatchProfileItem> SwipePageItemList
		{
			get { return swipePageItemList; }
			set { SetProperty(ref swipePageItemList, value); }
		}


		private List<string> _seniority;
		public List<string> Seniority
		{
			get { return _seniority; }
			set { SetProperty(ref _seniority, value); }
		}

		private List<string> _expertise;
		public List<string> Expertise
		{
			get { return _expertise; }
			set { SetProperty(ref _expertise, value); }
		}


		private string _industry;
		public string Industry
		{
			get { return _industry; }
			set { SetProperty(ref _industry, value); }
		}


		ApiServices _apiServices;
		public DelegateCommand PreviousCommand { get; set; }
		public DelegateCommand ChatCommand { get; set; }
		public DelegateCommand SwipeLeftCommand { get; set; }
		public DelegateCommand SuperMatchCommand { get; set; }
		public DelegateCommand SwipeRightCommand { get; set; }

		public SwipePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
		{
			Title = "Swipe";
			PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
			ChatCommand = new DelegateCommand(ChatCommandAction).ObservesCanExecute(() => CanNavigate);
			SwipeLeftCommand = new DelegateCommand(SwipeLeftCommandAction).ObservesCanExecute(() => CanNavigate);
			SuperMatchCommand = new DelegateCommand(SuperMatchCommandAction).ObservesCanExecute(() => CanNavigate);
			SwipeRightCommand = new DelegateCommand(SwipeRightCommandAction).ObservesCanExecute(() => CanNavigate);
			_apiServices = new ApiServices();

			

		}
	   


		//public  override  void OnNavigatedTo(NavigationParameters parameters)
		//{

		//    //get a single parameter as type object, which must be cast
		//    //if(parameters.ContainsKey("industryDiscovery"))
		//    //Industry = (string)parameters["industryDiscovery"];

		//    //if (parameters.ContainsKey("seniorityDiscovery"))
		//    //    Seniority = (List<string>)parameters["seniorityDiscovery"];

		//    //if (parameters.ContainsKey("expertiseDiscovery"))
		//    //    Expertise = (List<string>)parameters["expertiseDiscovery"];


		//    Industry = parameters["i"] as string;
		//    Seniority =parameters["s"] as List<string>;

		//  //  Expertise = parameters["e"] as List<string>;
		//    Expertise = parameters.GetValues<string>("e").ToList();
		//   //  Seniority = parameters["s"] as List<string>;


		//}



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


		public int PageNumber { get; set; } = 1;
		ResponseResultSwipeProfile swipeProfileleft;
		async void SwipeLeftCommandAction()
		{
			CanNavigate = false;
			IsBusy = true;
			var post = new PageDiscoveryInfo
			{
				PageSize = 1,
				curPage = PageNumber,
				swipeDirection = "Left",
				matchCandidate = swipeProfile1.Data[0].userdetails.Email,
				


				discovery = new DiscoverInfo
				{
					Expertise = DiscoveryPageViewModel._exp,
					Industry = DiscoveryPageViewModel._industry,
					Seniority = DiscoveryPageViewModel._sen,
					distanceKM = null,
					
				}


			};


			 swipeProfileleft = await _apiServices.PostDisCoveryApi(post);
			DemoData(swipeProfileleft);

			PageNumber++;
			IsBusy = false;
			CanNavigate = true;

		}

		async void SuperMatchCommandAction()
		{
			CanNavigate = false;

			await _navigationService.NavigateAsync("MPPage");
			CanNavigate = true;

		}
		public ResponseResultSwipeProfile swipeProfileRight;
	   
		async void SwipeRightCommandAction()
		{
			CanNavigate = false;
			IsBusy = true;
			try
			{ 
			var postd = new PageDiscoveryInfo
			{
				PageSize = 1,
				curPage = PageNumber,
				swipeDirection = "Right",
				matchCandidate = swipeProfile1.Data[0].userdetails.Email,

				discovery = new DiscoverInfo
				{
					Expertise = DiscoveryPageViewModel._exp,
					Industry = DiscoveryPageViewModel._industry,
					Seniority = DiscoveryPageViewModel._sen,
					distanceKM = null,
					
				}


			};


		   swipeProfileRight = await _apiServices.PostDisCoveryApi(postd);
			DemoData(swipeProfileRight);

			PageNumber++;
			}catch(Exception ex)
			{

			}
			// await _navigationService.NavigateAsync("MeetNowPage");
			IsBusy = false;
			CanNavigate = true;

		}
		private  int CalculateAge(DateTime dateOfBirth)
		{
			int age = 0;
			age = DateTime.Now.Year - dateOfBirth.Year;
			if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
				age = age - 1;

			return age;
		}

		ResponseResultSwipeProfile swipeProfile1;

		private string _color;
		public string ColorBack
		{
			get { return _color; }
			set { SetProperty(ref _color, value); }
		}
		public async void DemoData(ResponseResultSwipeProfile swipeProfile)
		{
			swipeProfile1 = swipeProfile;
			if (swipeProfile != null)
			{
				ColorBack = swipeProfile.Data[0].cardColor;

			  SwipePageItemList = new ObservableCollection<MatchProfileItem>()
			   {

				new MatchProfileItem{
				   PersonalViewModelItem = new PersonalViewModel
					{
						Name = swipeProfile.Data[0].userdetails.DisplayName,
						Email =swipeProfile.Data[0].userdetails.Email,
					  
						Designation = swipeProfile.Data[0].userCV.JobTitle,
						Distance = "2.1 KM.",
						ProfileImage = "matchprofilepic",
	   
						Birthday = CalculateAge(dateOfBirth:swipeProfile.Data[0].userdetails.Birthday),
						



					},
					ExperienceViewModelItem = new ExperienceViewModel
					{
						ExpertiseCollection=DiscoveryPage.expertise.Data
					},
					MatchAboutYouViewModelItem = new MatchAboutYouViewModel
					{
						Description = swipeProfile.Data[0].userCV.AboutYou

					},
					OtherPersonalInfoViewModelItem = new OtherPersonalInfoViewModel
					   {
						Industry = swipeProfile.Data[0].userCV.Industry,
						Organization =swipeProfile.Data[0].userCV.CurrentCompany,
						Education = swipeProfile.Data[0].userCV.Education
					   }

					},
				};


			}
			
			else
			{
				await _pageDialogService.DisplayAlertAsync("Alert", "No Profile found", "OK");
				await _navigationService.GoBackAsync();

			}

		}






		public class SwipePageItem

		{
			public string Type { get; set; }
		}
	}
}
