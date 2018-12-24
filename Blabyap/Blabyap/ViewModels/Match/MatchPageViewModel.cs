using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;

namespace Blabyap.ViewModels
{
    public class MatchPageViewModel : ViewModelBase
    {
        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand ChatCommand { get; set; }
        public DelegateCommand SwipingCommand { get; set; }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
            ChatCommand = new DelegateCommand(ChatCommandAction);
            SwipingCommand = new DelegateCommand(SwipingCommandAction);
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

        async void SwipingCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;

        }
    }
}
