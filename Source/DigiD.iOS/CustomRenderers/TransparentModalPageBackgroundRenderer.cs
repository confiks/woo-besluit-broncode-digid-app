
using DigiD.iOS.CustomRenderers;
using DigiD.UI.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainMenuPage), typeof(TransparentModalPageBackgroundRenderer))]
[assembly: ExportRenderer(typeof(WhatsNewTourPage), typeof(TransparentModalPageBackgroundRenderer))]
[assembly: ExportRenderer(typeof(HelpPage), typeof(TransparentModalPageBackgroundRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class TransparentModalPageBackgroundRenderer : PageRenderer
    {
        public override void DidMoveToParentViewController(UIViewController parent)
        {
            base.DidMoveToParentViewController(parent);

            if (ParentViewController != null)
            {
                ParentViewController.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            }
        }
    }
}
