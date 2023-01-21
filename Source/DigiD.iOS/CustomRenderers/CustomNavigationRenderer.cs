using System.Linq;
using DigiD.Common;
using DigiD.Common.BaseClasses;
using DigiD.Common.Helpers;
using DigiD.Common.Services;
using DigiD.Common.SessionModels;
using DigiD.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (CustomNavigationPage), typeof (CustomNavigationRenderer))] //Custom renderer to set left toolbar item and custom font for title
namespace DigiD.iOS.CustomRenderers
{
	internal class CustomNavigationRenderer : NavigationRenderer
	{
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            var currentUIStatusBarStyle = base.PreferredStatusBarStyle();

            if (ThemeHelper.IsInDarkMode && currentUIStatusBarStyle != UIStatusBarStyle.LightContent)
                return UIStatusBarStyle.LightContent;
            if (!ThemeHelper.IsInDarkMode && currentUIStatusBarStyle != UIStatusBarStyle.DarkContent)
                return UIStatusBarStyle.DarkContent;

            return base.PreferredStatusBarStyle();
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad();

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                NavigationBar.CompactAppearance.ShadowColor = null;
                NavigationBar.StandardAppearance.ShadowColor = null;
                NavigationBar.ScrollEdgeAppearance.ShadowColor = null;
                NavigationBar.ShadowImage = new UIImage(); // Deze is nog wel nodig!!
            }

            ModalPresentationStyle = UIModalPresentationStyle.None;

            if (!DependencyService.Get<IA11YService>().IsInVoiceOverMode())
                return;

            var nav = NativeView.Subviews.ToList().OfType<UINavigationBar>().FirstOrDefault();

            var navItem = nav?.Items.ToList().FirstOrDefault(x => x.LeftBarButtonItem != null);

            if (navItem?.LeftBarButtonItem != null)
            {
                if (AppSession.AccountStatus?.HasUnreadMessages == true)
                    navItem.LeftBarButtonItem.AccessibilityLabel = $"{AppResources.AccessibilityAppMainMenu}, {AppResources.NewNotificationsHeader}";
                else
                    navItem.LeftBarButtonItem.AccessibilityLabel = AppResources.AccessibilityAppMainMenu;
            }
        }
    }
}
