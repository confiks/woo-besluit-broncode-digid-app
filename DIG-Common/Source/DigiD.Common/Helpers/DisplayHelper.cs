using Xamarin.Forms;

namespace DigiD.Common.Helpers
{
    public static class DisplayHelper
    {
        public static double Width => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
        public static double? WidthAsOptionalDouble => Width;

        public static double Height => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
        public static double? HeightAsOptionalDouble => Height;
        
        public static string ScreenSize => $"{Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width}x{Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height}";

        public static Size Size => new Size(Width, Height);
    }
}
