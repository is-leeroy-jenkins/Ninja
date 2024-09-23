// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="Ipv4ProtocolStats.cs" company="Terry D. Eppler">
// 
//    Ninja is a network toolkit, support iperf, tcp, udp, websocket, mqtt,
//    sniffer, pcap, port scan, listen, ip scan .etc.
// 
//    Copyright ©  2019-2024 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   Ipv4ProtocolStats.cs
// </summary>
// ******************************************************************************************

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
        /// The byte count
        /// </summary>
        private long _byteCount;

        /// <summary>
        /// The packet count
        /// </summary>
        private long _packetCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ipv4ProtocolStats"/> class.
        /// </summary>
        public Ipv4ProtocolStats( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ipv4ProtocolStats"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public Ipv4ProtocolStats( IpV4Protocol protocol )
        {
            Protocol = protocol;
            PacketCount = 0;
            ByteCount = 0;
        }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public IpV4Protocol Protocol { get; set; }

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
                if( _packetCount != value )
                {
                    _packetCount = value;
                    OnPropertyChanged( nameof( PacketCount ) );
                }
            }
        }

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
                if( _byteCount != value )
                {
                    _byteCount = value;
                    OnPropertyChanged( nameof( ByteCount ) );
                }
            }
        }
    }
}