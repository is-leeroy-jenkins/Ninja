// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="RouteModel.cs" company="Terry D. Eppler">
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
//   RouteModel.cs
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
    public class RouteModel : MainWindowBase
    {
        /// <summary>
        /// The ip
        /// </summary>
        private string _ipAddress;

        /// <summary>
        /// The net information item source
        /// </summary>
        private NetInterfaceInfo[ ] _netInfoItemSource;

        /// <summary>
        /// The offline count
        /// </summary>
        private int _offlineCount;

        /// <summary>
        /// The online count
        /// </summary>
        private int _onlineCount;

        /// <summary>
        /// The scan button name
        /// </summary>
        private string _scanButtonName;

        /// <summary>
        /// The start ip
        /// </summary>
        private string _startAddress;

        /// <summary>
        /// The stop ip
        /// </summary>
        private string _stopAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteModel"/> class.
        /// </summary>
        public RouteModel( )
        {
            _startAddress = "192.168.1.1";
            _stopAddress = "192.168.1.255";
            _scanButtonName = "Start";
            _offlineCount = 0;
            _onlineCount = 0;
        }

        /// <summary>
        /// Gets or sets the net information item source.
        /// </summary>
        /// <value>
        /// The net information item source.
        /// </value>
        public NetInterfaceInfo[ ] NetInfoItemSource
        {
            get
            {
                return _netInfoItemSource;
            }
            set
            {
                if( _netInfoItemSource != value )
                {
                    _netInfoItemSource = value;
                    OnPropertyChanged( nameof( NetInfoItemSource ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the start ip.
        /// </summary>
        /// <value>
        /// The start ip.
        /// </value>
        public string StartAddress
        {
            get
            {
                return _startAddress;
            }
            set
            {
                if( _startAddress != value )
                {
                    _startAddress = value;
                    OnPropertyChanged( nameof( StartAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the stop ip.
        /// </summary>
        /// <value>
        /// The stop ip.
        /// </value>
        public string StopAddress
        {
            get
            {
                return _stopAddress;
            }
            set
            {
                if( _stopAddress != value )
                {
                    _stopAddress = value;
                    OnPropertyChanged( nameof( StopAddress ) );
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
        /// Gets or sets the online count.
        /// </summary>
        /// <value>
        /// The online count.
        /// </value>
        public int OnlineCount
        {
            get
            {
                return _onlineCount;
            }
            set
            {
                if( _onlineCount != value )
                {
                    _onlineCount = value;
                    OnPropertyChanged( nameof( OnlineCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the offline count.
        /// </summary>
        /// <value>
        /// The offline count.
        /// </value>
        public int OfflineCount
        {
            get
            {
                return _offlineCount;
            }
            set
            {
                if( _offlineCount != value )
                {
                    _offlineCount = value;
                    OnPropertyChanged( nameof( OfflineCount ) );
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
    }
}