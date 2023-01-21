using DigiD.Common.Helpers;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace DigiD.Common.Controls
{
    public class CustomSvgImage : SvgCachedImage
    {
        private double MaxWidth => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape ? DisplayHelper.Height - 40 : DisplayHelper.Width - 40;

        public CustomSvgImage()
        {
            FadeAnimationEnabled = false;
            Aspect = Aspect.AspectFit;
            CacheType = FFImageLoading.Cache.CacheType.All;
            SizeChanged -= CustomSvgImage_SizeChanged;
            SizeChanged += CustomSvgImage_SizeChanged;
        }

        private void CustomSvgImage_SizeChanged(object sender, System.EventArgs e)
        {
            if (Width == 0 && WidthRequest == -1)
                WidthRequest = MaxWidth;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (widthConstraint > MaxWidth)
                WidthRequest = MaxWidth;

            if (!double.IsInfinity(heightConstraint) && !double.IsNaN(heightConstraint))
                HeightRequest =  heightConstraint;

            if (widthConstraint == 0)
                WidthRequest = 40;

            if (heightConstraint == 0)
                HeightRequest = 40;

            return base.OnMeasure(widthConstraint, heightConstraint);
        }
    }
}
