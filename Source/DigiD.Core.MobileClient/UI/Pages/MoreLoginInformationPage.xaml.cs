using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Mobile.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreLoginInformationPage : BaseContentPage
    {
        public MoreLoginInformationPage()
        {
            InitializeComponent();
            OrientationChanged(OrientationHelper.IsInLandscapeMode ? DisplayOrientation.Landscape : DisplayOrientation.Portrait);
        }
        protected override void OrientationChanged(DisplayOrientation orientation)
        {
            base.OrientationChanged(orientation);
            if (orientation == DisplayOrientation.Landscape)
                NavigationPage.SetHasNavigationBar(this, true);
            else
                NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
