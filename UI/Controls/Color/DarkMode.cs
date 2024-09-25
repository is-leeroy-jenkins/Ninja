// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="DarkMode.cs" company="Terry D. Eppler">
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
//   DarkMode.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.Palette" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    public class DarkMode : Palette
    {
        /// <inheritdoc />
        /// <summary>
        /// Creates the colors.
        /// </summary>
        /// <returns></returns>
        private protected override SolidColorBrush[ ] CreateColors( )
        {
            try
            {
                var _array = new[ ]
                {
                    SteelBlueBrush,
                    GrayBrush,
                    YellowBrush,
                    RedBrush,
                    DarkBlueBrush,
                    KhakiBrush,
                    GreenBrush,
                    LightBlueBrush
                };

                return _array?.Length > 0
                    ? _array
                    : default( SolidColorBrush[ ] );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( SolidColorBrush[ ] );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the color model.
        /// </summary>
        /// <returns>
        /// List( Brush )
        /// </returns>
        private protected override List<Brush> CreateColorModel( )
        {
            try
            {
                var _list = new List<Brush>
                {
                    SteelBlueBrush,
                    GrayBrush,
                    YellowBrush,
                    RedBrush,
                    DarkBlueBrush,
                    KhakiBrush,
                    GreenBrush,
                    LightBlueBrush
                };

                return _list?.Count > 0
                    ? _list
                    : default( List<Brush> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( List<Brush> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the color map.
        /// </summary>
        /// <returns>
        /// Dictionary(string, Brush )
        /// </returns>
        private protected override IDictionary<string, Brush> CreateColorMap( )
        {
            try
            {
                var _map = new Dictionary<string, Brush>( );
                _map.Add( "ItemHoverColor", SteelBlueBrush );
                _map.Add( "GrayColor", GrayBrush );
                _map.Add( "YellowColor", YellowBrush );
                _map.Add( "RedColor", RedBrush );
                _map.Add( "DarkBlueColor", DarkBlueBrush );
                _map.Add( "KhakiColor", KhakiBrush );
                _map.Add( "GreenColor", GreenBrush );
                _map.Add( "LightBlue", LightBlueBrush );
                return _map?.Count > 0
                    ? _map
                    : default( IDictionary<string, Brush> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IDictionary<string, Brush> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Badger.DarkTheme" /> class.
        /// </summary>
        public DarkMode( )
            : base( )
        {
            Foreground = new SolidColorBrush( _foreColor );
            Background = new SolidColorBrush( _backColor );
            BorderBrush = new SolidColorBrush( _borderColor );
            WallBrush = new SolidColorBrush( _wallColor );
            ControlBackground = new SolidColorBrush( _controlBackColor );
            ControlInterior = new SolidColorBrush( _controlInteriorColor );
            MutedBorderBrush = new SolidColorBrush( _mutedBorderColor );
            SteelBlueBrush = new SolidColorBrush( _steelBlueColor );
            GrayBrush = new SolidColorBrush( Colors.DarkGray );
            YellowBrush = new SolidColorBrush( _yellowColor );
            RedBrush = new SolidColorBrush( _redColor );
            DarkBlueBrush = new SolidColorBrush( _darkBlueColor );
            DarkRedBrush = new SolidColorBrush( _darkRedColor );
            DarkGreenBrush = new SolidColorBrush( _darkGreenColor );
            DarkYellowBrush = new SolidColorBrush( _darkGreenColor );
            KhakiBrush = new SolidColorBrush( _khakiColor );
            GreenBrush = new SolidColorBrush( _greenColor );
            LightBlueBrush = new SolidColorBrush( _lightBlue );
            BlackBrush = new SolidColorBrush( _blackColor );
            WhiteForeground = new SolidColorBrush( _whiteColor );
            FontFamily = new FontFamily( "Segoe UI" );
            FontSize = 12;
            Padding = new Thickness( 1 );
            Margin = new Thickness( 1 );
            BorderThickness = new Thickness( 1 );
            WindowStyle = WindowStyle.SingleBorderWindow;
            SizeMode = ResizeMode.CanResize;
            StartLocation = WindowStartupLocation.CenterScreen;
            _color = CreateColors( );
            _colorModel = CreateColorModel( );
            _colorMap = CreateColorMap( );
        }

        /// <summary>
        /// Gets the color of the dark blue.
        /// </summary>
        /// <value>
        /// The color of the dark blue.
        /// </value>
        public SolidColorBrush DarkBlueBrush { get; private protected init; }

        /// <summary>
        /// Gets the color of the muted border.
        /// </summary>
        /// <value>
        /// The color of the muted border.
        /// </value>
        public SolidColorBrush MutedBorderBrush { get; private protected init; }

        /// <summary>
        /// Gets the dark red brush.
        /// </summary>
        /// <value>
        /// The dark red brush.
        /// </value>
        public SolidColorBrush DarkRedBrush { get; private protected init; }

        /// <summary>
        /// Gets the dark green brush.
        /// </summary>
        /// <value>
        /// The dark green brush.
        /// </value>
        public SolidColorBrush DarkGreenBrush { get; private protected init; }

        /// <summary>
        /// Gets the dark yellow brush.
        /// </summary>
        /// <value>
        /// The dark yellow brush.
        /// </value>
        public SolidColorBrush DarkYellowBrush { get; private protected init; }
    }
}