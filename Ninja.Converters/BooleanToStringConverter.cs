using System;
using System.Globalization;
using System.Windows.Data;
using Ninja.Localization.Resources;

namespace Ninja.Converters
{
    public sealed class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (bool)value)
                return Strings.Yes;

            return Strings.No;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}