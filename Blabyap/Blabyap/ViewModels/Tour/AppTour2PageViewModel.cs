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

namespace Blabyap.ViewModels
{
    public class AppTour2PageViewModel : ViewModelBase
    {
        public DelegateCommand GetStartedCommand { get; set; }


        public AppTour2PageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            GetStartedCommand = new DelegateCommand(GetStartedCommandAction).ObservesCanExecute(() => CanNavigate);
              

        }

        async void GetStartedCommandAction()
        {
            CanNavigate = false;
            await _navigationService.NavigateAsync("BlabyMasterDetailPage/NavigationPage/MyCVPage");
            CanNavigate = true;
        }
    }
}
