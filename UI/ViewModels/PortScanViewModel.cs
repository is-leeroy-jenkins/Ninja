// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-26-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-26-2024
// ******************************************************************************************
// <copyright file="PortScanViewModel.cs" company="Terry D. Eppler">
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
//   PortScanViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Models;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="MainWindowBase" />
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class PortScanViewModel : MainWindowBase
    {
        /// <summary>
        /// Gets or sets the port scan model.
        /// </summary>
        /// <value>
        /// The port scan model.
        /// </value>
        private protected PortScanModel _portScanModel;

        /// <summary>
        /// The scan token source
        /// </summary>
        private protected CancellationTokenSource _tokenSource;

        /// <summary>
        /// The cancel scan token
        /// </summary>
        private protected CancellationToken _cancelToken;

        /// <summary>
        /// The port count
        /// </summary>
        private protected int _portCount;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PortScanViewModel"/> class.
        /// </summary>
        public PortScanViewModel( )
        {
            _portScanModel = new PortScanModel( );
        }

        /// <summary>
        /// Gets or sets the port scan model.
        /// </summary>
        /// <value>
        /// The port scan model.
        /// </value>
        public PortScanModel PortScanModel
        {
            get
            {
                return _portScanModel;
            }
            private protected set
            {
                if( _portScanModel != value )
                {
                    _portScanModel = value;
                    OnPropertyChanged( nameof( PortScanModel ) );
                }
            }
        }

        /// <summary>
        /// The scan token source
        /// </summary>
        public CancellationTokenSource TokenSource
        {
            get
            {
                return _tokenSource;
            }
            private protected set
            {
                if( _tokenSource != value )
                {
                    _tokenSource = value;
                    OnPropertyChanged( nameof( TokenSource ) );
                }
            }
        }

        /// <summary>
        /// The cancel scan token
        /// </summary>
        public CancellationToken CancelToken
        {
            get
            {
                return _cancelToken;
            }
            private protected set
            {
                if( _cancelToken != value )
                {
                    _cancelToken = value;
                    OnPropertyChanged( nameof( CancelToken ) );
                }
            }
        }

        /// <summary>
        /// The port count
        /// </summary>
        public int PortCount
        {
            get
            {
                return _portCount;
            }
            private protected set
            {
                if( _portCount != value )
                {
                    _portCount = value;
                    OnPropertyChanged( nameof( PortCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the port scan results.
        /// </summary>
        /// <value>
        /// The port scan results.
        /// </value>
        public ObservableCollection<PortScanResult> PortScanResults { get; set; } =
            new ObservableCollection<PortScanResult>( );

        /// <summary>
        /// Scans the port.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        private void ScanPort( string ip, int port )
        {
            Console.WriteLine( "Begin Scan..." + port );
            PortScanModel.Port = port.ToString( );
            using( var _client = new TcpClient( ) )
            {
                if( _client.ConnectAsync( ip, port ).Wait( PortScanModel.SocketTimeout ) )
                {
                    Console.WriteLine( "port {0,5}tOpen.", port );
                    PortScanModel.OpenCnt++;
                    Application.Current.Dispatcher.Invoke( ( Action )( ( ) =>
                    {
                        PortScanResults.Add( new PortScanResult
                        {
                            Port = port,
                            Status = "open"
                        } );
                    } ) );
                }
                else
                {
                    Console.WriteLine( "port {0,5} Closed.", port );
                    PortScanModel.CloseCnt++;
                }
            }

            PortCount--;
            if( PortCount == 0 )
            {
                PortScanModel.ScanButtonName = "Start";
            }

            Console.WriteLine( "Port Scan Completed!" );
        }

        /// <summary>
        /// Starts the scan asynchronous.
        /// </summary>
        public async void StartScanAsync( )
        {
            _tokenSource = new CancellationTokenSource( );
            _cancelToken = _tokenSource.Token;
            var _startPortVal = PortScanModel.StartPort;
            var _stopPortVal = PortScanModel.StopPort;
            var _ipStr = PortScanModel.Ip;
            PortCount = _stopPortVal - _startPortVal;
            if( PortCount <= 0 )
            {
                PortScanModel.ScanButtonName = "Start";
                MessageBox.Show( "Please make sure (Start Port) < (Stop Port)!" );
                return;
            }

            try
            {
                await Task.Run( ( ) =>
                {
                    for( var _i = _startPortVal; _i <= _stopPortVal; _i++ )
                    {
                        Console.WriteLine( _i.ToString( ) );
                        if( _tokenSource.IsCancellationRequested )
                        {
                            break;
                        }

                        var _j = _i;
                        var _task = Task.Run( ( ) =>
                        {
                            ScanPort( _ipStr, _j );
                        }, _cancelToken );

                        Thread.Sleep( 5 );
                    }
                }, _cancelToken );
            }
            catch( Exception ex )
            {
            }
        }

        /// <summary>
        /// Stops the scan task.
        /// </summary>
        internal void StopScanTask( )
        {
            _tokenSource.Cancel( );
        }

        /// <summary>
        /// Ports the scan command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void PortScanCmd( object parameter )
        {
            if( PortScanModel.ScanButtonName == "Start" )
            {
                PortScanModel.ScanButtonName = "Stop";
                PortScanModel.OpenCnt = 0;
                PortScanModel.CloseCnt = 0;
                PortScanResults.Clear( );
                StartScanAsync( );
            }
            else
            {
                StopScanTask( );
                PortScanModel.ScanButtonName = "Start";
            }
        }

        /// <summary>
        /// Gets the port scan command.
        /// </summary>
        /// <value>
        /// The port scan command.
        /// </value>
        public ICommand PortScanCommand
        {
            get
            {
                return new RelayCommand( param => PortScanCmd( param ) );
            }
        }
    }
}