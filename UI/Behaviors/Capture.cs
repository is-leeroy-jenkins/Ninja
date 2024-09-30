// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-30-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-30-2024
// ******************************************************************************************
// <copyright file="Capture.cs" company="Terry D. Eppler">
// 
//     Ninja is a network toolkit that supports Iperf, TCP, UDP, Websocket, MQTT,
//     Sniffer, Pcap, Port Scan, Listen, IP Scan .etc.
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
//   Capture.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Windows.Controls;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.IDisposable" />
    public class Capture : IDisposable
    {
        /// <summary>
        /// Called when [text box on text changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TextChangedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnTextBoxOnTextChanged( object sender, TextChangedEventArgs args )
        {
            TextBox.ScrollToEnd( );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Capture"/> class.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public Capture( TextBox textBox )
        {
            TextBox = textBox;
            TextBox.TextChanged += OnTextBoxOnTextChanged;
        }

        /// <summary>
        /// Gets or sets the text box.
        /// </summary>
        /// <value>
        /// The text box.
        /// </value>
        private TextBox TextBox { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose( )
        {
            TextBox.TextChanged -= OnTextBoxOnTextChanged;
        }
    }
}