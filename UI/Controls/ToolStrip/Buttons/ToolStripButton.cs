// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="ToolStripButton.cs" company="Terry D. Eppler">
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
//   ToolStripButton.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Input;

    /// <inheritdoc />
    ///  <summary>
    ///  </summary>
    ///  <seealso cref="T:Badger.MetroButton" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "PublicConstructorInAbstractClass" ) ]
    public class ToolStripButton : MetroTile
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ToolStripButton"/> class.
        /// </summary>
        /// <inheritdoc />
        public ToolStripButton( )
            : base( )
        {
            // Basic Properties
            Width = 40;
            Height = 30;
            Background = _theme.Background;
            Foreground = _theme.Background;
            BorderBrush = _theme.Background;
            Margin = _theme.Margin;
            Padding = _theme.Padding;
            BorderThickness = _theme.BorderThickness;

            // Event Wiring
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
        }

        /// <inheritdoc />
        /// <summary> Called when [mouse enter]. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e">
        /// The
        /// <see cref="T:System.EventArgs" />
        /// instance containing the event data.
        /// </param>
        private protected override void OnMouseEnter( object sender, MouseEventArgs e )
        {
            try
            {
                Background = _theme.DarkBlueBrush;
                Foreground = _theme.WhiteForeground;
                BorderBrush = _theme.LightBlueBrush;
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <inheritdoc />
        /// <summary> Called when [mouse leave]. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e">
        /// The
        /// <see cref="T:System.EventArgs" />
        /// instance containing the event data.
        /// </param>
        private protected override void OnMouseLeave( object sender, MouseEventArgs e )
        {
            try
            {
                Background = _theme.Background;
                Foreground = _theme.Background;
                BorderBrush = _theme.Background;
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }
    }
}