using Windows.Devices.WiFi;

namespace Ninja.Models.Network;

public class WiFiAdapterInfo
{
    public NetworkInterfaceInfo NetworkInterfaceInfo { get; set; }
    public WiFiAdapter WiFiAdapter { get; set; }
}