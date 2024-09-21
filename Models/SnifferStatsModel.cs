
namespace Ninja.Models
{
    using PcapDotNet.Packets.IpV4;
    using ViewModels;

    public class SnifferStatsModel : MainWindowBase
    {
         #region Protocol Stats
        public class Ipv4ProtocolStats : MainWindowBase
        {
            public IpV4Protocol Protocol { get; set; }

            private long _PacketCount;
            public long PacketCount
            {
                get { return _PacketCount; }
                set
                {
                    if (_PacketCount != value)
                    {
                        _PacketCount = value;
                        OnPropertyChanged(nameof(PacketCount));
                    }
                }
            }

            private long _ByteCount;
            public long ByteCount
            {
                get { return _ByteCount; }
                set
                {
                    if (_ByteCount != value)
                    {
                        _ByteCount = value;
                        OnPropertyChanged(nameof(ByteCount));
                    }
                }
            }

            public Ipv4ProtocolStats( IpV4Protocol protocol )
            {
                Protocol = protocol;
                PacketCount = 0;
                ByteCount = 0;
            }
        }
        #endregion
        public SnifferStatsModel()
        {
        }
    }
}
