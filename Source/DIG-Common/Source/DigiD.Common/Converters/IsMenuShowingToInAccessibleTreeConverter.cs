using System;
using System.Globalization;
using Xamarin.Forms;

namespace DigiD.Common.Converters
{
    public class IsMenuShowingToInAccessibleTreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Als het menu getoond wordt op een iPhone dan mogen de andere 
            // zichtbare schermen niet in de AccessibleTree staan.
            var result = !(bool)value;
            // Op een tablet EN in landscape mode dan moeten alle zichtbare items
            // in de AccessibleTree gezet worden.
            var isInLandscapeMode = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation == Xamarin.Essentials.DisplayOrientation.Landscape;
            if (Device.Idiom == TargetIdiom.Tablet && isInLandscapeMode)
            {
                result = true;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
