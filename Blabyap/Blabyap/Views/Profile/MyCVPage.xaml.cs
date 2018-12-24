using Blabyap.Common.Model;
using Blabyap.Controls;
using Blabyap.Services;
using Blabyap.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class MyCVPage : ContentPage
    {



        int charValue = 27;
        public int CharValue
        {
            get { return charValue; }
            set
            {
                charValue = value;
            }
        }

        string aboutUsChar;
        public string AboutUsChar
        {
            get { return aboutUsChar; }
            set
            {
                aboutUsChar = value;
            }
        }

        EditorLengthValidatorBehavior editorLengthValidatorBehavior;

        ApiServices _apiServices;
        public MyCVPage()
        {
            InitializeComponent();
            _apiServices = new ApiServices();
            AboutUsChar = "About You Tell us more about you (" + CharValue + " characters left)";

            editorLengthValidatorBehavior = new EditorLengthValidatorBehavior();


        }

        ResponseResultLookUp national;
        ResponseResultCV cVData;
        public static ResponseResultLookUp industry;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            national = await _apiServices.GetNationality();
            industry = await _apiServices.GetIndustry();


            cVData = await _apiServices.GetCVApiUrl();
            (BindingContext as MyCVPageViewModel)?.DemoData(cVData, LoginPageViewModel.User, national, industry, expertise);


        }
        FamilyData cp;
        public  ResponseResultLookUp expertise;
        private async void SfAutoComplete_SelectionChanged(object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                cp = (FamilyData)e.Value;
                expertise = await _apiServices.GetExpertise(cp.Code);
                (BindingContext as MyCVPageViewModel)?.DemoData(cVData, LoginPageViewModel.User, national, industry,expertise);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


      

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
         CharValue  = editorLengthValidatorBehavior.Leftlen;
            AboutUsChar = "About You Tell us more about you (" + CharValue + " characters left)";

        }
    }
}
