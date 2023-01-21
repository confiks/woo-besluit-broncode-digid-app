
using DigiD.Common.Settings;
using DigiD.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.AdjustsFontForContentSizeCategory = true;
                Control.AccessibilityLanguage = DependencyService.Get<IGeneralPreferences>().Language;
            }

        }
    }
}
