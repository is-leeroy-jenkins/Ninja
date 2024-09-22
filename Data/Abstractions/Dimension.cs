// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 08-25-2020
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-25-2024
// ******************************************************************************************
// <copyright file="Dimension.cs" company="Terry D. Eppler">
//    Badger is budget execution and data analysis tool for EPA Analysts
//    based on WPF, NET6.0, and is written in C-Sharp.
// 
//     Copyright ©  2020, 2022, 2204 Terry D. Eppler
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
//   Dimension.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public abstract class Dimension
    {
        /// <summary>
        /// The records
        /// </summary>
        private protected int _records;

        /// <summary>
        /// The columns
        /// </summary>
        private protected int _columns;

        /// <summary>
        /// The dates
        /// </summary>
        private protected IList<string> _dates;

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        private protected IList<string> _fields;

        /// <summary>
        /// Gets or sets the numerics.
        /// </summary>
        /// <value>
        /// The numerics.
        /// </value>
        private protected IList<string> _numerics;

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        private protected IDictionary<string, double> _values;

        /// <summary>
        /// Gets or sets the data table.
        /// </summary>
        /// <value>
        /// The data table.
        /// </value>
        private protected DataTable _dataTable;

        /// <summary>
        /// Throws if null numeric.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// </exception>
        private protected void ThrowIfNotNumeric( string numeric )
        {
            try
            {
                ThrowIf.Null( numeric, nameof( numeric ) );
                if( _numerics?.Contains( numeric ) == false )
                {
                    var _message = $"{numeric} is not in Data Columns!";
                    throw new ArgumentOutOfRangeException( numeric, _message );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Throws if null field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// </exception>
        private protected void ThrowIfNotField( string field )
        {
            try
            {
                ThrowIf.Null( field, nameof( field ) );
                if( _fields?.Contains( field ) == false )
                {
                    var _message = $"{field} is not in Data Columns!";
                    throw new ArgumentOutOfRangeException( field, _message );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Throws if null date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// </exception>
        private protected void ThrowIfNotDate( string date )
        {
            try
            {
                ThrowIf.Null( date, nameof( date ) );
                if( _dates?.Contains( date ) == false )
                {
                    var _message = $"{date} is not a Data Column!";
                    throw new ArgumentOutOfRangeException( date, _message );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Throws if null criteria.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// </exception>
        private protected void ThrowIfNotCriteria( IDictionary<string, object> where )
        {
            try
            {
                ThrowIf.Null( where, nameof( where ) );
                var _keys = where.Keys.ToList( );
                foreach( var _name in _keys )
                {
                    var _colNames = _dataTable.GetColumnNames( );
                    if( _colNames.Contains( _name ) == false )
                    {
                        var _message = $"{_name} is not a Data Column!";
                        throw new ArgumentOutOfRangeException( _name, _message );
                    }
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private protected void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}