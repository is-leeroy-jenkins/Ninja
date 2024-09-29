// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="WebSocketViewModel.cs" company="Terry D. Eppler">
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
//   WebSocketViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using WebSocketSharp.Server;
    using WebSocketSharp;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class WebSocketViewModel : MainWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketViewModel"/> class.
        /// </summary>
        public WebSocketViewModel( )
        {
            WebSocketModel = new WebSocketModel( );
        }

        /// <summary>
        /// Gets or sets the web socket model.
        /// </summary>
        /// <value>
        /// The web socket model.
        /// </value>
        public WebSocketModel WebSocketModel { get; set; }

        #region WebSocket Server
        /// <summary>
        /// The ws server
        /// </summary>
        private WebSocketServer _wsServer;

        /// <summary>
        /// Gets or sets the ws recv.
        /// </summary>
        /// <value>
        /// The ws recv.
        /// </value>
        public static ObservableCollection<string> WsRecv { get; set; } =
            new ObservableCollection<string>( );

        /// <summary>
        /// Gets the start listen command.
        /// </summary>
        /// <value>
        /// The start listen command.
        /// </value>
        public ICommand StartListenCommand
        {
            get
            {
                return new RelayCommand( param => StartListen( param ) );
            }
        }

        /// <summary>
        /// Starts the listen.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void StartListen( object parameter )
        {
            if( WebSocketModel.ServerListenButtonName == "Start Listen" )
            {
                WebSocketModel.ServerListenButtonName = "Stop Listen";
                _wsServer = new WebSocketServer( WebSocketModel.ServerAddress );
                _wsServer.AddWebSocketService<EchoHandler>( "/echo" );
                _wsServer.Start( );
                WsRecv.Add( "[" + DateTime.Now + "][" + "WebSocket Server Started]\n" );
            }
            else
            {
                WebSocketModel.ServerListenButtonName = "Start Listen";
                _wsServer.Stop( );
                WsRecv.Add( "[" + DateTime.Now + "][" + "WebSocket Server Stopped]\n" );
            }
        }

        /// <summary>
        /// Gets the server automatic send command.
        /// </summary>
        /// <value>
        /// The server automatic send command.
        /// </value>
        public ICommand ServerAutoSendCommand
        {
            get
            {
                return new RelayCommand( param => ServerAutoSend( param ) );
            }
        }

        /// <summary>
        /// The m server automatic send timer
        /// </summary>
        private DispatcherTimer _mServerAutoSendTimer;

        /// <summary>
        /// Servers the automatic send timer function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ServerAutoSendTimerFunc( object sender, EventArgs e )
        {
            _wsServer.WebSocketServices.Broadcast( WebSocketModel.ServerSendStr );
        }

        /// <summary>
        /// Servers the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerAutoSend( object parameter )
        {
            if( WebSocketModel.ServerSendButtonName == "Auto Send Start" )
            {
                WebSocketModel.ServerSendButtonName = "Auto Send Stop";
                _mServerAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        WebSocketModel.ServerSendInterval )
                };

                _mServerAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _mServerAutoSendTimer.Start( );
            }
            else
            {
                WebSocketModel.ServerSendButtonName = "Auto Send Start";
                _mServerAutoSendTimer.Stop( );
            }
        }

        /// <summary>
        /// Gets the server send clear command.
        /// </summary>
        /// <value>
        /// The server send clear command.
        /// </value>
        public ICommand ServerSendClearCommand
        {
            get
            {
                return new RelayCommand( param => ServerSendClear( param ) );
            }
        }

        /// <summary>
        /// Servers the send clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerSendClear( object parameter )
        {
            WebSocketModel.ServerSend = "";
            WsRecv.Clear( );
        }

        /// <summary>
        /// Gets the server send command.
        /// </summary>
        /// <value>
        /// The server send command.
        /// </value>
        public ICommand ServerSendCommand
        {
            get
            {
                return new RelayCommand( param => ServerSend( param ) );
            }
        }

        /// <summary>
        /// Servers the send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerSend( object parameter )
        {
            _wsServer.WebSocketServices.Broadcast( WebSocketModel.ServerSend );
        }
        #endregion

        #region WebSocket Client
        /// <summary>
        /// The ws client
        /// </summary>
        public WebSocket WsClient;

        /// <summary>
        /// Gets or sets the ws client recv.
        /// </summary>
        /// <value>
        /// The ws client recv.
        /// </value>
        public static ObservableCollection<string> WsClientRecv { get; set; } =
            new ObservableCollection<string>( );

        /// <summary>
        /// Gets the client connect command.
        /// </summary>
        /// <value>
        /// The client connect command.
        /// </value>
        public ICommand ClientConnectCommand
        {
            get
            {
                return new RelayCommand( param => ClientConnect( param ) );
            }
        }

        /// <summary>
        /// Clients the connect.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientConnect( object parameter )
        {
            if( WebSocketModel.ClientConnectButtonName == "Connect" )
            {
                using( var _ws = new WebSocket( WebSocketModel.ClientAddress ) )
                {
                    _ws.OnOpen += ( sender, e ) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Open]\n";
                            WsClientRecv.Add( _time + _str );
                        } ) );
                    };

                    _ws.OnMessage += ( sender, e ) =>
                    {
                        var _fmt = "[WebSocket Message] {0}";
                        var _body = !e.IsPing
                            ? e.Data
                            : "A ping was received.";

                        Console.WriteLine( _fmt, _body );
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[" + e.Data + "]\n";
                            WsClientRecv.Add( _time + _str );
                        } ) );
                    };

                    _ws.OnError += ( sender, e ) =>
                    {
                        var _fmt = "[WebSocket Error] {0}";
                        Console.WriteLine( _fmt, e.Message );
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Error][" + e.Message + "]\n";
                            WsClientRecv.Add( _time + _str );
                            WebSocketModel.ClientConnectButtonName = "Connect";
                        } ) );
                    };

                    _ws.OnClose += ( sender, e ) =>
                    {
                        var _fmt = "[WebSocket Close ({0})] {1}";
                        Console.WriteLine( _fmt, e.Code, e.Reason );
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Close][" + e.Reason + "]\n";
                            WsClientRecv.Add( _time + _str );
                            WebSocketModel.ClientConnectButtonName = "Connect";
                        } ) );
                    };

                    // Connect to the server.
                    WsClient = _ws;
                }

                try
                {
                    Task.Run( ( ) =>
                    {
                        WsClient.Connect( );
                        WebSocketModel.ClientConnectButtonName = "DisConnect";
                    } );
                }
                catch( Exception )
                {
                    // ignore
                }
            }
            else
            {
                WebSocketModel.ClientConnectButtonName = "Connect";
                WsClient.Close( );
            }
        }

        /// <summary>
        /// Gets the client send clear command.
        /// </summary>
        /// <value>
        /// The client send clear command.
        /// </value>
        public ICommand ClientSendClearCommand
        {
            get
            {
                return new RelayCommand( param => ClientSendClear( param ) );
            }
        }

        /// <summary>
        /// Clients the send clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientSendClear( object parameter )
        {
            WebSocketModel.ClientSend = "";
            WsClientRecv.Clear( );
        }

        /// <summary>
        /// Gets the client send command.
        /// </summary>
        /// <value>
        /// The client send command.
        /// </value>
        public ICommand ClientSendCommand
        {
            get
            {
                return new RelayCommand( param => ClientSend( param ) );
            }
        }

        /// <summary>
        /// Clients the send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientSend( object parameter )
        {
            WsClient.Send( WebSocketModel.ClientSend );
        }

        /// <summary>
        /// Gets the client automatic send command.
        /// </summary>
        /// <value>
        /// The client automatic send command.
        /// </value>
        public ICommand ClientAutoSendCommand
        {
            get
            {
                return new RelayCommand( param => ClientAutoSend( param ) );
            }
        }

        /// <summary>
        /// The m client automatic send timer
        /// </summary>
        private DispatcherTimer _mClientAutoSendTimer;

        /// <summary>
        /// Clients the automatic send function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClientAutoSendFunc( object sender, EventArgs e )
        {
            WsClient.Send( WebSocketModel.ClientSendStr );
        }

        /// <summary>
        /// Clients the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientAutoSend( object parameter )
        {
            if( WebSocketModel.ClientSendBtnName == "Auto Send Start" )
            {
                WebSocketModel.ClientSendBtnName = "Auto Send Stop";
                _mClientAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        WebSocketModel.ClientSendInterval )
                };

                _mClientAutoSendTimer.Tick += ClientAutoSendFunc;
                _mClientAutoSendTimer.Start( );
            }
            else
            {
                WebSocketModel.ClientSendBtnName = "Auto Send Start";
                _mClientAutoSendTimer.Stop( );
            }
        }
        #endregion
    }
}