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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PcapDotNet.Packets.IpV4;
    using ViewModels;

    /// <inheritdoc />
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class Ipv4ProtocolStats : INotifyPropertyChanged
    {
        private protected IpV4Protocol _protocol;

        /// <summary>
        /// The byte count
        /// </summary>
        private protected long _byteCount;

        /// <summary>
        /// The packet count
        /// </summary>
        private protected long _packetCount;

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
            _protocol = protocol;
            _packetCount = 0;
            _byteCount = 0;
        }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public IpV4Protocol Protocol
        {
            get
            {
                return _protocol;
            }
            set
            {
                if(_protocol != value)
                {
                    _protocol = value;
                    OnPropertyChanged(nameof(Protocol));
                }
            }
        }

        /// <summary>
        /// Gets or sets the packet count.
        /// </summary>
        /// <value>
        /// The packet count.
        /// </value>
        public long PacketCount
        {
            get
            {
                return _packetCount;
            }
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
            get
            {
                return _byteCount;
            }
            set
            {
                if( _byteCount != value )
                {
                    _byteCount = value;
                    OnPropertyChanged( nameof( ByteCount ) );
                }
            }
        }

        /// <summary>
        /// Updates the specified field.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>( ref T field, T value,
            [CallerMemberName] 
            string propertyName = null )

        {
            if(EqualityComparer<T>.Default.Equals( field, value ) )
            {
                return;
            }

            field = value;
            OnPropertyChanged( propertyName );
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            var _handler = PropertyChanged;
            _handler?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}