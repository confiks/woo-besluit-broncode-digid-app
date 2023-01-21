using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DigiD.Droid.Extensions
{
    public static class Extensions
    {
        public static global::Android.Views.View GetViewForAccessibility(this Xamarin.Forms.VisualElement visualElement)
        {
            var renderer = Platform.GetRenderer(visualElement);

            if (visualElement is Layout)
                return renderer?.View;
            else if (renderer is ViewGroup vg && vg.ChildCount > 0)
                return vg.GetChildAt(0);
            else if (renderer != null)
                return renderer.View;


            return renderer?.View;
        }
    }
}
