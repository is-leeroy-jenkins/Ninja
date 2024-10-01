using System.Net;
using Ninja.Models.Network;
using Ninja.Settings;

namespace Ninja.Profiles.Application
{
    using Models.Network;
    using Settings;

    public static class WakeOnLAN
    {
        public static WakeOnLANInfo CreateInfo(ProfileInfo profileInfo)
        {
            var info = new WakeOnLANInfo
            {
                MagicPacket = Models.Network.WakeOnLAN.CreateMagicPacket(profileInfo.WakeOnLAN_MACAddress),
                Broadcast = IPAddress.Parse(profileInfo.WakeOnLAN_Broadcast),
                Port = SettingsManager.Current.WakeOnLAN_Port
            };

            return info;
        }
    }
}