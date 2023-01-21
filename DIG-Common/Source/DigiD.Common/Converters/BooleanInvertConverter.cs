using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Common.Converters
{
    public class BooleanInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Het kan op een andere manier, maar op deze manier blijft de bestaande code goed werken
            var isNullable = Nullable.GetUnderlyingType(targetType) != null;

            if (!isNullable)
            {
                if (parameter is bool boolParam)
                    return !(bool)value && boolParam;
                return !(bool)value;
            }
            else
            {
                if (value == null)
                    return default(bool?);

                var boolValue = ((bool?)value).Value;
                if (parameter is bool boolParam)
                    return  (bool?)(!boolValue && boolParam);
                return (bool?)(!boolValue);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
