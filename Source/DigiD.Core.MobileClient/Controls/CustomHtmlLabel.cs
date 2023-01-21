using LabelHtml.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace DigiD.Controls
{
    /// <summary>
    /// This subclass is only needed to support iOS 13 htmllabel with RO-font.
    /// The base-renderer is not hit, when it uses the baseclass
    /// </summary>
    public class CustomHtmlLabel : HtmlLabel
    {
        public CustomHtmlLabel()
        {
            if (Device.RuntimePlatform == Device.Android)
                FontFamily = "RO-Regular";
        }
    }
}
