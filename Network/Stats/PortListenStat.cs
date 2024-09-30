// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-29-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-29-2024
// ******************************************************************************************
// <copyright file="PortListenStat.cs" company="Terry D. Eppler">
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
//   PortListenStat.cs
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
    public class PortListenStat : INotifyPropertyChanged
    {
        /// <summary>
        /// The local address
        /// </summary>
        private protected string _localAddress;

        /// <summary>
        /// The local port
        /// </summary>
        private protected string _localPort;

        /// <summary>
        /// The pid
        /// </summary>
        private protected int _pid;

        /// <summary>
        /// The program
        /// </summary>
        private protected string _program;

        /// <summary>
        /// The protocol
        /// </summary>
        private protected string _protocol;

        /// <summary>
        /// The remote port
        /// </summary>
        private protected string _remotePort;

        /// <summary>
        /// The status
        /// </summary>
        private protected string _status;

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
        /// <see cref="PortListenStat"/> class.
        /// </summary>
        public PortListenStat( )
        {
        }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public string Protocol
        {
            get
            {
                return _protocol;
            }
            set
            {
                if( _protocol != value )
                {
                    _protocol = value;
                    OnPropertyChanged( nameof( Protocol ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the local address.
        /// </summary>
        /// <value>
        /// The local address.
        /// </value>
        public string LocalAddress
        {
            get
            {
                return _localAddress;
            }
            set
            {
                if( _localAddress != value )
                {
                    _localAddress = value;
                    OnPropertyChanged( nameof( LocalAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the local port.
        /// </summary>
        /// <value>
        /// The local port.
        /// </value>
        public string LocalPort
        {
            get
            {
                return _localPort;
            }
            set
            {
                if( _localPort != value )
                {
                    _protocol = value;
                    OnPropertyChanged( nameof( Protocol ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the remote address.
        /// </summary>
        /// <value>
        /// The remote address.
        /// </value>
        public string RemoteAddress
        {
            get
            {
                return _remotePort;
            }
            set
            {
                if( _remotePort != value )
                {
                    _remotePort = value;
                    OnPropertyChanged( nameof( RemoteAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the remote port.
        /// </summary>
        /// <value>
        /// The remote port.
        /// </value>
        public string RemotePort
        {
            get
            {
                return _remotePort;
            }
            set
            {
                if( _remotePort != value )
                {
                    _remotePort = value;
                    OnPropertyChanged( nameof( RemotePort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if( _status != value )
                {
                    _status = value;
                    OnPropertyChanged( nameof( Status ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the pid.
        /// </summary>
        /// <value>
        /// The pid.
        /// </value>
        public int Pid
        {
            get
            {
                return _pid;
            }
            set
            {
                if( _pid != value )
                {
                    _pid = value;
                    OnPropertyChanged( nameof( Pid ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program
        {
            get
            {
                return _program;
            }
            set
            {
                if( _program != value )
                {
                    _program = value;
                    OnPropertyChanged( nameof( Program ) );
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}