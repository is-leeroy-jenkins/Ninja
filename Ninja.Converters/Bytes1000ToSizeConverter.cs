using System;
using System.Globalization;
using System.Windows.Data;
using Ninja.Utilities;

namespace Ninja.Converters
{
    using Utilities;

    public sealed class Bytes1000ToSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? $"{FileSizeConverter.GetBytesReadable((long)value)}" : "-/-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}