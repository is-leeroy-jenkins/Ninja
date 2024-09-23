

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

    internal class UdpViewModel : MainWindowBase
    {
        public UdpModel UdpModel { get; set; }

        #region TcpServer
        private UdpServerSocket _udpServerSocket = null;

        public class UdpClientInfo
        {
            public string RemoteIp { get; set; }
            public string Port { get; set; }
            public int RecvBytes { get; set; }
            public DateTime Time { get; set; }
        }
        public ObservableCollection<UdpClientInfo> UdpClientInfos { get; set; } = new ObservableCollection<UdpClientInfo> { };

        public ICommand StartListenCommand
        {
            get
            {
                return new RelayCommand(param => StartListen(param));
            }
        }
        public void StartListen(object parameter)
        {
            if (UdpModel.ServerListenBtnName == "Start Listen")
            {
                UdpModel.ServerListenBtnName = "Stop Listen";

                _udpServerSocket = new UdpServerSocket(IPAddress.Any.ToString(), UdpModel.ListenPort);
                _udpServerSocket.RecvEvent = new Action<EndPoint,string,int>(Recv);
                _udpServerSocket.Start();
                UdpModel.ServerStatus += "Udp Server Started!\n";
            }
            else
            {
                UdpModel.ServerListenBtnName = "Start Listen";
                UdpClientInfos.Clear();
                UdpModel.ServerStatus += "Udp Server Stopped!\n";

            }
        }
        private void Recv(EndPoint point, string message, int len)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                UdpModel.ServerRecv += "[" + point.ToString() + "] :";
                UdpModel.ServerRecv += message;
                UdpModel.ServerRecv += "\n";
                var _time = DateTime.Now;
                UdpClientInfos.Add(new UdpClientInfo { RemoteIp = point.ToString().Split(':')[0], Port = point.ToString().Split(':')[1], RecvBytes = len, Time = _time });
                UdpModel.ServerStatus += "++[" + point.ToString() + "] connected at " + _time + "\n";
            }));

        }

        public ICommand ServerAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ServerAutoSend(param));
            }
        }
        private DispatcherTimer _mServerAutoSendTimer;
        /// <param name="e"></param>
        private void ServerAutoSendTimerFunc(object sender, EventArgs e)
        {
            _udpServerSocket.SendMessageToAllClientsAsync(UdpModel.ServerSendStr);
        }

        public void ServerAutoSend(object parameter)
        {
            if (UdpModel.ServerSendBtnName == "Auto Send Start")
            {
                UdpModel.ServerSendBtnName = "Auto Send Stop";
                _mServerAutoSendTimer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, UdpModel.ServerSendInterval)
                };

                _mServerAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _mServerAutoSendTimer.Start();

            }
            else
            {
                UdpModel.ServerSendBtnName = "Auto Send Start";
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
            UdpModel.ServerRecv = "";
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
            UdpModel.ServerSend = "";
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
            _udpServerSocket.SendMessageToAllClientsAsync(UdpModel.ServerSend);
        }
        #endregion

        #region UdpClient
        private UdpClientSocket _udpClientSocket = null;
        public ICommand ClientConnectCommand
        {
            get
            {
                return new RelayCommand(param => ClientConnect(param));
            }
        }
        public void ClientConnect(object parameter)
        {
            if (UdpModel.ClientConnectBtnName == "Connect")
            {
                UdpModel.ClientConnectBtnName = "Disconnect";
                _udpClientSocket = new UdpClientSocket(UdpModel.ServerIp, UdpModel.ServerPort);
                _udpClientSocket.RecvEvent = new Action<string>(ClientRecvCb);
                _udpClientSocket.Start();
            }
            else
            {
                UdpModel.ClientConnectBtnName = "Connect";
                _udpClientSocket.CloseClientSocket();
            }
        }

        private void ClientConnectCb(Socket socket)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var _time = DateTime.Now;
                UdpModel.ClientRecv += "++[" + socket.RemoteEndPoint.ToString() + "] connected at " + _time + "\n";
                UdpModel.LocalPort = socket.LocalEndPoint.ToString().Split(':')[1];
            }));
        }
        private void ClientDisConnectCb(Socket socket)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                UdpModel.ClientRecv += "--[" + socket.RemoteEndPoint.ToString() + "] disconnected at " + DateTime.Now + "\n";
                if (UdpModel.ClientConnectBtnName == "Disconnect")
                {
                    UdpModel.ClientConnectBtnName = "Connect";
                    _udpClientSocket.CloseClientSocket();
                }
            }));

        }
        private void ClientRecvCb(string msg)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                UdpModel.ClientRecv += msg;
                UdpModel.ClientRecv += "\n";
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
            UdpModel.ClientSend = "";
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
            _udpClientSocket.SendAsync(UdpModel.ClientSend);
        }
        public ICommand ClientAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ClientAutoSend(param));
            }
        }

        private DispatcherTimer _mClientAutoSendTimer;
        private void ClientAutoSendFunc(object sender, EventArgs e)
        {
            _udpClientSocket.SendAsync(UdpModel.ClientSendStr);
        }

        public void ClientAutoSend(object parameter)
        {
            if (UdpModel.ClientSendBtnName == "Auto Send Start")
            {
                UdpModel.ClientSendBtnName = "Auto Send Stop";
                _mClientAutoSendTimer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, UdpModel.ClientSendInterval)
                };
                _mClientAutoSendTimer.Tick += ClientAutoSendFunc;
                _mClientAutoSendTimer.Start();
            }
            else
            {
                UdpModel.ClientSendBtnName = "Auto Send Start";
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
            UdpModel.ClientRecv = "";
        }

        #endregion
        public UdpViewModel()
        {
            UdpModel = new UdpModel();
        }
    }
}
