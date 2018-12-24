using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Blabyap.Controls
{
    public class ExpandButtonTriggerAction : TriggerAction<Button>
    {
        protected async override void Invoke(Button button)
        {
            await button.ScaleTo(.8, 25, Easing.BounceOut);
            await button.ScaleTo(1,50,Easing.BounceIn);
        }
    }
}
