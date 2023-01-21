using System;
using System.Globalization;
using DigiD.Common.Helpers;
using Xamarin.Forms;

namespace DigiD.Converters
{
    public class MessageBoxBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return (Color)Xamarin.Forms.Application.Current.Resources["MessageBoxNewBackgroundColor"];

            return (Color)Xamarin.Forms.Application.Current.Resources["MessageBoxBackgroundColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MessageBoxTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Color.Black;

            return (Color)Application.Current.Resources[ThemeHelper.IsInDarkMode ? "TextColorDark" : "TextColorLight"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
