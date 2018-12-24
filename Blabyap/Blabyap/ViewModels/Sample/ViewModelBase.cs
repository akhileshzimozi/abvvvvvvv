using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;
using System.Diagnostics;

namespace Blabyap.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    //IActiveAware,
    //INavigationAware, IDestructible, 
    //IConfirmNavigation, 
    //IConfirmNavigationAsync, IApplicationLifecycleAware,
    //IPageLifecycleAware
    {
        protected IPageDialogService _pageDialogService { get; }

        protected IDeviceService _deviceService { get; }

     //   protected INavigationService _navigationService { get; }
        protected INavigationService _navigationService { get; private set; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService,
                             IDeviceService deviceService)
        {
            _pageDialogService = pageDialogService;
            _deviceService = deviceService;
            _navigationService = navigationService;
        }

      

        private bool _canNavigate = true;
        public bool CanNavigate
        {
            get { return _canNavigate; }
            set { SetProperty(ref _canNavigate, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Subtitle { get; set; }

        public string Icon { get; set; }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public bool IsNotBusy { get; set; }

        public bool CanLoadMore { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }

        private void OnIsBusyChanged() => IsNotBusy = !IsBusy;

        private void OnIsNotBusyChanged() => IsBusy = !IsNotBusy;

        #region IActiveAware

        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

        private void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if (IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive() { }

        protected virtual void OnIsNotActive() { }

        #endregion IActiveAware

        #region INavigationAware

        public virtual void OnNavigatingTo(NavigationParameters parameters) { }

        public virtual void OnNavigatedTo(NavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

        #endregion INavigationAware

        #region IDestructible

        public virtual void Destroy() { }

        #endregion IDestructible

        #region IConfirmNavigation

     
        #endregion IConfirmNavigation

        #region IApplicationLifecycleAware

        public virtual void OnResume() { }

        public virtual void OnSleep() { }

        #endregion IApplicationLifecycleAware





        #region IPageLifecycleAware

        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
           // throw new NotImplementedException();
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
        #endregion IPageLifecycleAware




    }

}