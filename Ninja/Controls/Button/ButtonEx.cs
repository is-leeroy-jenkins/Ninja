// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="ButtonEx.cs" company="Terry D. Eppler">
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
//   ButtonEx.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Button" />
    public class ButtonEx : Button
    {
        /// <summary>
        /// Initializes the <see cref="ButtonEx"/> class.
        /// </summary>
        static ButtonEx( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( ButtonEx ),
                new FrameworkPropertyMetadata( typeof( ButtonEx ) ) );
        }

        /// <summary>
        /// Gets or sets the type of the button.
        /// </summary>
        /// <value>
        /// The type of the button.
        /// </value>
        public ButtonType ButtonType
        {
            get { return ( ButtonType )GetValue( ButtonTypeProperty ); }
            set { SetValue( ButtonTypeProperty, value ); }
        }

        /// <summary>
        /// The button type property
        /// </summary>
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register( "ButtonType", typeof( ButtonType ), typeof( ButtonEx ),
                new PropertyMetadata( ButtonType.Normal ) );

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public ImageSource Icon
        {
            get { return ( ImageSource )GetValue( IconProperty ); }
            set { SetValue( IconProperty, value ); }
        }

        /// <summary>
        /// The icon property
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register( "Icon", typeof( ImageSource ), typeof( ButtonEx ),
                new PropertyMetadata( null ) );

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>
        /// The corner radius.
        /// </value>
        public CornerRadius CornerRadius
        {
            get { return ( CornerRadius )GetValue( CornerRadiusProperty ); }
            set { SetValue( CornerRadiusProperty, value ); }
        }

        /// <summary>
        /// The corner radius property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register( "CornerRadius", typeof( CornerRadius ), typeof( ButtonEx ),
                new PropertyMetadata( new CornerRadius( 0 ) ) );

        /// <summary>
        /// Gets or sets the mouse over foreground.
        /// </summary>
        /// <value>
        /// The mouse over foreground.
        /// </value>
        public Brush MouseOverForeground
        {
            get { return ( Brush )GetValue( MouseOverForegroundProperty ); }
            set { SetValue( MouseOverForegroundProperty, value ); }
        }

        /// <summary>
        /// The mouse over foreground property
        /// </summary>
        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register( "MouseOverForeground", typeof( Brush ), typeof( ButtonEx ),
                new PropertyMetadata( ) );

        /// <summary>
        /// Gets or sets the mouse pressed foreground.
        /// </summary>
        /// <value>
        /// The mouse pressed foreground.
        /// </value>
        public Brush MousePressedForeground
        {
            get { return ( Brush )GetValue( MousePressedForegroundProperty ); }
            set { SetValue( MousePressedForegroundProperty, value ); }
        }

        /// <summary>
        /// The mouse pressed foreground property
        /// </summary>
        public static readonly DependencyProperty MousePressedForegroundProperty =
            DependencyProperty.Register( "MousePressedForeground", typeof( Brush ),
                typeof( ButtonEx ), new PropertyMetadata( ) );

        /// <summary>
        /// Gets or sets the mouse over borderbrush.
        /// </summary>
        /// <value>
        /// The mouse over borderbrush.
        /// </value>
        public Brush MouseOverBorderbrush
        {
            get { return ( Brush )GetValue( MouseOverBorderbrushProperty ); }
            set { SetValue( MouseOverBorderbrushProperty, value ); }
        }

        /// <summary>
        /// The mouse over borderbrush property
        /// </summary>
        public static readonly DependencyProperty MouseOverBorderbrushProperty =
            DependencyProperty.Register( "MouseOverBorderbrush", typeof( Brush ),
                typeof( ButtonEx ), new PropertyMetadata( ) );

        /// <summary>
        /// Gets or sets the mouse over background.
        /// </summary>
        /// <value>
        /// The mouse over background.
        /// </value>
        public Brush MouseOverBackground
        {
            get { return ( Brush )GetValue( MouseOverBackgroundProperty ); }
            set { SetValue( MouseOverBackgroundProperty, value ); }
        }

        /// <summary>
        /// The mouse over background property
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register( "MouseOverBackground", typeof( Brush ), typeof( ButtonEx ),
                new PropertyMetadata( ) );

        /// <summary>
        /// Gets or sets the mouse pressed background.
        /// </summary>
        /// <value>
        /// The mouse pressed background.
        /// </value>
        public Brush MousePressedBackground
        {
            get { return ( Brush )GetValue( MousePressedBackgroundProperty ); }
            set { SetValue( MousePressedBackgroundProperty, value ); }
        }

        /// <summary>
        /// The mouse pressed background property
        /// </summary>
        public static readonly DependencyProperty MousePressedBackgroundProperty =
            DependencyProperty.Register( "MousePressedBackground", typeof( Brush ),
                typeof( ButtonEx ), new PropertyMetadata( ) );
    }
}