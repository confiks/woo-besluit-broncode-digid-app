using Foundation;
using UIKit;

namespace DigiD.iOS.Views
{
    public class CustomPinButton : UIButton
    {
        public CustomPinButton() : base()
        {
            
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            if (UIScreen.MainScreen.Captured && this.State == UIControlState.Highlighted)
                this.Highlighted = false;

            base.TouchesBegan(touches, evt);
        }
    }
}
