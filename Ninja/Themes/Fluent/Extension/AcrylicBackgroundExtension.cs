// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="AcrylicBackgroundExtension.cs" company="Terry D. Eppler">
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
//   AcrylicBackgroundExtension.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    /// <summary>
    /// Helper markup extension class to apply acrylic background for different containers
    /// </summary>
    /// <example>
    /// <code language="XAML">
    /// <![CDATA[
    /// <Grid x:Name="acrylicTargetContainer" Background="Red">
    /// <Border Width = "300" Height="200" HorizontalAlignment="Center" VerticalAlignment="Center" Background="{syncfusion:AcrylicBackground BackgroundLayerElement={Binding ElementName= acrylicTargetContainer}">
    /// <TextBlock Text = "Testing" HorizontalAlignment="Center" VerticalAlignment="Center"/>
    /// </Border>
    /// </Grid>
    /// ]]>
    /// </code>
    /// </example>
    /// <exclude/>
    [ EditorBrowsable( EditorBrowsableState.Never ) ]
    [ Browsable( false ) ]
    internal class AcrylicBackgroundExtension : MarkupExtension
    {
        /// <summary>
        /// Gets or sets the <see cref="AcrylicBackgroundExtension.BackgroundLayerElement"/> whose object UI will be utilized as acrylic background layer in application.
        /// </summary>
        /// <value>
        /// The <see cref="FrameworkElement"/> Target object. The default value is <b>null</b>.
        /// </value>     
        public FrameworkElement BackgroundLayerElement { get; set; }

        private Brush tintBrush = Brushes.White;

        /// <summary>
        /// Gets or sets the <see cref="AcrylicBackgroundExtension.TintBrush"/> property value which will be utilized as tint layer brush to apply acrylic background layer in application.
        /// </summary>
        /// <value>
        /// The <see cref="Brush"/> to achieve tint layer. The default value is <b><see cref="Brushes.White"/></b>.
        /// </value>      
        public Brush TintBrush
        {
            get { return tintBrush; }
            set { tintBrush = value; }
        }

        private Brush noiseBrush = Brushes.Transparent;

        /// <summary>
        /// Gets or sets the <see cref="AcrylicBackgroundExtension.NoiseBrush"/> property value which will be utilized as noise layer brush to apply acrylic background layer in application.
        /// </summary>
        /// <value>
        /// The <see cref="Brush"/> to achieve noise layer. The default value is <b><see cref="Brushes.Transparent"/></b>.
        /// </value>    
        public Brush NoiseBrush
        {
            get { return noiseBrush; }
            set { noiseBrush = value; }
        }

        private double tintOpacity = 0.3;

        /// <summary>
        /// Gets or sets the <see cref="AcrylicBackgroundExtension.TintOpacity"/> property value which will be utilized as tint layer opacity to apply acrylic background layer in application.
        /// </summary>
        /// <value>
        /// The <see cref="double"/> to achieve tint layer. The default value is <b>0.3</b>.
        /// </value>   
        public double TintOpacity
        {
            get { return tintOpacity; }
            set { tintOpacity = value; }
        }

        private double noiseOpacity = 0.9;

        /// <summary>
        /// Gets or sets the <see cref="AcrylicBackgroundExtension.NoiseOpacity"/> value which will be utilized as noise layer opacity to apply acrylic background layer in application.
        /// </summary>
        /// <value>
        /// The <see cref="double"/> to achieve noise layer. The default value is <b>0.9</b>.
        /// </value>     
        public double NoiseOpacity
        {
            get { return noiseOpacity; }
            set { noiseOpacity = value; }
        }

        private double blurRadius = 90.0;

        /// <summary>
        /// Gets or sets the <see cref="AcrylicBackgroundExtension.BlurRadius"/> property value which will be apply blur radius for acrylic background layer in application.
        /// </summary>
        /// <value>
        /// The <see cref="double"/> to achieve blur effect. The default value is <b>90</b>.
        /// </value>   
        public double BlurRadius
        {
            get { return blurRadius; }
            set { blurRadius = value; }
        }

        /// <summary>
        /// Default constructor receiving the target name to achieve acrylic background layer in application
        /// </summary>
        public AcrylicBackgroundExtension( )
        {
        }

        /// <summary>
        /// Constructor receiving the target name to achieve acrylic background layer in application
        /// </summary>
        /// <param name="target">Target object</param>
        public AcrylicBackgroundExtension( FrameworkElement target )
        {
            BackgroundLayerElement = target;
        }

        /// <summary>
        /// Helper method to process the acrylic background visual brush.
        /// </summary>
        /// <param name="serviceProvider">IServiceProvider</param>
        /// <returns>Acrylic Visual Brush</returns>
        /// <inheritdoc/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            var pvt =
                serviceProvider.GetService( typeof( IProvideValueTarget ) ) as IProvideValueTarget;

            var target = pvt.TargetObject as FrameworkElement;
            var acrylicPanel = new SfAcrylicPanel( )
            {
                TintBrush = TintBrush,
                NoiseBrush = NoiseBrush,
                TintOpacity = TintOpacity,
                NoiseOpacity = NoiseOpacity,
                BlurRadius = BlurRadius,
                BackgroundTarget = BackgroundLayerElement,
                Source = target,
                Width = target.Width,
                Height = target.Height
            };

            var brush = new VisualBrush( acrylicPanel )
            {
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                ViewboxUnits = BrushMappingMode.Absolute
            };

            return brush;
        }
    }
}