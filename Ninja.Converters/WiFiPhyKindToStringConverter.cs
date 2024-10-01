using System;
using System.Globalization;
using System.Windows.Data;
using Windows.Devices.WiFi;
using Ninja.Models.Network;

namespace Ninja.Converters
{
    using Models.Network;

    public sealed class WiFiPhyKindToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not WiFiPhyKind phyKind ? "-/-" : $"{WiFi.GetHumanReadablePhyKind(phyKind)}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}