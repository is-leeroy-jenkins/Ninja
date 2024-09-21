

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PcapDotNet.Packets.IpV4;

    public class Ipv4ConnectionStats
    {
        public IpV4Address AddressA { get; set; }

        public IpV4Address AddressB { get; set; }

        public long PacketCountAToB { get; set; }

        public long PacketCountBToA { get; set; }

        public long ByteCountAToB { get; set; }

        public long ByteCountBToA { get; set; }

        public Ipv4ConnectionStats(IpV4Address addressA, IpV4Address addressB)
        {
            AddressA = addressA;
            AddressB = addressB;
            PacketCountAToB = 0;
            PacketCountBToA = 0;
            ByteCountAToB = 0;
            ByteCountBToA = 0;
        }

        public Ipv4ConnectionStats( )
        {
        }
    }
}
