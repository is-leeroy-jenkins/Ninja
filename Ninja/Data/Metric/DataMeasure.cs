// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="DataMeasure.cs" company="Terry D. Eppler">
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
//   DataMeasure.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc />
    /// <summary> </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "PublicConstructorInAbstractClass" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Local" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public class DataMeasure : Calculation, IDataMeasure
    {
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns>
        /// IList(string)
        /// </returns>
        private protected IList<string> GetTextColumnNames( )
        {
            try
            {
                var _textColumns = _dataTable?.GetTextColumns( );
                var _list = _textColumns?.Select( c => c.ColumnName )?.ToList( );
                return _list?.Any( ) == true
                    ? _list
                    : default( IList<string> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<string> );
            }
        }

        /// <summary>
        /// Gets the date column names.
        /// </summary>
        /// <returns>
        /// IList(string)
        /// </returns>
        private protected IList<string> GetDateColumnNames( )
        {
            try
            {
                var _dateColumns = _dataTable?.GetDateColumns( );
                var _list = _dateColumns?.Select( c => c.ColumnName )?.ToList( );
                return _list?.Any( ) == true
                    ? _list
                    : default( IList<string> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<string> );
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DataMeasure"/> class.
        /// </summary>
        /// <param name="dataTable">
        /// The data table.
        /// </param>
        public DataMeasure( DataTable dataTable )
        {
            _dataTable = dataTable;
            _fields = GetTextColumnNames( );
            _numerics = GetNumericColumnNames( );
            _dates = GetDateColumns( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the dates.
        /// </summary>
        /// <value>
        /// The dates.
        /// </value>
        public IList<string> Dates
        {
            get
            {
                return _dates;
            }
            private protected set
            {
                _dates = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the data table.
        /// </summary>
        /// <value>
        /// The data table.
        /// </value>
        public DataTable DataTable
        {
            get
            {
                return _dataTable;
            }
            private protected set
            {
                _dataTable = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the numerics.
        /// </summary>
        /// <value>
        /// The numerics.
        /// </value>
        public IList<string> Numerics
        {
            get
            {
                return _numerics;
            }
            private protected set
            {
                _numerics = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public IList<string> Fields
        {
            get
            {
                return _fields;
            }
            private protected set
            {
                _fields = value;
            }
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IDictionary<string, double> Values
        {
            get
            {
                return _values;
            }
            private protected set
            {
                _values = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Calculates the maximum.
        /// </summary>
        /// <param name="numeric">
        /// The numeric.
        /// </param>
        /// <returns>
        /// </returns>
        public double CalculateMaximum( string numeric )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                var _query = _dataTable?.AsEnumerable( )?.Select( p => p.Field<double>( numeric ) )
                    ?.Max( );

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

        /// <inheritdoc />
        /// <summary>
        /// Calculates the minimum.
        /// </summary>
        /// <param name="numeric">
        /// The numeric.
        /// </param>
        /// <returns>
        /// </returns>
        public double CalculateMinimum( string numeric )
        {
            try
            {
                ThrowIfNotNumeric( numeric );
                var _query = _dataTable.AsEnumerable( ).Select( p => p.Field<double>( numeric ) )
                    .Min( );

                return _query;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return 0.0d;
            }
        }
    }
}