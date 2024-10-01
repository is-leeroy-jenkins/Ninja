using System;
using System.Globalization;
using System.Windows.Data;
using Ninja.Models.Network;

namespace Ninja.Converters
{
    using Models.Network;

    public sealed class WiFiChannelCenterFrequencyToFrequencyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not int channelCenterFrequencyInKilohertz
                ? "-/-"
                : $"{WiFi.ConvertChannelFrequencyToGigahertz(channelCenterFrequencyInKilohertz)} GHz";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}