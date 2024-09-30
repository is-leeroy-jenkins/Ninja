// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-29-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-29-2024
// ******************************************************************************************
// <copyright file="UdpClientInfo.cs" company="Terry D. Eppler">
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
//   UdpClientInfo.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class UdpClientInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// The port
        /// </summary>
        private protected int _port;

        /// <summary>
        /// The recv bytes
        /// </summary>
        private protected int _recvBytes;

        /// <summary>
        /// The remote address
        /// </summary>
        private protected string _remoteAddress;

        /// <summary>
        /// The time
        /// </summary>
        private protected DateTime _time;

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UdpClientInfo"/> class.
        /// </summary>
        public UdpClientInfo( )
        {
        }

        /// <summary>
        /// Gets or sets the remote ip.
        /// </summary>
        /// <value>
        /// The remote ip.
        /// </value>
        public string RemoteAddress
        {
            get
            {
                return _remoteAddress;
            }
            set
            {
                if( _remoteAddress != value )
                {
                    _remoteAddress = value;
                    OnPropertyChanged( nameof( RemoteAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                if( _port != value )
                {
                    _port = value;
                    OnPropertyChanged( nameof( Port ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the recv bytes.
        /// </summary>
        /// <value>
        /// The recv bytes.
        /// </value>
        public int RecvBytes
        {
            get
            {
                return _recvBytes;
            }
            set
            {
                if( _recvBytes != value )
                {
                    _recvBytes = value;
                    OnPropertyChanged( nameof( RecvBytes ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                if( _time != value )
                {
                    _time = value;
                    OnPropertyChanged( nameof( Time ) );
                }
            }
        }

        /// <summary>
        /// Updates the specified field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>(ref T field, T value,
            [ CallerMemberName ]
            string propertyName = null)

        {
            if(EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            var _handler = PropertyChanged;
            _handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}