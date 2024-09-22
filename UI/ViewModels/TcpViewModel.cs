

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

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.ViewModels.MainWindowBase" />
    public class TcpViewModel : MainWindowBase
    {
        /// <summary>
        /// Gets or sets the TCP model.
        /// </summary>
        /// <value>
        /// The TCP model.
        /// </value>
        public TcpModel TcpModel { get; set; }

        #region TcpServer
        /// <summary>
        /// The TCP server socket
        /// </summary>
        private TcpServerSocket _tcpServerSocket = null;

        /// <summary>
        /// Gets or sets the TCP server infos.
        /// </summary>
        /// <value>
        /// The TCP server infos.
        /// </value>
        public ObservableCollection<TcpServerInfo> TcpServerInfos { get; set; } = new ObservableCollection<TcpServerInfo> { };

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
                return new RelayCommand(param => StartListen(param));
            }
        }
        /// <summary>
        /// Starts the listen.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void StartListen(object parameter)
        {
            if (TcpModel.ServerListenBtnName == "Start Listen")
            {
                TcpModel.ServerListenBtnName = "Stop Listen";

                _tcpServerSocket = new TcpServerSocket(IPAddress.Any.ToString(), TcpModel.ListenPort);
                _tcpServerSocket.RecvEvent = new Action<Socket, string>(Recv);
                _tcpServerSocket.ConnectEvent = new Action<Socket>(ConnectCallback);
                _tcpServerSocket.DisConnectEvent = new Action<Socket>(DisConnectCallback);
                _tcpServerSocket.Start();
                TcpModel.ServerStatus += "Tcp Server Started!\n";
            }
            else
            {
                TcpModel.ServerListenBtnName = "Start Listen";
                _tcpServerSocket.CloseAllClientSocket();
                TcpServerInfos.Clear();
                TcpModel.ServerStatus += "Tcp Server Stopped!\n";

            }
        }
        /// <summary>
        /// Recvs the specified socket.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="message">The message.</param>
        private void Recv(Socket socket, string message)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                TcpModel.ServerRecv += "[" + socket.RemoteEndPoint.ToString() + "] :";
                TcpModel.ServerRecv += message;
                TcpModel.ServerRecv += "\n";
            }));

        }
        /// <summary>
        /// Connects the callback.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void ConnectCallback(Socket socket)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var _time = DateTime.Now;
                TcpServerInfos.Add(new TcpServerInfo {RemoteIp = socket.RemoteEndPoint.ToString().Split(':')[0], Port = socket.RemoteEndPoint.ToString().Split(':')[1], Time = _time });
                TcpModel.ServerStatus += "++[" + socket.RemoteEndPoint.ToString() + "] connected at " + _time + "\n";
            }));

        }
        /// <summary>
        /// Dises the connect callback.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void DisConnectCallback(Socket socket)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (var _info in TcpServerInfos)
                {
                    if (_info.RemoteIp == socket.RemoteEndPoint.ToString().Split(':')[0] && _info.Port == socket.RemoteEndPoint.ToString().Split(':')[1])
                    {
                        TcpServerInfos.Remove(_info);
                        TcpModel.ServerStatus += "--[" + socket.RemoteEndPoint.ToString() + "] disconnected at " + _info.Time + "\n";
                        break;
                    }
                }
            }));
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
                return new RelayCommand(param => ServerAutoSend(param));
            }
        }
        /// <summary>
        /// The m server automatic send timer
        /// </summary>
        private System.Windows.Threading.DispatcherTimer _mServerAutoSendTimer;
        /// <summary>
        /// Servers the automatic send timer function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ServerAutoSendTimerFunc(object sender, EventArgs e)
        {
            if (_tcpServerSocket!=null)
            {
                _tcpServerSocket.SendMessageToAllClientsAsync(TcpModel.ServerSendStr);
            }
        }

        /// <summary>
        /// Servers the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerAutoSend(object parameter)
        {
            if (TcpModel.ServerSendBtnName == "Auto Send Start")
            {
                TcpModel.ServerSendBtnName = "Auto Send Stop";
                _mServerAutoSendTimer = new System.Windows.Threading.DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, TcpModel.ServerSendInterval)
                };

                _mServerAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _mServerAutoSendTimer.Start();

            }
            else
            {

                TcpModel.ServerSendBtnName = "Auto Send Start";
                _mServerAutoSendTimer.Stop();
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
                return new RelayCommand(param => ServerRecvClear(param));
            }
        }
        /// <summary>
        /// Servers the recv clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerRecvClear(object parameter)
        {
            TcpModel.ServerRecv = "";
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
                return new RelayCommand(param => ServerSendClear(param));
            }
        }
        /// <summary>
        /// Servers the send clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerSendClear(object parameter)
        {
            TcpModel.ServerSend = "";
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
                return new RelayCommand(param => ServerSend(param));
            }
        }
        /// <summary>
        /// Servers the send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerSend(object parameter)
        {
            if(_tcpServerSocket != null)
            {
                _tcpServerSocket.SendMessageToAllClientsAsync(TcpModel.ServerSend);
            }

        }
        #endregion

        #region TcpClient
        /// <summary>
        /// The TCP client socket
        /// </summary>
        private TcpClientSocket _tcpClientSocket = null;
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
                return new RelayCommand(param => ClientConnect(param));
            }
        }
        /// <summary>
        /// Clients the connect.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientConnect(object parameter)
        {
            if (TcpModel.ClientConnectBtnName == "Connect")
            {
                TcpModel.ClientConnectBtnName = "Disconnect";
                _tcpClientSocket = new TcpClientSocket(TcpModel.ServerIp, TcpModel.ServerPort);
                _tcpClientSocket.RecvEvent = new Action<string>(ClientRecvCb);
                _tcpClientSocket.ConnectEvent = new Action<Socket>(ClientConnectCb);
                _tcpClientSocket.DisConnectEvent = new Action<Socket>(ClientDisConnectCb);
                _tcpClientSocket.Start();
            }
            else
            {
                TcpModel.ClientConnectBtnName = "Connect";
                _tcpClientSocket.CloseClientSocket();
            }
        }

        /// <summary>
        /// Clients the connect cb.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void ClientConnectCb(Socket socket)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var _time = DateTime.Now;
                TcpModel.ClientRecv += "++[" + socket.RemoteEndPoint.ToString() + "] connected at " + _time + "\n";
                TcpModel.LocalPort = socket.LocalEndPoint.ToString().Split(':')[1];
            }));
        }
        /// <summary>
        /// Clients the dis connect cb.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private void ClientDisConnectCb(Socket socket)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                TcpModel.ClientRecv += "--[" + socket.RemoteEndPoint.ToString() + "] disconnected at " + DateTime.Now + "\n";
                if (TcpModel.ClientConnectBtnName == "Disconnect")
                {
                    TcpModel.ClientConnectBtnName = "Connect";
                    _tcpClientSocket.CloseClientSocket();
                }
            }));

        }
        /// <summary>
        /// Clients the recv cb.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        private void ClientRecvCb(string msg)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                TcpModel.ClientRecv += msg;
                TcpModel.ClientRecv += "\n";
            }));

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
                return new RelayCommand(param => ClientSendClear(param));
            }
        }
        /// <summary>
        /// Clients the send clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientSendClear(object parameter)
        {
            TcpModel.ClientSend = "";
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
                return new RelayCommand(param => ClientSend(param));
            }
        }
        /// <summary>
        /// Clients the send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientSend(object parameter)
        {
            if(_tcpClientSocket != null)
            {
                _tcpClientSocket.SendAsync(TcpModel.ClientSend);
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
                return new RelayCommand(param => ClientAutoSend(param));
            }
        }

        /// <summary>
        /// The m client automatic send timer
        /// </summary>
        private System.Windows.Threading.DispatcherTimer _mClientAutoSendTimer;
        /// <summary>
        /// Clients the automatic send function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClientAutoSendFunc(object sender, EventArgs e)
        {
            if(_tcpClientSocket != null)
            {
                _tcpClientSocket.SendAsync(TcpModel.ClientSendStr);
            }

        }

        /// <summary>
        /// Clients the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientAutoSend(object parameter)
        {
            if (TcpModel.ClientSendBtnName == "Auto Send Start")
            {
                TcpModel.ClientSendBtnName = "Auto Send Stop";
                _mClientAutoSendTimer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, TcpModel.ClientSendInterval)
                };
                _mClientAutoSendTimer.Tick += ClientAutoSendFunc;
                _mClientAutoSendTimer.Start();
            }
            else
            {
                TcpModel.ClientSendBtnName = "Auto Send Start";
                _mClientAutoSendTimer.Stop();
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
                return new RelayCommand(param => ClientRecvClear(param));
            }
        }
        /// <summary>
        /// Clients the recv clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientRecvClear(object parameter)
        {
            TcpModel.ClientRecv = "";
        }

        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpViewModel"/> class.
        /// </summary>
        public TcpViewModel()
        {
            TcpModel = new TcpModel();
        }
    }
}
