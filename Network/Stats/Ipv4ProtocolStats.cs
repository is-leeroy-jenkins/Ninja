

namespace Ninja 
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using PcapDotNet.Packets.IpV4;
    using ViewModels;

    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class Ipv4ProtocolStats : MainWindowBase
    {
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public IpV4Protocol Protocol { get; set; }

        /// <summary>
        /// The packet count
        /// </summary>
        private long _packetCount;

        /// <summary>
        /// Gets or sets the packet count.
        /// </summary>
        /// <value>
        /// The packet count.
        /// </value>
        public long PacketCount
        {
            get { return _packetCount; }
            set
            {
                if(_packetCount != value)
                {
                    _packetCount = value;
                    OnPropertyChanged(nameof(PacketCount));
                }
            }
        }

        /// <summary>
        /// The byte count
        /// </summary>
        private long _byteCount;
        /// <summary>
        /// Gets or sets the byte count.
        /// </summary>
        /// <value>
        /// The byte count.
        /// </value>
        public long ByteCount
        {
            get { return _byteCount; }
            set
            {
                if(_byteCount != value)
                {
                    _byteCount = value;
                    OnPropertyChanged(nameof(ByteCount));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ipv4ProtocolStats"/> class.
        /// </summary>
        public Ipv4ProtocolStats()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ipv4ProtocolStats"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public Ipv4ProtocolStats(IpV4Protocol protocol)
        {
            Protocol = protocol;
            PacketCount = 0;
            ByteCount = 0;
        }
    }
}
