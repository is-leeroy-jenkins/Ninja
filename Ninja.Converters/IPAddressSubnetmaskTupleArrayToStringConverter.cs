using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Data;
using Ninja.Models.Network;

namespace Ninja.Converters;

using Models.Network;

public sealed class IPAddressSubnetmaskTupleArrayToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            return "-/-";

        if (value is not Tuple<IPAddress, IPAddress>[] ipAddresses)
            return "-/-";

        return IPv4Address.ConvertIPAddressWithSubnetmaskListToString(ipAddresses);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}