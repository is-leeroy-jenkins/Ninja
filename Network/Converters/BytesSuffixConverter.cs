using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ninja.Interfaces
{
    [ValueConversion(typeof(long), typeof(string))]
    public class BytesSuffixConverter : IValueConverter
    {
        private readonly string[] _suffix = { "B", "KB", "MB", "GB", "TB" };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var _bytes = (long)value;
            return FormatBytes(_bytes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string FormatBytes(long bytes)
        {
            var _index = 0;
            string _formatBytes;
            double _fileSize = bytes;
            while ((int)((double)_fileSize / 1024.0f) > 0)
            {
                _fileSize /= 1024.0f;
                _index++;
            }

            if (_index == 0)
            {
                _formatBytes = String.Format("{0} {1}", bytes, _suffix[_index]);
            }
            else
            {
                _formatBytes = String.Format("{0:0.00} {1}", _fileSize, _suffix[_index]);
            }

            return _formatBytes;
        }
    }
}
