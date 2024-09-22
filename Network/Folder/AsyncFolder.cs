// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="AsyncFolder.cs" company="Terry D. Eppler">
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
//   AsyncFolder.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.IO.Compression;
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    public class AsyncFolder : AsyncFolderBase
    {
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
            private protected set
            {
                _fileCount = value;
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
            private protected set
            {
                _folderCount = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Folder" /> class.
        /// </summary>
        public AsyncFolder( )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Folder" /> class.
        /// </summary>
        /// <param name="input"></param>
        public AsyncFolder( string input )
            : base( input )
        {
            _input = input;
            _fullPath = input;
            _folderExists = Directory.Exists( input );
            _folderName = Path.GetDirectoryName( input );
            if( input != null )
            {
                _hasSubFiles = Directory.GetFiles( input )?.Length > 0;
                _hasSubFolders = Directory.GetDirectories( input )?.Length > 0;
                _created = Directory.GetCreationTime( input );
                _modified = Directory.GetLastWriteTime( input );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Folder" /> class.
        /// </summary>
        /// <param name="folder">The folder.</param>
        public AsyncFolder( Folder folder )
            : base( folder )
        {
            _input = folder.Input;
            _fullPath = folder.FullPath;
            _fileExists = Directory.Exists( folder.Input );
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public Task<string> GetNameAsync( )
        {
            if( _folderExists )
            {
                var _async = new TaskCompletionSource<string>( );
                try
                {
                    var _name = Path.GetDirectoryName( _input );
                    _async.SetResult( _name );
                    return _async.Task;
                }
                catch( Exception ex )
                {
                    _async.SetException( ex );
                    AsyncFolder.Fail( ex );
                    return default( Task<string> );
                }
            }
            else
            {
                return default( Task<string> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Transfers the specified folder.
        /// </summary>
        /// <param name="destination">
        /// The folder.
        /// </param>
        public Task<object> CopyContentsAsync( string destination )
        {
            var _async = new TaskCompletionSource<object>( );
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                Directory.Move( _input, destination );
                _async.SetResult( destination );
                return _async.Task;
            }
            catch( IOException ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return _async.Task;
            }
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <returns></returns>
        public static Task<string> GetCurrentDirectoryAsync( )
        {
            var _async = new TaskCompletionSource<string>( );
            try
            {
                _async.SetResult( Environment.CurrentDirectory );
                return _async.Task;
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<string> );
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
        public static Task<DirectoryInfo> CreateFolderAsync( string filePath )
        {
            var _async = new TaskCompletionSource<DirectoryInfo>( );
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                if( Directory.Exists( filePath ) )
                {
                    var _message = @$"Folder at {filePath} already exists!";
                    throw new ArgumentException( _message );
                }
                else
                {
                    var _stream = Directory.CreateDirectory( filePath );
                    _async.SetResult( _stream );
                    return _async.Task;
                }
            }
            catch( Exception ex )
            {
                AsyncFolder.Fail( ex );
                return default( Task<DirectoryInfo> );
            }
        }

        /// <summary>
        /// Deletes the specified folder name.
        /// </summary>
        /// <param name="folderName">
        /// Name of the folder.
        /// </param>
        public Task<object> DeleteFolderAsync( string folderName )
        {
            var _async = new TaskCompletionSource<object>( );
            try
            {
                ThrowIf.Null( folderName, nameof( folderName ) );
                Directory.Delete( folderName, true );
                _async.SetResult( folderName );
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<object> );
            }

            return default( Task<object> );
        }

        /// <summary>
        /// Creates the zip file.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="destination">
        /// The destination.
        /// </param>
        public Task<object> GetZipFileAsync( string source, string destination )
        {
            var _async = new TaskCompletionSource<object>( );
            try
            {
                ThrowIf.Null( source, nameof( source ) );
                ThrowIf.Null( destination, nameof( destination ) );
                ZipFile.CreateFromDirectory( source, destination );
                _async.SetResult( destination );
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<object> );
            }

            return default( Task<object> );
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the subdirectory.
        /// </summary>
        /// <param name="dirPath">The folderName.</param>
        /// <returns></returns>
        public Task<DirectoryInfo> CreateSubFolderAsync( string dirPath )
        {
            var _async = new TaskCompletionSource<DirectoryInfo>( );
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
                    var _folder = new DirectoryInfo( _fullPath )
                        ?.CreateSubdirectory( dirPath );

                    _async.SetResult( _folder );
                    return _async.Task;
                }
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<DirectoryInfo> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Moves the specified fullName.
        /// </summary>
        /// <param name="destination">
        /// The fullName.
        /// </param>
        public Task<object> MoveToAsync( string destination )
        {
            var _async = new TaskCompletionSource<object>( );
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                var _directory = new DirectoryInfo( _fullPath );
                _directory.MoveTo( destination );
                _async.SetResult( destination );
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<object> );
            }

            return default( Task<object> );
        }

        /// <inheritdoc />
        /// <summary>
        /// Zips the specified destination.
        /// </summary>
        /// <param name="destination">
        /// The destination.
        /// </param>
        public Task<object> ZipAsync( string destination )
        {
            var _async = new TaskCompletionSource<object>( );
            try
            {
                ThrowIf.Null( destination, nameof( destination ) );
                ZipFile.CreateFromDirectory( _fileName, destination );
                _async.SetResult( destination );
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<object> );
            }

            return default( Task<object> );
        }

        /// <inheritdoc />
        /// <summary>
        /// Uns the zip.
        /// </summary>
        /// <param name="zipPath">
        /// The zipPath.
        /// </param>
        public Task<object> UnZipAsync( string zipPath )
        {
            var _async = new TaskCompletionSource<object>( );
            try
            {
                ThrowIf.Null( zipPath, nameof( zipPath ) );
                ZipFile.ExtractToDirectory( zipPath, FullPath );
                _async.SetResult( zipPath );
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                AsyncFolder.Fail( ex );
                return default( Task<object> );
            }

            return default( Task<object> );
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
                var _subfolders = _folder.FileCount;
                var _bytes = ( _folder.Size.ToString( "N0" ) ?? "0" ) + " bytes";
                var _nl = Environment.NewLine;
                var _tb = char.ToString( '\t' );
                var _text = _nl + _tb + "Folder Name: " + _tb + _name + _nl + _nl +
                    _tb + "Folder Path: " + _tb + _path + _nl + _nl +
                    _tb + "Parent Path: " + _tb + _dirPath + _nl + _nl +
                    _tb + "Sub-Files: " + _tb + _subfiles + _nl + _nl +
                    _tb + "Sub-Folders: " + _tb + _subfolders + _nl + _nl +
                    _tb + "File Size: " + _tb + _bytes + _nl + _nl +
                    _tb + "Created On: " + _tb + _create.ToShortDateString( ) + _nl + _nl +
                    _tb + "Modified On: " + _tb + _modify.ToShortDateString( ) + _nl + _nl +
                    _tb + "Path Separator: " + _tb + _pathsep + _nl + _nl +
                    _tb + "Drive Separator: " + _tb + _drivesep + _nl + _nl +
                    _tb + "Folder Separator: " + _tb + _foldersep + _nl + _nl;

                return !string.IsNullOrEmpty( _text )
                    ? _text
                    : string.Empty;
            }
            catch( IOException ex )
            {
                AsyncFolder.Fail( ex );
                return string.Empty;
            }
        }
    }
}