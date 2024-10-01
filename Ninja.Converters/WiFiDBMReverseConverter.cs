﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Ninja.Converters
{
    public sealed class WiFiDBMReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is double) ? "-/-" : $"-{100 - (double)value} dBm";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}