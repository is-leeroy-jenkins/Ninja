// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="MetroCalendar.cs" company="Terry D. Eppler">
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
//   MetroCalendar.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Syncfusion.Windows.Shared;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Controls.Canvas" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class MetroCalendar : CalendarEdit
    {
        /// <summary>
        /// The border color
        /// </summary>
        private readonly Color _borderColor = new Color( )
        {
            A = 255,
            R = 0,
            G = 120,
            B = 212
        };

        /// <summary>
        /// The border hover color
        /// </summary>
        private readonly Color _borderHover = new Color( )
        {
            A = 255,
            R = 50,
            G = 93,
            B = 129
        };

        /// <summary>
        /// The back color
        /// </summary>
        private protected Color _backColor = new Color( )
        {
            A = 255,
            R = 45,
            G = 45,
            B = 45
        };

        /// <summary>
        /// The back hover color
        /// </summary>
        private protected Color _backHover = new Color( )
        {
            A = 255,
            R = 24,
            G = 49,
            B = 89
        };

        /// <summary>
        /// The fore color
        /// </summary>
        private protected Color _foreColor = new Color( )
        {
            A = 255,
            R = 106,
            G = 189,
            B = 252
        };

        /// <summary>
        /// The fore hover color
        /// </summary>
        private protected Color _foreHover = new Color( )
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255
        };

        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ninja.MetroCalendar" /> class.
        /// </summary>
        public MetroCalendar( )
            : base( )
        {
            // Control Properties
            BorderThickness = new Thickness( 0 );
            SetResourceReference( StyleProperty, typeof( CalendarEdit ) );
            FontFamily = _theme.FontFamily;
            Background = new SolidColorBrush( _backColor );
            BlackoutDatesBackground = new SolidColorBrush( _backColor );
            BlackoutDatesCrossBrush = new SolidColorBrush( _backColor );
            BlackoutDatesForeground = new SolidColorBrush( _backColor );
            BorderBrush = new SolidColorBrush( _borderColor );
            Foreground = new SolidColorBrush( _foreColor );
            HeaderBackground = new SolidColorBrush( _backColor );
            HeaderForeground = new SolidColorBrush( _foreColor );
            MouseOverBackground = new SolidColorBrush( Colors.SteelBlue );
            MouseOverForeground = new SolidColorBrush( Colors.White );
            NotCurrentMonthForeground = new SolidColorBrush( _foreColor );
            SelectedDayCellBackground = new SolidColorBrush( Colors.SteelBlue );
            SelectedDayCellForeground = new SolidColorBrush( Colors.White );
            SelectedDayCellBorderBrush = new SolidColorBrush( Colors.SteelBlue );
            SelectedDayCellHoverBackground = new SolidColorBrush( Colors.SteelBlue );
            TodayCellBackground = new SolidColorBrush( Colors.SteelBlue );
            TodayCellForeground = new SolidColorBrush( Colors.White );
            TodayCellSelectedBackground = new SolidColorBrush( Colors.SteelBlue );
            TodayCellSelectedBorderBrush = new SolidColorBrush( Colors.White );
            WeekNumberBackground = new SolidColorBrush( _backColor );
            WeekNumberBorderBrush = new SolidColorBrush( _backColor );
            WeekNumberForeground = new SolidColorBrush( _foreColor );
            WeekNumberHoverBackground = new SolidColorBrush( _backHover );
            WeekNumberHoverForeground = new SolidColorBrush( Colors.White );
            WeekNumberHoverBorderBrush = new SolidColorBrush( Colors.White );
            WeekNumberSelectionBackground = new SolidColorBrush( Colors.SteelBlue );
            WeekNumberSelectionForeground = new SolidColorBrush( Colors.White );
            WeekNumberSelectionBorderBrush = new SolidColorBrush( Colors.SteelBlue );
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