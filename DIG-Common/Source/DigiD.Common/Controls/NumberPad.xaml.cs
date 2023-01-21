using DigiD.Common.Helpers;
using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public partial class NumberPad : Grid
    {
        private const int maxHeightPortrait = 736;
        private const int maxHeightLandscape = 552;
        private const double factor = 0.4;

        private bool IsInLandscapeMode => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape;

        public NumberPad()
        {
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Desktop)
                HeightRequest = 400;
            else if (Device.Idiom == TargetIdiom.Tablet)
            {
                HeightRequest = IsInLandscapeMode ? maxHeightLandscape * .45 : maxHeightPortrait * factor;
                WidthRequest = DisplayHelper.Width * (IsInLandscapeMode ? factor : (factor + 0.1));
            }
            else
            {
                HeightRequest = DisplayHelper.Height * factor;
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                HeightRequest = IsInLandscapeMode ? maxHeightLandscape * .45 : maxHeightPortrait * factor;
                WidthRequest = DisplayHelper.Width * (IsInLandscapeMode ? factor : (factor + 0.1));
            }
            base.OnSizeAllocated(width, height);
        }
    }
}
