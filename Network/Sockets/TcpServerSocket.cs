// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="TcpServerSocket.cs" company="Terry D. Eppler">
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
//   TcpServerSocket.cs
// </summary>
// ******************************************************************************************

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
    public class TcpServerSocket
    {
        private int _bufferSize = 1024;

        private List<Socket> _clientSockets;

        private string _hostIp = "";

        private Socket _listenSocket;

        private Semaphore _maxNumberAcceptedClients;

        private int _numConnections = 65536;

        private int _port;

        public Action<Socket> ConnectEvent = null;

        public Action<Socket> DisConnectEvent = null;

        public Action<Socket, string> RecvEvent = null;

        public Action<int> SendResultEvent = null;

        public void Start( )
        {
            try
            {
                _listenSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream,
                    ProtocolType.Tcp );

                _listenSocket.Bind( new IPEndPoint( IPAddress.Parse( _hostIp ), _port ) );
                _listenSocket.Listen( _numConnections );
                AcceptAsync( );
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex.ToString( ) );
            }
        }

        private async void AcceptAsync( )
        {
            await Task.Run( new Action( ( ) =>
            {
                while( true )
                {
                    try
                    {
                        _maxNumberAcceptedClients.WaitOne( );
                        var _acceptSocket = _listenSocket.Accept( );
                        if( _acceptSocket == null )
                        {
                            continue;
                        }

                        _clientSockets.Add( _acceptSocket );
                        if( ConnectEvent != null )
                        {
                            ConnectEvent( _acceptSocket );
                        }

                        RecvAsync( _acceptSocket );
                        _maxNumberAcceptedClients.Release( );
                    }
                    catch( Exception )
                    {
                        //maxNumberAcceptedClients.Release();
                        Thread.Sleep( 1000 );
                    }
                }
            } ) );
        }

        private async void RecvAsync( Socket acceptSocket )
        {
            await Task.Run( ( ) =>
            {
                var _len = 0;
                var _buffer = new byte[ _bufferSize ];
                try
                {
                    while( ( _len = acceptSocket.Receive( _buffer, _bufferSize, SocketFlags.None ) )
                        > 0 )
                    {
                        if( RecvEvent != null )
                        {
                            RecvEvent( acceptSocket, Encoding.UTF8.GetString( _buffer, 0, _len ) );
                        }
                    }

                    if( DisConnectEvent != null )
                    {
                        DisConnectEvent( acceptSocket );
                    }
                }
                catch( Exception )
                {
                    CloseClientSocket( acceptSocket );
                }
            } );
        }

        public async void SendAsync( Socket acceptSocket, string message )
        {
            await Task.Run( ( ) =>
            {
                var _len = 0;
                var _buffer = Encoding.UTF8.GetBytes( message );
                try
                {
                    if( ( _len = acceptSocket.Send( _buffer, _buffer.Length, SocketFlags.None ) )
                        > 0 )
                    {
                        if( SendResultEvent != null )
                        {
                            SendResultEvent( _len );
                        }
                    }
                }
                catch( Exception )
                {
                    CloseClientSocket( acceptSocket );
                }
            } );
        }

        public async void SendMessageToAllClientsAsync( string message )
        {
            await Task.Run( ( ) =>
            {
                foreach( var _socket in _clientSockets )
                {
                    SendAsync( _socket, message );
                }
            } );
        }

        private void CloseClientSocket( Socket acceptSocket )
        {
            try
            {
                acceptSocket.Shutdown( SocketShutdown.Both );
            }
            catch { }

            try
            {
                acceptSocket.Close( );
            }
            catch { }

            try
            {
                _maxNumberAcceptedClients.Release( );
            }
            catch
            {
            }
        }

        public void CloseAllClientSocket( )
        {
            try
            {
                foreach( var _socket in _clientSockets )
                {
                    _socket.Shutdown( SocketShutdown.Both );
                }
            }
            catch { }

            try
            {
                foreach( var _socket in _clientSockets )
                {
                    _socket.Close( );
                }
            }
            catch { }

            try
            {
                _listenSocket.Shutdown( SocketShutdown.Both );
            }
            catch { }

            try
            {
                _listenSocket.Close( );
            }
            catch { }

            try
            {
                _maxNumberAcceptedClients.Release( _clientSockets.Count );
                _clientSockets.Clear( );
            }
            catch { }
        }

        public TcpServerSocket( string ip, int port )
        {
            if( string.IsNullOrEmpty( ip ) )
            {
                throw new ArgumentNullException( "host cannot be null" );
            }

            if( port < 1
                || port > 65535 )
            {
                throw new ArgumentOutOfRangeException( "port is out of range" );
            }

            if( _numConnections <= 0
                || _numConnections > int.MaxValue )
            {
                throw new ArgumentOutOfRangeException( "_numConnections is out of range" );
            }

            _hostIp = ip;
            _port = port;
            _clientSockets = new List<Socket>( );
            _maxNumberAcceptedClients = new Semaphore( _numConnections, _numConnections );
        }
    }
}