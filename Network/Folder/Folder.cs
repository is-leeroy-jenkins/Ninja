// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="Folder.cs" company="Terry D. Eppler">
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
//   Folder.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.IO.Compression;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.FolderBase" />
    /// <seealso cref="T:Ninja.IFolder" />
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoProperty" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoPropertyWhenPossible" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class Folder : FolderBase
    {
        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory( )
        {
            try
            {
                return Environment.CurrentDirectory ?? string.Empty;
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
                return default( string );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the specified full path.
        /// </summary>
        /// <param name="filePath">
        /// The full path.
        /// </param>
        /// <returns>
        /// DirectoryInfo
        /// </returns>
        public static new DirectoryInfo Create( string filePath )
        {
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                return Directory.CreateDirectory( filePath );
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
                return default( DirectoryInfo );
            }
        }

        /// <summary>
        /// Deletes the specified folder name.
        /// </summary>
        /// <param name="folderName">
        /// Name of the folder.
        /// </param>
        public static void Delete( string folderName )
        {
            try
            {
                ThrowIf.Null( folderName, nameof( folderName ) );
                Directory.Delete( folderName, true );
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <summary>
        /// Creates the zip file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void ZipFiles( string source, string destination )
        {
            try
            {
                ThrowIf.Null( source, nameof( source ) );
                ThrowIf.Null( destination, nameof( destination ) );
                ZipFile.CreateFromDirectory( source, destination );
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the subdirectory.
        /// </summary>
        /// <param name="dirPath">The folderName.</param>
        /// <returns></returns>
        public DirectoryInfo CreateSubDirectory( string dirPath )
        {
            try
            {
                ThrowIf.Null( dirPath, nameof( dirPath ) );
                if( Directory.Exists( dirPath ) )
                {
                    var _message = @$"Folder at {dirPath} already exists!";
                    throw new ArgumentException( _message );
                }
                else
                {
                    var _stream = new DirectoryInfo( _fullPath );
                    var _folder = _stream.CreateSubdirectory( dirPath );
                    return _folder.Exists
                        ? _folder
                        : default( DirectoryInfo );
                }
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
                return default( DirectoryInfo );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Moves the specified fullName.
        /// </summary>
        /// <param name="destination">
        /// The fullName.
        /// </param>
        public override void MoveTo( string destination )
        {
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                if( Directory.Exists( destination ) )
                {
                    var _message = @$"Folder at {destination} already exists!";
                    throw new ArgumentException( _message );
                }
                else
                {
                    var _directory = new DirectoryInfo( _fullPath );
                    _directory.MoveTo( destination );
                }
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Zips the specified destination.
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        public void ZipFrom( string destination )
        {
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                ZipFile.CreateFromDirectory( _fileName, destination );
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Uns the zip.
        /// </summary>
        /// <param name="destination">
        /// The zipPath.
        /// </param>
        public void UnZipTo( string destination )
        {
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                ZipFile.ExtractToDirectory( destination, _fullPath );
            }
            catch( Exception ex )
            {
                PathBase.Fail( ex );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Folder" /> class.
        /// </summary>
        public Folder( )
            : base( )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Folder" /> class.
        /// </summary>
        /// <param name="input"></param>
        public Folder( string input )
            : base( input )
        {
            _input = input;
            _fullPath = input;
            _folderExists = Directory.Exists( input );
            _folderName = Path.GetDirectoryName( input );
            _hasSubFiles = Directory.GetFiles( input )?.Length > 0;
            _fileCount = Directory.GetFiles( input ).Length;
            _hasSubFolders = Directory.GetDirectories( input )?.Length > 0;
            _folderCount = Directory.GetDirectories( input ).Length;
            _created = Directory.GetCreationTime( input );
            _modified = Directory.GetLastWriteTime( input );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Folder" /> class.
        /// </summary>
        /// <param name="folder">The folder.</param>
        public Folder( Folder folder )
            : this( )
        {
            _input = folder.Input;
            _fullPath = folder.FullPath;
            _folderName = folder.FolderName;
            _hasSubFiles = Directory.GetFiles( folder.FullPath )?.Length > 0;
            _hasSubFolders = Directory.GetDirectories( folder.FullPath )?.Length > 0;
            _created = folder.Created;
            _modified = folder.Modified;
        }

        /// <summary>
        /// Deconstructs the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="fullPath">The full path.</param>
        /// <param name="folderName">Name of the file.</param>
        /// <param name="hasSubFiles"> </param>
        /// <param name="hasSubFolders"> </param>
        /// <param name="created">The created.</param>
        /// <param name="modified">The modified.</param>
        public void Deconstruct( out string buffer, out string fullPath, out string folderName,
            out bool hasSubFiles, out bool hasSubFolders, out DateTime created,
            out DateTime modified )
        {
            buffer = _input;
            fullPath = _fullPath;
            folderName = _folderName;
            hasSubFiles = _hasSubFiles;
            hasSubFolders = _hasSubFolders;
            created = _created;
            modified = _modified;
        }

        /// <inheritdoc />
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" />
        /// that represents this instance.
        /// </returns>
        public override string ToString( )
        {
            try
            {
                var _folder = new Folder( _input );
                var _name = _folder.FolderName ?? string.Empty;
                var _path = _folder.FullPath ?? string.Empty;
                var _dirPath = _folder.ParentPath ?? string.Empty;
                var _create = _folder.Created;
                var _modify = _folder.Modified;
                var _pathsep = _folder.PathSeparator;
                var _drivesep = _folder.DriveSeparator;
                var _foldersep = _folder.FolderSeparator;
                var _subfiles = _folder.FileCount;
                var _subfolders = _folder.FolderCount;
                var _bytes = ( _folder.Size.ToString( "N0" ) ?? "0" ) + " bytes";
                var _nl = Environment.NewLine;
                var _tb = char.ToString( '\t' );
                var _text = _nl + _tb + "Folder Name: " + _tb + _name + _nl + _nl + _tb
                    + "Folder Path: " + _tb + _path + _nl + _nl + _tb + "Parent Path: " + _tb
                    + _dirPath + _nl + _nl + _tb + "Sub-Files: " + _tb + _subfiles + _nl + _nl + _tb
                    + "Sub-Folders: " + _tb + _subfolders + _nl + _nl + _tb + "File Size: " + _tb
                    + _bytes + _nl + _nl + _tb + "Created On: " + _tb + _create.ToShortDateString()
                    + _nl + _nl + _tb + "Modified On: " + _tb + _modify.ToShortDateString() + _nl
                    + _nl + _tb + "Path Separator: " + _tb + _pathsep + _nl + _nl + _tb
                    + "Drive Separator: " + _tb + _drivesep + _nl + _nl + _tb + "Folder Separator: "
                    + _tb + _foldersep + _nl + _nl;

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

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        /// <value>
        /// The name of the folder.
        /// </value>
        public string FolderName
        {
            get
            {
                return _folderExists
                    ? Path.GetDirectoryName( _fullPath )
                    : string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the parent folder.
        /// </summary>
        /// <value>
        /// The parent folder.
        /// </value>
        public DirectoryInfo ParentFolder
        {
            get
            {
                return _hasParent
                    ? new DirectoryInfo( _fullPath ).Parent
                    : default( DirectoryInfo );
            }
        }

        /// <summary>
        /// Gets the file count.
        /// </summary>
        /// <value>
        /// The file count.
        /// </value>
        public int FileCount
        {
            get
            {
                return _fileCount;
            }
            set
            {
                if( _fileCount != value )
                {
                    _fileCount = value;
                    OnPropertyChanged( nameof( FileCount ) );
                }
            }
        }

        /// <summary>
        /// Gets the folder count.
        /// </summary>
        /// <value>
        /// The folder count.
        /// </value>
        public int FolderCount
        {
            get
            {
                return _folderCount;
            }
            set
            {
                if( _folderCount != value )
                {
                    _folderCount = value;
                    OnPropertyChanged( nameof( FolderCount ) );
                }
            }
        }
    }
}