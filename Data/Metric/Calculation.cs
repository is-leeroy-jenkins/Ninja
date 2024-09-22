// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="Calculation.cs" company="Terry D. Eppler">
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
//   Calculation.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using LinqStatistics;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "PublicConstructorInAbstractClass" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Local" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeRedundantParentheses" ) ]
    public abstract class Calculation : Dimension
    {
        /// <summary>
        /// Counts the values.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns>
        /// An int
        /// </returns>
        public int CountValues( string numeric )
        {
            try
            {
                ThrowIf.Null( numeric, nameof( numeric ) );
                var _dataRows = _dataTable
                    ?.AsEnumerable( )
                    ?.Select( p => p.Field<double>( numeric ) );

                return _dataRows?.Any( ) == true
                    ? _dataRows.Count( )
                    : 0;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0;
            }
        }

        /// <summary>
        /// Counts the values.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns>
        /// An int
        /// </returns>
        public int CountValues( string numeric, IDictionary<string, object> where )
        {
            try
            {
                ThrowIf.Null( numeric, nameof( numeric ) );
                ThrowIfNotCriteria( where );
                var _dataRows = _dataTable
                    ?.Filter( where )
                    ?.Select( p => p.Field<double>( numeric ) );

                return _dataRows?.Any( ) == true
                    ? _dataRows.Count( )
                    : 0;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0;
            }
        }

        /// <summary>
        /// Calculates the total.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns>
        /// A double
        /// </returns>
        public double CalculateTotal( string numeric )
        {
            try
            {
                ThrowIf.Null( numeric, nameof( numeric ) );
                var _select = _dataTable
                    ?.AsEnumerable( )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.Sum( );

                return _select > 0
                    ? double.Parse( _select?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the total.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public double CalculateTotal( string numeric, IDictionary<string, object> where )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                ThrowIfNotCriteria( where );
                var _select = _dataTable
                    ?.Filter( where )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.Sum( );

                return _select > 0
                    ? double.Parse( _select?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the average.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns></returns>
        public double CalculateAverage( string numeric )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                var _query = _dataTable
                    ?.AsEnumerable( )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.Average( );

                return _query > 0
                    ? double.Parse( _query?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the average.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public double CalculateAverage( string numeric, IDictionary<string, object> where )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                ThrowIfNotCriteria( where );
                var _query = _dataTable
                    ?.Filter( where )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.Average( );

                return _query > 0
                    ? double.Parse( _query?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the percentage.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public double CalculatePercentage( string numeric, IDictionary<string, object> where )
        {
            try
            {
                ThrowIfNotCriteria( where );
                ThrowIf.Null( numeric, nameof( numeric ) );
                var _total = _dataTable
                    .AsEnumerable( )
                    .Select( p => p.Field<double>( numeric ) )
                    .Sum( );

                var _select = _dataTable
                    .Filter( where )
                    .Select( p => p.Field<double>( numeric ) )
                    .Sum( );

                var _ratio = ( _select / _total ) * 100;
                return ( _ratio > 0 )
                    ? _ratio
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the deviation.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns></returns>
        public double CalculateDeviation( string numeric )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                var _query = _dataTable
                    ?.AsEnumerable( )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.StandardDeviation( );

                return _query > 0
                    ? double.Parse( _query?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the deviation.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns>
        /// A double
        /// </returns>
        public double CalculateDeviation( string numeric, IDictionary<string, object> where )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                ThrowIfNotCriteria( where );
                var _query = _dataTable
                    ?.Filter( where )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.StandardDeviation( );

                return _query > 0
                    ? double.Parse( _query?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the variance.
        /// </summary>
        /// <param name="numeric">
        /// The numeric.
        /// </param>
        /// <returns>
        /// A double
        /// </returns>
        public double CalculateVariance( string numeric )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                var _query = _dataTable
                    ?.AsEnumerable( )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.Variance( );

                return _query > 0
                    ? double.Parse( _query?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Calculates the variance.
        /// </summary>
        /// <param name="numeric">
        /// The numeric.
        /// </param>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <returns>
        /// A double
        /// </returns>
        public double CalculateVariance( IDictionary<string, object> where, string numeric )
        {
            try
            {
                ThrowIfNotCriteria( where );
                ThrowIfNotNumeric( numeric );
                var _query = _dataTable
                    ?.Filter( where )
                    ?.Select( p => p.Field<double>( numeric ) )
                    ?.Variance( );

                return _query > 0
                    ? double.Parse( _query?.ToString( "N1" ) )
                    : 0.0d;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }

        /// <summary>
        /// Gets the numerics.
        /// </summary>
        /// <returns>
        /// IList(string)
        /// </returns>
        private protected IList<string> GetNumericColumnNames( )
        {
            try
            {
                var _names = new List<string>( );
                foreach( DataColumn _dataColumn in _dataTable.Columns )
                {
                    if( ( !_dataColumn.ColumnName.EndsWith( "Id" )
                            && ( _dataColumn.Ordinal > 0 )
                            && ( _dataColumn.DataType == typeof( double ) ) )
                        | ( _dataColumn.DataType == typeof( short ) )
                        | ( _dataColumn.DataType == typeof( int ) )
                        | ( _dataColumn.DataType == typeof( ushort ) )
                        | ( _dataColumn.DataType == typeof( long ) )
                        | ( _dataColumn.DataType == typeof( decimal ) )
                        | ( _dataColumn.DataType == typeof( float ) ) )
                    {
                        _names.Add( _dataColumn.ColumnName );
                    }
                }

                return _names?.Any( ) == true
                    ? _names
                    : default( IList<string> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<string> );
            }
        }

        /// <summary>
        /// Gets the dates.
        /// </summary>
        /// <returns>
        /// IList(string)
        /// </returns>
        private protected IList<string> GetDateColumns( )
        {
            try
            {
                var _names = new List<string>( );
                foreach( DataColumn _dataColumn in _dataTable.Columns )
                {
                    if( ( _dataColumn.Ordinal > 0 )
                        && ( ( _dataColumn.DataType == typeof( DateTime ) )
                            | ( _dataColumn.DataType == typeof( DateTimeOffset ) )
                            | _dataColumn.ColumnName.EndsWith( "Day" )
                            | _dataColumn.ColumnName.EndsWith( "Date" ) ) )
                    {
                        _names.Add( _dataColumn.ColumnName );
                    }
                }

                return _names?.Any( ) == true
                    ? _names
                    : default( IList<string> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<string> );
            }
        }
    }
}