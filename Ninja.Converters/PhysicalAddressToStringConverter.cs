using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Windows.Data;
using Ninja.Utilities;

namespace Ninja.Converters;

using Utilities;

public sealed class PhysicalAddressToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not PhysicalAddress physicalAddress)
            return "-/-";

        var macAddress = physicalAddress.ToString();

        return string.IsNullOrEmpty(macAddress) ? "-/-" : MACAddressHelper.GetDefaultFormat(macAddress);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}