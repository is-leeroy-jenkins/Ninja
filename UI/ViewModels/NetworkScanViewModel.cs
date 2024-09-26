// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="NetworkScanViewModel.cs" company="Terry D. Eppler">
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
//   NetworkScanViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class NetworkScanViewModel : MainWindowBase
    {
        /// <summary>
        /// Gets or sets the network scan model.
        /// </summary>
        /// <value>
        /// The network scan model.
        /// </value>
        public NetworkScanModel NetworkScanModel { get; set; }

        /// <summary>
        /// The ip interface
        /// </summary>
        private protected IpInterface IpInterface;

        /// <summary>
        /// The ip count
        /// </summary>
        private protected int IpCnt;

        /// <summary>
        /// The scan token source
        /// </summary>
        private protected CancellationTokenSource scanTokenSource;

        /// <summary>
        /// The cancel scan token
        /// </summary>
        private protected CancellationToken cancelScanToken;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NetworkScanViewModel"/> class.
        /// </summary>
        public NetworkScanViewModel()
        {
            NetworkScanModel = new NetworkScanModel();
            IpInterface = new IpInterface();
            NetworkScanModel.NetInfoItemSource = GetLocalNetworkInterface();
        }
        
        /// <summary>
        /// Gets or sets the ip scan results.
        /// </summary>
        /// <value>
        /// The ip scan results.
        /// </value>
        public ObservableCollection<IpScanResult> IpScanResults { get; set; } = 
            new ObservableCollection<IpScanResult>( );

        /// <summary>
        /// Does the scan.
        /// </summary>
        /// <param name="ip">The ip.</param>
        private void DoScan( string ip )
        {
            PingReply _reply = null;
            string _status = null;
            string _hostname = null;
            string _time = null;
            string _ttl = null;
            Console.WriteLine( ip );
            _reply = IpInterface.PingSweep( ip );
            NetworkScanModel.Ip = ip;
            if( _reply != null )
            {
                if( _reply.Status == IPStatus.Success )
                {
                    _status = "Online";
                    _hostname = IpInterface.GetHostName( ip );
                    _time = _reply.RoundtripTime.ToString( );
                    _ttl = _reply.Options.Ttl.ToString( );
                    Application.Current.Dispatcher.Invoke( ( Action )( ( ) =>
                    {
                        IpScanResults.Add( new IpScanResult
                        {
                            IpAddress = ip,
                            Name = _hostname,
                            Status = _status,
                            Time = _time,
                            Ttl = _ttl
                        } );
                    } ) );

                    NetworkScanModel.OnlineCnt++;
                }
                else
                {
                    _status = "Timeout";
                    NetworkScanModel.OfflineCnt++;
                }
            }
            else
            {
                _status = "Offline";
                NetworkScanModel.OfflineCnt++;
            }

            IpCnt--;
            Console.WriteLine( IpCnt.ToString( ) );
            if( IpCnt == 0 )
            {
                NetworkScanModel.ScanButtonName = "Start";
            }
        }

        /// <summary>
        /// Starts the scan asynchronous.
        /// </summary>
        public async void StartScanAsync( )
        {
            scanTokenSource = new CancellationTokenSource( );
            cancelScanToken = scanTokenSource.Token;
            var _startIpVal = IpInterface.IpAddressToLongBackwards( NetworkScanModel.StartIp );
            var _startIpStr = IpInterface.LongToIpAddress( _startIpVal );
            var _endIpVal = IpInterface.IpAddressToLongBackwards( NetworkScanModel.StopIp );
            var _endIpStr = IpInterface.LongToIpAddress( _endIpVal );
            IpCnt = ( int )( _endIpVal - _startIpVal );
            if( IpCnt <= 0 )
            {
                MessageBox.Show( "Please make sure (Start Ip) < (Stop Ip)!" );
                return;
            }

            try
            {
                await Task.Run( ( ) =>
                {
                    for( var _i = _startIpVal; _i <= _endIpVal; _i++ )
                    {
                        Console.WriteLine( _i.ToString( ) );
                        var _j = _i;
                        var _ipStr = IpInterface.LongToIpAddress( _j );
                        var _task = Task.Run( ( ) =>
                        {
                            DoScan( _ipStr );
                        }, cancelScanToken );

                        Thread.Sleep( 1 );
                    }
                }, cancelScanToken );
            }
            catch( Exception ex )
            {
            }
        }

        /// <summary>
        /// Stops the scan task.
        /// </summary>
        public void StopScanTask( )
        {
            scanTokenSource.Cancel( );
        }

        /// <summary>
        /// Ips the scan command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void IpScanCmd( object parameter )
        {
            if( NetworkScanModel.ScanButtonName == "Start" )
            {
                NetworkScanModel.ScanButtonName = "Stop";
                NetworkScanModel.OfflineCnt = 0;
                NetworkScanModel.OnlineCnt = 0;
                IpScanResults.Clear( );

                //Do Scan
                StartScanAsync( );
            }
            else
            {
                //Do Stop
                StopScanTask( );
                NetworkScanModel.ScanButtonName = "Start";
            }
        }

        /// <summary>
        /// Gets the ip scan command.
        /// </summary>
        /// <value>
        /// The ip scan command.
        /// </value>
        public ICommand IpScanCommand
        {
            get
            {
                return new RelayCommand( param => IpScanCmd( param ) );
            }
        }

        /// <summary>
        /// Gets the local network interface.
        /// </summary>
        /// <returns></returns>
        public NetInterfaceInfo[ ] GetLocalNetworkInterface( )
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces( );
            var _len = _interfaces.Length;
            var _info = new NetInterfaceInfo[ _len ];
            var _j = 0;
            for( var _i = 0; _i < _len; _i++ )
            {
                var _ni = _interfaces[ _i ];
                if( _ni.OperationalStatus == OperationalStatus.Up )
                {
                    var _property = _ni.GetIPProperties( );
                    foreach( var _ip in _property.UnicastAddresses )
                    {
                        if( _ip.Address.AddressFamily == AddressFamily.InterNetwork )
                        {
                            var _address = _ip.Address.ToString( );
                            var _niname = _ni.Name;
                            var _mask = _ip.IPv4Mask.ToString( );
                            _info[ _j ] = new NetInterfaceInfo
                            {
                                Description = _ni.Description,
                                Name = _niname,
                                Ip = _address,
                                Mask = _mask
                            };

                            _j++;
                        }
                    }
                }
            }

            return _info;
        }
    }
}