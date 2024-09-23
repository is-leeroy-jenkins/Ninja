// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="FileBase.cs" company="Terry D. Eppler">
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
//   FileBase.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Badger.BasicPath" />
    [ SuppressMessage( "ReSharper", "PublicConstructorInAbstractClass" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    public abstract class FileBase : DataPath
    {
        /// <summary>
        /// The busy
        /// </summary>
        private protected bool _busy;

        /// <summary>
        /// The exists
        /// </summary>
        private protected bool _fileExists;

        /// <summary>
        /// The parent name
        /// </summary>
        private protected string _parentName;

        /// <summary>
        /// The parent path
        /// </summary>
        private protected string _parentPath;

        /// <summary>
        /// The locked object
        /// </summary>
        private object _path = new object( );

        /// <summary>
        /// The size
        /// </summary>
        private protected long _size;

        /// <summary>
        /// Begins the initialize.
        /// </summary>
        private protected void Busy( )
        {
            try
            {
                lock( _path )
                {
                    _busy = true;
                }
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <summary>
        /// Ends the initialize.
        /// </summary>
        private protected void Chill( )
        {
            try
            {
                lock( _path )
                {
                    _busy = false;
                }
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <summary>
        /// Moves the specified file path.
        /// </summary>
        /// <param name="destination">
        /// The file path.
        /// </param>
        public virtual void MoveTo( string destination )
        {
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                if( File.Exists( destination ) )
                {
                    var _message = @$"File at {destination} already exists!";
                    throw new ArgumentException( _message );
                }
                else
                {
                    var _source = new FileInfo( _fullPath );
                    _source.MoveTo( destination );
                }
            }
            catch( IOException ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <summary>
        /// Copies the specified file path.
        /// </summary>
        /// <param name="destination">
        /// The file path.
        /// </param>
        public void CopyTo( string destination )
        {
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                if( File.Exists( destination ) )
                {
                    var _message = @$"File at {destination} already exists!";
                    throw new ArgumentException( _message );
                }
                else
                {
                    var _source = new FileInfo( _fullPath );
                    _source.CopyTo( destination );
                }
            }
            catch( IOException ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete( )
        {
            try
            {
                if( File.Exists( _fullPath ) )
                {
                    File.Delete( _fullPath );
                }
            }
            catch( IOException ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <summary>
        /// Gets the base stream.
        /// </summary>
        /// <returns></returns>
        private protected FileStream CreateBaseStream( )
        {
            if( _fileExists )
            {
                try
                {
                    var _file = new FileInfo( _fullPath );
                    var _stream = _file?.Create( );
                    return _stream;
                }
                catch( Exception ex )
                {
                    PathBase.Fail( ex );
                    return default( FileStream );
                }
            }

            return default( FileStream );
        }

        /// <summary>
        /// Reads the lines.
        /// </summary>
        /// <returns></returns>
        private protected IList<string> ReadLines( )
        {
            if( _fileExists )
            {
                try
                {
                    var _list = new List<string>( );
                    foreach( var _line in File.ReadAllLines( _input ) )
                    {
                        if( !string.IsNullOrEmpty( _line ) )
                        {
                            _list.Add( _line );
                        }
                    }

                    return _list?.Any( ) == true
                        ? _list
                        : default( List<string> );
                }
                catch( IOException ex )
                {
                    PathBase.Fail( ex );
                    return default( IList<string> );
                }
            }

            return default( IList<string> );
        }

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <returns>
        /// </returns>
        private protected byte[ ] ReadBytes( )
        {
            if( _fileExists )
            {
                try
                {
                    var _data = File.ReadAllBytes( _input );
                    return _data.Length > 0
                        ? _data
                        : default( byte[ ] );
                }
                catch( IOException ex )
                {
                    PathBase.Fail( ex );
                    return default( byte[ ] );
                }
            }

            return default( byte[ ] );
        }

        /// <summary>
        /// Writes the lines.
        /// </summary>
        /// <returns></returns>
        private protected string WriteLines( )
        {
            if( _fileExists )
            {
                try
                {
                    var _text = string.Empty;
                    var _list = ReadLines( );
                    for( var _i = 0; _i < _list.Count; _i++ )
                    {
                        _text += _list[ _i ];
                    }

                    return !string.IsNullOrEmpty( _text )
                        ? _text
                        : string.Empty;
                }
                catch( IOException ex )
                {
                    PathBase.Fail( ex );
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FileBase"/> class.
        /// </summary>
        /// <inheritdoc />
        protected FileBase( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FileBase"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <inheritdoc />
        protected FileBase( string input )
            : base( input )
        {
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        /// <c> true </c>
        /// if this instance is busy; otherwise,
        /// <c> false </c>
        /// </value>
        public bool IsBusy
        {
            get
            {
                lock( _path )
                {
                    return _busy;
                }
            }
        }
    }
}