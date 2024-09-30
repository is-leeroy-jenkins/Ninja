using System.Net;

namespace Ninja.Models.Network;

public class WakeOnLANInfo
{
    public IPAddress Broadcast;
    public byte[] MagicPacket;
    public int Port;

    public WakeOnLANInfo()
    {
    }

    public WakeOnLANInfo(byte[] magicPacket, IPAddress broadcast, int port)
    {
        MagicPacket = magicPacket;
        Broadcast = broadcast;
        Port = port;
    }
}