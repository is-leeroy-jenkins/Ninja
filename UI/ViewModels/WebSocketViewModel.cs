

namespace Ninja.ViewModels
{
    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;
    using WebSocketSharp.Server;
    using WebSocketSharp;

    public class WebSocketViewModel : MainWindowBase
    {
        public WebSocketModel WebSocketModel { get; set; }

        #region WebSocket Server
        private WebSocketServer _wsServer = null;

        public static ObservableCollection<string> WsRecv { get; set; } = 
            new ObservableCollection<string> { };

        public ICommand StartListenCommand
        {
            get
            {
                return new RelayCommand(param => StartListen(param));
            }
        }

        public void StartListen(object parameter)
        {
            if (WebSocketModel.ServerListenBtnName == "Start Listen")
            {
                WebSocketModel.ServerListenBtnName = "Stop Listen";
                _wsServer = new WebSocketServer(WebSocketModel.ServerAddress);
                _wsServer.AddWebSocketService<EchoHandler>("/echo");
                _wsServer.Start();
                WsRecv.Add("[" + DateTime.Now + "][" + "WebSocket Server Started]\n");
            }
            else
            {
                WebSocketModel.ServerListenBtnName = "Start Listen";
                _wsServer.Stop();
                WsRecv.Add("[" + DateTime.Now + "][" + "WebSocket Server Stopped]\n");
            }
        }
        
        public ICommand ServerAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ServerAutoSend(param));
            }
        }

        private DispatcherTimer _mServerAutoSendTimer;

        private void ServerAutoSendTimerFunc(object sender, EventArgs e)
        {
            _wsServer.WebSocketServices.Broadcast( WebSocketModel.ServerSendStr );
        }

        public void ServerAutoSend(object parameter)
        {
            if (WebSocketModel.ServerSendBtnName == "Auto Send Start")
            {
                WebSocketModel.ServerSendBtnName = "Auto Send Stop";
                _mServerAutoSendTimer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, WebSocketModel.ServerSendInterval)
                };

                _mServerAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _mServerAutoSendTimer.Start();
            }
            else
            {
                WebSocketModel.ServerSendBtnName = "Auto Send Start";
                _mServerAutoSendTimer.Stop();
            }
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
            WebSocketModel.ServerSend = "";
            WsRecv.Clear();
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
            _wsServer.WebSocketServices.Broadcast( WebSocketModel.ServerSend ); 
        }
        #endregion

        #region WebSocket Client

        public WebSocket WsClient;

        public static ObservableCollection<string> WsClientRecv { get; set; } = new ObservableCollection<string> { };

        public ICommand ClientConnectCommand
        {
            get
            {
                return new RelayCommand(param => ClientConnect(param));
            }
        }

        public void ClientConnect(object parameter)
        {
            if (WebSocketModel.ClientConnectBtnName == "Connect")
            {
                using (var _ws = new WebSocket(WebSocketModel.ServerIp))
                {
                    _ws.OnOpen += (sender, e) => {
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Open]\n";
                            WsClientRecv.Add(_time + _str);
                        }));
                    };

                    _ws.OnMessage += (sender, e) => {
                        var _fmt = "[WebSocket Message] {0}";
                        var _body = !e.IsPing ? e.Data : "A ping was received.";
                        Console.WriteLine(_fmt, _body);
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[" + e.Data + "]\n";
                            WsClientRecv.Add(_time + _str);
                        }));
                    };

                    _ws.OnError += (sender, e) => {
                        var _fmt = "[WebSocket Error] {0}";
                        Console.WriteLine(_fmt, e.Message);
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Error][" + e.Message + "]\n";
                            WsClientRecv.Add(_time + _str);
                            WebSocketModel.ClientConnectBtnName = "Connect";
                        }));
                    };

                    _ws.OnClose += (sender, e) => {
                        var _fmt = "[WebSocket Close ({0})] {1}";
                        Console.WriteLine(_fmt, e.Code, e.Reason);
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Close][" + e.Reason + "]\n";
                            WsClientRecv.Add(_time + _str);
                            WebSocketModel.ClientConnectBtnName = "Connect";
                        }));
                    };

                    // Connect to the server.
                    WsClient = _ws;
                }

                try
                {
                    Task.Run(() =>
                    {
                        WsClient.Connect();
                        WebSocketModel.ClientConnectBtnName = "DisConnect";
                    });
                }
                catch (Exception ex)
                {
                    // ignore
                }
            }
            else
            {
                WebSocketModel.ClientConnectBtnName = "Connect";
                WsClient.Close();
            }
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
            WebSocketModel.ClientSend = "";
            WsClientRecv.Clear();
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
            WsClient.Send(WebSocketModel.ClientSend);
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
            WsClient.Send(WebSocketModel.ClientSendStr);
        }

        public void ClientAutoSend(object parameter)
        {
            if (WebSocketModel.ClientSendBtnName == "Auto Send Start")
            {
                WebSocketModel.ClientSendBtnName = "Auto Send Stop";
                _mClientAutoSendTimer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, WebSocketModel.ClientSendInterval)
                };
                _mClientAutoSendTimer.Tick += ClientAutoSendFunc;
                _mClientAutoSendTimer.Start();
            }
            else
            {
                WebSocketModel.ClientSendBtnName = "Auto Send Start";
                _mClientAutoSendTimer.Stop();
            }
        }
         #endregion
 
        public WebSocketViewModel()
        {
            WebSocketModel = new WebSocketModel();
        }
    }
}
