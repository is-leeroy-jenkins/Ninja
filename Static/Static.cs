// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="Static.cs" company="Terry D. Eppler">
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
//   Static.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public static class Static
    {
        /// <summary>
        /// Converts to log string.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// string
        /// </returns>
        public static string ToLogString( this Exception exception, string message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _builder = new StringBuilder( );
                _builder.Append( message );
                _builder.Append( Environment.NewLine );
                _builder.Append( Environment.NewLine );
                var _exception = exception;
                _builder.Append( "Exception: " );
                _builder.Append( Environment.NewLine );
                _builder.Append( Environment.NewLine );
                while( _exception != null )
                {
                    _builder.Append( _exception.Message );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                    _exception = _exception.InnerException;
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                }

                if( exception.Data != null )
                {
                    foreach( var _i in exception.Data )
                    {
                        _builder.Append( "Data : " );
                        _builder.Append( _i );
                        _builder.Append( Environment.NewLine );
                        _builder.Append( Environment.NewLine );
                    }
                }

                if( exception.StackTrace != null )
                {
                    _builder.Append( "Stack Trace: " );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( exception.StackTrace );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                }

                if( exception.Source != null )
                {
                    _builder.Append( "Error Source: " );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( exception.Source );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                }

                if( exception.TargetSite != null )
                {
                    _builder.Append( "Target Site: " );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( exception.TargetSite );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                }

                var _baseException = exception.GetBaseException( );
                if( _baseException != null )
                {
                    _builder.Append( "Base Exception: " );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( Environment.NewLine );
                    _builder.Append( exception.GetBaseException( ) );
                }

                return !string.IsNullOrEmpty( _builder.ToString( ) )
                    ? _builder.ToString( )
                    : string.Empty;
            }
            catch( Exception ex )
            {
                Static.Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts to dictionary.
        /// </summary>
        /// <param name="nvm">The NVM.</param>
        /// <returns>
        /// </returns>
        public static IDictionary<string, object> ToDictionary( this NameValueCollection nvm )
        {
            try
            {
                var _dictionary = new Dictionary<string, object>( );
                if( nvm != null )
                {
                    for( var _i = 0; _i < nvm.AllKeys.Length; _i++ )
                    {
                        var _key = nvm.AllKeys[ _i ];
                        if( _key != null )
                        {
                            _dictionary.Add( _key, nvm[ _key ] );
                        }
                    }
                }

                return _dictionary;
            }
            catch( Exception ex )
            {
                Static.Fail( ex );
                return default( IDictionary<string, object> );
            }
        }

        /// <summary>
        /// Fails the specified exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        private static void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}