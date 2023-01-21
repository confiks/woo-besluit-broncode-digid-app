using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Common.Converters
{
    public class BooleanAndConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is bool boolParameter)
                return boolValue && boolParameter;
            // geen booleans dus false teruggeven.
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
