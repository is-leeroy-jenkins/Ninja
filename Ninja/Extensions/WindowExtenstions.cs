// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="WindowExtenstions.cs" company="Terry D. Eppler">
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
//   WindowExtenstions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Windows;
    using ToastNotifications;
    using ToastNotifications.Lifetime;
    using ToastNotifications.Messages;
    using ToastNotifications.Position;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "PublicConstructorInAbstractClass" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Local" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeRedundantParentheses" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public static class WindowExtenstions
    {
        /// <summary>
        /// Creates the notifier.
        /// </summary>
        /// <returns></returns>
        private static Notifier CreateNotifier( this Window window )
        {
            try
            {
                var _position = new PrimaryScreenPositionProvider( Corner.BottomRight, 10, 10 );
                var _lifeTime = new TimeAndCountBasedLifetimeSupervisor( TimeSpan.FromSeconds( 5 ),
                    MaximumNotificationCount.UnlimitedNotifications( ) );

                return new Notifier( _cfg =>
                {
                    _cfg.LifetimeSupervisor = _lifeTime;
                    _cfg.PositionProvider = _position;
                    _cfg.Dispatcher = Application.Current.Dispatcher;
                } );
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
                return default( Notifier );
            }
        }

        /// <summary>
        /// Sends the notification.
        /// </summary>
        /// <param name = "window" > </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void SendNotification( this Window window, string message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _notification = window.CreateNotifier( );
                _notification.ShowInformation( message );
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
            }
        }

        /// <summary>
        /// Shows the splash message.
        /// </summary>
        public static void SendMessage( this Window window, string message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _notification = window.CreateNotifier( );
                _notification.ShowSuccess( message );
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
            }
        }

        /// <summary>
        /// Fades the in asynchronous.
        /// </summary>
        /// <param name="window">The o.</param>
        /// <param name="interval">The interval.</param>
        private static async void FadeInAsync( this Window window, int interval = 80 )
        {
            try
            {
                ThrowIf.Null( window, nameof( window ) );
                while( window.Opacity < 1.0 )
                {
                    await Task.Delay( interval );
                    window.Opacity += 0.05;
                }

                window.Opacity = 1;
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
            }
        }

        /// <summary>
        /// Fades the out asynchronous.
        /// </summary>
        /// <param name="window">The o.</param>
        /// <param name="interval">The interval.</param>
        public static async void FadeOutAsync( this Window window, int interval = 80 )
        {
            try
            {
                ThrowIf.Null( window, nameof( window ) );
                while( window.Opacity > 0.0 )
                {
                    await Task.Delay( interval );
                    window.Opacity -= 0.05;
                }

                window.Opacity = 0;
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
            }
        }

        /// <summary>
        /// Invokes if needed.
        /// </summary>
        /// <param name = "window" > </param>
        /// <param name="action">The action.</param>
        public static void InvokeIf( this Window window, Action action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( window.Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( );
                }
                else
                {
                    window.Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
            }
        }

        /// <summary>
        /// Invokes if.
        /// </summary>
        /// <param name = "window" > </param>
        /// <param name="action">The action.</param>
        public static void InvokeIf( this Window window, Action<object> action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( window.Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( null );
                }
                else
                {
                    window.Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception ex )
            {
                WindowExtenstions.Fail( ex );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}