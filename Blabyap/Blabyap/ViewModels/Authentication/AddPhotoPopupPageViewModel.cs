using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using System.IO;
using Blabyap.Common.Model;
using Blabyap.Services;
using Rg.Plugins.Popup.Services;
using System.Diagnostics;

namespace Blabyap.ViewModels
{
    public class AddPhotoPopupPageViewModel : ViewModelBase
    {
        public static ImageSource myPhoto { get; set; }

       // public static Stream stream;
        private static Stream _stream;
        public  Stream stream
        {
            get { return _stream; }
            set { SetProperty(ref _stream, value); }
        }
        public DelegateCommand PickImage { get; set; }
        public DelegateCommand TakePhoto { get; set; }
        ApiServices _apiServices;

        public AddPhotoPopupPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService) : base(navigationService, pageDialogService, deviceService)
        {

            PickImage = new DelegateCommand(PickImageAction).ObservesCanExecute(()=> CanNavigate);
            TakePhoto = new DelegateCommand(TakePhotoAction).ObservesCanExecute(() => CanNavigate);
            _apiServices = new ApiServices();
        }



        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }



        private async void TakePhotoAction()
        {
            CanNavigate = false;
            try { 
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _pageDialogService.DisplayAlertAsync("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            await _pageDialogService.DisplayAlertAsync("File Location", file.Path, "OK");

            myPhoto = ImageSource.FromStream(() =>
            {
                 stream = file.GetStream();
                file.Dispose();
                return stream;
            });
                
            await _navigationService.GoBackAsync();
            await _navigationService.NavigateAsync("SignUpPage");
            await PopupNavigation.Instance.PopAsync();

            var image = new ImageUpload
            {
                Image = ReadFully(stream)
            };

            await _apiServices.PostImage(image);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            CanNavigate = true;
        }

        private async void PickImageAction()
        {
            CanNavigate = true;
            try
            {

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await _pageDialogService.DisplayAlertAsync("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });



                if (file == null)  
                    return;

                myPhoto = ImageSource.FromStream(() =>
               {
                   stream = file.GetStream();
                   file.Dispose();
                   return stream;
               });

                await _navigationService.GoBackAsync();
                await _navigationService.NavigateAsync("SignUpPage");
                await PopupNavigation.Instance.PopAsync();



                var image = new ImageUpload
                {

                    Image = ReadFully(stream)
                };

                await _apiServices.PostImage(image);
               

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            CanNavigate = true;
        }

      


    }
}
