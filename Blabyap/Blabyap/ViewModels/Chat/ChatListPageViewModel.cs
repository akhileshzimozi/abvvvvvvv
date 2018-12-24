using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Blabyap.ViewModels
{
    public class ChatListPageViewModel : ViewModelBase
    {
        ObservableCollection<ChatModel> chatModelList;
        public ObservableCollection<ChatModel> ChatModelList
        {
            get => chatModelList;
            set => SetProperty(ref chatModelList, value);
        }

        public ChatListPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) 
            : base(navigationService, pageDialogService, deviceService)
        {

            ChatModelList = new ObservableCollection<ChatModel>
            {
                new ChatModel(){ImageUrl="ChatPic1",Name="Meridth",
                    Description="How's your day going"},
                new ChatModel(){ImageUrl="ChatPic2", Name="Leo",Description="So when can we meet?",ImageIcon="Star"},
                new ChatModel(){ImageUrl="ChatPic3",Name="Alessandro",Description="Thanks for the coffee"},
                new ChatModel(){ImageUrl="ChatPic4",Name="Natasha",Description="Hi there"},
                new ChatModel(){ImageUrl="ChatPic2",Name="Jenie",Description="How are you doing?"},
                new ChatModel(){ImageUrl="ChatPic1",Name="Priya",Description="Meet you by 2mrw",ImageIcon="Star"},
                new ChatModel(){ImageUrl="ChatPic3",Name="Gene",Description="Where is your location"},

            };
        }
    }


    public class ChatModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageIcon { get; set; }
        public string Type { get; set; }
        public string CreatedDate { get; set; }
    }
    public class SellerBuyerTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SellerTemplate { get; set; }
        public DataTemplate BuyerTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //this is to return dummy template

            var retunTemplate = DefaultTemplate;

            var selectedItem = (ChatModel)item;

            if (selectedItem.Type == "Buyer")
            {
                retunTemplate = BuyerTemplate;
            }

            else if (selectedItem.Type == "Seller")
            {
                retunTemplate = SellerTemplate;
            }


            return retunTemplate;

        }
       

    }
}
