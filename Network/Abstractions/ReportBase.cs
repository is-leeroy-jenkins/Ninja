// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="BasicReport.cs" company="Terry D. Eppler">
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
//   BasicReport.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using OfficeOpenXml.Drawing.Chart;
    using OfficeOpenXml.Style;
    using OfficeOpenXml.Table;
    using OfficeOpenXml.Table.PivotTable;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Color = System.Drawing.Color;
    using Font = System.Drawing.Font;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantCheckBeforeAssignment" ) ]
    [ SuppressMessage( "ReSharper", "NotAccessedField.Global" ) ]
    public abstract class ReportBase
    {
        /// <summary>
        /// The primary back color
        /// </summary>
        private protected Color _primaryBackColor;

        /// <summary>
        /// The secondary back color
        /// </summary>
        private protected Color _secondaryBackColor;

        /// <summary>
        /// The row height
        /// </summary>
        private protected double _rowHeight;

        /// <summary>
        /// The column width
        /// </summary>
        private protected double _columnWidth;

        /// <summary>
        /// The zoom level
        /// </summary>
        private protected int _zoomLevel;

        /// <summary>
        /// The font color
        /// </summary>
        private protected Color _fontColor;

        /// <summary>
        /// The font
        /// </summary>
        private protected Font _font;

        /// <summary>
        /// The title font
        /// </summary>
        private protected Font _titleFont;

        /// <summary>
        /// The left margin
        /// </summary>
        private protected decimal _leftMargin;

        /// <summary>
        /// The right margin
        /// </summary>
        private protected decimal _rightMargin;

        /// <summary>
        /// The header margin
        /// </summary>
        private protected decimal _headerMargin;

        /// <summary>
        /// The top margin
        /// </summary>
        private protected decimal _topMargin;

        /// <summary>
        /// The footer margin
        /// </summary>
        private protected decimal _footerMargin;

        /// <summary>
        /// The bottom margin
        /// </summary>
        private protected decimal _bottomMargin;

        /// <summary>
        /// The data connection
        /// </summary>
        private protected OleDbConnection _dataConnection;

        /// <summary>
        /// The data command
        /// </summary>
        private protected OleDbCommand _dataCommand;

        /// <summary>
        /// The data adapter
        /// </summary>
        private protected OleDbDataAdapter _dataAdapter;

        /// <summary>
        /// The file information
        /// </summary>
        private protected FileInfo _fileInfo;

        /// <summary>
        /// The comment range
        /// </summary>
        private protected ExcelRange _commentRange;

        /// <summary>
        /// The header range
        /// </summary>
        private protected ExcelRange _headerRange;

        /// <summary>
        /// The footer range
        /// </summary>
        private protected ExcelRange _footerRange;

        /// <summary>
        /// The data range
        /// </summary>
        private protected ExcelRange _dataRange;

        /// <summary>
        /// The excel package
        /// </summary>
        private protected ExcelPackage _excelPackage;

        /// <summary>
        /// The pivot range
        /// </summary>
        private protected ExcelRange _pivotRange;

        /// <summary>
        /// The chart range
        /// </summary>
        private protected ExcelRange _chartRange;

        /// <summary>
        /// The excel workbook
        /// </summary>
        private protected ExcelWorkbook _excelWorkbook;

        /// <summary>
        /// The excel table
        /// </summary>
        private protected ExcelTable _excelTable;

        /// <summary>
        /// The pie chart
        /// </summary>
        private protected ExcelPieChart _pieChart;

        /// <summary>
        /// The bar chart
        /// </summary>
        private protected ExcelBarChart _barChart;

        /// <summary>
        /// The area chart
        /// </summary>
        private protected ExcelAreaChart _areaChart;

        /// <summary>
        /// The pivot table
        /// </summary>
        private protected ExcelPivotTable _pivotTable;

        /// <summary>
        /// The data worksheet
        /// </summary>
        private protected ExcelWorksheet _dataWorksheet;

        /// <summary>
        /// The pivot worksheet
        /// </summary>
        private protected ExcelWorksheet _pivotWorksheet;

        /// <summary>
        /// The chart worksheet
        /// </summary>
        private protected ExcelWorksheet _chartWorksheet;

        /// <summary>
        /// The comments
        /// </summary>
        private protected IList<ExcelComment> _comments;

        /// <summary>
        /// The theme path
        /// </summary>
        private protected string _themePath;

        /// <summary>
        /// The data table
        /// </summary>
        private protected DataTable _dataTable;

        /// <summary>
        /// The data metric
        /// </summary>
        private protected DataMeasure DataMeasure;

        /// <summary>
        /// The internal path
        /// </summary>
        private protected string _internalPath;

        /// <summary>
        /// The save path
        /// </summary>
        private protected string _savePath;

        /// <summary>
        /// The file path
        /// </summary>
        private protected string _filePath;

        /// <summary>
        /// The file name
        /// </summary>
        private protected string _fileName;

        /// <summary>
        /// The row index
        /// </summary>
        private protected int _startRow;

        /// <summary>
        /// The row count
        /// </summary>
        private protected int _rowCount;

        /// <summary>
        /// The row index
        /// </summary>
        private protected int _startColumn;

        /// <summary>
        /// The column count
        /// </summary>
        private protected int _columnCount;

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int StartRow
        {
            get
            {
                return _startRow;
            }

            private protected set
            {
                _startRow = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int StartColumn
        {
            get
            {
                return _startColumn;
            }

            private protected set
            {
                _startColumn = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        protected ReportBase( )
        {
        }

        /// <summary>
        /// Sets the color of the range background.
        /// </summary>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="color">The color.</param>
        public void SetRangeBackgroundColor( ExcelRange excelRange, Color color )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                _dataRange = _dataWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                _dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                _dataRange.Style.Fill.BackgroundColor.SetColor( color );
                _dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
            }
            catch( Exception ex )
            {
                _dataRange?.Dispose( );
                Fail( ex );
            }
        }

        /// <summary>
        /// Gets the name of the excel column.
        /// </summary>
        /// <param name="columnIndex">StartRow of the column.</param>
        /// <returns></returns>
        private protected string GetExcelColumnName( int columnIndex )
        {
            try
            {
                ThrowIf.NegativeOrZero( columnIndex, nameof( columnIndex ) );
                if( columnIndex < 26 )
                {
                    return ( (char)( 'A' + columnIndex ) ).ToString( );
                }

                var _first = (char)( 'A' + columnIndex / 26 - 1 );
                var _second = (char)( 'A' + columnIndex % 26 );
                return $"{_first}{_second}";
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( string );
            }
        }

        /// <summary>
        /// Determines whether [is nullable type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is nullable type]
        /// [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        private protected bool IsNullableType( Type type )
        {
            try
            {
                return type == typeof( string )
                    || type.IsArray
                    || ( type.IsGenericType
                        && type.GetGenericTypeDefinition( ) == typeof( Nullable<> ) );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Releases unmanaged and
        /// - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both
        /// managed and unmanaged resources; <c>false</c>
        /// to release only unmanaged resources.</param>
        private void Dispose( bool disposing )
        {
            try
            {
                _font?.Dispose( );
                _titleFont?.Dispose( );
                _dataConnection?.Dispose( );
                _dataCommand?.Dispose( );
                _dataAdapter?.Dispose( );
                _excelTable = null;
                _dataRange?.Dispose( );
                _pivotRange?.Dispose( );
                _dataWorksheet?.Dispose( );
                _chartWorksheet?.Dispose( );
                _pivotWorksheet?.Dispose( );
                _excelWorkbook?.Dispose( );
                _excelPackage?.Dispose( );
                if( _fileInfo != null )
                {
                    _fileInfo = null;
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose( )
        {
            try
            {
                Dispose( true );
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