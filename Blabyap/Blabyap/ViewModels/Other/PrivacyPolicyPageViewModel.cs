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
    public class PrivacyPolicyPageViewModel : ViewModelBase
    {
        public string Policy { get; set; }
        public DelegateCommand PreviousCommand { get; set; }
        public PrivacyPolicyPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            Policy = "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper." +
                "Lorem ipsum dolor sit amet, et eam illud mazim pertinax." +
                      "Ea tempor laoreet neglegentur duo. Vis ex fastidi suscipiantur," +
                      "eam et aeque ullamcorper. Ea tempor laoreet neglegentur duo.Vis ex " +
                      "fastidi suscipiantur eam et aeque ullamcorper.";
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=> CanNavigate);
        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;
        }
    }
}
