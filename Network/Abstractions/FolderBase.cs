// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="FolderBase.cs" company="Terry D. Eppler">
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
//   FolderBase.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Security.AccessControl;

    /// <inheritdoc/>
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "VirtualMemberNeverOverridden.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    public abstract class FolderBase : DataFile
    {
        /// <summary>
        /// The sub files
        /// </summary>
        private protected int _fileCount;

        /// <summary>
        /// The sub folders
        /// </summary>
        private protected int _folderCount;

        /// <summary>
        /// The folder exists
        /// </summary>
        private protected bool _folderExists;

        /// <summary>
        /// The folder name
        /// </summary>
        private protected string _folderName;

        /// <summary>
        /// The security
        /// </summary>
        private protected DirectorySecurity _folderSecurity;

        /// <summary>
        /// The sub files
        /// </summary>
        private protected bool _hasSubFiles;

        /// <summary>
        /// The sub folders
        /// </summary>
        private protected bool _hasSubFolders;

        /// <summary>
        /// The parent folder
        /// </summary>
        private protected DirectoryInfo _parentFolder;

        /// <summary>
        /// Gets the special folders.
        /// </summary>
        /// <returns>
        /// </returns>
        public IEnumerable<string> GetSpecialFolderNames( )
        {
            try
            {
                var _folders = Enum.GetNames( typeof( Environment.SpecialFolder ) );
                return _folders?.Any( ) == true
                    ? _folders
                    : default( string[ ] );
            }
            catch( IOException ex )
            {
                PathBase.Fail( ex );
                return default( IEnumerable<string> );
            }
        }

        /// <summary>
        /// Gets the sub file data.
        /// </summary>
        /// <returns>
        /// Dictionary
        /// </returns>
        public IDictionary<string, FileInfo> GetSubFiles( )
        {
            if( _hasSubFiles )
            {
                try
                {
                    var _data = new Dictionary<string, FileInfo>( );
                    var _paths = Directory.EnumerateFiles( _fullPath, "*",
                        SearchOption.AllDirectories );

                    foreach( var _path in _paths )
                    {
                        if( File.Exists( _path ) )
                        {
                            var _name = Path.GetFileName( _path );
                            var _subFile = new FileInfo( _path );
                            _data.Add( _name, _subFile );
                        }
                    }

                    return _data?.Any( ) == true
                        ? _data
                        : default( IDictionary<string, FileInfo> );
                }
                catch( IOException ex )
                {
                    PathBase.Fail( ex );
                    return default( IDictionary<string, FileInfo> );
                }
            }

            return default( IDictionary<string, FileInfo> );
        }

        /// <summary>
        /// Gets the subdirectory data.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, DirectoryInfo> GetSubFolders( )
        {
            if( _hasSubFolders )
            {
                try
                {
                    var _data = new Dictionary<string, DirectoryInfo>( );
                    var _folders = Directory.EnumerateDirectories( _fullPath, "*",
                        SearchOption.AllDirectories );

                    foreach( var _path in _folders )
                    {
                        if( Directory.Exists( _path ) )
                        {
                            var _name = Path.GetDirectoryName( _path );
                            var _directory = new DirectoryInfo( _path );
                            if( _name != null )
                            {
                                _data.Add( _name, _directory );
                            }
                        }
                    }

                    return _data?.Any( ) != true
                        ? _data
                        : default( IDictionary<string, DirectoryInfo> );
                }
                catch( Exception ex )
                {
                    PathBase.Fail( ex );
                    return default( IDictionary<string, DirectoryInfo> );
                }
            }

            return default( IDictionary<string, DirectoryInfo> );
        }

        /// <summary>
        /// Walks the paths.
        /// </summary>
        /// <returns>
        /// </returns>
        private protected IEnumerable<string> WalkDown( )
        {
            if( _hasSubFiles )
            {
                try
                {
                    var _list = new List<string>( );
                    var _paths = Directory.GetFiles( _input );
                    foreach( var _fp in _paths )
                    {
                        var _first = Directory.GetFiles( _fp )?.Where( f => File.Exists( f ) )
                            ?.Select( f => Path.GetFullPath( f ) )?.ToList( );

                        _list.AddRange( _first );
                        var _folders = Directory.GetDirectories( _fp );
                        foreach( var _fr in _folders )
                        {
                            if( !_fr.Contains( "My " ) )
                            {
                                var _second = Directory.GetFiles( _fr )
                                    ?.Where( s => File.Exists( s ) )
                                    ?.Select( s => Path.GetFullPath( s ) )?.ToList( );

                                _list.AddRange( _second );
                                var _subfolders = Directory.GetDirectories( _fr );
                                for( var _i = 0; _i < _subfolders.Length; _i++ )
                                {
                                    var _path = _subfolders[ _i ];
                                    var _last = Directory.GetFiles( _path )
                                        ?.Where( l => File.Exists( l ) )
                                        ?.Select( l => Path.GetFullPath( l ) )?.ToList( );

                                    _list.AddRange( _last );
                                }
                            }
                        }
                    }

                    return _list?.Any( ) == true
                        ? _list
                        : default( IEnumerable<string> );
                }
                catch( Exception ex )
                {
                    PathBase.Fail( ex );
                    return default( IEnumerable<string> );
                }
            }

            return default( IEnumerable<string> );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FolderBase"/> class.
        /// </summary>
        /// <inheritdoc />
        protected FolderBase( )
            : base( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FolderBase"/> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <inheritdoc />
        protected FolderBase( string input )
            : base( input )
        {
        }
    }
}