// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="WebMinion.cs" company="Terry D. Eppler">
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
//   WebMinion.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using static System.Configuration.ConfigurationManager;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public static class WebMinion
    {
        /// <summary>
        /// Launches the edge.
        /// </summary>
        public static void RunEdge( )
        {
            try
            {
                var _path = AppSettings[ "Edge" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                    _startInfo.Arguments =
                        @"C:\Users\terry\source\repos\Badger\Resources\Web\index.html";
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Runs the edge.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public static void RunEdge( string uri )
        {
            try
            {
                var _path = AppSettings[ "Edge" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized,
                    Arguments = uri
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Runs the budget browser.
        /// </summary>
        public static void RunBudgetBrowser( )
        {
            try
            {
                var _path = AppSettings[ "Baby" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the chrome.
        /// </summary>
        public static void RunChrome( )
        {
            try
            {
                var _path = AppSettings[ "Chrome" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                    _startInfo.Arguments =
                        @"C:\Users\terry\source\repos\Badger\Resources\Web\index.html";
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Runs the chrome.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public static void RunChrome( string uri )
        {
            try
            {
                var _path = AppSettings[ "Chrome" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized,
                    Arguments = uri
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Runs the firefox.
        /// </summary>
        public static void RunFirefox( )
        {
            try
            {
                var _path = AppSettings[ "Firefox" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                    _startInfo.Arguments =
                        @"C:\Users\terry\source\repos\Badger\Resources\Web\index.html";
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Runs the firefox.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public static void RunFirefox( string uri )
        {
            try
            {
                var _path = AppSettings[ "Firefox" ];
                var _startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized,
                    Arguments = uri
                };

                if( !string.IsNullOrEmpty( _path ) )
                {
                    _startInfo.FileName = _path;
                }

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WebMinion.Fail( ex );
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