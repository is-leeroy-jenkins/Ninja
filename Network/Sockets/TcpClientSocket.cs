// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="TcpClientSocket.cs" company="Terry D. Eppler">
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
//   TcpClientSocket.cs
// </summary>
// ******************************************************************************************


namespace Ninja.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class TcpClientSocket
    {
        /// <summary>
        /// The buffer size
        /// </summary>
        private int _bufferSize = 1024;

        /// <summary>
        /// The connect socket
        /// </summary>
        private Socket _connectSocket;

        /// <summary>
        /// The host ip
        /// </summary>
        private string _hostIp = "";

        /// <summary>
        /// The port
        /// </summary>
        private int _port;

        /// <summary>
        /// The connect event
        /// </summary>
        public Action<Socket> ConnectEvent = null;

        /// <summary>
        /// The dis connect event
        /// </summary>
        public Action<Socket> DisConnectEvent = null;

        /// <summary>
        /// The recv event
        /// </summary>
        public Action<string> RecvEvent = null;

        /// <summary>
        /// The send result event
        /// </summary>
        public Action<int> SendResultEvent = null;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start( )
        {
            try
            {
                _connectSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream,
                    ProtocolType.Tcp );

                _connectSocket.Connect( _hostIp, _port );
                if( ConnectEvent != null )
                {
                    ConnectEvent( _connectSocket );
                }

                RecvAsync( );
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex.ToString( ) );
            }
        }

        /// <summary>
        /// Recvs the asynchronous.
        /// </summary>
        private async void RecvAsync( )
        {
            await Task.Run( ( ) =>
            {
                var _len = 0;
                var _buffer = new byte[ _bufferSize ];
                try
                {
                    while( ( _len =
                        _connectSocket.Receive( _buffer, _bufferSize, SocketFlags.None ) ) > 0 )
                    {
                        if( RecvEvent != null )
                        {
                            RecvEvent( Encoding.UTF8.GetString( _buffer, 0, _len ) );
                        }
                    }

                    if( DisConnectEvent != null )
                    {
                        DisConnectEvent( _connectSocket );
                    }
                }
                catch( Exception ex )
                {
                    Console.WriteLine( ex.ToString( ) );
                }
            } );
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        public async void SendAsync( string message )
        {
            await Task.Run( ( ) =>
            {
                var _len = 0;
                var _buffer = Encoding.UTF8.GetBytes( message );
                try
                {
                    if( ( _len = _connectSocket.Send( _buffer, _buffer.Length, SocketFlags.None ) )
                        > 0 )
                    {
                        if( SendResultEvent != null )
                        {
                            SendResultEvent( _len );
                        }
                    }
                }
                catch( Exception ex )
                {
                    Console.WriteLine( ex.ToString( ) );
                }
            } );
        }

        /// <summary>
        /// Closes the client socket.
        /// </summary>
        public void CloseClientSocket( )
        {
            try
            {
                _connectSocket.Shutdown( SocketShutdown.Both );
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex.ToString( ) );
            }

            try
            {
                _connectSocket.Close( );
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex.ToString( ) );
            }
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart( )
        {
            CloseClientSocket( );
            Start( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpClientSocket"/> class.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        /// <exception cref="ArgumentNullException">host ip cannot be null</exception>
        /// <exception cref="ArgumentOutOfRangeException">port is out of range</exception>
        public TcpClientSocket( string ip, int port )
        {
            if( string.IsNullOrEmpty( ip ) )
            {
                throw new ArgumentNullException( "host ip cannot be null" );
            }

            if( port < 1
                || port > 65535 )
            {
                throw new ArgumentOutOfRangeException( "port is out of range" );
            }

            _hostIp = ip;
            _port = port;
        }
    }
}