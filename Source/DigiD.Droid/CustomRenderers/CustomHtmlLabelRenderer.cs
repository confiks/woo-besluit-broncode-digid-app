using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using DigiD.Controls;
using DigiD.Droid.CustomRenderers;
using LabelHtml.Forms.Plugin.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomHtmlLabel), typeof(CustomHtmlLabelRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class CustomHtmlLabelRenderer : HtmlLabelRenderer
    {
        public CustomHtmlLabelRenderer(Context context) : base(context)
        { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.PaintFlags = PaintFlags.SubpixelText | PaintFlags.AntiAlias;
            Control.SetAllCaps(false);

            //Als de HtmlLabel tekst geen hyperlink(s) bevat, dan de tekst ook niet klikbaar maken
            if (e.PropertyName == Label.TextProperty.PropertyName && (string.IsNullOrEmpty(Element.Text) || (Element.Text != null && !(Element.Text.Contains("<a href") || Element.Text.Contains("&lt;a href")))))
            {
                Control.Clickable = false;
                Control.LongClickable = false;
                if (string.IsNullOrEmpty(Element.Text))
                    AutomationProperties.SetIsInAccessibleTree(Element, false);
            }
        }
    }
}
