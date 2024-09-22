// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="BasicPath.cs" company="Terry D. Eppler">
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
//   BasicPath.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.AccessControl;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "PublicConstructorInAbstractClass" ) ]
    [ SuppressMessage( "ReSharper", "VirtualMemberNeverOverridden.Global" ) ]
    [ SuppressMessage( "ReSharper", "ReplaceAutoPropertyWithComputedProperty" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public abstract class PathBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The has extension
        /// </summary>
        private protected bool _hasExtension;

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
        private protected bool _hasParent;

        /// <summary>
        /// The directory separator
        /// </summary>
        private protected char _folderSeparator;

        /// <summary>
        /// The path separator
        /// </summary>
        private protected char _pathSeparator;

        /// <summary>
        /// The drive separator
        /// </summary>
        private protected char _driveSeparator;

        /// <summary>
        /// The drive 
        /// </summary>
        private protected string _drive;

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        private protected string _input;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        private protected string _fileName;

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>
        /// The full path.
        /// </value>
        private protected string _fullPath;

        /// <summary>
        /// Gets or sets the absolute path.
        /// </summary>
        /// <value>
        /// The absolute path.
        /// </value>
        private protected string _absolutePath;

        /// <summary>
        /// The relative path
        /// </summary>
        private protected string _relativePath;

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        private protected DateTime _modified;

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        private protected DateTime _created;

        /// <summary>
        /// The is rooted
        /// </summary>
        private protected bool _isRooted;

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        private protected long _length;

        /// <summary>
        /// 
        /// </summary>
        private protected string _fileExtension;

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        private protected FileAttributes _fileAttributes;

        /// <summary>
        /// Gets or sets the file security.
        /// </summary>
        /// <value>
        /// The file security.
        /// </value>
        private protected FileSecurity _fileSecurity;

        /// <summary>
        /// The invalid path chars
        /// </summary>
        private protected char[ ] _invalidPathChars;

        /// <summary>
        /// The invalid name chars
        /// </summary>
        private protected char[ ] _invalidNameChars;

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Updates the specified field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>( ref T field, T value,
            [ CallerMemberName ]
            string propertyName = null )

        {
            if( EqualityComparer<T>.Default.Equals( field, value ) )
            {
                return;
            }

            field = value;
            OnPropertyChanged( propertyName );
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            var _handler = PropertyChanged;
            _handler?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

        /// <summary>
        /// Fails the specified _ex.
        /// </summary>
        /// <param name="_ex">The _ex.</param>
        private protected static void Fail( Exception _ex )
        {
            var _error = new ErrorWindow( _ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}