using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja.Interfaces
{
    public class UdpServerSocket
    {

        public Action<EndPoint,string,int> RecvEvent = null;

        public Action<int> SendResultEvent = null;

        private int _numConnections = 65536;

        private Socket _listenSocket = null;

        private string _hostIp = "";

        private int _port = 0;

        private Semaphore _maxNumberAcceptedClients = null;
        private int _bufferSize = 1024;
        private List<EndPoint> _clientSockets = null;
 
        public UdpServerSocket(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip))
                throw new ArgumentNullException("host cannot be null");
            if (port < 1 || port > 65535)
                throw new ArgumentOutOfRangeException("port is out of range");
            if (_numConnections <= 0 || _numConnections > int.MaxValue)
                throw new ArgumentOutOfRangeException("_numConnections is out of range");

            this._hostIp = ip;
            this._port = port;

            _clientSockets = new List<EndPoint>();
            _maxNumberAcceptedClients = new Semaphore(_numConnections, _numConnections);
        }

        public void Start()
        {
            try
            {
                _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _listenSocket.Bind(new IPEndPoint(IPAddress.Parse(_hostIp), _port));

                RecvAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void RecvAsync()
        {
            await Task.Run(new Action(() =>
            {
                var _len = 0;
                var _buffer = new byte[_bufferSize];
                EndPoint _point = new IPEndPoint(IPAddress.Any, 0);
                try
                {
                    while ((_len = _listenSocket.ReceiveFrom(_buffer, ref _point)) > 0)
                    {
                        if (RecvEvent != null)
                            RecvEvent( _point, Encoding.UTF8.GetString(_buffer, 0, _len), _len);
                        _clientSockets.Add(_point);
                    }

                }
                catch (Exception)
                {
                    //CloseClientSocket(acceptSocket);
                }
            }));
        }

        public async void SendAsync(EndPoint point, string message)
        {
            await Task.Run(new Action(() =>
            {
                var _len = 0;
                var _buffer = Encoding.UTF8.GetBytes(message);

                try
                {
                    if ((_len = _listenSocket.SendTo(_buffer, point)) > 0)
                    {
                        if (SendResultEvent != null)
                            SendResultEvent(_len);
                    }
                }
                catch (Exception)
                {
                    //CloseClientSocket(acceptSocket);
                }
            }));
        }

        public async void SendMessageToAllClientsAsync(string message)
        {
            await Task.Run(new Action(() =>
            {
                foreach (var _point in _clientSockets)
                {
                    SendAsync(_point, message);
                }
            }));
        }

        private void CloseClientSocket(Socket acceptSocket)
        {
            try
            {
                acceptSocket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            try
            {
                acceptSocket.Close();
            }
            catch { }
            try
            {
                _maxNumberAcceptedClients.Release();
            }
            catch { 
            }
        }

        public void CloseAllClientSocket()
        {
            try
            {
                foreach (var _socket in _clientSockets)
                {
                    //socket.Shutdown(SocketShutdown.Both);
                }
            }
            catch { }
            try
            {
                foreach (var _socket in _clientSockets)
                {
                    //socket.Close();
                }
            }
            catch { }

            try
            {
                _listenSocket.Shutdown(SocketShutdown.Both);
            }
            catch { }
            try
            {
                _listenSocket.Close();
            }
            catch { }

            try
            {
                _maxNumberAcceptedClients.Release(_clientSockets.Count);
                _clientSockets.Clear();
            }
            catch { }
        }
    }
}
