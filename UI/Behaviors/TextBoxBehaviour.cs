// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-21-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-21-2024
// ******************************************************************************************
// <copyright file="TextBoxBehaviour.cs" company="Terry D. Eppler">
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
//   TextBoxBehaviour.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Themes
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// </summary>
    public class TextBoxBehaviour
    {
        /// <summary>
        /// The associations
        /// </summary>
        private static readonly Dictionary<TextBox, Capture> _associations =
            new Dictionary<TextBox, Capture>( );

        /// <summary>
        /// Gets the scroll on text changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static bool GetScrollOnTextChanged( DependencyObject dependencyObject )
        {
            return ( bool )dependencyObject.GetValue( ScrollOnTextChangedProperty );
        }

        /// <summary>
        /// Sets the scroll on text changed.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetScrollOnTextChanged( DependencyObject dependencyObject, bool value )
        {
            dependencyObject.SetValue( ScrollOnTextChangedProperty, value );
        }

        /// <summary>
        /// The scroll on text changed property
        /// </summary>
        public static readonly DependencyProperty ScrollOnTextChangedProperty =
            DependencyProperty.RegisterAttached( "ScrollOnTextChanged", typeof( bool ),
                typeof( TextBoxBehaviour ),
                new UIPropertyMetadata( false, TextBoxBehaviour.OnScrollOnTextChanged ) );

        /// <summary>
        /// Called when [scroll on text changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnScrollOnTextChanged( DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e )
        {
            var _textBox = dependencyObject as TextBox;
            if( _textBox == null )
            {
                return;
            }

            bool _oldValue = ( bool )e.OldValue, _newValue = ( bool )e.NewValue;
            if( _newValue == _oldValue )
            {
                return;
            }

            if( _newValue )
            {
                _textBox.Loaded += TextBoxBehaviour.OnTextBoxLoaded;
                _textBox.Unloaded += TextBoxBehaviour.OnTextBoxUnloaded;
            }
            else
            {
                _textBox.Loaded -= TextBoxBehaviour.OnTextBoxLoaded;
                _textBox.Unloaded -= TextBoxBehaviour.OnTextBoxUnloaded;
                if( _associations.ContainsKey( _textBox ) )
                {
                    _associations[ _textBox ].Dispose( );
                }
            }
        }

        /// <summary>
        /// Texts the box unloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnTextBoxUnloaded( object sender, RoutedEventArgs routedEventArgs )
        {
            var _textBox = ( TextBox )sender;
            _associations[ _textBox ].Dispose( );
            _textBox.Unloaded -= TextBoxBehaviour.OnTextBoxUnloaded;
        }

        /// <summary>
        /// Texts the box loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="routedEventArgs">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnTextBoxLoaded( object sender, RoutedEventArgs routedEventArgs )
        {
            var _textBox = ( TextBox )sender;
            _textBox.Loaded -= TextBoxBehaviour.OnTextBoxLoaded;
            _associations[ _textBox ] = new Capture( _textBox );
        }
    }
}