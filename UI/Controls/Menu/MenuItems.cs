// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-30-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-30-2024
// ******************************************************************************************
// <copyright file="MenuItems.cs" company="Terry D. Eppler">
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
//   MenuItems.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class MenuItems : INotifyPropertyChanged
    {
        /// <summary>
        /// The menu image
        /// </summary>
        private protected string _menuImage;

        /// <summary>
        /// The menu name
        /// </summary>
        private protected string _menuName;

        /// <summary>
        /// Updates the specified field.
        /// </summary>
        /// <typeparam name="_"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>( ref T field, T value,
            [ CallerMemberName ] 
            string propertyName = null )

        {
            if( EqualityComparer<T>.Default.Equals( field, value ) )
            {
                return;
            }

            field = value;
            OnPropertyChanged( propertyName );
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            var _handler = PropertyChanged;
            _handler?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MenuItems"/> class.
        /// </summary>
        public MenuItems( )
        {
        }

        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        /// <value>
        /// The name of the menu.
        /// </value>
        public string MenuName
        {
            get
            {
                return _menuName;
            }
            set
            {
                if( _menuName != value )
                {
                    _menuName = value;
                    OnPropertyChanged( nameof( MenuName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the menu image.
        /// </summary>
        /// <value>
        /// The menu image.
        /// </value>
        public string MenuImage
        {
            get
            {
                return _menuImage;
            }
            set
            {
                if( _menuImage != value )
                {
                    _menuImage = value;
                    OnPropertyChanged( nameof( MenuImage ) );
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}