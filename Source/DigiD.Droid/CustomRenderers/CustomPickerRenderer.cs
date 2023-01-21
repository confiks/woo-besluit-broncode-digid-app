using Android.Content;
using Android.Graphics;
using DigiD.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPickerRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.PaintFlags = Control.PaintFlags | PaintFlags.SubpixelText;
                Control.Background = null;
            }
		}
	}
}
