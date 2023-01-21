using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Converters
{
    public class TabStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                if (parameter != null && parameter.ToString() == "1")
                    return !val ? (Style) Application.Current.Resources["ActiveTabActive"] : null;
                return val ? (Style) Application.Current.Resources["ActiveTabActive"] : null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TabLabelStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                string style;
                if (parameter != null && parameter.ToString() == "1")
                    style = !val ? "TabLabelActive" : "TabLabelInactive";
                else
                    style = val ? "TabLabelActive" : "TabLabelInactive";

                return (Style) Application.Current.Resources[style];
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
