using System;
using System.Globalization;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Common.Converters
{
    public class LanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            switch (value.ToString())
            {
                case "nl":
                    return AppResources.Dutch;
                case "en":
                    return AppResources.English;
            }

            return "Unknown language";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyService.Get<IGeneralPreferences>().Language;

            return value.ToString() == AppResources.English ? "en" : "nl";
        }
    }
}
