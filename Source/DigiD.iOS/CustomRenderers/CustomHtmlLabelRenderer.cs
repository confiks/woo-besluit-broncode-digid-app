using DigiD.Controls;
using DigiD.iOS.CustomRenderers;
using LabelHtml.Forms.Plugin.Abstractions;
using LabelHtml.Forms.Plugin.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CustomHtmlLabel), typeof(CustomHtmlLabelRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class CustomHtmlLabelRenderer : HtmlLabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<HtmlLabel> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Font = UIKit.UIFont.FromName("RijksoverheidSansWebText-Regular", Control.Font?.PointSize ?? 17);
            }
        }
    }
}
