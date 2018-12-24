using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using System.Collections.ObjectModel;
using Blabyap.Views.Match;
using Rg.Plugins.Popup.Services;
using Blabyap.Common.Model;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Blabyap.Services;
using System.Windows.Input;

namespace Blabyap.ViewModels
{

    public class ChatPageViewModel : ViewModelBase

    {
        //private IChatClientService _chatClientService;
        //private ChatMessageViewModel _cVChat;

        //private ObservableCollection<ChatMessageViewModel> _messages;

        //private ObservableCollection<ChatMessageViewModel> Messages
        //{
        //    get { return _messages; }
        //    set { _messages = value; OnPropertyChanged("Messages"); }
        //}
        //private string _chatMsg;

        //public string ChatMsg
        //{
        //    get { return _chatMsg; }
        //    set
        //    {
        //        _chatMsg = value;
        //        OnPropertyChanged("ChatMsg");

        //    }
        //}
        //private string _candidateEmail;
        //public string CandidateEmail
        //{
        //    get { return _candidateEmail; }
        //    set
        //    {
        //        _chatMsg = value;
        //        OnPropertyChanged("CandidateEmail");

        //    }
        //}
        //private bool _isMine;

        //public bool IsMine
        //{
        //    get { return _isMine; }
        //    set
        //    {
        //        _isMine = value;
        //        OnPropertyChanged("IsMine");
        //    }
        //}






        public ObservableCollection<ChatModel> ChatModelList { get; set; }


        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand UnMatchProfileCommand { get; set; }
        public DelegateCommand SendMsgCommand { get; set; }

        public ChatPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService ) : base(navigationService, pageDialogService, deviceService)
        {
            //_chatClientService = chatClientService;
            //_cVChat = new ChatMessageViewModel();
            //_messages = new ObservableCollection<ChatMessageViewModel>();
            //_chatClientService.Connect();
            //_chatClientService.OnMessageReceived += _chatClientService_OnMessageReceived;
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
            UnMatchProfileCommand = new DelegateCommand(UnMatchProfileCommandAction).ObservesCanExecute(() => CanNavigate);
            SendMsgCommand = new DelegateCommand(SendMsgCommandAction).ObservesCanExecute(() => CanNavigate);

            ChatModelList = new ObservableCollection<ChatModel>
                    {
                        new ChatModel(){Type= "Seller", CreatedDate="Read 16:08",ImageUrl="ChatPic1",Name="Meridth",
                            Description="How's your day going"},
                        new ChatModel(){Type= "Buyer", CreatedDate="Read 16:08",ImageUrl="ChatPic2", Name="Leo",Description="So when can we meet?",ImageIcon="Star"},
                        new ChatModel(){Type= "Seller", CreatedDate="Read 16:08",ImageUrl="ChatPic3",Name="Jenie",Description="Thanks for the coffee"},
                        new ChatModel(){Type= "Buyer", CreatedDate="Read 16:08",ImageUrl="ChatPic4",Name="Natasha",Description="Hi there"},
                        new ChatModel(){Type= "Seller", CreatedDate="Read 16:08",ImageUrl="ChatPic2",Name="Jenie",Description="How are you doing?"},
                        new ChatModel(){Type= "Buyer", CreatedDate="Read 16:08 am",ImageUrl="ChatPic1",Name="Priya",Description="Meet you by 2mrw",ImageIcon="Star"},
                        new ChatModel(){Type= "Seller", CreatedDate="Read 16:08",ImageUrl="ChatPic3",Name="Gene",Description="Where is your location"},

                    };
        }

        //void _chatClientService_OnMessageReceived(object sender, CVChat e)
        //{
        //    _messages.Add(new ChatMessageViewModel { ChatMsg = e.ChatMsg, CandidateEmail = e.CandidateEmail });
        //}


        //Command sendMessageCommand;

        ///// <summary>
        ///// Command to Send Message
        ///// </summary>
        //public Command SendMessageCommand
        //{
        //    get
        //    {
        //        return sendMessageCommand ??
        //        (sendMessageCommand = new Command(ExecuteSendMessageCommand));
        //    }
        //}

        //async void ExecuteSendMessageCommand()
        //{
        //    IsBusy = true;
        //    await _chatClientService.Send(new CVChat { ChatMsg = _cVChat.ChatMsg, CandidateEmail = _cVChat.CandidateEmail });
        //    IsBusy = false;
        //}






        private void SendMsgCommandAction()
        {
            CanNavigate = false;

            CanNavigate = true;

        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;

        }

        async void UnMatchProfileCommandAction()
        {
            CanNavigate = false;
            await PopupNavigation.Instance.PushAsync(new UnMatchUserPopupPage());
            CanNavigate = true;

        }
        //ApiServices _apiServices { get; set; }

        //public DateTime? CreatedDate { get; set; }

        //public ICommand SendMessageCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            var datapost = new CVChat
        //            {
        //                ChatMsg = "hii",
        //                Email = "Choudharyanchal803@gmail.com",
        //                CreatedDate = DateTime.Now,
        //            };
        //            if (true)
        //            {

        //                await _apiServices.PostChatApiIDUrl1(datapost);

        //            }
        //        });

    }
}