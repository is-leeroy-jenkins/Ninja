// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="MetroScatterChart.cs" company="Terry D. Eppler">
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
//   MetroScatterChart.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Syncfusion.UI.Xaml.Charts;
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Syncfusion.UI.Xaml.Charts.SfChart3D" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class MetroScatterChart : SfChart3D
    {
        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.MetroScatterChart" /> class.
        /// </summary>
        public MetroScatterChart( )
            : base( )
        {
            // Control Properties
            SetResourceReference( MetroScatterChart.StyleProperty, typeof( SfChart3D ) );
            Width = 800;
            Height = 454;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            EnableRotation = true;
            Depth = 250;
            EnableSegmentSelection = true;
            EnableSeriesSelection = true;
            PerspectiveAngle = 100;
            Padding = _theme.Padding;
            BorderThickness = _theme.BorderThickness;
            ColorModel = CreateColorModel( );
            Background = _theme.Background;
            RightWallBrush = _theme.WallBrush;
            LeftWallBrush = _theme.WallBrush;
            BackWallBrush = _theme.WallBrush;
            TopWallBrush = _theme.WallBrush;
            BottomWallBrush = _theme.BlackBrush;
            BorderBrush = _theme.WallBrush;
            Foreground = _theme.WallBrush;
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
                _model.CustomBrushes.Add( _theme.SteelBlueBrush );
                _model.CustomBrushes.Add( _theme.GrayBrush );
                _model.CustomBrushes.Add( _theme.YellowBrush );
                _model.CustomBrushes.Add( _theme.RedBrush );
                _model.CustomBrushes.Add( _theme.KhakiBrush );
                _model.CustomBrushes.Add( _theme.GreenBrush );
                _model.CustomBrushes.Add( _theme.LightBlueBrush );
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
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}