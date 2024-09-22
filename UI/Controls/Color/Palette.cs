// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="Palette.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts
//    based on WPF, NET6.0, and written in C-Sharp.
// 
//    Copyright ©  2020  Terry D. Eppler
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
//    You can contact me at: terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   Palette.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Media;
    using Color = System.Windows.Media.Color;
    using Colors = System.Windows.Media.Colors;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.Dimensions" />
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "UseObjectOrCollectionInitializer" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantExplicitArrayCreation" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public abstract class Palette : Dimensions
    {
        /// <inheritdoc />
        /// <summary>
        /// The back color
        /// </summary>
        private protected Color _backColor = new Color( )
        {
            A = 255,
            R = 20,
            G = 20,
            B = 20
        };

        /// <summary>
        /// The black color
        /// </summary>
        private protected Color _blackColor = new Color( )
        {
            A = 255,
            R = 0,
            G = 0,
            B = 0
        };

        /// <summary>
        /// The border color
        /// </summary>
        private protected Color _borderColor = new Color( )
        {
            A = 255,
            R = 0,
            G = 120,
            B = 212
        };

        /// <summary>
        /// The colors
        /// </summary>
        private protected SolidColorBrush[ ] _color;

        /// <summary>
        /// The color map
        /// </summary>
        private protected IDictionary<string, Brush> _colorMap;

        /// <summary>
        /// The model palette
        /// </summary>
        private protected List<Brush> _colorModel;

        /// <summary>
        /// The control color
        /// </summary>
        private protected Color _controlBackColor = new Color( )
        {
            A = 255,
            R = 40,
            G = 40,
            B = 40
        };

        /// <summary>
        /// The control color
        /// </summary>
        private protected Color _controlInteriorColor = new Color( )
        {
            A = 255,
            R = 45,
            G = 45,
            B = 45
        };

        /// <summary>
        /// The control hover color
        /// </summary>
        private protected Color _darkBlueColor = new Color( )
        {
            A = 255,
            R = 14,
            G = 60,
            B = 94
        };

        /// <summary>
        /// The fore color
        /// </summary>
        private protected Color _foreColor = new Color( )
        {
            A = 255,
            R = 222,
            G = 222,
            B = 222
        };

        /// <summary>
        /// The gray color
        /// </summary>
        private protected Color _grayColor = Colors.DarkGray;

        /// <inheritdoc />
        /// <summary>
        /// The green
        /// </summary>
        private protected Color _greenColor = Colors.DarkOliveGreen;

        /// <inheritdoc />
        /// <summary>
        /// The steel blue
        /// </summary>
        private protected Color _steelBlueColor = Colors.SteelBlue;

        /// <inheritdoc />
        /// <summary>
        /// The yellow
        /// </summary>
        private protected Color _khakiColor = Colors.DarkKhaki;

        /// <summary>
        /// The light blue
        /// </summary>
        private protected Color _lightBlue = new Color( )
        {
            A = 255,
            R = 160,
            G = 189,
            B = 252
        };

        /// <inheritdoc />
        /// <summary>
        /// The maroon
        /// </summary>
        private protected Color _redColor = Colors.Maroon;

        /// <summary>
        /// The transparent color
        /// </summary>
        private protected Color _transparentColor = new Color( )
        {
            A = 0,
            R = 0,
            G = 0,
            B = 0
        };

        /// <summary>
        /// The wall color
        /// </summary>
        private protected Color _wallColor = new Color( )
        {
            A = 255,
            R = 55,
            G = 55,
            B = 55
        };

        /// <summary>
        /// The black color
        /// </summary>
        private protected Color _whiteColor = new Color( )
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255
        };

        /// <inheritdoc />
        /// <summary>
        /// The orange
        /// </summary>
        private protected Color _yellowColor = Colors.Yellow;

        /// <summary>
        /// The muted border color
        /// </summary>
        private protected Color _mutedBorderColor = new Color( )
        {
            A = 255,
            R = 90,
            G = 90,
            B = 90
        };

        /// <summary>
        /// The dark green color
        /// </summary>
        private protected Color _darkGreenColor = new Color( )
        {
            A = 255,
            R = 1,
            G = 61,
            B = 17
        };

        /// <summary>
        /// The dark red color
        /// </summary>
        private protected Color _darkRedColor = new Color( )
        {
            A = 255,
            R = 40,
            G = 0,
            B = 0
        };

        /// <summary>
        /// The dark yellow color
        /// </summary>
        private protected Color _darkYellowColor = new Color( )
        {
            A = 255,
            R = 48,
            G = 59,
            B = 1
        };

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Palette" /> class.
        /// </summary>
        protected Palette( )
            : base( )
        {
        }

        /// <summary>
        /// Gets the color of the white.
        /// </summary>
        /// <value>
        /// The color of the white.
        /// </value>
        public SolidColorBrush WhiteForeground { get; private protected init; }

        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        /// <value>
        /// The colors.
        /// </value>
        public SolidColorBrush[ ] Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color map.
        /// </summary>
        /// <value>
        /// The color map.
        /// </value>
        public IDictionary<string, Brush> ColorMap
        {
            get
            {
                return _colorMap;
            }
            private protected set
            {
                _colorMap = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color model.
        /// </summary>
        /// <value>
        /// The color model.
        /// </value>
        public List<Brush> ColorModel
        {
            get
            {
                return _colorModel;
            }
            private protected set
            {
                _colorModel = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the fore.
        /// </summary>
        /// <value>
        /// The color of the fore.
        /// </value>
        public SolidColorBrush TransparentBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the black.
        /// </summary>
        /// <value>
        /// The color of the black.
        /// </value>
        public SolidColorBrush BlackBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the fore.
        /// </summary>
        /// <value>
        /// The color of the fore.
        /// </value>
        public SolidColorBrush Foreground { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the back.
        /// </summary>
        /// <value>
        /// The color of the back.
        /// </value>
        public SolidColorBrush Background { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        public SolidColorBrush BorderBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the color of the control.
        /// </summary>
        /// <value>
        /// The color of the control.
        /// </value>
        public SolidColorBrush ControlBackground { get; private protected init; }

        /// <summary>
        /// Gets the color of the interior.
        /// </summary>
        /// <value>
        /// The color of the interior.
        /// </value>
        public SolidColorBrush ControlInterior { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the color of the wall.
        /// </summary>
        /// <value>
        /// The color of the wall.
        /// </value>
        public SolidColorBrush WallBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the hover.
        /// </summary>
        /// <value>
        /// The color of the hover.
        /// </value>
        public SolidColorBrush SteelBlueBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the red.
        /// </summary>
        /// <value>
        /// The color of the red.
        /// </value>
        public SolidColorBrush RedBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the khaki.
        /// </summary>
        /// <value>
        /// The color of the khaki.
        /// </value>
        public SolidColorBrush KhakiBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the green.
        /// </summary>
        /// <value>
        /// The color of the green.
        /// </value>
        public SolidColorBrush GreenBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the light blue.
        /// </summary>
        /// <value>
        /// The light blue.
        /// </value>
        public SolidColorBrush LightBlueBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the yellow.
        /// </summary>
        /// <value>
        /// The color of the yellow.
        /// </value>
        public SolidColorBrush YellowBrush { get; private protected init; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the color of the gray.
        /// </summary>
        /// <value>
        /// The color of the gray.
        /// </value>
        public SolidColorBrush GrayBrush { get; private protected init; }

        /// <summary>
        /// Creates the colors.
        /// </summary>
        /// <returns></returns>
        private protected virtual SolidColorBrush[ ] CreateColors( )
        {
            try
            {
                var _array = new SolidColorBrush[ ]
                {
                    SteelBlueBrush,
                    GrayBrush,
                    YellowBrush,
                    RedBrush,
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
        /// <returns></returns>
        private protected virtual IList<Brush> CreateColorModel( )
        {
            try
            {
                var _list = new List<Brush>
                {
                    SteelBlueBrush,
                    GrayBrush,
                    YellowBrush,
                    RedBrush,
                    KhakiBrush,
                    GreenBrush,
                    LightBlueBrush
                };

                return _list?.Count > 0
                    ? _list
                    : default( IList<Brush> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<Brush> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the color map.
        /// </summary>
        /// <returns></returns>
        private protected virtual IDictionary<string, Brush> CreateColorMap( )
        {
            try
            {
                var _map = new Dictionary<string, Brush>( );
                _map.Add( "HoverColor", SteelBlueBrush );
                _map.Add( "GrayColor", GrayBrush );
                _map.Add( "YellowColor", YellowBrush );
                _map.Add( "RedColor", RedBrush );
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