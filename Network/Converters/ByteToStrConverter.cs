using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Ninja.Interfaces
{
    public class ByteToStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var _bytes = (byte[])value;
            var _asciiString = Encoding.ASCII.GetString(_bytes, 0, _bytes.Length);
            return _asciiString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
