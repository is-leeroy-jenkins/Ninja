// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="WinMinion.cs" company="Terry D. Eppler">
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
//   WinMinion.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public static class WinMinion
    {
        /// <summary>
        /// Launches the windows calculator.
        /// </summary>
        public static void LaunchCalculator( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "CALC.EXE",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the outlook.
        /// </summary>
        public static void LaunchOutlook( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "Outlook.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the windows media player.
        /// </summary>
        public static void LaunchMediaPlayer( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "wmplayer.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the windows maps.
        /// </summary>
        public static void LaunchMaps( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "Maps.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the one drive.
        /// </summary>
        public static void LaunchOneDrive( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "OneDrive.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the calendar.
        /// </summary>
        public static void LaunchCalendar( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "calendar.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the clock.
        /// </summary>
        public static void LaunchClock( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "Clock.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the control panel.
        /// </summary>
        public static void LaunchControlPanel( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "control.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
            }
        }

        /// <summary>
        /// Launches the task manager.
        /// </summary>
        public static void LaunchTaskManager( )
        {
            try
            {
                var _startInfo = new ProcessStartInfo
                {
                    FileName = "taskmgr.exe",
                    UseShellExecute = true,
                    LoadUserProfile = true,
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                Process.Start( _startInfo );
            }
            catch( Exception ex )
            {
                WinMinion.Fail( ex );
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