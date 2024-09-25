// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="MetroListViewItem.cs" company="Terry D. Eppler">
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
//   MetroListViewItem.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Controls.ListViewItem" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    public class MetroListViewItem : ListViewItem
    {
        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <summary>
        /// Gets or sets an arbitrary object value that can be used
        /// to store custom information about this element.
        /// </summary>
        public new object Tag { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// </summary>
        public MetroListViewItem( )
            : base( )
        {
            // Control Properties
            Height = 40;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            Background = _theme.ControlInterior;
            BorderBrush = _theme.ControlInterior;
            Foreground = _theme.LightBlueBrush;
            Margin = new Thickness( 10, 1, 1, 1 );
            Padding = _theme.Padding;
            BorderThickness = _theme.BorderThickness;
            HorizontalAlignment = HorizontalAlignment.Left;

            // Event Wiring
            MouseEnter += OnItemMouseEnter;
            MouseLeave += OnItemMouseLeave;
        }

        /// <summary>
        /// Called when [item mouse enter].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private protected void OnItemMouseEnter( object sender, MouseEventArgs e )
        {
            try
            {
                if( sender is MetroListBoxItem _item )
                {
                    _item.Foreground = _theme.WhiteForeground;
                    _item.Background = _theme.SteelBlueBrush;
                    _item.BorderBrush = _theme.SteelBlueBrush;
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Called when [item mouse leave].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private protected void OnItemMouseLeave( object sender, MouseEventArgs e )
        {
            try
            {
                if( sender is MetroListBoxItem _item )
                {
                    _item.Foreground = _theme.Foreground;
                    _item.Background = _theme.ControlInterior;
                    _item.BorderBrush = _theme.ControlInterior;
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private protected void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}