using Android.Content;
using DigiD.Droid.CustomRenderers;
using DigiD.Droid.Views;
using DigiD.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BlurView), typeof(BlurViewRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class BlurViewRenderer : ViewRenderer<BlurView, Android.Views.View>
    {
        public BlurViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BlurView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var view = new RealtimeBlurView(Context, null);
                view.SetBlurRadius(20f);
                SetNativeControl(view);
            }
        }
    }
}
