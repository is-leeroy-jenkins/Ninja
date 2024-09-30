// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-29-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-29-2024
// ******************************************************************************************
// <copyright file="TcpViewModel.cs" company="Terry D. Eppler">
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
//   TcpViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Interfaces;
    using Models;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Input;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="Ninja.ViewModels.MainWindowBase" />
    public class TcpViewModel : MainWindowBase
    {
        /// <summary>
        /// The TCP model
        /// </summary>
        private protected TcpModel _tcpModel;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TcpViewModel"/> class.
        /// </summary>
        public TcpViewModel( )
        {
            TcpModel = new TcpModel( );
        }

        /// <summary>
        /// Gets or sets the TCP model.
        /// </summary>
        /// <value>
        /// The TCP model.
        /// </value>
        public TcpModel TcpModel
        {
            get
            {
                return _tcpModel;
            }
            set
            {
                if( _tcpModel != value )
                {
                    _tcpModel = value;
                    OnPropertyChanged( nameof( TcpModel ) );
                }
            }
        }

        #region TcpServer
        /// <summary>
        /// The TCP server socket
        /// </summary>
        private TcpServerSocket _tcpServerSocket;

        /// <summary>
        /// Gets or sets the TCP server infos.
        /// </summary>
        /// <value>
        /// The TCP server infos.
        /// </value>
        public ObservableCollection<TcpServerInfo> TcpServerInfos { get; set; } =
            new ObservableCollection<TcpServerInfo>( );

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
            if( _tcpModel.ServerListenButtonName == "Start Listen" )
            {
                _tcpModel.ServerListenButtonName = "Stop Listen";
                _tcpServerSocket =
                    new TcpServerSocket( IPAddress.Any.ToString( ), _tcpModel.ListenPort );

                _tcpServerSocket.RecvEvent = Recv;
                _tcpServerSocket.ConnectEvent = ConnectCallback;
                _tcpServerSocket.DisConnectEvent = DisConnectCallback;
                _tcpServerSocket.Start( );
                _tcpModel.ServerStatus += "Tcp Server Started!\n";
            }
            else
            {
                _tcpModel.ServerListenButtonName = "Start Listen";
                _tcpServerSocket.CloseAllClientSocket( );
                TcpServerInfos.Clear( );
                _tcpModel.ServerStatus += "Tcp Server Stopped!\n";
            }
        }

        /// <summary>
        /// Recvs the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="message">The message.</param>
        private void Recv( Socket socket, string message )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                _tcpModel.ServerRecv += "[" + socket.RemoteEndPoint + "] :";
                _tcpModel.ServerRecv += message;
                _tcpModel.ServerRecv += "\n";
            } ) );
        }

        /// <summary>
        /// Connects the callback.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void ConnectCallback( Socket socket )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                var _time = DateTime.Now;
                TcpServerInfos.Add( new TcpServerInfo
                {
                    IpAddress = socket.RemoteEndPoint.ToString( ).Split( ':' )[ 0 ],
                    Port = socket.RemoteEndPoint.ToString( ).Split( ':' )[ 1 ],
                    Time = _time
                } );

                _tcpModel.ServerStatus +=
                    "++[" + socket.RemoteEndPoint + "] connected at " + _time + "\n";
            } ) );
        }

        /// <summary>
        /// Dises the connect callback.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void DisConnectCallback( Socket socket )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                foreach( var _info in TcpServerInfos )
                {
                    if( _info.IpAddress == socket.RemoteEndPoint.ToString( ).Split( ':' )[ 0 ]
                        && _info.Port ==  socket.RemoteEndPoint.ToString( ).Split( ':' )[ 1 ] ) 
                    {
                        TcpServerInfos.Remove( _info );
                        _tcpModel.ServerStatus += "--[" + socket.RemoteEndPoint
                            + "] disconnected at " + _info.Time + "\n";

                        break;
                    }
                }
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
            if( _tcpServerSocket != null )
            {
                _tcpServerSocket.SendMessageToAllClientsAsync( _tcpModel.ServerSendStr );
            }
        }

        /// <summary>
        /// Servers the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerAutoSend( object parameter )
        {
            if( _tcpModel.ServerSendButtonName == "Auto Send Start" )
            {
                _tcpModel.ServerSendButtonName = "Auto Send Stop";
                _serverAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        _tcpModel.ServerSendInterval )
                };

                _serverAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _serverAutoSendTimer.Start( );
            }
            else
            {
                _tcpModel.ServerSendButtonName = "Auto Send Start";
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
            _tcpModel.ServerRecv = "";
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
            _tcpModel.ServerSend = "";
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
            if( _tcpServerSocket != null )
            {
                _tcpServerSocket.SendMessageToAllClientsAsync( _tcpModel.ServerSend );
            }
        }
        #endregion

        #region TcpClient
        /// <summary>
        /// The TCP client socket
        /// </summary>
        private TcpClientSocket _tcpClientSocket;

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
            if( _tcpModel.ClientConnectButtonName == "Connect" )
            {
                _tcpModel.ClientConnectButtonName = "Disconnect";
                _tcpClientSocket =
                    new TcpClientSocket( _tcpModel.ServerAddress, _tcpModel.ServerPort );

                _tcpClientSocket.RecvEvent = ClientRecvCb;
                _tcpClientSocket.ConnectEvent = ClientConnectCb;
                _tcpClientSocket.DisConnectEvent = ClientDisConnectCb;
                _tcpClientSocket.Start( );
            }
            else
            {
                _tcpModel.ClientConnectButtonName = "Connect";
                _tcpClientSocket.CloseClientSocket( );
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
                _tcpModel.ClientRecv +=
                    "++[" + socket.RemoteEndPoint + "] connected at " + _time + "\n";

                _tcpModel.LocalPort = int.Parse( socket.LocalEndPoint.ToString( ).Split( ':' )[ 1 ] );
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
                _tcpModel.ClientRecv += "--[" + socket.RemoteEndPoint + "] disconnected at "
                    + DateTime.Now + "\n";

                if( _tcpModel.ClientConnectButtonName == "Disconnect" )
                {
                    _tcpModel.ClientConnectButtonName = "Connect";
                    _tcpClientSocket.CloseClientSocket( );
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
                _tcpModel.ClientRecv += msg;
                _tcpModel.ClientRecv += "\n";
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
            _tcpModel.ClientSend = "";
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
            if( _tcpClientSocket != null )
            {
                _tcpClientSocket.SendAsync( _tcpModel.ClientSend );
            }
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
            if( _tcpClientSocket != null )
            {
                _tcpClientSocket.SendAsync( _tcpModel.ClientSendStr );
            }
        }

        /// <summary>
        /// Clients the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientAutoSend( object parameter )
        {
            if( _tcpModel.ClientSendButtonName == "Auto Send Start" )
            {
                _tcpModel.ClientSendButtonName = "Auto Send Stop";
                _clientAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        _tcpModel.ClientSendInterval )
                };

                _clientAutoSendTimer.Tick += ClientAutoSendFunc;
                _clientAutoSendTimer.Start( );
            }
            else
            {
                _tcpModel.ClientSendButtonName = "Auto Send Start";
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
            _tcpModel.ClientRecv = "";
        }
        #endregion
    }
}