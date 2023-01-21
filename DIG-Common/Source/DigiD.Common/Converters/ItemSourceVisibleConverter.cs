using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Common.Converters
{
    public class ItemSourceVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var items = (IList) value;
            return items.Count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
