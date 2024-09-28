// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="IperfModel.cs" company="Terry D. Eppler">
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
//   IperfModel.cs
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
    public class IperfModel : MainWindowBase
    {
        /// <summary>
        /// The band width
        /// </summary>
        private int _bandWidth;

        /// <summary>
        /// The band width unit
        /// </summary>
        private string _bandWidthUnit;

        /// <summary>
        /// The command
        /// </summary>
        private string _command;

        /// <summary>
        /// The interval
        /// </summary>
        private int _interval;

        /// <summary>
        /// The output
        /// </summary>
        private string _output;

        /// <summary>
        /// The packet length
        /// </summary>
        private int _packetLength;

        /// <summary>
        /// The parallel
        /// </summary>
        private int _parallel;

        /// <summary>
        /// The port
        /// </summary>
        private int _port;

        /// <summary>
        /// The reverse
        /// </summary>
        private bool _reverse;

        /// <summary>
        /// The role
        /// </summary>
        private string _role;

        /// <summary>
        /// The server ip
        /// </summary>
        private string _serverIp;

        /// <summary>
        /// The TCP flag
        /// </summary>
        private bool _tcpFlag;

        /// <summary>
        /// The TCP window size
        /// </summary>
        private int _tcpWindowSize;

        /// <summary>
        /// The TCP window unit
        /// </summary>
        private string _tcpWindowUnit;

        /// <summary>
        /// The throughput
        /// </summary>
        private double _throughput;

        /// <summary>
        /// The time
        /// </summary>
        private int _time;

        /// <summary>
        /// The UDP flag
        /// </summary>
        private bool _udpFlag;

        /// <summary>
        /// The version
        /// </summary>
        private string _version;

        /// <summary>
        /// Initializes a new instance of the <see cref="IperfModel"/> class.
        /// </summary>
        public IperfModel( )
        {
            _version = "iperf.exe";
            _role = "-c";
            _serverIp = "10.21.68.29";
            _port = 5001;
            _parallel = 4;
            _time = 60;
            _interval = 1;
            _tcpFlag = true;
            _tcpWindowSize = 2;
            _tcpWindowUnit = "M";
            _udpFlag = false;
            _bandWidth = 100;
            _bandWidthUnit = "M";
            _packetLength = 0;
            _reverse = false;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                if( _version != value )
                {
                    _version = value;
                    OnPropertyChanged( nameof( Version ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role
        {
            get
            {
                return _role;
            }
            set
            {
                if( _role != value )
                {
                    _role = value;
                    OnPropertyChanged( nameof( Role ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIp
        {
            get
            {
                return _serverIp;
            }
            set
            {
                if( _serverIp != value )
                {
                    _serverIp = value;
                    OnPropertyChanged( nameof( ServerIp ) );
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
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public int Time
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
        /// Gets or sets the parallel.
        /// </summary>
        /// <value>
        /// The parallel.
        /// </value>
        public int Parallel
        {
            get
            {
                return _parallel;
            }
            set
            {
                if( _parallel != value )
                {
                    _parallel = value;
                    OnPropertyChanged( nameof( Parallel ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                if( _interval != value )
                {
                    _interval = value;
                    OnPropertyChanged( nameof( Interval ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the TCP window.
        /// </summary>
        /// <value>
        /// The size of the TCP window.
        /// </value>
        public int TcpWindowSize
        {
            get
            {
                return _tcpWindowSize;
            }
            set
            {
                if( _tcpWindowSize != value )
                {
                    _tcpWindowSize = value;
                    OnPropertyChanged( nameof( TcpWindowSize ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the TCP window unit.
        /// </summary>
        /// <value>
        /// The TCP window unit.
        /// </value>
        public string TcpWindowUnit
        {
            get
            {
                return _tcpWindowUnit;
            }
            set
            {
                if( _tcpWindowUnit != value )
                {
                    _tcpWindowUnit = value;
                    OnPropertyChanged( nameof( TcpWindowUnit ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the band.
        /// </summary>
        /// <value>
        /// The width of the band.
        /// </value>
        public int BandWidth
        {
            get
            {
                return _bandWidth;
            }
            set
            {
                if( _bandWidth != value )
                {
                    _bandWidth = value;
                    OnPropertyChanged( nameof( BandWidth ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the band width unit.
        /// </summary>
        /// <value>
        /// The band width unit.
        /// </value>
        public string BandWidthUnit
        {
            get
            {
                return _bandWidthUnit;
            }
            set
            {
                if( _bandWidthUnit != value )
                {
                    _bandWidthUnit = value;
                    OnPropertyChanged( nameof( BandWidthUnit ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the length of the packet.
        /// </summary>
        /// <value>
        /// The length of the packet.
        /// </value>
        public int PacketLength
        {
            get
            {
                return _packetLength;
            }
            set
            {
                if( _packetLength != value )
                {
                    _packetLength = value;
                    OnPropertyChanged( nameof( PacketLength ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [TCP flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [TCP flag]; otherwise, <c>false</c>.
        /// </value>
        public bool TcpFlag
        {
            get
            {
                return _tcpFlag;
            }
            set
            {
                if( _tcpFlag != value )
                {
                    _tcpFlag = value;
                    OnPropertyChanged( nameof( TcpFlag ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [UDP flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [UDP flag]; otherwise, <c>false</c>.
        /// </value>
        public bool UdpFlag
        {
            get
            {
                return _udpFlag;
            }
            set
            {
                if( _udpFlag != value )
                {
                    _udpFlag = value;
                    OnPropertyChanged( nameof( UdpFlag ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IperfModel"/> is reverse.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reverse; otherwise, <c>false</c>.
        /// </value>
        public bool Reverse
        {
            get
            {
                return _reverse;
            }
            set
            {
                if( _reverse != value )
                {
                    _reverse = value;
                    OnPropertyChanged( nameof( Reverse ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the throughput.
        /// </summary>
        /// <value>
        /// The throughput.
        /// </value>
        public double Throughput
        {
            get
            {
                return _throughput;
            }
            set
            {
                if( _throughput != value )
                {
                    _throughput = value;
                    OnPropertyChanged( nameof( Throughput ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public string Command
        {
            get
            {
                return _command;
            }
            set
            {
                if( _command != value )
                {
                    _command = value;
                    OnPropertyChanged( nameof( Command ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>
        /// The output.
        /// </value>
        public string Output
        {
            get
            {
                return _output;
            }
            set
            {
                if( _output != value )
                {
                    _output = value;
                    OnPropertyChanged( nameof( Output ) );
                }
            }
        }
    }
}