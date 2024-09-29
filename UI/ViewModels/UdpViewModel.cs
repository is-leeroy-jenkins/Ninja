// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-29-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-29-2024
// ******************************************************************************************
// <copyright file="UdpViewModel.cs" company="Terry D. Eppler">
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
//   UdpViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Interfaces;
    using Models;
    using Models;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Input;
    using Interfaces;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="Ninja.ViewModels.MainWindowBase" />
    public class UdpViewModel : MainWindowBase
    {
        /// <summary>
        /// The upd model
        /// </summary>
        private protected UdpModel _udpModel;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UdpViewModel"/> class.
        /// </summary>
        public UdpViewModel( )
        {
            UdpModel = new UdpModel( );
        }

        /// <summary>
        /// Gets or sets the UDP model.
        /// </summary>
        /// <value>
        /// The UDP model.
        /// </value>
        public UdpModel UdpModel
        {
            get
            {
                return _udpModel;
            }
            set
            {
                if( _udpModel != value )
                {
                    _udpModel = value;
                    OnPropertyChanged( nameof( UdpModel ) );
                }
            }
        }

        #region TcpServer
        /// <summary>
        /// The UDP server socket
        /// </summary>
        private UdpServerSocket _udpServerSocket;

        /// <summary>
        /// Gets or sets the UDP client infos.
        /// </summary>
        /// <value>
        /// The UDP client infos.
        /// </value>
        public ObservableCollection<UdpClientInfo> UdpClientInfos { get; set; } =
            new ObservableCollection<UdpClientInfo>( );

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
            if( _udpModel.ServerListenButtonName == "Start Listen" )
            {
                _udpModel.ServerListenButtonName = "Stop Listen";
                _udpServerSocket =
                    new UdpServerSocket( IPAddress.Any.ToString( ), _udpModel.ListenPort );

                _udpServerSocket.RecvEvent = Recv;
                _udpServerSocket.Start( );
                _udpModel.ServerStatus += "Udp Server Started!\n";
            }
            else
            {
                _udpModel.ServerListenButtonName = "Start Listen";
                UdpClientInfos.Clear( );
                _udpModel.ServerStatus += "Udp Server Stopped!\n";
            }
        }

        /// <summary>
        /// Recvs the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="message">The message.</param>
        /// <param name="len">The length.</param>
        private void Recv( EndPoint point, string message, int len )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                _udpModel.ServerRecv += "[" + point + "] :";
                _udpModel.ServerRecv += message;
                _udpModel.ServerRecv += "\n";
                var _time = DateTime.Now;
                UdpClientInfos.Add( new UdpClientInfo
                {
                    RemoteIp = point.ToString( ).Split( ':' )[ 0 ],
                    Port = point.ToString( ).Split( ':' )[ 1 ],
                    RecvBytes = len,
                    Time = _time
                } );

                _udpModel.ServerStatus += "++[" + point + "] connected at " + _time + "\n";
            } ) );
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
        /// The server automatic send timer
        /// </summary>
        private DispatcherTimer _serverAutoSendTimer;

        /// <summary>
        /// Servers the automatic send timer function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ServerAutoSendTimerFunc( object sender, EventArgs e )
        {
            _udpServerSocket.SendMessageToAllClientsAsync( _udpModel.ServerSendStr );
        }

        /// <summary>
        /// Servers the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerAutoSend( object parameter )
        {
            if( _udpModel.ServerSendButtonName == "Auto Send Start" )
            {
                _udpModel.ServerSendButtonName = "Auto Send Stop";
                _serverAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        _udpModel.ServerSendInterval )
                };

                _serverAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _serverAutoSendTimer.Start( );
            }
            else
            {
                _udpModel.ServerSendButtonName = "Auto Send Start";
                _serverAutoSendTimer.Stop( );
            }
        }

        /// <summary>
        /// Gets the server recv clear command.
        /// </summary>
        /// <value>
        /// The server recv clear command.
        /// </value>
        public ICommand ServerRecvClearCommand
        {
            get
            {
                return new RelayCommand( param => ServerRecvClear( param ) );
            }
        }

        /// <summary>
        /// Servers the recv clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerRecvClear( object parameter )
        {
            _udpModel.ServerRecv = "";
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
            _udpModel.ServerSend = "";
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
            _udpServerSocket.SendMessageToAllClientsAsync( UdpModel.ServerSend );
        }
        #endregion

        #region UdpClient
        /// <summary>
        /// The UDP client socket
        /// </summary>
        private UdpClientSocket _udpClientSocket;

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
            if( _udpModel.ClientConnectButtonName == "Connect" )
            {
                _udpModel.ClientConnectButtonName = "Disconnect";
                _udpClientSocket =
                    new UdpClientSocket( _udpModel.ServerAddress, _udpModel.ServerPort );

                _udpClientSocket.RecvEvent = ClientRecvCb;
                _udpClientSocket.Start( );
            }
            else
            {
                _udpModel.ClientConnectButtonName = "Connect";
                _udpClientSocket.CloseClientSocket( );
            }
        }

        /// <summary>
        /// Clients the connect cb.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void ClientConnectCb( Socket socket )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                var _time = DateTime.Now;
                _udpModel.ClientRecv +=
                    "++[" + socket.RemoteEndPoint + "] connected at " + _time + "\n";

                _udpModel.LocalPort = socket.LocalEndPoint.ToString( ).Split( ':' )[ 1 ];
            } ) );
        }

        /// <summary>
        /// Clients the dis connect cb.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void ClientDisConnectCb( Socket socket )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                _udpModel.ClientRecv += "--[" + socket.RemoteEndPoint + "] disconnected at "
                    + DateTime.Now + "\n";

                if( _udpModel.ClientConnectButtonName == "Disconnect" )
                {
                    _udpModel.ClientConnectButtonName = "Connect";
                    _udpClientSocket.CloseClientSocket( );
                }
            } ) );
        }

        /// <summary>
        /// Clients the recv cb.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void ClientRecvCb( string msg )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                _udpModel.ClientRecv += msg;
                _udpModel.ClientRecv += "\n";
            } ) );
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
            _udpModel.ClientSend = "";
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
            _udpClientSocket.SendAsync( _udpModel.ClientSend );
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
            _udpClientSocket.SendAsync( UdpModel.ClientSendStr );
        }

        /// <summary>
        /// Clients the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientAutoSend( object parameter )
        {
            if( _udpModel.ClientSendButtonName == "Auto Send Start" )
            {
                _udpModel.ClientSendButtonName = "Auto Send Stop";
                _clientAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        _udpModel.ClientSendInterval )
                };

                _clientAutoSendTimer.Tick += ClientAutoSendFunc;
                _clientAutoSendTimer.Start( );
            }
            else
            {
                _udpModel.ClientSendButtonName = "Auto Send Start";
                _clientAutoSendTimer.Stop( );
            }
        }

        /// <summary>
        /// Gets the client recv clear command.
        /// </summary>
        /// <value>
        /// The client recv clear command.
        /// </value>
        public ICommand ClientRecvClearCommand
        {
            get
            {
                return new RelayCommand( param => ClientRecvClear( param ) );
            }
        }

        /// <summary>
        /// Clients the recv clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientRecvClear( object parameter )
        {
            _udpModel.ClientRecv = "";
        }
        #endregion
    }
}