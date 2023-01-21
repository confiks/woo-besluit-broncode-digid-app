using DigiD.Common.Helpers;
using DigiD.Common.Mobile.BaseClasses;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivationRdaConfirmPage : BaseContentPage
    {
        public ActivationRdaConfirmPage()
        {
            InitializeComponent();
            SetAnimationSizes(DeviceDisplay.MainDisplayInfo.Orientation);
        }

        protected override void OrientationChanged(DisplayOrientation orientation)
        {
            base.OrientationChanged(orientation);
            SetAnimationSizes(orientation);
        }

        private void SetAnimationSizes(DisplayOrientation orientation)
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                if (orientation == DisplayOrientation.Landscape)
                {
                    animationViewPortrait.HeightRequest = 0;
                    animationViewLandscape.HeightRequest = DisplayHelper.Height - 60;  // zie json "w":500, "h:500"
                }
                else
                {
                    animationViewPortrait.HeightRequest = DisplayHelper.Width - 40;  // zie json "w":500, "h:500"
                    animationViewLandscape.HeightRequest = 0;
                }
            }
            else
            {   // Tablet
                animationViewPortrait.HeightRequest = DisplayHelper.Height - 60;  // zie json "w":500, "h:500"
                animationViewLandscape.HeightRequest = 0;
            }
        }
    }
}
