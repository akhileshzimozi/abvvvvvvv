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
    public class ChooseSkinPageViewModel : ViewModelBase
    {
        public ObservableCollection<SkinList> SkinListItem { get; set; }
        public DelegateCommand PreviousCommand { get; set; }
        public DelegateCommand UseSkinCommand { get; set; }
        public ChooseSkinPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {
            PreviousCommand = new DelegateCommand(PreviousCommandAction).ObservesCanExecute(()=>CanNavigate);
            UseSkinCommand = new DelegateCommand(UseSkinCommandAction).ObservesCanExecute(() => CanNavigate);
            SkinListItem = new ObservableCollection<SkinList>();
            DemoData();
        }

        public void DemoData()
        {
            if (SkinListItem.Count > 0)
            {
                SkinListItem.Clear();
            }

            SkinListItem = new ObservableCollection<SkinList>()
            {
                new SkinList()
                {
                    SkinType1="Group11",SkinType2="Group12"
                },
                new SkinList()
                {
                    SkinType1="Group21",SkinType2="Group22"
                },
                new SkinList()
                {
                    SkinType1="Group31",SkinType2="Group32"
                },
                new SkinList()
                {
                    SkinType1="Group11",SkinType2="Group12"
                },
            };
        }

        async void PreviousCommandAction()
        {
            CanNavigate = false;
            await _navigationService.GoBackAsync();
            CanNavigate = true;
        }

        private void UseSkinCommandAction()
        {
            CanNavigate = false;
            _pageDialogService.DisplayAlertAsync("Choose Skin Coming Soon", "Choose Skin Clicked", "Ok");
            CanNavigate = true;
        }
    }

    public class SkinList
    {
        public string SkinType1 { get; set; }
        public string SkinType2 { get; set; }
    }
}
