using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DigiD.Common.Helpers
{
    public static class ThemeHelper
    {
        public static bool IsAutomaticAppThemePossible => DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 10 
                                                          || DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Version.Major >= 13 
                                                          || Device.Idiom == TargetIdiom.Desktop;

        public static bool IsInDarkMode => Application.Current?.RequestedTheme == OSAppTheme.Dark;

        public static string Convert(string imageSource, bool forceDark = false)
        {
            if (imageSource == null)
                throw new ArgumentNullException(nameof(imageSource));

            const string suffix = "_dark";

            if (IsInDarkMode || forceDark)
            {
                var svg = imageSource.Contains(".svg");

                // In darkmode, nu controleren of de filenaam eindigt op '_dark'
                // zo niet dan filenaam aanpassen.
                var f = imageSource.Split(new[] { ".svg",".png" }, StringSplitOptions.RemoveEmptyEntries);
                if (!f[0].EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase))
                    imageSource = string.Concat(f[0], suffix, svg ? ".svg" : ".png", f.Length == 1 ? "" : f[1]);
            }
            else
            {
                var f = imageSource.Split(new[] { suffix }, StringSplitOptions.RemoveEmptyEntries);
                if (f.Length > 1)
                {
                    // filenaam bevat '_dark', deze verwijderen.
                    imageSource = string.Concat(f[0], f[1]);
                }
            }

            return imageSource;
        }
    }
}
