using Android.Content;
using Android.Graphics;
using DigiD.Droid.CustomRenderers;
using DigiD.Common.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(Button), typeof(CustomButtonRenderer))]
namespace DigiD.Droid.CustomRenderers
{
	internal class CustomButtonRenderer : ButtonRenderer
    {
	    public CustomButtonRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged (ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged (e);

            if (e.NewElement == null || Control == null)
                return;

            Control.SetAllCaps(false);
            Control.PaintFlags = Control.PaintFlags | PaintFlags.SubpixelText;
            Control.SoundEffectsEnabled = false;
            Control.BreakStrategy = Android.Text.BreakStrategy.Balanced;

            Control.SetMaxLines(3);


            foreach (var effect in Element.Effects)
            {
                if (effect is A11YEffect)
                {
                    var controlType = A11YEffect.GetControlType(Element);
                    if (controlType == A11YControlTypes.MenuItem || controlType == A11YControlTypes.Link)
                        Control.Gravity = Android.Views.GravityFlags.Left | Android.Views.GravityFlags.CenterVertical;
                }
            }
        }
    }
}
