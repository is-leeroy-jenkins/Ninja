// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="PortScanModel.cs" company="Terry D. Eppler">
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
//   PortScanModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Models
{
    using System.Diagnostics.CodeAnalysis;
    using ViewModels;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class PortScanModel : MainWindowBase
    {
        /// <summary>
        /// The close count
        /// </summary>
        private int _closeCount;

        /// <summary>
        /// The ip
        /// </summary>
        private string _ipAddress;

        /// <summary>
        /// The open count
        /// </summary>
        private int _openCount;

        /// <summary>
        /// The port
        /// </summary>
        private string _port;

        /// <summary>
        /// The scan button name
        /// </summary>
        private string _scanButtonName;

        /// <summary>
        /// The socket timeout
        /// </summary>
        private int _socketTimeout;

        /// <summary>
        /// The start port
        /// </summary>
        private int _startPort;

        /// <summary>
        /// The stop port
        /// </summary>
        private int _stopPort;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PortScanModel"/> class.
        /// </summary>
        public PortScanModel( )
        {
            _ipAddress = "192.168.1.1";
            _startPort = 1;
            _stopPort = 65535;
            _socketTimeout = 1000;
            _scanButtonName = "Start";
            _closeCount = 0;
            _openCount = 0;
        }

        /// <summary>
        /// Gets or sets the start port.
        /// </summary>
        /// <value>
        /// The start port.
        /// </value>
        public int StartPort
        {
            get
            {
                return _startPort;
            }
            set
            {
                if( _startPort != value )
                {
                    _startPort = value;
                    OnPropertyChanged( nameof( StartPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the stop port.
        /// </summary>
        /// <value>
        /// The stop port.
        /// </value>
        public int StopPort
        {
            get
            {
                return _stopPort;
            }
            set
            {
                if( _stopPort != value )
                {
                    _stopPort = value;
                    OnPropertyChanged( nameof( StopPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
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
        /// Gets or sets the open count.
        /// </summary>
        /// <value>
        /// The open count.
        /// </value>
        public int OpenCount
        {
            get
            {
                return _openCount;
            }
            set
            {
                if( _openCount != value )
                {
                    _openCount = value;
                    OnPropertyChanged( nameof( OpenCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the close count.
        /// </summary>
        /// <value>
        /// The close count.
        /// </value>
        public int CloseCount
        {
            get
            {
                return _closeCount;
            }
            set
            {
                if( _closeCount != value )
                {
                    _closeCount = value;
                    OnPropertyChanged( nameof( CloseCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the scan button.
        /// </summary>
        /// <value>
        /// The name of the scan button.
        /// </value>
        public string ScanButtonName
        {
            get
            {
                return _scanButtonName;
            }
            set
            {
                if( _scanButtonName != value )
                {
                    _scanButtonName = value;
                    OnPropertyChanged( nameof( ScanButtonName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the socket timeout.
        /// </summary>
        /// <value>
        /// The socket timeout.
        /// </value>
        public int SocketTimeout
        {
            get
            {
                return _socketTimeout;
            }
            set
            {
                if( _socketTimeout != value )
                {
                    _socketTimeout = value;
                    OnPropertyChanged( nameof( SocketTimeout ) );
                }
            }
        }
    }
}