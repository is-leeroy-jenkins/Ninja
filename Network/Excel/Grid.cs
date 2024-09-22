// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="Grid.cs" company="Terry D. Eppler">
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
//   Grid.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:OfficeOpenXml.ExcelCellBase" />
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "VirtualMemberNeverOverridden.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "FunctionComplexityOverflow" ) ]
    [ SuppressMessage( "ReSharper", "ConvertSwitchStatementToSwitchExpression" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoProperty" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoPropertyWhenPossible" ) ]
    public class Grid : ExcelCellAddress
    {
        /// <summary>
        /// From
        /// </summary>
        private protected (int Row, int Column) _from;

        /// <summary>
        /// The excel address
        /// </summary>
        private protected ExcelAddress _excelAddress;

        /// <summary>
        /// The range
        /// </summary>
        private protected ExcelRange _excelRange;

        /// <summary>
        /// To
        /// </summary>
        private protected (int Row, int Column) _to;

        /// <summary>
        /// The worksheet
        /// </summary>
        private protected ExcelWorksheet _excelWorksheet;

        /// <summary>
        /// The cells
        /// </summary>
        private protected IList<ExcelRangeBase> _cells;

        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <value>
        /// The range.
        /// </value>
        public ExcelRange ExcelRange
        {
            get
            {
                return _excelRange;
            }
            private protected set
            {
                _excelRange = value;
            }
        }

        /// <summary>
        /// Gets the excel address.
        /// </summary>
        /// <value>
        /// The excel address.
        /// </value>
        public ExcelAddress ExcelAddress
        {
            get
            {
                return _excelAddress;
            }
            private protected set
            {
                _excelAddress = value;
            }
        }

        /// <summary>
        /// Gets the worksheet.
        /// </summary>
        /// <value>
        /// The worksheet.
        /// </value>
        public ExcelWorksheet ExcelWorksheet
        {
            get
            {
                return _excelWorksheet;
            }
            private protected set
            {
                _excelWorksheet = value;
            }
        }

        /// <summary>
        /// Gets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public (int Row, int Column) From
        {
            get
            {
                return _from;
            }
            private protected set
            {
                _from = value;
            }
        }

        /// <summary>
        /// Gets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public (int Row, int Column) To
        {
            get
            {
                return _to;
            }
            private protected set
            {
                _to = value;
            }
        }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <value>
        /// The cells.
        /// </value>
        public IList<ExcelRangeBase> Cells
        {
            get
            {
                return _cells;
            }
            private protected set
            {
                _cells = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        public Grid( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="range">The range.</param>
        public Grid( ExcelPackage excel, ExcelRange range )
            : base( range.Start.Row, range.Start.Column )
        {
            _excelWorksheet = excel.Workbook.Worksheets[ 0 ];
            _excelRange = range;
            _from = ( range.Start.Row, range.Start.Column );
            _to = ( range.End.Row, range.End.Column );
            _excelAddress = new ExcelAddress( range.Start.Row, range.Start.Column,
                range.End.Row, range.End.Column );

            _cells = GetCells( );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="address">The address.</param>
        public Grid( ExcelPackage excel, ExcelAddress address )
        {
            _excelWorksheet = excel.Workbook.Worksheets[ 0 ];
            _from = ( address.Start.Row, address.Start.Column );
            _to = ( address.End.Row, address.End.Column );
            _excelRange = _excelWorksheet.Cells[ _from.Row, _from.Column, _to.Row, _to.Column ];
            _excelAddress = address;
            _cells = GetCells( );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="startRow">From row.</param>
        /// <param name="startColumn">From column.</param>
        /// <param name="endRow">To row.</param>
        /// <param name="endColumn">To column.</param>
        public Grid( ExcelPackage excel, int startRow = 1, int startColumn = 1, int endRow = 55,
            int endColumn = 12 )
        {
            _excelWorksheet = excel.Workbook.Worksheets[ 0 ];
            _from = ( startRow, startColumn );
            _to = ( endRow, endColumn );
            _excelRange = _excelWorksheet.Cells[ startRow, startColumn, endRow, endColumn ];
            _excelAddress = new ExcelAddress( startRow, startColumn, endRow, endColumn );
            _cells = GetCells( );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="cell">The cell.</param>
        public Grid( ExcelPackage excel, IList<int> cell )
        {
            _excelWorksheet = excel.Workbook.Worksheets[ 0 ];
            _from = ( cell[ 0 ], cell[ 1 ] );
            _to = ( cell[ 2 ], cell[ 3 ] );
            _excelRange = _excelWorksheet.Cells[ cell[ 0 ], cell[ 1 ], cell[ 2 ], cell[ 3 ] ];
            _excelAddress = new ExcelAddress( cell[ 0 ], cell[ 1 ], cell[ 2 ], cell[ 3 ] );
            _cells = GetCells( );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public Grid( ExcelPackage excel, (int Row, int Column) from, (int Row, int Column) to )
        {
            _excelWorksheet = excel.Workbook.Worksheets[ 0 ];
            _excelRange = _excelWorksheet.Cells[ from.Row, from.Column, to.Row, to.Column ];
            _from = from;
            _to = to;
            _excelAddress = new ExcelAddress( from.Row, from.Column, to.Row, to.Column );
            _cells = GetCells( );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        /// <param name="excel">The excel.</param>
        /// <param name="from">From.</param>
        public Grid( ExcelPackage excel, (int Row, int Column) from )
        {
            _excelWorksheet = excel.Workbook.Worksheets[ 0 ];
            _excelRange = _excelWorksheet.Cells[ from.Row, from.Column ];
            _from = from;
            _to = from;
            _excelAddress = new ExcelAddress( from.Row, from.Column, from.Row, from.Column );
            _cells = GetCells( );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Grid"/> class.
        /// </summary>
        /// <param name="grid">The grid.</param>
        public Grid( Grid grid )
        {
            _excelWorksheet = grid.ExcelWorksheet;
            _excelRange = grid.ExcelRange;
            _from = grid.From;
            _to = grid.To;
            _excelAddress = grid._excelAddress;
            _cells = GetCells( );
        }

        /// <summary>
        /// Deconstructs the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="excelWorksheet">The excel worksheet.</param>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="excelAddress">The excel address.</param>
        /// <param name="cells"> </param>
        public void Deconstruct( out (int Row, int Column) from,
            out (int Row, int Column) to, out ExcelWorksheet excelWorksheet,
            out ExcelRange excelRange, out ExcelAddress excelAddress,
            out IList<ExcelRangeBase> cells )
        {
            from = _from;
            to = _to;
            excelWorksheet = _excelWorksheet;
            excelRange = _excelRange;
            excelAddress = _excelAddress;
            cells = _cells;
        }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <returns></returns>
        private protected IList<ExcelRangeBase> GetCells( )
        {
            try
            {
                var _list = new List<ExcelRangeBase>( );
                foreach( var _cell in _excelRange )
                {
                    _list.Add( _cell.Current );
                }

                return _list?.Any( ) == true
                    ? _list
                    : default( IList<ExcelRangeBase> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<ExcelRangeBase> );
            }
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="values">The values.</param>
        private protected void SetValues( IList<object> values )
        {
            try
            {
                ThrowIf.Null( values, nameof( values ) );
                var _vals = values.ToArray( );
                var _data = _cells.ToArray( );
                if( _data.Length == _vals.Length )
                {
                    for( var _c = 0; _c < _data.Length; _c++ )
                    {
                        if( _vals[ _c ] != null )
                        {
                            switch( _vals[ _c ] )
                            {
                                case string _text:
                                {
                                    _data[ _c ].Value = _text;
                                    break;
                                }
                                case int _index:
                                {
                                    _data[ _c ].Value = _index;
                                    break;
                                }
                                case double _num:
                                {
                                    _data[ _c ].Value = _num;
                                    break;
                                }
                                case decimal _money:
                                {
                                    _data[ _c ].Value = _money;
                                    break;
                                }
                                case DateTime _dateTime:
                                {
                                    _data[ _c ].Value = _dateTime;
                                    break;
                                }
                                default:
                                {
                                    _data[ _c ].Value = _vals[ _c ];
                                    break;
                                }
                            }
                        }
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