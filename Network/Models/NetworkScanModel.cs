// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="NetworkScanModel.cs" company="Terry D. Eppler">
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
//   NetworkScanModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Models
{
    using ViewModels;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Ninja.ViewModels.MainWindowBase" />
    public class NetworkScanModel : MainWindowBase
    {
        /// <summary>
        /// The net information item source
        /// </summary>
        private NetInterfaceInfo[ ] _netInfoItemSource;

        /// <summary>
        /// Gets or sets the net information item source.
        /// </summary>
        /// <value>
        /// The net information item source.
        /// </value>
        public NetInterfaceInfo[ ] NetInfoItemSource
        {
            get { return _netInfoItemSource; }
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
        /// The start ip
        /// </summary>
        private string _startIp;

        /// <summary>
        /// Gets or sets the start ip.
        /// </summary>
        /// <value>
        /// The start ip.
        /// </value>
        public string StartIp
        {
            get { return _startIp; }
            set
            {
                if( _startIp != value )
                {
                    _startIp = value;
                    OnPropertyChanged( nameof( StartIp ) );
                }
            }
        }

        /// <summary>
        /// The stop ip
        /// </summary>
        private string _stopIp;

        /// <summary>
        /// Gets or sets the stop ip.
        /// </summary>
        /// <value>
        /// The stop ip.
        /// </value>
        public string StopIp
        {
            get { return _stopIp; }
            set
            {
                if( _stopIp != value )
                {
                    _stopIp = value;
                    OnPropertyChanged( nameof( StopIp ) );
                }
            }
        }

        /// <summary>
        /// The ip
        /// </summary>
        private string _ip;

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        public string Ip
        {
            get { return _ip; }
            set
            {
                if( _ip != value )
                {
                    _ip = value;
                    OnPropertyChanged( nameof( Ip ) );
                }
            }
        }

        /// <summary>
        /// The online count
        /// </summary>
        private int _onlineCnt;

        /// <summary>
        /// Gets or sets the online count.
        /// </summary>
        /// <value>
        /// The online count.
        /// </value>
        public int OnlineCnt
        {
            get { return _onlineCnt; }
            set
            {
                if( _onlineCnt != value )
                {
                    _onlineCnt = value;
                    OnPropertyChanged( nameof( OnlineCnt ) );
                }
            }
        }

        /// <summary>
        /// The offline count
        /// </summary>
        private int _offlineCnt;

        /// <summary>
        /// Gets or sets the offline count.
        /// </summary>
        /// <value>
        /// The offline count.
        /// </value>
        public int OfflineCnt
        {
            get { return _offlineCnt; }
            set
            {
                if( _offlineCnt != value )
                {
                    _offlineCnt = value;
                    OnPropertyChanged( nameof( OfflineCnt ) );
                }
            }
        }

        /// <summary>
        /// The scan button name
        /// </summary>
        private string _scanButtonName;

        /// <summary>
        /// Gets or sets the name of the scan button.
        /// </summary>
        /// <value>
        /// The name of the scan button.
        /// </value>
        public string ScanButtonName
        {
            get { return _scanButtonName; }
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
        /// Initializes a new instance of the <see cref="NetworkScanModel"/> class.
        /// </summary>
        public NetworkScanModel( )
        {
            StartIp = "192.168.1.1";
            StopIp = "192.168.1.255";
            ScanButtonName = "Start";
            OfflineCnt = 0;
            OnlineCnt = 0;
        }
    }
}