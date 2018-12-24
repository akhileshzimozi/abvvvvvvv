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
    public class UnMatchUserPopupPageViewModel : ViewModelBase
    {
        public UnMatchUserPopupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {

        }
    }
}
