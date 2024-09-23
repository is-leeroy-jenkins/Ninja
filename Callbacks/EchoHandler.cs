﻿// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-21-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-21-2024
// ******************************************************************************************
// <copyright file="EchoHandler.cs" company="Terry D. Eppler">
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
//   EchoHandler.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WebSocketSharp.Server.WebSocketBehavior" />
    public class EchoHandler : WebSocketBehavior
    {
        /// <summary>
        /// The ws server
        /// </summary>
        private WebSocketServer _wsServer = null;

        /// <summary>
        /// The ws client
        /// </summary>
        public WebSocket WsClient;

        /// <summary>
        /// Gets or sets the ws recv.
        /// </summary>
        /// <value>
        /// The ws recv.
        /// </value>
        public static ObservableCollection<string> WsRecv { get; set; } =
            new ObservableCollection<string>( );

        /// <summary>
        /// Gets or sets the ws client recv.
        /// </summary>
        /// <value>
        /// The ws client recv.
        /// </value>
        public static ObservableCollection<string> WsClientRecv { get; set; } =
            new ObservableCollection<string>( );

        /// <summary>
        /// Raises the <see cref="E:Message" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        protected override void OnMessage( MessageEventArgs e )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                var _time = "[" + StartTime + "][";
                var _from = Context.UserEndPoint.ToString( );
                var _str = "][" + e.Data + "]\n";
                WsRecv.Add( _time + _from + _str );
            } ) );

            Send( e.Data );
        }

        /// <summary>
        /// Called when [open].
        /// </summary>
        protected override void OnOpen( )
        {
            var _time = "[" + StartTime + "][";
            var _from = Context.UserEndPoint.ToString( );
            var _status = "][" + State + "]\n";
            WsRecv.Add( _time + _from + _status );
        }

        /// <summary>
        /// Raises the Close event.
        /// </summary>
        /// <param name="e">The <see cref="CloseEventArgs"/> instance containing the event data.</param>
        protected override void OnClose( CloseEventArgs e )
        {
            var _time = "[" + StartTime + "][";
            var _reason = e.Reason;
            var _status = "][" + State + "]\n";
            WsRecv.Add( _time + _reason + _status );
        }

        /// <summary>
        /// Raises the <see cref="E:Error" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ErrorEventArgs"/> instance containing the event data.</param>
        protected override void OnError( ErrorEventArgs e )
        {
            var _time = "[" + StartTime + "][";
            var _reason = e.Message;
            var _status = "][" + State + "]\n";
            WsRecv.Add( _time + _reason + _status );
        }
    }
}