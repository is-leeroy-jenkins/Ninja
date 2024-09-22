// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="Dimensions.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts
//    based on WPF, NET6.0, and written in C-Sharp.
// 
//    Copyright ©  2024  Terry D. Eppler
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
//   Dimensions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public abstract class Dimensions
    {
        /// <summary>
        /// The font famly
        /// </summary>
        private protected FontFamily _fontFamily;

        /// <summary>
        /// The font size
        /// </summary>
        private protected double _fontSize;

        /// <summary>
        /// The height
        /// </summary>
        private protected double _height;

        /// <summary>
        /// The width
        /// </summary>
        private protected double _width;

        /// <summary>
        /// The maximum height
        /// </summary>
        private protected double _maxHeight;

        /// <summary>
        /// The minimum height
        /// </summary>
        private protected double _minHeight;

        /// <summary>
        /// The maximum width
        /// </summary>
        private protected double _maxWidth;

        /// <summary>
        /// The minimum width
        /// </summary>
        private protected double _minWidth;

        /// <summary>
        /// The start up location
        /// </summary>
        private protected WindowStartupLocation _startLocation;

        /// <summary>
        /// The border thickness
        /// </summary>
        private protected Thickness _borderThickness;

        /// <summary>
        /// The padding
        /// </summary>
        private protected Thickness _padding;

        /// <summary>
        /// The margin
        /// </summary>
        private protected Thickness _margin;

        /// <summary>
        /// The re size mode
        /// </summary>
        private protected ResizeMode _reSizeMode;

        /// <summary>
        /// The window style
        /// </summary>
        private protected WindowStyle _windowStyle;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Dimensions"/> class.
        /// </summary>
        protected Dimensions( )
        {
            _fontFamily = new FontFamily( "Segoe UI" );
            _fontSize = 12;
            _minWidth = 1200;
            _width = 1400;
            _maxWidth = 1500;
            _minHeight = 600;
            _height = 800;
            _maxHeight = 1000;
            _borderThickness = new Thickness( 1 );
            _startLocation = WindowStartupLocation.CenterScreen;
            _windowStyle = WindowStyle.SingleBorderWindow;
            _reSizeMode = ResizeMode.CanResize;
        }

        /// <summary>
        /// Gets the window style.
        /// </summary>
        /// <value>
        /// The window style.
        /// </value>
        public WindowStyle WindowStyle
        {
            get
            {
                return _windowStyle;
            }
            private protected set
            {
                _windowStyle = value;
            }
        }

        /// <summary>
        /// Gets the start location.
        /// </summary>
        /// <value>
        /// The start location.
        /// </value>
        public WindowStartupLocation StartLocation
        {
            get
            {
                return _startLocation;
            }
            private protected set
            {
                _startLocation = value;
            }
        }

        /// <summary>
        /// Gets the size mode.
        /// </summary>
        /// <value>
        /// The size mode.
        /// </value>
        public ResizeMode SizeMode
        {
            get
            {
                return _reSizeMode;
            }
            private protected set
            {
                _reSizeMode = value;
            }
        }

        /// <summary>
        /// Gets the font family.
        /// </summary>
        /// <value>
        /// The font family.
        /// </value>
        public FontFamily FontFamily
        {
            get
            {
                return _fontFamily;
            }
            private protected set
            {
                _fontFamily = value;
            }
        }

        /// <summary>
        /// Gets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            private protected set
            {
                _fontSize = value;
            }
        }

        /// <summary>
        /// Gets the padding.
        /// </summary>
        /// <value>
        /// The padding.
        /// </value>
        public Thickness Padding
        {
            get
            {
                return _padding;
            }
            private protected set
            {
                _padding = value;
            }
        }

        /// <summary>
        /// Gets the margin.
        /// </summary>
        /// <value>
        /// The margin.
        /// </value>
        public Thickness Margin
        {
            get
            {
                return _margin;
            }
            private protected set
            {
                _margin = value;
            }
        }

        /// <summary>
        /// Gets the border thickness.
        /// </summary>
        /// <value>
        /// The border thickness.
        /// </value>
        public Thickness BorderThickness
        {
            get
            {
                return _borderThickness;
            }
            private protected set
            {
                _borderThickness = value;
            }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height
        {
            get
            {
                return _height;
            }
            private protected set
            {
                _height = value;
            }
        }

        /// <summary>
        /// Gets the minimum height.
        /// </summary>
        /// <value>
        /// The minimum height.
        /// </value>
        public double MinHeight
        {
            get
            {
                return _minHeight;
            }
            private protected set
            {
                _minHeight = value;
            }
        }

        /// <summary>
        /// Gets the height of the MSX.
        /// </summary>
        /// <value>
        /// The height of the MSX.
        /// </value>
        public double MaxHeight
        {
            get
            {
                return _maxHeight;
            }
            private protected set
            {
                _maxHeight = value;
            }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width
        {
            get
            {
                return _width;
            }
            private protected set
            {
                _width = value;
            }
        }

        /// <summary>
        /// Gets the minimum width.
        /// </summary>
        /// <value>
        /// The minimum width.
        /// </value>
        public double MinWidth
        {
            get
            {
                return _minWidth;
            }
            private protected set
            {
                _minWidth = value;
            }
        }

        /// <summary>
        /// Gets the maximum width.
        /// </summary>
        /// <value>
        /// The maximum width.
        /// </value>
        public double MaxWidth
        {
            get
            {
                return _maxWidth;
            }
            private protected set
            {
                _maxWidth = value;
            }
        }
    }
}