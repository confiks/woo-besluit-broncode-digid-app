using System;
using System.Globalization;
using DigiD.Common.NFC.Enums;
using Xamarin.Forms;

namespace DigiD.Converters
{
    public class DocumentTypeVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DocumentType) value == (DocumentType) parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
