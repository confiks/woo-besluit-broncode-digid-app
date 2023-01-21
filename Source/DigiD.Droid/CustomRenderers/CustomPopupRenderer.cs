using Android.Content;
using AndroidX.Core.Content.Resources;
using DigiD.Droid.CustomRenderers;
using DigiD.UI.Popups;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Popup), typeof(CustomPopupRenderer))]
namespace DigiD.Droid.CustomRenderers
{
    public class CustomPopupRenderer : PopupRenderer
    {
        public CustomPopupRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<BasePopup> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && Window != null)
            {
                if (!(e.NewElement is MainMenuPopup))
                {
                    var drawable = ResourcesCompat.GetDrawable(Context.Resources, Resource.Drawable.popup_background, null);
                    Window.SetBackgroundDrawable(drawable);
                }
                else
                {
                    Window.DecorView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }    
        }
    }
}
