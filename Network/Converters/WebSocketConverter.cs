// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-21-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-21-2024
// ******************************************************************************************
// <copyright file="WebSocketConverter.cs" company="Terry D. Eppler">
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
//   WebSocketConverter.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Diagnostics;
    using Models;
    using System.Collections.ObjectModel;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Data.IMultiValueConverter" />
    public class WebSocketConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts source values to a value for the binding target. The data binding
        /// engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        /// <param name="values">The array of values that the source
        /// bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces.
        /// The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates
        /// that the source binding has no value to provide for conversion.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value.
        /// If the method returns <see langword="null" />, the valid <see langword="null" /> value is used.
        /// A return value of <see cref="T:System.Windows.DependencyProperty" />.
        /// <see cref="F:System.Windows.DependencyProperty.UnsetValue" />
        /// indicates that the converter did not produce a value, and that the binding
        /// will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" />
        /// if it is available, or else will use the default value.
        /// A return value of <see cref="T:System.Windows.Data.Binding" />.
        /// <see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the
        /// binding does not transfer the value or use the
        /// <see cref="P:System.Windows.Data.BindingBase.FallbackValue" />
        /// or the default value.
        /// </returns>
        public object Convert( object[ ] values, Type targetType, object parameter,
            CultureInfo culture )
        {
            try
            {
                var _str = "";
                var _recv = ( ObservableCollection<string> )values[ 0 ];
                var _cnt = ( int )values[ 1 ];
                foreach( var _item in _recv )
                {
                    _str += System.Convert.ToString( _item );
                }

                return _str;
            }
            catch( Exception ex )
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a binding target value to the source binding values.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// An array of values that have been converted from the target value back to the source values.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter,
            CultureInfo culture )
        {
            throw new NotImplementedException( );
        }
    }
}