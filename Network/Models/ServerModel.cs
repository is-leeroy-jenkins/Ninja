﻿// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="ServerModel.cs" company="Terry D. Eppler">
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
//   ServerModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Models
{
    using System.Diagnostics.CodeAnalysis;
    using ViewModels;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class ServerModel : MainWindowBase
    {
        /// <summary>
        /// The net information item source
        /// </summary>
        private NetInterfaceInfo[ ] _netInfoItemSource;

        /// <summary>
        /// Gets or sets the net information item source.
        /// </summary>
        /// <value>
        /// The net information item source.
        /// </value>
        public NetInterfaceInfo[ ] NetInfoItemSource
        {
            get { return _netInfoItemSource; }
            set
            {
                if( _netInfoItemSource != value )
                {
                    _netInfoItemSource = value;
                    OnPropertyChanged( nameof( NetInfoItemSource ) );
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ServerModel"/> class.
        /// </summary>
        public ServerModel( )
        {
        }
    }
}