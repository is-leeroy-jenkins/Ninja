// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="DataFile.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts.
//    Copyright ©  2024  Terry D. Eppler
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
//    You can contact me at: terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   DataFile.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using static System.IO.Directory;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.FileBase" />
    /// <seealso cref="T:Ninja.IFile" />
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public class DataFile : FileBase
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                if(_size != value)
                {
                    _size = value;
                    OnPropertyChanged(nameof(Size));
                }
            }
        }

        /// <summary>
        /// Gets the name of the parent.
        /// </summary>
        /// <value>
        /// The name of the parent.
        /// </value>
        public string ParentName
        {
            get
            {
                return _parentName;
            }
            set
            {
                if(_parentName != value)
                {
                    _parentName = value;
                    OnPropertyChanged(nameof(ParentName));
                }
            }
        }

        /// <summary>
        /// Gets the parent path.
        /// </summary>
        /// <value>
        /// The parent path.
        /// </value>
        public string ParentPath
        {
            get
            {
                return _parentPath;
            }
            set
            {
                if(_parentPath != value)
                {
                    _parentPath = value;
                    OnPropertyChanged(nameof(ParentPath));
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.DataFile" /> class.
        /// </summary>
        public DataFile( )
            : base( )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.DataFile" /> class.
        /// </summary>
        /// <param name="input">The input.</param>
        public DataFile( string input )
            : base( input )
        {
            _input = input;
            _fileName = Path.GetFileNameWithoutExtension( input );
            _fileExists = File.Exists( input );
            _hasExtension = Path.HasExtension( input );
            _fullPath = Path.GetFullPath( input );
            _absolutePath = Path.GetFullPath( input );
            _fileAttributes = File.GetAttributes( input );
            _created = File.GetCreationTime( input );
            _modified = File.GetLastWriteTime( input );
            _hasParent = !string.IsNullOrEmpty( GetParent( input )?.Name );
            _parentName = Path.GetDirectoryName( input );
            _parentPath = GetParent( _fullPath )?.FullName;
            _size = File.Open( input, FileMode.Open ).Length;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.DataFile" /> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public DataFile( DataFile file )
            : this( )
        {
            _input = file.Input;
            _fileName = file.FileName;
            _fileExists = File.Exists( file.FullPath );
            _hasExtension = Path.HasExtension( file.FullPath );
            _fullPath = file.FullPath;
            _absolutePath = file.AbsolutePath;
            _fileAttributes = file.FileAttributes;
            _created = file.Created;
            _modified = file.Modified;
            _hasParent = !string.IsNullOrEmpty( GetParent( file.FullPath )?.Name );
            _parentName = file.ParentName;
            _parentPath = file.ParentPath;
            _size = file.Size;
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified search];
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains( string search )
        {
            if( _fileExists )
            {
                try
                {
                    ThrowIf.Null( search, nameof( search ) );
                    using var _stream = File.Open( _input, FileMode.Open );
                    using var _reader = new StreamReader( _stream );
                    if( _reader != null )
                    {
                        var _text = _reader?.ReadLine( );
                        var _result = false;
                        while( !string.IsNullOrEmpty( _text ) )
                        {
                            if( Regex.IsMatch( _text, search ) )
                            {
                                _result = true;
                                break;
                            }

                            _text = _reader.ReadLine( );
                        }

                        return _result;
                    }

                    return false;
                }
                catch( IOException ex )
                {
                    Fail( ex );
                    return false;
                }
            }

            return false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Searches the specified pattern.
        /// </summary>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// </returns>
        public IEnumerable<FileInfo> Search( string pattern )
        {
            if( _fileExists )
            {
                try
                {
                    IEnumerable<string> _enumerable = GetDirectories( _input, pattern );
                    var _list = new List<FileInfo>( );
                    foreach( var _file in _enumerable )
                    {
                        _list.Add( new FileInfo( _file ) );
                    }

                    return _list?.Any( ) == true
                        ? _list
                        : default( List<FileInfo> );
                }
                catch( IOException ex )
                {
                    Fail( ex );
                    return default( IEnumerable<FileInfo> );
                }
            }

            return default( IEnumerable<FileInfo> );
        }

        /// <summary>
        /// Creates the specified file path.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// FileInfo
        /// </returns>
        public static FileInfo Create( string filePath )
        {
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                if( File.Exists( filePath ) )
                {
                    var _message = @$"File at {filePath} already exists!";
                    throw new ArgumentException( _message );
                }
                else
                {
                    return new FileInfo( filePath );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( FileInfo );
            }
        }

        /// <summary>
        /// Opens the dialog.
        /// </summary>
        /// <returns>
        /// string
        /// </returns>
        public static string ShowOpenFileDialog( )
        {
            try
            {
                var _dialog = new OpenFileDialog
                {
                    CheckFileExists = true,
                    CheckPathExists = true
                };

                _dialog.ShowDialog( );
                return !string.IsNullOrEmpty( _dialog.FileName )
                    ? _dialog.FileName
                    : string.Empty;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public void OpenSaveFileDialog( )
        {
            FileStream _stream = null;
            try
            {
                var _dialog = new SaveFileDialog
                {
                    CreatePrompt = true,
                    OverwritePrompt = true,
                    CheckFileExists = true,
                    CheckPathExists = true
                };

                _dialog.ShowDialog( );
                _stream = File.Create( _dialog.FileName );
                _stream.Close( );
            }
            catch( Exception ex )
            {
                Fail( ex );
                _stream?.Close( );
            }
            finally
            {
                _stream?.Close( );
            }
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
                var _file = new DataFile( _input );
                var _extenstion = _file.Extension ?? string.Empty;
                var _name = _file.FileName ?? string.Empty;
                var _filePath = _file.FullPath ?? string.Empty;
                var _create = _file.Created;
                var _modify = _file.Modified;
                var _len = _file.Length.ToString( "N0" ) ?? string.Empty;
                var _pathsep = _file.PathSeparator;
                var _drivesep = _file.DriveSeparator;
                var _foldersep = _file.FolderSeparator;
                var _root = _file.Drive;
                var _nl = Environment.NewLine;
                var _attrs = _file.FileAttributes;
                var _tb = char.ToString( '\t' );
                var _text = _nl + _tb + "File Name: " + _tb + _name + _nl + _nl +
                    _tb + "File Path: " + _tb + _filePath + _nl + _nl +
                    _tb + "File Attributes: " + _tb + _attrs + _nl + _nl +
                    _tb + "Extension: " + _tb + _extenstion + _nl + _nl +
                    _tb + "Path Root: " + _tb + _root + _nl + _nl +
                    _tb + "Path Separator: " + _tb + _pathsep + _nl + _nl +
                    _tb + "Drive Separator: " + _tb + _drivesep + _nl + _nl +
                    _tb + "Folder Separator: " + _tb + _foldersep + _nl + _nl +
                    _tb + "Length: " + _tb + _len + _nl + _nl +
                    _tb + "Created: " + _tb + _create.ToShortDateString( ) + _nl + _nl +
                    _tb + "Modified: " + _tb + _modify.ToShortDateString( ) + _nl + _nl;

                return !string.IsNullOrEmpty( _text )
                    ? _text
                    : string.Empty;
            }
            catch( IOException ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }
    }
}