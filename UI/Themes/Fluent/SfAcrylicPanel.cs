// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="SfAcrylicPanel.cs" company="Terry D. Eppler">
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
//   SfAcrylicPanel.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Controls.ContentControl" />
    [ EditorBrowsable( EditorBrowsableState.Never ) ]
    [ Browsable( false ) ]
    public class SfAcrylicPanel : ContentControl
    {
        /// <summary>
        /// The panel rect
        /// </summary>
        private Rectangle _panelRect;

        /// <summary>
        /// Gets or sets the background target.
        /// </summary>
        /// <value>
        /// The background target.
        /// </value>
        public FrameworkElement BackgroundTarget
        {
            get { return ( FrameworkElement )GetValue( BackgroundTargetProperty ); }
            set { SetValue( BackgroundTargetProperty, value ); }
        }

        /// <summary>
        /// The background target property
        /// </summary>
        public static readonly DependencyProperty BackgroundTargetProperty =
            DependencyProperty.Register( "BackgroundTarget", typeof( FrameworkElement ),
                typeof( SfAcrylicPanel ), new PropertyMetadata( null ) );

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public FrameworkElement Source
        {
            get { return ( FrameworkElement )GetValue( SourceProperty ); }
            set { SetValue( SourceProperty, value ); }
        }

        /// <summary>
        /// The source property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register( "Source", typeof( FrameworkElement ),
                typeof( SfAcrylicPanel ), new PropertyMetadata( null ) );

        /// <summary>
        /// Gets or sets the tint brush.
        /// </summary>
        /// <value>
        /// The tint brush.
        /// </value>
        public Brush TintBrush
        {
            get { return ( Brush )GetValue( TintBrushProperty ); }
            set { SetValue( TintBrushProperty, value ); }
        }

        /// <summary>
        /// The tint brush property
        /// </summary>
        public static readonly DependencyProperty TintBrushProperty =
            DependencyProperty.Register( "TintBrush", typeof( Brush ), typeof( SfAcrylicPanel ),
                new PropertyMetadata( new SolidColorBrush( Colors.White ) ) );

        /// <summary>
        /// Gets or sets the noise brush.
        /// </summary>
        /// <value>
        /// The noise brush.
        /// </value>
        public Brush NoiseBrush
        {
            get { return ( Brush )GetValue( NoiseBrushProperty ); }
            set { SetValue( NoiseBrushProperty, value ); }
        }

        /// <summary>
        /// The noise brush property
        /// </summary>
        public static readonly DependencyProperty NoiseBrushProperty =
            DependencyProperty.Register( "NoiseBrush", typeof( Brush ), typeof( SfAcrylicPanel ),
                new PropertyMetadata( new SolidColorBrush( Colors.White ) ) );

        /// <summary>
        /// Gets or sets the tint opacity.
        /// </summary>
        /// <value>
        /// The tint opacity.
        /// </value>
        public double TintOpacity
        {
            get { return ( double )GetValue( TintOpacityProperty ); }
            set { SetValue( TintOpacityProperty, value ); }
        }

        /// <summary>
        /// The tint opacity property
        /// </summary>
        public static readonly DependencyProperty TintOpacityProperty =
            DependencyProperty.Register( "TintOpacity", typeof( double ), typeof( SfAcrylicPanel ),
                new PropertyMetadata( 0.3 ) );

        /// <summary>
        /// Gets or sets the noise opacity.
        /// </summary>
        /// <value>
        /// The noise opacity.
        /// </value>
        public double NoiseOpacity
        {
            get { return ( double )GetValue( NoiseOpacityProperty ); }
            set { SetValue( NoiseOpacityProperty, value ); }
        }

        /// <summary>
        /// The noise opacity property
        /// </summary>
        public static readonly DependencyProperty NoiseOpacityProperty =
            DependencyProperty.Register( "NoiseOpacity", typeof( double ), typeof( SfAcrylicPanel ),
                new PropertyMetadata( 0.9 ) );

        /// <summary>
        /// Gets or sets the blur radius.
        /// </summary>
        /// <value>
        /// The blur radius.
        /// </value>
        public double BlurRadius
        {
            get { return ( double )GetValue( BlurRadiusProperty ); }
            set { SetValue( BlurRadiusProperty, value ); }
        }

        /// <summary>
        /// The blur radius property
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty =
            DependencyProperty.Register( "BlurRadius", typeof( double ), typeof( SfAcrylicPanel ),
                new PropertyMetadata( 90.0 ) );

        /// <inheritdoc />
        /// <summary>
        /// Initializes the <see cref="T:Badger.SfAcrylicPanel" /> class.
        /// </summary>
        static SfAcrylicPanel( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( SfAcrylicPanel ),
                new FrameworkPropertyMetadata( typeof( SfAcrylicPanel ) ) );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Badger.SfAcrylicPanel" /> class.
        /// </summary>
        public SfAcrylicPanel( )
        {
            Source = this;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate( )
        {
            base.OnApplyTemplate( );
            _panelRect = GetTemplateChild( "panelRect" ) as Rectangle;
            if( _panelRect != null )
            {
                _panelRect.LayoutUpdated += ( _, __ ) =>
                {
                    if( BackgroundTarget != null )
                    {
                        var relativePosition =
                            BackgroundTarget.TranslatePoint( new Point( 0, 0 ), Source );

                        _panelRect.RenderTransform =
                            new TranslateTransform( relativePosition.X, relativePosition.Y );
                    }
                };
            }
        }
    }
}