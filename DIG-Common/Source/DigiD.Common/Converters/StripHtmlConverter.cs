using System;
using System.Globalization;
using Xamarin.Forms;
using DigiD.Common.Helpers;

namespace DigiD.Common.Converters
{
    public class StripHtmlConverter : IValueConverter
    {
        public StripHtmlConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string htmlString)
                return htmlString.StripHtml();
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
