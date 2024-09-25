// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="MetroComboFrame.xaml.cs" company="Terry D. Eppler">
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
//   MetroComboFrame.xaml.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for MetroComboFrame.xaml
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public partial class MetroComboFrame : UserControl
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
        /// The focused text
        /// </summary>
        private readonly Color _focusedText = new Color( )
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255
        };

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
        /// The back hover color
        /// </summary>
        private protected Color _containerColor = new Color( )
        {
            A = 255,
            R = 40,
            G = 40,
            B = 40
        };

        /// <summary>
        /// The fore color
        /// </summary>
        private protected Color _foreColor = new Color( )
        {
            A = 255,
            R = 160,
            G = 189,
            B = 252
        };

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption
        {
            get
            {
                return Label.Content.ToString( );
            }
            set
            {
                Label.Content = value;
            }
        }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public string Input
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                TextBox.Text = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ninja.MetroComboFrame" /> class.
        /// </summary>
        public MetroComboFrame( )
        {
            InitializeComponent( );

            // Basic Settings
            FontFamily = new FontFamily( "Roboto-Regular" );
            Width = 220;
            MinWidth = 180;
            MaxWidth = 270;
            Height = 120;
            MinHeight = 118;
            MaxHeight = 140;
            Label.Width = 200;
            Label.MinWidth = 150;
            Label.MaxWidth = 250;
            Label.Height = 25;
            Label.MinHeight = 18;
            Label.MaxHeight = 30;
            ComboBox.Width = 200;
            ComboBox.MinWidth = 150;
            ComboBox.MaxWidth = 250;
            ComboBox.Height = 30;
            ComboBox.MinHeight = 25;
            ComboBox.MaxHeight = 35;
            TextBox.Width = 200;
            TextBox.MinWidth = 150;
            TextBox.MaxWidth = 250;
            TextBox.Height = 25;
            TextBox.MinHeight = 18;
            TextBox.MaxHeight = 30;
            Background = new SolidColorBrush( Colors.Transparent );
            Foreground = new SolidColorBrush( _foreColor );
            BorderBrush = new SolidColorBrush( _borderColor );
            TextBox.Background = new SolidColorBrush( _containerColor );
            TextBox.Foreground = new SolidColorBrush( _foreColor );
            Label.Background = new SolidColorBrush( Colors.Transparent );
            Label.Foreground = new SolidColorBrush( _foreColor );
            Label.Content = "Name";
            TextBox.Text = "Value";
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