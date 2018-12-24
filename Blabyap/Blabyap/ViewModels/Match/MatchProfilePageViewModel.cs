using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using System.Collections.ObjectModel;
using Blabyap.ViewModels.Match;
using Xamarin.Forms;
using Blabyap.ViewModels.Profile;

namespace Blabyap.ViewModels
{
    // I have moved the code to the new mpmodel. you can later delete this class
    public class MatchProfilePageViewModel : ViewModelBase
    {
        public MatchProfilePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
        }
    }
}
