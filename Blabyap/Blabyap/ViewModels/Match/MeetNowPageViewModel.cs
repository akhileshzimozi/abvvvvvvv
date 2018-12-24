using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using System.Collections.ObjectModel;

namespace Blabyap.ViewModels
{
    public class MeetNowPageViewModel : ViewModelBase
    {

        private ObservableCollection<MeetnowModel> meetnowModelsList;
        public ObservableCollection<MeetnowModel> MeetnowModelsList
        {
            get { return meetnowModelsList; }
            set { SetProperty(ref meetnowModelsList, value); }
        }


 
        public MeetNowPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            Title = "MeetNow";

            MeetnowModelsList = new ObservableCollection<MeetnowModel>
            {
                new MeetnowModel(){ImageUrl="ChatPic1",Name="Meridth",
                    Description="Managing Director of Asia and Japan, Google Cloud at Google",Percentage="92%",Minutes="780m"},
                new MeetnowModel(){ImageUrl="ChatPic2", Name="John Leo",
                    Description="Director of the Genetics and Aging Research unit of Massachusetts General Hospital",
                    Percentage="92%",Minutes="780m"},
                new MeetnowModel(){ImageUrl="ChatPic3",Name="Alessandro",Description="Senior Director, Corporate Partnerships and Sales at ONE Championship"
                ,Percentage="92%",Minutes="780m"},
                new MeetnowModel(){ImageUrl="ChatPic4",Name="Natasha",Description="Artificial Intelligence and process Automation at McKinsey and Company"
                ,Percentage="92%",Minutes="780m"},
                new MeetnowModel(){ImageUrl="ChatPic2",Name="Sue Brention",Description="Head of Modern Trade & E-Commerce and MT Operation at Makarizo"
                ,Percentage="92%",Minutes="780m"},
                new MeetnowModel(){ImageUrl="ChatPic1",Name="Alisa Mercado",Description="Co-Founder, Director at MELLO"
                ,Percentage="92%",Minutes="780m"},
                new MeetnowModel(){ImageUrl="ChatPic3",Name="Donal Trump",Description="Where is your location"
                ,Percentage="92%",Minutes="780m"},
                  new MeetnowModel(){ImageUrl="ChatPic3",Name="Donal Trump",Description="Twitter afficionado"
                ,Percentage="92%",Minutes="780m"}


            };
        }
    }

    public class MeetnowModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Percentage { get; set; }
        public string Minutes { get; set; }
    }
}
