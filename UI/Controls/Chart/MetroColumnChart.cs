// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="MetroColumnChart.cs" company="Terry D. Eppler">
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
//   MetroColumnChart.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Syncfusion.UI.Xaml.Charts;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Syncfusion.UI.Xaml.Charts.SfChart3D" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeRedundantParentheses" ) ]
    [ SuppressMessage( "ReSharper", "MergeConditionalExpression" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public class MetroColumnChart : SfChart3D
    {
        /// <summary>
        /// The model palette
        /// </summary>
        private protected ChartColorModel _palette;

        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Badger.Chart3D" /> class.
        /// </summary>
        public MetroColumnChart( )
            : base( )
        {
            // Control Properties
            SetResourceReference( StyleProperty, typeof( SfChart3D ) );
            Width = 800;
            Height = 500;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            SideBySideSeriesPlacement = true;
            EnableRotation = true;
            Depth = 250;
            EnableSegmentSelection = true;
            EnableSeriesSelection = true;
            EnableRotation = true;
            PerspectiveAngle = 100;
            Padding = _theme.Padding;
            BorderThickness = _theme.BorderThickness;
            Background = _theme.Background;
            RightWallBrush = _theme.WallBrush;
            LeftWallBrush = _theme.WallBrush;
            BackWallBrush = _theme.WallBrush;
            TopWallBrush = _theme.WallBrush;
            BottomWallBrush = _theme.BlackBrush;
            BorderBrush = _theme.BorderBrush;
            Foreground = _theme.Foreground;
            PrimaryAxis = CreateCategoricalAxis( );
            SecondaryAxis = CreateNumericalAxis( );
            ColorModel = CreateColorModel( );
        }

        /// <summary>
        /// Creates the categorical axis3 d.
        /// </summary>
        /// <returns>
        /// </returns>
        private CategoryAxis3D CreateCategoricalAxis( )
        {
            try
            {
                var _categoricalAxis = new CategoryAxis3D
                {
                    FontSize = 10,
                    ShowOrigin = true,
                    Header = "Dimension",
                    Interval = 1,
                    Name = "Dimension",
                    Foreground = _theme.BorderBrush,
                    ShowGridLines = true,
                    IsIndexed = true,
                    IsEnabled = true,
                    LabelPlacement = LabelPlacement.OnTicks
                };

                return ( _categoricalAxis != null )
                    ? _categoricalAxis
                    : default( CategoryAxis3D );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( CategoryAxis3D );
            }
        }

        /// <summary>
        /// Creates the numerical axis.
        /// </summary>
        /// <returns>
        /// NumericalAxis3D 
        /// </returns>
        private NumericalAxis3D CreateNumericalAxis( int min = 0, int max = 1 )
        {
            try
            {
                var _numericalAxis = new NumericalAxis3D
                {
                    FontSize = 10,
                    ShowOrigin = true,
                    Header = "Value",
                    Name = "Value",
                    Minimum = min,
                    Maximum = max,
                    Interval = ( max - min ) / 10,
                    Foreground = _theme.BorderBrush,
                    ShowGridLines = true,
                    LabelsPosition = AxisElementPosition.Outside
                };

                return _numericalAxis;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( NumericalAxis3D );
            }
        }

        /// <summary>
        /// Creates the adornment.
        /// </summary>
        /// <returns></returns>
        private ChartAdornmentInfo3D CreateAdornmentInfo( )
        {
            try
            {
                var _adornment = new ChartAdornmentInfo3D
                {
                    ShowLabel = true,
                    ShowMarker = true,
                    ShowConnectorLine = true,
                    FontSize = 10,
                    AdornmentsPosition = AdornmentsPosition.Top,
                    LabelPosition = AdornmentsLabelPosition.Outer,
                    UseSeriesPalette = true,
                    BorderThickness = _theme.BorderThickness,
                    HighlightOnSelection = true,
                    ConnectorRotationAngle = 45,
                    Symbol = ChartSymbol.Diamond,
                    SymbolInterior = _theme.LightBlueBrush,
                    SymbolHeight = 8,
                    BorderBrush = _theme.BorderBrush,
                    Foreground = _theme.Foreground,
                    Background = _theme.ControlBackground
                };

                return _adornment;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ChartAdornmentInfo3D );
            }
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
                return ( _model.CustomBrushes.Count > 0 )
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
        /// Creates the palette.
        /// </summary>
        /// <returns></returns>
        private protected IList<Brush> CreatePalette( )
        {
            try
            {
                var _model = new List<Brush>( );
                _model.Add( _theme.SteelBlueBrush );
                _model.Add( _theme.GrayBrush );
                _model.Add( _theme.YellowBrush );
                _model.Add( _theme.RedBrush );
                _model.Add( _theme.KhakiBrush );
                _model.Add( _theme.GreenBrush );
                _model.Add( _theme.LightBlueBrush );
                return ( _model.Count > 0 )
                    ? _model
                    : default( IList<Brush> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<Brush> );
            }
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private protected void OnLoaded( object sender, RoutedEventArgs e )
        {
            try
            {
                ColorModel = CreateColorModel( );
            }
            catch( Exception ex )
            {
                Fail( ex );
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