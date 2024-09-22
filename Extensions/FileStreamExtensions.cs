// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="FileStreamExtensions.cs" company="Terry D. Eppler">
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
//   FileStreamExtensions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public static class FileStreamExtensions
    {
        /// <summary>
        /// The method provides an iterator through all lines of the str reader.
        /// </summary>
        /// <param name="reader">The str reader.</param>
        /// <returns>
        /// The iterator
        /// </returns>
        public static IEnumerable<string> IterateLines( this TextReader reader )
        {
            while( reader.ReadLine( ) != null )
            {
                yield return reader.ReadLine( );
            }
        }

        /// <summary>
        /// The method executes the passed delegate /lambda expression) for all lines of the str reader.
        /// </summary>
        /// <param name="reader">The str reader.</param>
        /// <param name="action">The action.</param>
        public static void IterateLines( this TextReader reader, Action<string> action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                foreach( var _line in reader.IterateLines( ) )
                {
                    action( _line );
                }
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Opens a StreamReader using the default encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The stream reader
        /// </returns>
        public static StreamReader GetReader( this Stream stream )
        {
            try
            {
                return stream.GetReader( null );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return default( StreamReader );
            }
        }

        /// <summary>
        /// Opens a StreamReader using the specified encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// The stream reader
        /// </returns>
        public static StreamReader GetReader( this Stream stream, Encoding encoding )
        {
            try
            {
                encoding ??= Encoding.Default;
                return new StreamReader( stream, encoding );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return default( StreamReader );
            }
        }

        /// <summary>
        /// Gets the writer.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static StreamWriter GetWriter( this Stream stream )
        {
            try
            {
                return stream.GetWriter( null );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return default( StreamWriter );
            }
        }

        /// <summary>
        /// Opens a StreamWriter using the specified encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// The stream writer
        /// </returns>
        public static StreamWriter GetWriter( this Stream stream, Encoding encoding )
        {
            if( stream?.CanWrite == true )
            {
                try
                {
                    encoding ??= Encoding.Default;
                    return new StreamWriter( stream, encoding );
                }
                catch( Exception ex )
                {
                    FileStreamExtensions.Fail( ex );
                    return default( StreamWriter );
                }
            }

            return default( StreamWriter );
        }

        /// <summary>
        /// Reads all str from the stream using the default encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The result str.
        /// </returns>
        public static string ReadToEnd( this Stream stream )
        {
            try
            {
                return stream.ReadToEnd( null );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Reads all str from the stream using a specified encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// The result str.
        /// </returns>
        public static string ReadToEnd( this Stream stream, Encoding encoding )
        {
            try
            {
                using var _reader = stream.GetReader( encoding );
                return _reader.ReadToEnd( );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Sets the stream cursor to the beginning of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The stream
        /// </returns>
        public static Stream SeekBeginning( this Stream stream )
        {
            if( stream?.CanSeek == true )
            {
                try
                {
                    stream.Seek( 0, SeekOrigin.Begin );
                    return stream;
                }
                catch( Exception ex )
                {
                    FileStreamExtensions.Fail( ex );
                    return default( Stream );
                }
            }

            return default( Stream );
        }

        /// <summary>
        /// Sets the stream cursor to the end of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The stream
        /// </returns>
        public static Stream SeekEnding( this Stream stream )
        {
            if( stream?.CanSeek == true )
            {
                try
                {
                    stream.Seek( 0, SeekOrigin.End );
                    return stream;
                }
                catch( Exception ex )
                {
                    FileStreamExtensions.Fail( ex );
                    return default( Stream );
                }
            }

            return default( Stream );
        }

        /// <summary>
        /// Copies one stream into another one.
        /// </summary>
        /// <param name="stream">The source stream.</param>
        /// <param name="target">The target stream.</param>
        /// <param name="buffer">The buffer size used to read / write.</param>
        /// <returns>
        /// The source stream.
        /// </returns>
        public static Stream CopyTo( this Stream stream, Stream target, int buffer )
        {
            if( target != null
                && stream.CanRead
                && target.CanWrite )
            {
                try
                {
                    var _buffer = new byte[ buffer ];
                    int _count;
                    while( ( _count = stream.Read( _buffer, 0, buffer ) ) > 0 )
                    {
                        target.Write( _buffer, 0, _count );
                    }

                    return stream;
                }
                catch( Exception ex )
                {
                    FileStreamExtensions.Fail( ex );
                    return default( MemoryStream );
                }
            }

            return default( MemoryStream );
        }

        /// <summary>
        /// Copies any stream into a local MemoryStream
        /// </summary>
        /// <param name="stream">The source stream.</param>
        /// <returns>
        /// The copied memory stream.
        /// </returns>
        public static MemoryStream CopyToMemory( this Stream stream )
        {
            try
            {
                var _memory = new MemoryStream( ( int )stream.Length );
                stream.CopyTo( _memory, ( int )stream.Length );
                return _memory;
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return default( MemoryStream );
            }
        }

        /// <summary>
        /// Reads the entire stream and returns an IEnumerable byte.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>
        /// The IEnumerable byte
        /// </returns>
        public static IEnumerable<byte> ReadAllBytes( this Stream stream )
        {
            try
            {
                var _memory = stream.CopyToMemory( );
                return _memory.ToArray( );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return default( IEnumerable<byte> );
            }
        }

        /// <summary>
        /// Reads a fixed number of bytes.
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <param name="bufsize">The number of bytes to read.</param>
        /// <returns>
        /// the read byte[]
        /// </returns>
        public static IEnumerable<byte> ReadFixedbuffersize( this Stream stream, int bufsize )
        {
            try
            {
                ThrowIf.NegativeOrZero( bufsize, nameof( bufsize ) );
                var _buffer = new byte[ bufsize ];
                var _offset = 0;
                do
                {
                    var _read = stream.Read( _buffer, _offset, bufsize - _offset );
                    if( _read == 0 )
                    {
                        return null;
                    }

                    _offset += _read;
                }
                while( _offset < bufsize );

                return _buffer;
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
                return default( IEnumerable<byte> );
            }
        }

        /// <summary>
        /// Writes all passed bytes to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="bytes">The byte array / buffer.</param>
        public static void Write( this Stream stream, byte[ ] bytes )
        {
            try
            {
                ThrowIf.Empty( bytes, nameof( bytes ) );
                stream.Write( bytes, 0, bytes.Length );
            }
            catch( Exception ex )
            {
                FileStreamExtensions.Fail( ex );
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