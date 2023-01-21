using DigiD.Common.BaseClasses;
using DigiD.Common.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.Common.Mobile.Helpers
{
    public static class OrientationHelper
    {
        public static bool IsInLandscapeMode => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape;
        public static double TabletPaddingWidth => IsInLandscapeMode && Device.Idiom == TargetIdiom.Tablet ? DisplayHelper.Width * .2 : 0;    // Bij normale pages 20% nemen.

        public static void ResizeViews(VisualElement visualElement, Size toSize, bool isLandingPage)
        {
            var isLandscape = toSize.IsZero ? IsInLandscapeMode : toSize.Width > toSize.Height;

            SetNavigationPage(Application.Current.MainPage);

            var padding = isLandingPage || Device.Idiom == TargetIdiom.Phone ? 0.0 : OrientationHelper.TabletPaddingWidth;

            if (visualElement is CommonBaseContentPage contentPage)
            {
                SetContentPage(padding, contentPage, isLandscape);
            }
        }

        private static void SetNavigationPage(Element navPage)
        {
            navPage.SetDynamicResource(VisualElement.BackgroundColorProperty, "PageBackgroundColor");
            SetAsDemoBarIfNecessary(navPage);
        }

        private static void SetAsDemoBarIfNecessary(Element navPage)
        {
            if (navPage is NavigationPage navigationPage)
            {
                if (IsInLandscapeMode && DemoHelper.CurrentUser != null)
                {
                    navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, "DemoBarColor");
                    navigationPage.CurrentPage.Title = $"{DemoHelper.CurrentUser.UserName} - {((CommonBaseViewModel)navigationPage.CurrentPage.BindingContext).PageId}";
                    navigationPage.SetValue(NavigationPage.BarTextColorProperty, Color.Black);
                }
                else
                {
                    navigationPage.SetDynamicResource(NavigationPage.BarBackgroundColorProperty, "PageBackgroundColor");
                    navigationPage.CurrentPage.Title = null;
                    navigationPage.SetDynamicResource(NavigationPage.BarTextColorProperty, "BarTextColor");
                }
            }
        }

        private static void SetContentPage(double padding, ContentPage contentPage, bool isLandscape)
        {
            if (isLandscape)
            {
                contentPage.BackgroundColor = (Color)Application.Current.Resources["PageBackgroundColor"];
                if (contentPage.Content != null)
                    contentPage.Content.BackgroundColor = (Color)Application.Current.Resources["PageBackgroundColor"];
                contentPage.Padding = new Thickness(padding, Device.Idiom == TargetIdiom.Phone ? 0 : 20, padding, 0);
            }
            else
            {
                if (contentPage.BindingContext is CommonBaseViewModel bvm)
                    NavigationPage.SetHasBackButton(contentPage, bvm.HasBackButton);

                contentPage.BackgroundColor = (Color)Application.Current.Resources["PageBackgroundColor"];
                if (contentPage.Content != null)
                    contentPage.Content.BackgroundColor = (Color)Application.Current.Resources["PageBackgroundColor"];
                contentPage.Padding = new Thickness(0, 0);
            }
        }
    }
}
