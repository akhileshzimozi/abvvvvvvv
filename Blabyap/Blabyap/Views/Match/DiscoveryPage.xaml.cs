using Blabyap.Common.Model;
using Blabyap.Services;
using Blabyap.ViewModels;
using Syncfusion.SfAutoComplete.XForms;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class DiscoveryPage : ContentPage
    {

        ApiServices _apiServices;
        public DiscoveryPage()
        {
            InitializeComponent();
            _apiServices = new ApiServices();
        }

        public static ResponseResultLookUp industry;

        protected override async  void OnAppearing()
        {
            base.OnAppearing();


            industry = await _apiServices.GetIndustry();

            (BindingContext as DiscoveryPageViewModel)?.DemoData();


            



        }
        public static ResponseResultLookUp expertise;

       
        private async void SfAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                FamilyData cp = (FamilyData)e.Value;

               
                expertise = await _apiServices.GetExpertise(cp.Code);

                (BindingContext as DiscoveryPageViewModel)?.DemoData();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
