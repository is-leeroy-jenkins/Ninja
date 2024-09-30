// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-30-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-30-2024
// ******************************************************************************************
// <copyright file="TcpServerInfo.cs" company="Terry D. Eppler">
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
//   TcpServerInfo.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
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
    public class TcpServerInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// The server ip
        /// </summary>
        private protected string _ipAddress;

        /// <summary>
        /// The server listen BTN name
        /// </summary>
        private protected string _listenButtonName;

        /// <summary>
        /// The listen port
        /// </summary>
        private protected string _listenPort;

        /// <summary>
        /// The local port
        /// </summary>
        private protected string _localPort;

        /// <summary>
        /// The server port
        /// </summary>
        private protected string _port;

        /// <summary>
        /// The server recv
        /// </summary>
        private protected string _recv;

        /// <summary>
        /// The server send
        /// </summary>
        private protected string _send;

        /// <summary>
        /// The server send BTN name
        /// </summary>
        private protected string _sendButtonName;

        /// <summary>
        /// The server send interval
        /// </summary>
        private protected int _sendInterval;

        /// <summary>
        /// The server send string
        /// </summary>
        private protected string _sendStr;

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
        /// <see cref="TcpServerInfo"/> class.
        /// </summary>
        public TcpServerInfo( )
        {
            _port = "65432";
            _sendStr = "Hello Client!";
            _sendInterval = 1000;
            _send = "Hello Client!";
            _sendButtonName = "Auto Send Start";
            _listenButtonName = "Start Listen";
            _ipAddress = "127.0.0.1";
            _listenPort = "65432";
            _localPort = "0";
        }

        /// <summary>
        /// Gets or sets the recv.
        /// </summary>
        /// <value>
        /// The recv.
        /// </value>
        public int SendInterval
        {
            get
            {
                return _sendInterval;
            }
            set
            {
                if( _sendInterval != value )
                {
                    _sendInterval = value;
                    OnPropertyChanged( nameof( SendInterval ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the send.
        /// </summary>
        /// <value>
        /// The send.
        /// </value>
        public string Send
        {
            get
            {
                return _send;
            }
            set
            {
                if( _send != value )
                {
                    _send = value;
                    OnPropertyChanged( nameof( Send ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the recv.
        /// </summary>
        /// <value>
        /// The recv.
        /// </value>
        public string Recv
        {
            get
            {
                return _recv;
            }
            set
            {
                if( _recv != value )
                {
                    _recv = value;
                    OnPropertyChanged( nameof( Recv ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the remote ip.
        /// </summary>
        /// <value>
        /// The remote ip.
        /// </value>
        public string IpAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                if( _ipAddress != value )
                {
                    _ipAddress = value;
                    OnPropertyChanged( nameof( IpAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public string Port
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
        /// <typeparam name="_"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>(ref T field, T value,
            [CallerMemberName] string propertyName = null)

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
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var _handler = PropertyChanged;
            _handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}