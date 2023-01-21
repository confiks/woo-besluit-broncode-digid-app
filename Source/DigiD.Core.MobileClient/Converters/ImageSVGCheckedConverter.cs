using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Converters
{
    internal class ImageSvgCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value
                ? "resource://DigiD.Resources.digid_icon_have_read.svg"
                : "resource://DigiD.Resources.digid_icon_have_not_read.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
