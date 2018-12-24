using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Blabyap.Views.Match
{
    public partial class UnMatchUserPopupPage : PopupPage
    {
        public UnMatchUserPopupPage()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();

            FrameContainer.HeightRequest = -1;

            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                UnmatchButton.Scale = 1;
                UnmatchButton.Opacity = 1;

                ReasonUnmatch.TranslationX = OtherDetails.TranslationX = 0;
                ReasonUnmatch.Opacity = OtherDetails.Opacity = 1;

                return;
            }

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            UnmatchButton.Scale = 0.3;
            UnmatchButton.Opacity = 0;

            ReasonUnmatch.TranslationX = OtherDetails.TranslationX = -10;
            ReasonUnmatch.Opacity = OtherDetails.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var translateLength = 400u;

            await Task.WhenAll(
                ReasonUnmatch.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                ReasonUnmatch.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    await Task.Delay(200);
                    await Task.WhenAll(
                        OtherDetails.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        OtherDetails.FadeTo(1));

                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                UnmatchButton.ScaleTo(1),
                UnmatchButton.FadeTo(1));
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;

            await Task.WhenAll(
                ReasonUnmatch.FadeTo(0),
                OtherDetails.FadeTo(0),
                UnmatchButton.FadeTo(0));

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        //private async void OnLogin(object sender, EventArgs e)
        //{
        //    var loadingPage = new LoadingPopupPage();
        //    await Navigation.PushPopupAsync(loadingPage);
        //    await Task.Delay(2000);
        //    await Navigation.RemovePopupPageAsync(loadingPage);
        //    await Navigation.PushPopupAsync(new LoginSuccessPopupPage());
        //}

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
