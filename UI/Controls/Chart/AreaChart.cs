// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="AreaChart.cs" company="Terry D. Eppler">
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
//   AreaChart.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Syncfusion.UI.Xaml.Charts;
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
    public class AreaChart : SfChart3D
    {
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
        /// The green
        /// </summary>
        private protected Color _green = Colors.DarkOliveGreen;

        /// <summary>
        /// The yellow
        /// </summary>
        private protected Color _khaki = Colors.DarkKhaki;

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

        /// <summary>
        /// The maroon
        /// </summary>
        private protected Color _maroon = Colors.Maroon;

        /// <summary>
        /// The model palette
        /// </summary>
        private protected ChartColorModel _modelPalette;

        /// <summary>
        /// The steel blue
        /// </summary>
        private protected Color _steelBlue = Colors.SteelBlue;

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
        /// The orange
        /// </summary>
        private protected Color _yellow = Colors.Yellow;

        /// <summary>
        /// Gets the model palette.
        /// </summary>
        /// <value>
        /// The model palette.
        /// </value>
        public ChartColorModel ModelPalette
        {
            get
            {
                return _modelPalette;
            }
            private protected set
            {
                _modelPalette = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.AreaChart" /> class.
        /// </summary>
        public AreaChart( )
            : base( )
        {
            // Control Properties
            SetResourceReference( AreaChart.StyleProperty, typeof( SfChart3D ) );
            Width = 800;
            Height = 500;
            FontFamily = new FontFamily( "Segoe UI" );
            FontSize = 12;
            EnableRotation = true;
            Depth = 250;
            EnableSegmentSelection = true;
            EnableSeriesSelection = true;
            EnableRotation = true;
            PerspectiveAngle = 100;
            SideBySideSeriesPlacement = true;
            Padding = new Thickness( 1 );
            BorderThickness = new Thickness( 1 );
            Background = new SolidColorBrush( _backColor );
            BorderBrush = new SolidColorBrush( _borderColor );
            Foreground = new SolidColorBrush( _foreColor );
            Header = "Area Chart";
            PrimaryAxis = new CategoryAxis3D( );
            PrimaryAxis.Header = "X-Axis";
            PrimaryAxis.Name = "Dimension";
            SecondaryAxis = new NumericalAxis3D( );
            SecondaryAxis.Header = "Y-Axis";
            SecondaryAxis.Name = "Value";
            _modelPalette = CreateColorModel( );
        }

        /// <summary>
        /// Creates the color model.
        /// </summary>
        /// <returns>
        /// ChartColorModel
        /// </returns>
        private protected ChartColorModel CreateColorModel( )
        {
            try
            {
                var _model = new ChartColorModel( );
                _model.CustomBrushes.Add( new SolidColorBrush( _steelBlue ) );
                _model.CustomBrushes.Add( new SolidColorBrush( _khaki ) );
                _model.CustomBrushes.Add( new SolidColorBrush( _maroon ) );
                _model.CustomBrushes.Add( new SolidColorBrush( _lightBlue ) );
                _model.CustomBrushes.Add( new SolidColorBrush( _yellow ) );
                _model.CustomBrushes.Add( new SolidColorBrush( _green ) );
                _model.CustomBrushes.Add( new SolidColorBrush( Colors.DarkGray ) );
                return _model.CustomBrushes.Count > 0
                    ? _model
                    : default( ChartColorModel );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ChartColorModel );
            }
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