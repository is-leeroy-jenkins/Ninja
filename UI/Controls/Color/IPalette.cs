// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="IPalette.cs" company="Terry D. Eppler">
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
//   IPalette.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// 
    /// </summary>
    internal interface IPalette
    {
        /// <summary>
        /// Gets the color of the fore.
        /// </summary>
        /// <value>
        /// The color of the fore.
        /// </value>
        SolidColorBrush Transparent { get; }

        /// <summary>
        /// Gets the color of the black.
        /// </summary>
        /// <value>
        /// The color of the black.
        /// </value>
        SolidColorBrush BlackColor { get; }

        /// <summary>
        /// Gets the color of the fore.
        /// </summary>
        /// <value>
        /// The color of the fore.
        /// </value>
        SolidColorBrush ForeColor { get; }

        /// <summary>
        /// Gets the color of the back.
        /// </summary>
        /// <value>
        /// The color of the back.
        /// </value>
        SolidColorBrush BackColor { get; }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        /// <value>
        /// The color of the border.
        /// </value>
        SolidColorBrush BorderColor { get; }

        /// <summary>
        /// Gets or sets the color of the control.
        /// </summary>
        /// <value>
        /// The color of the control.
        /// </value>
        SolidColorBrush ControlColor { get; }

        /// <summary>
        /// Gets or sets the color of the wall.
        /// </summary>
        /// <value>
        /// The color of the wall.
        /// </value>
        SolidColorBrush WallColor { get; }

        /// <summary>
        /// Gets the light blue.
        /// </summary>
        /// <value>
        /// The light blue.
        /// </value>
        SolidColorBrush LightBlueColor { get; }

        /// <summary>
        /// Gets the color of the hover.
        /// </summary>
        /// <value>
        /// The color of the hover.
        /// </value>
        SolidColorBrush ItemHoverColor { get; }

        /// <summary>
        /// Gets the color of the red.
        /// </summary>
        /// <value>
        /// The color of the red.
        /// </value>
        SolidColorBrush RedColor { get; }

        /// <summary>
        /// Gets the color of the khaki.
        /// </summary>
        /// <value>
        /// The color of the khaki.
        /// </value>
        SolidColorBrush KhakiColor { get; }

        /// <summary>
        /// Gets the color of the green.
        /// </summary>
        /// <value>
        /// The color of the green.
        /// </value>
        SolidColorBrush GreenColor { get; }

        /// <summary>
        /// Gets the color of the yellow.
        /// </summary>
        /// <value>
        /// The color of the yellow.
        /// </value>
        SolidColorBrush YellowColor { get; }

        /// <summary>
        /// Gets the color of the gray.
        /// </summary>
        /// <value>
        /// The color of the gray.
        /// </value>
        SolidColorBrush GrayColor { get; }

        /// <summary>
        /// Gets the color map.
        /// </summary>
        /// <value>
        /// The color map.
        /// </value>
        IDictionary<string, Brush> ColorMap { get; }

        /// <summary>
        /// Gets the color model.
        /// </summary>
        /// <value>
        /// The color model.
        /// </value>
        IList<Brush> ColorModel { get; }
    }
}