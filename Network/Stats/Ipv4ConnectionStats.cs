// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="Ipv4ConnectionStats.cs" company="Terry D. Eppler">
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
//   Ipv4ConnectionStats.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using PcapDotNet.Packets.IpV4;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    public class Ipv4ConnectionStats
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ipv4ConnectionStats"/> class.
        /// </summary>
        /// <param name="addressA">The address a.</param>
        /// <param name="addressB">The address b.</param>
        public Ipv4ConnectionStats( IpV4Address addressA, IpV4Address addressB )
        {
            AddressA = addressA;
            AddressB = addressB;
            PacketCountAToB = 0;
            PacketCountBToA = 0;
            ByteCountAToB = 0;
            ByteCountBToA = 0;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ipv4ConnectionStats"/> class.
        /// </summary>
        public Ipv4ConnectionStats( )
        {
        }

        /// <summary>
        /// Gets or sets the address a.
        /// </summary>
        /// <value>
        /// The address a.
        /// </value>
        public IpV4Address AddressA { get; set; }

        /// <summary>
        /// Gets or sets the address b.
        /// </summary>
        /// <value>
        /// The address b.
        /// </value>
        public IpV4Address AddressB { get; set; }

        /// <summary>
        /// Gets or sets the packet count a to b.
        /// </summary>
        /// <value>
        /// The packet count a to b.
        /// </value>
        public long PacketCountAToB { get; set; }

        /// <summary>
        /// Gets or sets the packet count b to a.
        /// </summary>
        /// <value>
        /// The packet count b to a.
        /// </value>
        public long PacketCountBToA { get; set; }

        /// <summary>
        /// Gets or sets the byte count a to b.
        /// </summary>
        /// <value>
        /// The byte count a to b.
        /// </value>
        public long ByteCountAToB { get; set; }

        /// <summary>
        /// Gets or sets the byte count b to a.
        /// </summary>
        /// <value>
        /// The byte count b to a.
        /// </value>
        public long ByteCountBToA { get; set; }
    }
}