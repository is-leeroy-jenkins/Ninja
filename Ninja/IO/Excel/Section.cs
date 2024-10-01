// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="Section.cs" company="Terry D. Eppler">
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
//   Section.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.Grid" />
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoProperty" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "LoopCanBePartlyConvertedToQuery" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public class Section : Grid
    {
        /// <summary>
        /// The anchor
        /// </summary>
        private protected (int Row, int Column) _anchor;

        /// <summary>
        /// The area
        /// </summary>
        private protected int _area;

        /// <summary>
        /// The depth
        /// </summary>
        private protected int _depth;

        /// <summary>
        /// The span
        /// </summary>
        private protected int _span;

        /// <summary>
        /// The values
        /// </summary>
        private protected IList<object> _values;

        /// <inheritdoc />
        /// <summary>
        /// Deconstructs the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="excelWorksheet">The excel worksheet.</param>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="excelAddress">The excel address.</param>
        public void Deconstruct( out (int Row, int Column) from, out (int Row, int Column) to,
            out ExcelWorksheet excelWorksheet, out ExcelRange excelRange,
            out ExcelAddress excelAddress )
        {
            from = _from;
            to = _to;
            excelWorksheet = _excelWorksheet;
            excelRange = _excelRange;
            excelAddress = _excelAddress;
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="row">The row.</param>
        public void SetValues( DataRow row )
        {
            try
            {
                ThrowIf.Null( row, nameof( row ) );
                if( _cells.Count == row.ItemArray.Length )
                {
                    SetValues( row.ItemArray );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        public void SetValues( IDictionary<string, object> dict )
        {
            try
            {
                ThrowIf.Empty( dict, nameof( dict ) );
                var _items = dict.Values.ToArray( );
                if( _cells.Count == _items.Length )
                {
                    SetValues( _items );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SetValues( IEnumerable<string> data )
        {
            try
            {
                ThrowIf.Empty( data, nameof( data ) );
                var _items = data.ToArray( );
                if( _cells.Count == _items?.Length )
                {
                    SetValues( _items );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Section" /> class.
        /// </summary>
        public Section( )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Section" /> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="range">The range.</param>
        public Section( ExcelPackage excel, ExcelRange range )
            : base( excel, range )
        {
            _anchor = ( range.Start.Row, range.Start.Column );
            _span = ExcelRange.Columns;
            _depth = ExcelRange.Rows;
            _area = ExcelRange.Rows * ExcelRange.Columns;
            _cells = GetCells( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Section" /> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="startRow">From row.</param>
        /// <param name="startColumn">From column.</param>
        /// <param name="endRow">To row.</param>
        /// <param name="endColumn">To column.</param>
        public Section( ExcelPackage excel, int startRow = 1, int startColumn = 1,
            int endRow = 55, int endColumn = 12 )
            : base( excel, startRow, startColumn, endRow,
                endColumn )
        {
            _anchor = ( startRow, startColumn );
            _span = ExcelRange.Columns;
            _depth = ExcelRange.Rows;
            _area = ExcelRange.Rows * ExcelRange.Columns;
            _cells = GetCells( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Section" /> class.
        /// </summary>
        /// <param name="excel">The worksheet.</param>
        /// <param name="cell">The cell.</param>
        public Section( ExcelPackage excel, IList<int> cell )
            : base( excel, cell )
        {
            _anchor = ( cell[ 0 ], cell[ 1 ] );
            _span = ExcelRange.Columns;
            _depth = ExcelRange.Rows;
            _area = ExcelRange.Rows * ExcelRange.Columns;
            _cells = GetCells( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Section" /> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public Section( ExcelPackage excel, (int Row, int Column) from, (int Row, int Column) to )
            : base( excel, from, to )
        {
            _anchor = ( from.Row, from.Column );
            _span = ExcelRange.Columns;
            _depth = ExcelRange.Rows;
            _area = ExcelRange.Rows * ExcelRange.Columns;
            _cells = GetCells( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Section" /> class.
        /// </summary>
        /// <param name="section">The section.</param>
        public Section( Section section )
        {
            _anchor = section.Anchor;
            _span = section.Span;
            _depth = section.Depth;
            _area = section.Area;
            _cells = section.Cells;
        }

        /// <summary>
        /// Gets the span.
        /// </summary>
        /// <value>
        /// The span.
        /// </value>
        public int Span
        {
            get
            {
                return _span;
            }
            private protected set
            {
                _span = value;
            }
        }

        /// <summary>
        /// Gets the depth.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        public int Depth
        {
            get
            {
                return _depth;
            }
            private protected set
            {
                _depth = value;
            }
        }

        /// <summary>
        /// Gets the anchor.
        /// </summary>
        /// <value>
        /// The anchor.
        /// </value>
        public (int Row, int Column) Anchor
        {
            get
            {
                return _anchor;
            }
            private protected set
            {
                _anchor = value;
            }
        }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        public int Area
        {
            get
            {
                return _area;
            }
            private protected set
            {
                _area = value;
            }
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IList<object> Values
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
    }
}