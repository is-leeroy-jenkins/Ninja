using Ninja.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Input;
using Ninja.Interfaces;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Ninja.ViewModels
{
    using Interfaces;
    using Models;

    internal class TcpViewModel : MainWindowBase
    {
        public TcpModel TcpModel { get; set; }

        #region TcpServer
        private TcpServerSocket _tcpServerSocket = null;

        public class TcpServerInfo
        {
            public string RemoteIp { get; set; }
            public string Port { get; set; }
            public DateTime Time { get; set; }
        }
        public ObservableCollection<TcpServerInfo> TcpServerInfos { get; set; } = new ObservableCollection<TcpServerInfo> { };

        public ICommand StartListenCommand
        {
            get
            {
                return new RelayCommand(param => StartListen(param));
            }
        }
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
        private void Recv(Socket socket, string message)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                TcpModel.ServerRecv += "[" + socket.RemoteEndPoint.ToString() + "] :";
                TcpModel.ServerRecv += message;
                TcpModel.ServerRecv += "\n";
            }));

        }
        private void ConnectCallback(Socket socket)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var _time = DateTime.Now;
                TcpServerInfos.Add(new TcpServerInfo {RemoteIp = socket.RemoteEndPoint.ToString().Split(':')[0], Port = socket.RemoteEndPoint.ToString().Split(':')[1], Time = _time });
                TcpModel.ServerStatus += "++[" + socket.RemoteEndPoint.ToString() + "] connected at " + _time + "\n";
            }));

        }
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

        public ICommand ServerAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ServerAutoSend(param));
            }
        }
        private System.Windows.Threading.DispatcherTimer _mServerAutoSendTimer;
        /// <param name="e"></param>
        private void ServerAutoSendTimerFunc(object sender, EventArgs e)
        {
            if (_tcpServerSocket!=null)
            {
                _tcpServerSocket.SendMessageToAllClientsAsync(TcpModel.ServerSendStr);
            }
        }

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

        public ICommand ServerRecvClearCommand
        {
            get
            {
                return new RelayCommand(param => ServerRecvClear(param));
            }
        }
        public void ServerRecvClear(object parameter)
        {
            TcpModel.ServerRecv = "";
        }

        public ICommand ServerSendClearCommand
        {
            get
            {
                return new RelayCommand(param => ServerSendClear(param));
            }
        }
        public void ServerSendClear(object parameter)
        {
            TcpModel.ServerSend = "";
        }
        public ICommand ServerSendCommand
        {
            get
            {
                return new RelayCommand(param => ServerSend(param));
            }
        }
        public void ServerSend(object parameter)
        {
            if(_tcpServerSocket != null)
            {
                _tcpServerSocket.SendMessageToAllClientsAsync(TcpModel.ServerSend);
            }

        }
        #endregion

        #region TcpClient
        private TcpClientSocket _tcpClientSocket = null;
        public ICommand ClientConnectCommand
        {
            get
            {
                return new RelayCommand(param => ClientConnect(param));
            }
        }
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

        private void ClientConnectCb(Socket socket)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var _time = DateTime.Now;
                TcpModel.ClientRecv += "++[" + socket.RemoteEndPoint.ToString() + "] connected at " + _time + "\n";
                TcpModel.LocalPort = socket.LocalEndPoint.ToString().Split(':')[1];
            }));
        }
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
        private void ClientRecvCb(string msg)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                TcpModel.ClientRecv += msg;
                TcpModel.ClientRecv += "\n";
            }));

        }
        public ICommand ClientSendClearCommand
        {
            get
            {
                return new RelayCommand(param => ClientSendClear(param));
            }
        }
        public void ClientSendClear(object parameter)
        {
            TcpModel.ClientSend = "";
        }
        public ICommand ClientSendCommand
        {
            get
            {
                return new RelayCommand(param => ClientSend(param));
            }
        }
        public void ClientSend(object parameter)
        {
            if(_tcpClientSocket != null)
            {
                _tcpClientSocket.SendAsync(TcpModel.ClientSend);
            }

        }
        public ICommand ClientAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ClientAutoSend(param));
            }
        }

        private System.Windows.Threading.DispatcherTimer _mClientAutoSendTimer;
        private void ClientAutoSendFunc(object sender, EventArgs e)
        {
            if(_tcpClientSocket != null)
            {
                _tcpClientSocket.SendAsync(TcpModel.ClientSendStr);
            }

        }

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
        public ICommand ClientRecvClearCommand
        {
            get
            {
                return new RelayCommand(param => ClientRecvClear(param));
            }
        }
        public void ClientRecvClear(object parameter)
        {
            TcpModel.ClientRecv = "";
        }

        #endregion
        public TcpViewModel()
        {
            TcpModel = new TcpModel();
        }
    }
}
