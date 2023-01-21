using DigiD.Common.Controls;
using DigiD.iOS.CustomRenderers;
using DigiD.iOS.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumberPadButton), typeof(CustomPinButtonRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class CustomPinButtonRenderer : ImageButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var btn = new CustomPinButton();

                if (UIScreen.MainScreen.Captured)
                    btn.SetImage(Control.ImageView.Image, UIControlState.Highlighted);

                btn.SetImage(Control.ImageView.Image, UIControlState.Normal);

                SetNativeControl(btn);
            }
        }
    }
}
