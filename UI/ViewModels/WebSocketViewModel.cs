// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-29-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-29-2024
// ******************************************************************************************
// <copyright file="WebSocketViewModel.cs" company="Terry D. Eppler">
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
    /// <seealso cref="Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class WebSocketViewModel : MainWindowBase
    {
        /// <summary>
        /// The web socket model
        /// </summary>
        private protected WebSocketModel _webSocketModel;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="WebSocketViewModel"/> class.
        /// </summary>
        public WebSocketViewModel( )
        {
            _webSocketModel = new WebSocketModel( );
        }

        /// <summary>
        /// Gets or sets the web socket model.
        /// </summary>
        /// <value>
        /// The web socket model.
        /// </value>
        public WebSocketModel WebSocketModel
        {
            get
            {
                return _webSocketModel;
            }
            set
            {
                if( _webSocketModel != value )
                {
                    _webSocketModel = value;
                    OnPropertyChanged( nameof( _webSocketModel ) );
                }
            }
        }

        #region WebSocket Server
        /// <summary>
        /// The ws server
        /// </summary>
        private WebSocketServer _webSocketServer;

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
            if( _webSocketModel.ServerListenButtonName == "Start Listen" )
            {
                _webSocketModel.ServerListenButtonName = "Stop Listen";
                _webSocketServer = new WebSocketServer( WebSocketModel.ServerAddress );
                _webSocketServer.AddWebSocketService<EchoHandler>( "/echo" );
                _webSocketServer.Start( );
                WsRecv.Add( "[" + DateTime.Now + "][" + "WebSocket Server Started]\n" );
            }
            else
            {
                _webSocketModel.ServerListenButtonName = "Start Listen";
                _webSocketServer.Stop( );
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
        private DispatcherTimer _serverAutoSendTimer;

        /// <summary>
        /// Servers the automatic send timer function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ServerAutoSendTimerFunc( object sender, EventArgs e )
        {
            _webSocketServer.WebSocketServices.Broadcast( _webSocketModel.ServerSendStr );
        }

        /// <summary>
        /// Servers the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerAutoSend( object parameter )
        {
            if( _webSocketModel.ServerSendButtonName == "Auto Send Start" )
            {
                _webSocketModel.ServerSendButtonName = "Auto Send Stop";
                _serverAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        _webSocketModel.ServerSendInterval )
                };

                _serverAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _serverAutoSendTimer.Start( );
            }
            else
            {
                _webSocketModel.ServerSendButtonName = "Auto Send Start";
                _serverAutoSendTimer.Stop( );
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
            _webSocketModel.ServerSend = "";
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
            _webSocketServer.WebSocketServices.Broadcast( _webSocketModel.ServerSend );
        }
        #endregion

        #region WebSocket Client
        /// <summary>
        /// The ws client
        /// </summary>
        private protected WebSocket _webSocketClient;

        /// <summary>
        /// The web socket client
        /// </summary>
        public WebSocket WebSocketClient
        {
            get
            {
                return _webSocketClient;
            }
            set
            {
                if( _webSocketClient != value )
                {
                    _webSocketClient = value;
                    OnPropertyChanged( nameof( WebSocketClient ) );
                }
            }
        }

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
            if( _webSocketModel.ClientConnectButtonName == "Connect" )
            {
                using( var _ws = new WebSocket( _webSocketModel.ClientAddress ) )
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
                            _webSocketModel.ClientConnectButtonName = "Connect";
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
                            _webSocketModel.ClientConnectButtonName = "Connect";
                        } ) );
                    };

                    // Connect to the server.
                    _webSocketClient = _ws;
                }

                try
                {
                    Task.Run( ( ) =>
                    {
                        _webSocketClient.Connect( );
                        _webSocketModel.ClientConnectButtonName = "DisConnect";
                    } );
                }
                catch( Exception )
                {
                    // ignore
                }
            }
            else
            {
                _webSocketModel.ClientConnectButtonName = "Connect";
                _webSocketClient.Close( );
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
            _webSocketModel.ClientSend = "";
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
            _webSocketClient.Send( _webSocketModel.ClientSend );
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
        private DispatcherTimer _clientAutoSendTimer;

        /// <summary>
        /// Clients the automatic send function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClientAutoSendFunc( object sender, EventArgs e )
        {
            _webSocketClient.Send( _webSocketModel.ClientSendStr );
        }

        /// <summary>
        /// Clients the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientAutoSend( object parameter )
        {
            if( _webSocketModel.ClientSendButtonName == "Auto Send Start" )
            {
                _webSocketModel.ClientSendButtonName = "Auto Send Stop";
                _clientAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        _webSocketModel.ClientSendInterval )
                };

                _clientAutoSendTimer.Tick += ClientAutoSendFunc;
                _clientAutoSendTimer.Start( );
            }
            else
            {
                _webSocketModel.ClientSendButtonName = "Auto Send Start";
                _clientAutoSendTimer.Stop( );
            }
        }
        #endregion
    }
}