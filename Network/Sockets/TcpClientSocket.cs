using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Interfaces
{
    public class TcpClientSocket
    {
        public Action<Socket> ConnectEvent = null;
        public Action<Socket> DisConnectEvent = null;

        public Action<string> RecvEvent = null;
        public Action<int> SendResultEvent = null;

        private Socket _connectSocket = null;

        private string _hostIp = "";

        private int _port = 0;
        private int _bufferSize = 1024;

        public TcpClientSocket(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip))
                throw new ArgumentNullException("host ip cannot be null");
            if (port < 1 || port > 65535)
                throw new ArgumentOutOfRangeException("port is out of range");

            this._hostIp = ip;
            this._port = port;
        }

        public void Start()
        {
            try
            {
                _connectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _connectSocket.Connect(_hostIp, _port);
                if (ConnectEvent != null)
                    ConnectEvent(_connectSocket);
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
                try
                {
                    while ((_len = _connectSocket.Receive(_buffer, _bufferSize, SocketFlags.None)) > 0)
                    {
                        if (RecvEvent != null)
                            RecvEvent(Encoding.UTF8.GetString(_buffer, 0, _len));
                    }
                    if (DisConnectEvent != null)
                        DisConnectEvent(_connectSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }));
        }

        public async void SendAsync(string message)
        {
            await Task.Run(new Action(() =>
            {
                var _len = 0;
                var _buffer = Encoding.UTF8.GetBytes(message);
                try
                {
                    if ((_len = _connectSocket.Send(_buffer, _buffer.Length, SocketFlags.None)) > 0)
                    {
                        if (SendResultEvent != null)
                            SendResultEvent(_len);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }));
        }

        public void CloseClientSocket()
        {
            try
            {
                _connectSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _connectSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Restart()
        {
            CloseClientSocket();
            Start();
        }

    }

}
