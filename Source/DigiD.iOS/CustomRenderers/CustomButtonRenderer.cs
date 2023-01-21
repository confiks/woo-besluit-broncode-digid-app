using DigiD.Common.Controls;
using DigiD.Common.Settings;
using DigiD.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderedButton), typeof(CustomButtonRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            if (Control is UIButton button)
            {
                button.TitleLabel.AdjustsFontForContentSizeCategory = true;

                button.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                button.TitleLabel.TextAlignment = UITextAlignment.Center;
                button.TitleLabel.Lines = 0;
                button.AccessibilityLanguage = DependencyService.Get<IGeneralPreferences>().Language;
            }
        }
    }

}
