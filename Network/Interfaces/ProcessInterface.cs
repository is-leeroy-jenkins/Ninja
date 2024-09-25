// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="ProcessInterface.cs" company="Terry D. Eppler">
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
//   ProcessInterface.cs
// </summary>
// ******************************************************************************************

namespace Ninja.Interfaces
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class ProcessInterface
    {
        /// <summary>
        /// The process
        /// </summary>
        public Process Process;

        /// <summary>
        /// Attaches the console.
        /// </summary>
        /// <param name="dwProcessId">The dw process identifier.</param>
        /// <returns></returns>
        [ DllImport( "kernel32.dll", SetLastError = true ) ]
        private static extern bool AttachConsole( uint dwProcessId );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private delegate Boolean _consoleCtrlDelegate( CtrlTypes type );

        /// <summary>
        /// Generates the console control event.
        /// </summary>
        /// <param name="dwCtrlEvent">The dw control event.</param>
        /// <param name="dwProcessGroupId">The dw process group identifier.</param>
        /// <returns></returns>
        [ DllImport( "kernel32.dll" ) ]
        [ return: MarshalAs( UnmanagedType.Bool ) ]
        private static extern bool GenerateConsoleCtrlEvent( CtrlTypes dwCtrlEvent,
            uint dwProcessGroupId );

        /// <summary>
        /// Sets the console control handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="add">if set to <c>true</c> [add].</param>
        /// <returns></returns>
        [ DllImport( "kernel32.dll" ) ]
        private static extern bool SetConsoleCtrlHandler( _consoleCtrlDelegate handler, bool add );

        /// <summary>
        /// Frees the console.
        /// </summary>
        /// <returns></returns>
        [ DllImport( "kernel32.dll", SetLastError = true, ExactSpelling = true ) ]
        private static extern bool FreeConsole( );

        /// <summary>
        /// Stops the process.
        /// </summary>
        public void StopProcess( )
        {
            // It's impossible to be attached to 2 consoles at the same time,
            // so release the current one.
            try
            {
                var _pid = ( uint )Process.Id;
                ProcessInterface.FreeConsole( );

                // This does not require the console window to be visible.
                if( ProcessInterface.AttachConsole( _pid ) )
                {
                    // Disable Ctrl-C handling for our program
                    ProcessInterface.SetConsoleCtrlHandler( null, true );
                    ProcessInterface.GenerateConsoleCtrlEvent( CtrlTypes.CtrlCEvent, 0 );

                    // Must wait here. If we don't and re-enable Ctrl-C
                    // handling below too fast, we might terminate ourselves.
                    Thread.Sleep( 500 );
                    ProcessInterface.FreeConsole( );

                    // Re-enable Ctrl-C handling or any subsequently started
                    // programs will inherit the disabled state.
                    ProcessInterface.SetConsoleCtrlHandler( null, false );
                    Process.Kill( );
                }
            }
            catch( Exception )
            {
            }
        }

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="exePath">The executable path.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="outputhandler">The outputhandler.</param>
        public async void StartProcess( string exePath, string parameters,
            DataReceivedEventHandler outputhandler )
        {
            try
            {
                Process = new Process( );
                Process.StartInfo.FileName = exePath;
                Process.StartInfo.Arguments = parameters;
                Process.StartInfo.UseShellExecute = false;
                Process.StartInfo.CreateNoWindow = true;
                Process.StartInfo.RedirectStandardOutput = true;
                Process.StartInfo.RedirectStandardError = true;
                Process.Start( );
                Process.BeginOutputReadLine( );
                Process.BeginErrorReadLine( );
                Process.OutputDataReceived += outputhandler;
                Process.ErrorDataReceived += outputhandler;
            }
            catch( Exception ex )
            {
            }
        }
    }
}