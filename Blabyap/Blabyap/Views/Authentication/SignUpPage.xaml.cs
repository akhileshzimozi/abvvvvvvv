using Blabyap.ViewModels;
using Syncfusion.XForms.DataForm;
using Xamarin.Forms;

namespace Blabyap.Views
{
    public partial class SignUpPage : ContentPage
    {

      
        public SignUpPage()
        {
            InitializeComponent();
            dataForm.LayoutManager = new DataFormLayoutManagerExt(dataForm);

            SignUpBtn.IsEnabled = false;
        }

         void  Photo()
        {
            if(AddPhotoPopupPageViewModel.myPhoto!=null)
             image.Source = AddPhotoPopupPageViewModel.myPhoto;
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();

            Photo();



        }

        private void checkBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {

            if((checkBox.IsChecked == e.IsChecked.HasValue && e.IsChecked.Value) && (checkBox1.IsChecked == e.IsChecked.HasValue && e.IsChecked.Value))
            {
               // SignUpBtn.IsVisible = true;
                SignUpBtn.IsEnabled = true;
                SignUpBtn.Opacity = 1;
            }
               
            else {
                SignUpBtn.IsEnabled = false;

              SignUpBtn.Opacity = .5;

            }

            // SignUpBtn.IsVisible = false;

        }

        private void checkBox1_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {


            if ((checkBox.IsChecked == e.IsChecked.HasValue && e.IsChecked.Value) && (checkBox1.IsChecked == e.IsChecked.HasValue && e.IsChecked.Value))
            {
                // SignUpBtn.IsVisible = true;
                SignUpBtn.IsEnabled = true;
                SignUpBtn.Opacity = 1;

            }
            else
            {
                SignUpBtn.IsEnabled = false;
               SignUpBtn.Opacity = .5;

            }

            // SignUpBtn.IsVisible = false;

        }
    }




    public class DataFormLayoutManagerExt : DataFormLayoutManager
    {
        public DataFormLayoutManagerExt(SfDataForm dataForm) : base(dataForm)
        {

        }

        protected override View GenerateViewForLabel(DataFormItem dataFormItem)
        {
            var label = base.GenerateViewForLabel(dataFormItem);
            if (label is Label)
            {
                (label as Label).TextColor = Color.White;
            }
            return label;
        }

        protected override void OnEditorCreated(DataFormItem dataFormItem, View editor)
        {
            if (editor is Entry)
                (editor as Entry).TextColor = Color.White;
        }
    }
}
