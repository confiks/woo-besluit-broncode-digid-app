using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Converters
{
    public class BoolValidatedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bValue && bValue)
            {
                return (Color) Xamarin.Forms.Application.Current.Resources["MrzValidated"];
            }
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
