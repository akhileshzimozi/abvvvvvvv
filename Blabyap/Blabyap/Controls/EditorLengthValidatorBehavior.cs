using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Blabyap.Controls
{
   public class EditorLengthValidatorBehavior: Behavior<Xamarin.Forms.Editor> ,INotifyPropertyChanged
    {
        public int MaxLength { get; set; }

       
       

        int leftlen=0;
        public int Leftlen
        {
            get { return leftlen; }
            set
            {
                leftlen = value;
            }
        }

       

        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
           var  entry = (Editor)sender;
           Leftlen = entry.Text.Length;
            // if Entry text is longer then valid length
            if (entry.Text.Length > this.MaxLength)
            {
                string entryText = entry.Text;

                entryText = entryText.Remove(entryText.Length - 1); // remove last char

                entry.Text = entryText;
           //  await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");

            }
        }
    }
}
