using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Common.Converters
{
    public class SpeakLetterForLetterConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                var parts = stringValue.Split(':');
                string result;

                if (parts.Length > 1)
                    result = parts[0] + ":" + string.Join(" , ", parts[1].ToCharArray());
                else
                    result = string.Join(" , ", stringValue.ToCharArray());

                return result;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
