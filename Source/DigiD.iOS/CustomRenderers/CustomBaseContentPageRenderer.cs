using CoreGraphics;
using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Mobile.Helpers;
using DigiD.Common.SessionModels;
using DigiD.iOS.CustomRenderers;
using DigiD.UI.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(CustomBaseContentPageRenderer))]
namespace DigiD.iOS.CustomRenderers
{
    public class CustomBaseContentPageRenderer : PageRenderer
    {
        private bool _isDisposed;
        
        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            if (Element is BaseContentPage page && page.Content != null
                && page.On<Xamarin.Forms.PlatformConfiguration.iOS>().UsingSafeArea())
            {
                page.Content.Margin = new Thickness(
                    NativeView.SafeAreaInsets.Left,
                    NativeView.SafeAreaInsets.Top,
                    NativeView.SafeAreaInsets.Right,
                    OrientationHelper.IsInLandscapeMode ? 0 : NativeView.SafeAreaInsets.Bottom);
            }
        }

        public override UIViewController ChildViewControllerForStatusBarStyle()
        {
            var uiViewController = ChildViewControllers.Length >= 1 ? ChildViewControllers[0] : base.ChildViewControllerForStatusBarStyle();
            return uiViewController;
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            var currentUIStatusBarStyle = base.PreferredStatusBarStyle();

            if (ThemeHelper.IsInDarkMode && currentUIStatusBarStyle != UIStatusBarStyle.LightContent)
                return UIStatusBarStyle.LightContent;
            if (!ThemeHelper.IsInDarkMode && currentUIStatusBarStyle != UIStatusBarStyle.DarkContent)
                return UIStatusBarStyle.DarkContent;
            return base.PreferredStatusBarStyle();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                _isDisposed = true;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            _isDisposed = false;

            if (!(e.NewElement is BaseContentPage newPage))
                return;

            if (!(newPage is MainMenuPage))
                newPage.BackgroundColor = (Color)Xamarin.Forms.Application.Current.Resources["PageBackgroundColor"];
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);

            OrientationHelper.ResizeViews(Element, new Size(toSize.Width, toSize.Height), Element is LandingPage);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            OrientationHelper.ResizeViews(Element, Size.Zero, Element is LandingPage);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            SetNeedsStatusBarAppearanceUpdate();

            SetLayout();
        }

        private void SetLayout()
        {
            if (_isDisposed)
                return;

            if (ParentViewController == null || ParentViewController.IsBeingDismissed)
                return;

            var item = ParentViewController?.NavigationItem?.LeftBarButtonItem;

            if (item?.AccessibilityLabel != null && (item.AccessibilityLabel.StartsWith("icon_menu") || item.AccessibilityLabel.StartsWith("icon menu")))
            {
                if (AppSession.AccountStatus?.HasUnreadMessages == true)
                    item.AccessibilityLabel = $"{Common.AppResources.AccessibilityAppMainMenu}, {Common.AppResources.NewNotificationsHeader}";
                else
                    item.AccessibilityLabel = $"{Common.AppResources.AccessibilityAppMainMenu}";
            }
        }
    }
}
