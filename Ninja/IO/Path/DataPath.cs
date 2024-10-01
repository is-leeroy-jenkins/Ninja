// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="DataPath.cs" company="Terry D. Eppler">
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
//   DataPath.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Security.AccessControl;

    /// <inheritdoc/>
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Badger.BasicPath"/>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "SuggestBaseTypeForParameterInConstructor" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class DataPath : PathBase
    {
        /// <summary>
        /// Deconstructs the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="absolutePath">The abs path.</param>
        /// <param name="name">The name.</param>
        /// <param name="fullPath">The full path.</param>
        /// <param name="length">The length.</param>
        /// <param name="extension"> </param>
        /// <param name="createDate">The created.</param>
        /// <param name="modifyDate">The modified.</param>
        public void Deconstruct( out string buffer, out string absolutePath, out string name,
            out string fullPath, out long length, out string extension,
            out DateTime createDate, out DateTime modifyDate )
        {
            buffer = _input;
            absolutePath = _absolutePath;
            name = _fileName;
            fullPath = _fullPath;
            extension = _fileExtension;
            length = _length;
            createDate = _created;
            modifyDate = _modified;
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
                var _path = new DataPath( _input );
                var _extenstion = _path.Extension ?? string.Empty;
                var _name = _path.FileName ?? string.Empty;
                var _filePath = _path.FullPath ?? string.Empty;
                var _create = _path.Created;
                var _modify = _path.Modified;
                var _len = _path.Length.ToString( "N0" ) ?? string.Empty;
                var _pathsep = _path.PathSeparator;
                var _drivesep = _path.DriveSeparator;
                var _foldersep = _path.FolderSeparator;
                var _root = _path.Drive;
                var _nl = Environment.NewLine;
                var _attrs = _path.FileAttributes;
                var _tb = char.ToString( '\t' );
                var _text = _nl + _tb + "File Name: " + _tb + _name + _nl + _nl + _tb
                    + "File Path: " + _tb + _filePath + _nl + _nl + _tb + "File Attributes: " + _tb
                    + _attrs + _nl + _nl + _tb + "Extension: " + _tb + _extenstion + _nl + _nl + _tb
                    + "Path Root: " + _tb + _root + _nl + _nl + _tb + "Path Separator: " + _tb
                    + _pathsep + _nl + _nl + _tb + "Drive Separator: " + _tb + _drivesep + _nl + _nl
                    + _tb + "Folder Separator: " + _tb + _foldersep + _nl + _nl + _tb + "Length: "
                    + _tb + _len + _nl + _nl + _tb + "Created: " + _tb
                    + _create.ToShortDateString( ) + _nl + _nl + _tb + "Modified: " + _tb
                    + _modify.ToShortDateString( ) + _nl + _nl;

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

        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.DataPath"/>
        /// class.
        /// </summary>
        public DataPath( )
        {
        }

        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.DataPath"/>
        /// class.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        public DataPath( string input )
        {
            _input = input;
            _hasExtension = Path.HasExtension( input );
            _fileExtension = Path.GetExtension( input );
            _hasParent = !string.IsNullOrEmpty( Directory.GetParent( input )?.Name );
            _isRooted = Path.IsPathRooted( input );
            _absolutePath = Path.GetFullPath( input );
            _relativePath = Environment.CurrentDirectory + input;
            _fileName = Path.GetFileNameWithoutExtension( input );
            _fullPath = Path.GetFullPath( input );
            _length = input.Length;
            _created = File.GetCreationTime( input );
            _modified = File.GetLastWriteTime( input );
            _invalidPathChars = Path.GetInvalidPathChars( );
            _invalidNameChars = Path.GetInvalidFileNameChars( );
            _pathSeparator = Path.PathSeparator;
            _folderSeparator = Path.AltDirectorySeparatorChar;
            _driveSeparator = Path.DirectorySeparatorChar;
            _drive = Path.GetPathRoot( input );
            _fileAttributes = File.GetAttributes( input );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DataPath"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public DataPath( DataPath path )
        {
            _input = path.Input;
            _hasExtension = Path.HasExtension( path.Input );
            _fileExtension = path.Extension;
            _hasParent = !string.IsNullOrEmpty( Directory.GetParent( path.Input )?.Name );
            _absolutePath = path.AbsolutePath;
            _relativePath = path.RelativePath;
            _fileName = path.FileName;
            _fullPath = path.FullPath;
            _length = path.Length;
            _created = path.Created;
            _modified = path.Modified;
            _invalidPathChars = path.InvalidPathChars;
            _invalidNameChars = path.InvalidNameChars;
            _pathSeparator = path.PathSeparator;
            _folderSeparator = path.FolderSeparator;
            _driveSeparator = path.DriveSeparator;
            _drive = path.Drive;
            _fileAttributes = path.FileAttributes;
        }

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                if( _input != value )
                {
                    _input = value;
                    OnPropertyChanged( nameof( Input ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
            set
            {
                if( _fullPath != value )
                {
                    _fullPath = value;
                    OnPropertyChanged( nameof( FullPath ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the absolute path.
        /// </summary>
        /// <value>
        /// The absolute path.
        /// </value>
        public string AbsolutePath
        {
            get
            {
                return _absolutePath;
            }
            set

            {
                if( _absolutePath != value )
                {
                    _absolutePath = value;
                    OnPropertyChanged( nameof( AbsolutePath ) );
                }
            }
        }

        /// <summary>
        /// Gets the relative path.
        /// </summary>
        /// <value>
        /// The relative path.
        /// </value>
        public string RelativePath
        {
            get
            {
                return _relativePath;
            }
            set
            {
                if( _relativePath != value )
                {
                    _relativePath = value;
                    OnPropertyChanged( nameof( RelativePath ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if( _fileName != value )
                {
                    _fileName = value;
                    OnPropertyChanged( nameof( FileName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        public DateTime Modified
        {
            get
            {
                return _modified;
            }
            set
            {
                if( _modified != value )
                {
                    _modified = value;
                    OnPropertyChanged( nameof( Modified ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether this instance has parent.
        /// </summary>
        /// <value>
        /// <c>true</c>
        /// if this instance has parent;
        /// otherwise,
        /// <c>false</c>.
        /// </value>
        public DirectoryInfo Parent
        {
            get
            {
                return _hasParent
                    ? Directory.GetParent( _input )
                    : default( DirectoryInfo );
            }
        }

        /// <summary>
        /// Gets the drive.
        /// </summary>
        /// <value>
        /// The drive.
        /// </value>
        public string Drive
        {
            get
            {
                return _isRooted
                    ? Path.GetPathRoot( _input )
                    : string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime Created
        {
            get
            {
                return _created;
            }
            set
            {
                if( _created != value )
                {
                    _created = value;
                    OnPropertyChanged( nameof( Created ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public long Length
        {
            get
            {
                return _length;
            }
            set
            {
                if( _length != value )
                {
                    _length = value;
                    OnPropertyChanged( nameof( Length ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension
        {
            get
            {
                return _fileExtension;
            }
            set
            {
                if( _fileExtension != value )
                {
                    _fileExtension = value;
                    OnPropertyChanged( nameof( Extension ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public FileAttributes FileAttributes
        {
            get
            {
                return _fileAttributes;
            }
            set
            {
                if( _fileAttributes != value )
                {
                    _fileAttributes = value;
                    OnPropertyChanged( nameof( FileAttributes ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the file security.
        /// </summary>
        /// <value>
        /// The file security.
        /// </value>
        public FileSecurity FileSecurity
        {
            get
            {
                return _fileSecurity;
            }
            set
            {
                if( _fileSecurity != value )
                {
                    _fileSecurity = value;
                    OnPropertyChanged( nameof( FileSecurity ) );
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the invalid path chars.
        /// </summary>
        /// <value>
        /// The invalid path chars.
        /// </value>
        public char[ ] InvalidPathChars
        {
            get
            {
                return _invalidPathChars;
            }
            set
            {
                if( _invalidPathChars != value )
                {
                    _invalidPathChars = value;
                    OnPropertyChanged( nameof( InvalidPathChars ) );
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the invalid name chars.
        /// </summary>
        /// <value>
        /// The invalid name chars.
        /// </value>
        public char[ ] InvalidNameChars
        {
            get
            {
                return _invalidNameChars;
            }
            set
            {
                if( _invalidNameChars != value )
                {
                    _invalidNameChars = value;
                    OnPropertyChanged( nameof( InvalidNameChars ) );
                }
            }
        }

        /// <summary>
        /// Gets the path separator.
        /// </summary>
        /// <value>
        /// The path separator.
        /// </value>
        public char PathSeparator
        {
            get
            {
                return _pathSeparator;
            }
            set
            {
                if( _pathSeparator != value )
                {
                    _pathSeparator = value;
                    OnPropertyChanged( nameof( PathSeparator ) );
                }
            }
        }

        /// <summary>
        /// Gets the folder separator.
        /// </summary>
        /// <value>
        /// The folder separator.
        /// </value>
        public char FolderSeparator
        {
            get
            {
                return _folderSeparator;
            }
            set
            {
                if( _folderSeparator != value )
                {
                    _folderSeparator = value;
                    OnPropertyChanged( nameof( FolderSeparator ) );
                }
            }
        }

        /// <summary>
        /// Gets the drive separator.
        /// </summary>
        /// <value>
        /// The drive separator.
        /// </value>
        public char DriveSeparator
        {
            get
            {
                return _driveSeparator;
            }
            set
            {
                if( _driveSeparator != value )
                {
                    _driveSeparator = value;
                    OnPropertyChanged( nameof( DriveSeparator ) );
                }
            }
        }
    }
}