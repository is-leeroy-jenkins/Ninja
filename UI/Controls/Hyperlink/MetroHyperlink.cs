// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="MetroHyperlink.cs" company="Terry D. Eppler">
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
//   MetroHyperlink.cs
// </summary>
// ******************************************************************************************

// ReSharper disable All

namespace Ninja
{
    using ModernWpf.Controls;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="Wpf.Ui.Controls.HyperlinkButton" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public class MetroHyperlink : HyperlinkButton
    {
        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MetroHyperlink"/> class.
        /// </summary>
        public MetroHyperlink( )
            : base( )
        {
            // Basic Settings
            Height = 110;
            Width = 22;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            Background = _theme.TransparentBrush;
            Foreground = _theme.BorderBrush;
            BorderBrush = _theme.TransparentBrush;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MetroHyperlink"/> class.
        /// </summary>
        /// <param name = "text" > </param>
        /// <param name="uri">The URI.</param>
        public MetroHyperlink( string text, string uri )
            : this( )
        {
            Content = text;
            NavigateUri = new Uri( uri );
        }

        /// <summary>
        /// Fails the specified _ex.
        /// </summary>
        /// <param name="_ex">The _ex.</param>
        private protected void Fail( Exception _ex )
        {
            var _error = new ErrorWindow( _ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}