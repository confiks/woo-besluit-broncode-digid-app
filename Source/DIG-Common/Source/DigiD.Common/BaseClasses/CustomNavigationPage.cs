using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace DigiD.Common.BaseClasses
{
    public class CustomNavigationPage : Xamarin.Forms.NavigationPage
    {
        public bool IgnoreVersionCheck { get; }
        
        public CustomNavigationPage(Xamarin.Forms.Page page, bool ignoreVersionCheck = false) : base(page)
        {
            SetDynamicResource(BackgroundColorProperty, "BarBackgroundColor");
            SetDynamicResource(BarBackgroundColorProperty, "BarBackgroundColor");
            SetDynamicResource(BarTextColorProperty, "BarTextColor");

            On<iOS>().SetHideNavigationBarSeparator(true);

            IgnoreVersionCheck = ignoreVersionCheck;
        }
    }
}

