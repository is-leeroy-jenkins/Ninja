// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="RouteViewModel.cs" company="Terry D. Eppler">
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
//   RouteViewModel.cs
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

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class RouteViewModel : MainWindowBase
    {
        /// <summary>
        /// Gets or sets the route model.
        /// </summary>
        /// <value>
        /// The route model.
        /// </value>
        public RouteModel RouteModel { get; set; }

        /// <summary>
        /// The route process
        /// </summary>
        public ProcessInterface RouteProcess;

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
                return new RelayCommand( param => RunRoute( param ) );
            }
        }

        /// <summary>
        /// Gets or sets the ipv4 results.
        /// </summary>
        /// <value>
        /// The ipv4 results.
        /// </value>
        public ObservableCollection<Ipv4Result> Ipv4Results { get; set; } =
            new ObservableCollection<Ipv4Result>( );

        ///<summary>
        /// Gets or sets the ipv6 results.
        /// </summary>
        /// <value>
        /// The ipv6 results.
        /// </value>
        public ObservableCollection<Ipv6Result> Ipv6Results { get; set; } =
            new ObservableCollection<Ipv6Result>( );

        /// <summary>
        /// Gets the local network interface.
        /// </summary>
        /// <returns></returns>
        public NetInterfaceInfo[ ] GetLocalNetworkInterface( )
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces( );
            var _len = _interfaces.Length;
            var _info = new NetInterfaceInfo[ _len ];

            //string[] name = new string[len];
            for( var _i = 0; _i < _len; _i++ )
            {
                var _ni = _interfaces[ _i ];
                _info[ _i ] = new NetInterfaceInfo
                {
                    Description = _ni.Description
                };

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
        /// Processes the route information.
        /// </summary>
        /// <param name="str">The string.</param>
        public void ProcessRouteInfo( string str )
        {
            var _ipv6Pattern = @"(?<a>(\d+))\s*(?<b>(\d+))\s*(?<c>.* )\s*(?<d>.*)";
            var _ipv4Pattern =
                @"(?<a>(\d+)\.(\d+)\.(\d+)\.(\d+))\s*(?<b>(\d+)\.(\d+)\.(\d+)\.(\d+))\s*(?<c>.* )\s*(?<d>(\d+)\.(\d+)\.(\d+)\.(\d+))\s*(?<e>\d+)";

            if( str != null
                && !str.Contains( "..." ) )
            {
                var _m = Regex.Match( str, _ipv4Pattern );
                if( _m.Success )
                {
                    var _dest = _m.Groups[ "a" ].Value;
                    var _mask = _m.Groups[ "b" ].Value;
                    var _gateway = _m.Groups[ "c" ].Value;
                    var _interfaces = _m.Groups[ "d" ].Value;
                    var _metric = _m.Groups[ "e" ].Value;
                    Application.Current.Dispatcher.Invoke( ( Action )( ( ) =>
                    {
                        Ipv4Results.Add( new Ipv4Result
                        {
                            DestAddress = _dest,
                            Mask = _mask,
                            Gateway = _gateway.Trim( ),
                            Interface = _interfaces,
                            Metric = Convert.ToInt32( _metric )
                        } );
                    } ) );
                }
                else
                {
                }

                if( str.Contains( ":" ) )
                {
                    var _n = Regex.Match( str, _ipv6Pattern );
                    if( _n.Success )
                    {
                        var _interfaces = _n.Groups[ "a" ].Value;
                        var _metric = _n.Groups[ "b" ].Value;
                        var _dest = _n.Groups[ "c" ].Value;
                        var _gateway = _n.Groups[ "d" ].Value;
                        if( _dest == " "
                            && _gateway != "" )
                        {
                            _dest = _gateway;
                            _gateway = "";
                        }

                        Application.Current.Dispatcher.Invoke( ( Action )( ( ) =>
                        {
                            Ipv6Results.Add( new Ipv6Result
                            {
                                Interface = _interfaces,
                                Metric = Convert.ToInt32( _metric ),
                                DestAddress = _dest.Trim( ),
                                Gateway = _gateway
                            } );
                        } ) );
                    }
                    else
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Processes the output data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs"/> instance containing the event data.</param>
        private void ProcessOutputDataReceived( object sender, DataReceivedEventArgs e )
        {
            ProcessRouteInfo( e.Data );
            Console.WriteLine( e.Data );
        }

        /// <summary>
        /// Gets the route information.
        /// </summary>
        public void GetRouteInfo( )
        {
            var _routeCmd = "cmd.exe";
            RouteProcess.StartProcess( _routeCmd, "/c route print", ProcessOutputDataReceived );
        }

        /// <summary>
        /// Runs the route.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void RunRoute( object parameter )
        {
            Ipv4Results.Clear( );
            Ipv6Results.Clear( );
            GetRouteInfo( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteViewModel"/> class.
        /// </summary>
        public RouteViewModel( )
        {
            RouteModel = new RouteModel( );
            IpInterface = new IpInterface( );
            RouteProcess = new ProcessInterface( );
            GetRouteInfo( );
            RouteModel.NetInfoItemSource = GetLocalNetworkInterface( );
        }
    }
}