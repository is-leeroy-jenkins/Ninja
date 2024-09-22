// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="PortViewModel.cs" company="Terry D. Eppler">
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
//   PortViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class PortViewModel : MainWindowBase
    {
        /// <summary>
        /// Gets or sets the port model.
        /// </summary>
        /// <value>
        /// The port model.
        /// </value>
        public PortModel PortModel { get; set; }

        /// <summary>
        /// The port process
        /// </summary>
        public ProcessInterface PortProcess;

        /// <summary>
        /// Gets or sets the port listen stats.
        /// </summary>
        /// <value>
        /// The port listen stats.
        /// </value>
        public ObservableCollection<PortListenStat> PortListenStats { get; set; } =
            new ObservableCollection<PortListenStat>( );

        /// <summary>
        /// Gets the local network interface.
        /// </summary>
        /// <returns></returns>
        public PortModel.NetInterfaceInfo[ ] GetLocalNetworkInterface( )
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces( );
            var _len = _interfaces.Length;
            var _info = new PortModel.NetInterfaceInfo[ _len ];

            //string[] name = new string[len];
            for( var _i = 0; _i < _len; _i++ )
            {
                var _ni = _interfaces[ _i ];
                _info[ _i ] = new PortModel.NetInterfaceInfo( );
                _info[ _i ].Description = _ni.Description;
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
                            _info[ _i ].Name = _niname;
                            _info[ _i ].Ip = _address;
                            _info[ _i ].Mask = _mask;
                        }
                    }
                }
            }

            return _info;
        }

        /// <summary>
        /// The ip interface
        /// </summary>
        public IpInterface IpInterface;

        /// <summary>
        /// Lookups the process.
        /// </summary>
        /// <param name="pid">The pid.</param>
        /// <returns></returns>
        public static string LookupProcess( int pid )
        {
            string _procName;
            try { _procName = Process.GetProcessById( pid ).ProcessName; }
            catch( Exception ) { _procName = "-"; }

            return _procName + ".exe";
        }

        /// <summary>
        /// Processes the port information.
        /// </summary>
        /// <param name="str">The string.</param>
        public void ProcessPortInfo( string str )
        {
            var _tcpIpv4Pattern =
                @"(?<a>[A-Za-z]+)\s*(?<b>(\d+)\.(\d+)\.(\d+)\.(\d+))\:(?<c>(\d+))\s*(?<d>(\d+)\.(\d+)\.(\d+)\.(\d+))\:(?<e>(\d+))\s*(?<f>.* )\s*(?<g>\d+)";

            var _tcpIpv6Pattern =
                @"(?<a>[A-Za-z]+)\s*(?<b>\[.*\])\:(?<c>(\d+))\s*(?<d>\[.*\])\:(?<e>(\d+))\s*(?<f>[A-Za-z]+)\s*(?<g>\d+)";

            var _udpIpv4Pattern =
                @"(?<a>[A-Za-z]+)\s*(?<b>(\d+)\.(\d+)\.(\d+)\.(\d+))\:(?<c>(\d+))\s*(?<d>.* )\s*(?<e>(\d+))";

            var _udpIpv6Pattern =
                @"(?<a>[A-Za-z]+)\s*(?<b>\[.*\])\:(?<c>(\d+))\s*(?<d>.* )\s*(?<e>(\d+))";

            var _protocol = "";
            var _local = "";
            var _localPort = "";
            var _remote = "";
            var _remotePort = "";
            var _status = "";
            var _program = "";
            var _pid = 0;
            try
            {
                var _match = Match.Empty;
                if( str != null
                    && str.Contains( "TCP" ) )
                {
                    var _m = Regex.Match( str, _tcpIpv4Pattern );
                    var _n = Regex.Match( str, _tcpIpv6Pattern );
                    if( _m.Success )
                    {
                        _match = _m;
                    }
                    else if( _n.Success )
                    {
                        _match = _n;
                    }

                    _remotePort = _m.Groups[ "e" ].Value;
                    _status = _m.Groups[ "f" ].Value;
                    _pid = int.Parse( _match.Groups[ "g" ].Value );
                    _protocol = _match.Groups[ "a" ].Value;
                    _local = _match.Groups[ "b" ].Value;
                    _localPort = _match.Groups[ "c" ].Value;
                    _remote = _match.Groups[ "d" ].Value;
                    _program = PortViewModel.LookupProcess( _pid );
                    Application.Current.Dispatcher.Invoke( ( Action )( ( ) =>
                    {
                        PortListenStats.Add( new PortListenStat
                        {
                            Protocol = _protocol,
                            LocalAddress = _local,
                            LocalPort = _localPort,
                            RemoteAddress = _remote,
                            RemotePort = _remotePort,
                            Status = _status,
                            Pid = _pid,
                            Program = _program
                        } );
                    } ) );
                }
                else if( str != null
                    && str.Contains( "UDP" ) )
                {
                    var _s = Regex.Match( str, _udpIpv4Pattern );
                    var _t = Regex.Match( str, _udpIpv6Pattern );
                    if( _s.Success )
                    {
                        _match = _s;
                    }
                    else if( _t.Success )
                    {
                        _match = _t;
                    }

                    _remotePort = "";
                    _status = "";
                    _pid = int.Parse( _match.Groups[ "e" ].Value );
                    _protocol = _match.Groups[ "a" ].Value;
                    _local = _match.Groups[ "b" ].Value;
                    _localPort = _match.Groups[ "c" ].Value;
                    _remote = _match.Groups[ "d" ].Value;
                    _program = PortViewModel.LookupProcess( _pid );
                    Application.Current.Dispatcher.Invoke( ( Action )( ( ) =>
                    {
                        PortListenStats.Add( new PortListenStat
                        {
                            Protocol = _protocol,
                            LocalAddress = _local,
                            LocalPort = _localPort,
                            RemoteAddress = _remote,
                            RemotePort = _remotePort,
                            Status = _status,
                            Pid = _pid,
                            Program = _program
                        } );
                    } ) );
                }
            }
            catch( Exception e )
            {
                // ignore
            }
        }

        /// <summary>
        /// Processes the output data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs"/> instance containing the event data.</param>
        private void ProcessOutputDataReceived( object sender, DataReceivedEventArgs e )
        {
            ProcessPortInfo( e.Data );
            Console.WriteLine( e.Data );
        }

        /// <summary>
        /// Gets the port information.
        /// </summary>
        public void GetPortInfo( )
        {
            var _routeCmd = "cmd.exe";
            PortProcess.StartProcess( _routeCmd, "/c netstat -aon", ProcessOutputDataReceived );
        }

        /// <summary>
        /// Runs the port command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void RunPortCmd( object parameter )
        {
            PortListenStats.Clear( );
            GetPortInfo( );
        }

        /// <summary>
        /// Gets the refresh command.
        /// </summary>
        /// <value>
        /// The refresh command.
        /// </value>
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( param => RunPortCmd( param ) );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortViewModel"/> class.
        /// </summary>
        public PortViewModel( )
        {
            PortModel = new PortModel( );
            IpInterface = new IpInterface( );
            PortProcess = new ProcessInterface( );
            GetPortInfo( );
            PortModel.NetInfoItemSource = GetLocalNetworkInterface( );
        }
    }
}