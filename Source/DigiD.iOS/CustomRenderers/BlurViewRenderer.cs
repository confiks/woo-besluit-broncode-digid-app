using DigiD.iOS.CustomRenderers;
using DigiD.UI.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BlurView), typeof(BlurViewRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class BlurViewRenderer : ViewRenderer<BlurView, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BlurView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var view = UIApplication.SharedApplication.Windows[0];
                if (!UIAccessibility.IsReduceTransparencyEnabled)
                {
                    view.BackgroundColor = UIColor.Clear;
                    var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Regular);
                    var blurEffectView = new UIVisualEffectView(blurEffect)
                    {
                        Frame = view.Bounds,
                        AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
                    };

                    if (Element != null)
                        SetNativeControl(blurEffectView);
                }
            }
        }
    }
}
