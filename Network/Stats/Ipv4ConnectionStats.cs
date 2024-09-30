// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-29-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-29-2024
// ******************************************************************************************
// <copyright file="Ipv4ConnectionStats.cs" company="Terry D. Eppler">
// 
//     Ninja is a network toolkit that supports Iperf, TCP, UDP, Websocket, MQTT,
//     Sniffer, Pcap, Port Scan, Listen, IP Scan .etc.
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PcapDotNet.Packets.IpV4;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public class Ipv4ConnectionStats : INotifyPropertyChanged
    {
        /// <summary>
        /// The address a
        /// </summary>
        private protected IpV4Address _addressA;

        /// <summary>
        /// The address b
        /// </summary>
        private protected IpV4Address _addressB;

        /// <summary>
        /// The byte count a to b
        /// </summary>
        private protected long _byteCountAToB;

        /// <summary>
        /// The byte count b to a
        /// </summary>
        private protected long _byteCountBToA;

        /// <summary>
        /// The packet count a to b
        /// </summary>
        private protected long _packetCountAToB;

        /// <summary>
        /// The packet count b to a
        /// </summary>
        private protected long _packetCountBToA;

        /// <summary>
        /// Updates the specified field.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>( ref T field, T value,
            [ CallerMemberName ] 
            string propertyName = null )

        {
            if( EqualityComparer<T>.Default.Equals( field, value ) )
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

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ipv4ConnectionStats"/> class.
        /// </summary>
        /// <param name="addressA">The address a.</param>
        /// <param name="addressB">The address b.</param>
        public Ipv4ConnectionStats( IpV4Address addressA, IpV4Address addressB )
        {
            _addressA = addressA;
            _addressB = addressB;
            _packetCountAToB = 0;
            _packetCountBToA = 0;
            _byteCountAToB = 0;
            _byteCountBToA = 0;
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
        public IpV4Address AddressA
        {
            get
            {
                return _addressA;
            }
            set
            {
                if( _addressA != value )
                {
                    _addressA = value;
                    OnPropertyChanged( nameof( AddressA ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the address b.
        /// </summary>
        /// <value>
        /// The address b.
        /// </value>
        public IpV4Address AddressB
        {
            get
            {
                return _addressB;
            }
            set
            {
                if( _addressB != value )
                {
                    _addressB = value;
                    OnPropertyChanged( nameof( AddressB ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the packet count a to b.
        /// </summary>
        /// <value>
        /// The packet count a to b.
        /// </value>
        public long PacketCountAToB
        {
            get
            {
                return _packetCountAToB;
            }
            set
            {
                if( _packetCountAToB != value )
                {
                    _packetCountAToB = value;
                    OnPropertyChanged( nameof( PacketCountAToB ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the packet count b to a.
        /// </summary>
        /// <value>
        /// The packet count b to a.
        /// </value>
        public long PacketCountBToA
        {
            get
            {
                return _packetCountBToA;
            }
            set
            {
                if( _packetCountBToA != value )
                {
                    _packetCountBToA = value;
                    OnPropertyChanged( nameof( PacketCountBToA ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the byte count a to b.
        /// </summary>
        /// <value>
        /// The byte count a to b.
        /// </value>
        public long ByteCountAToB
        {
            get
            {
                return _byteCountAToB;
            }
            set
            {
                if( _byteCountAToB != value )
                {
                    _byteCountAToB = value;
                    OnPropertyChanged( nameof( ByteCountAToB ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the byte count b to a.
        /// </summary>
        /// <value>
        /// The byte count b to a.
        /// </value>
        public long ByteCountBToA
        {
            get
            {
                return _byteCountBToA;
            }
            set
            {
                if( _byteCountBToA != value )
                {
                    _byteCountBToA = value;
                    OnPropertyChanged( nameof( ByteCountBToA ) );
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}