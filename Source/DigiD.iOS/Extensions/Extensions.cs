using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace DigiD.iOS.Extensions
{
    public static class Extensions
    {
        public static UIView GetViewForAccessibility(this VisualElement visualElement)
        {
            return Platform.GetRenderer(visualElement)?.NativeView ?? Platform.CreateRenderer(visualElement)?.NativeView;
        }
    }
}
