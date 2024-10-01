// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="ExcelExtensions.cs" company="Terry D. Eppler">
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
//   ExcelExtensions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using Syncfusion.Data.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public static class ExcelExtensions
    {
        /// <summary>
        /// Converts to data set.
        /// </summary>
        /// <param name="excelPackage">The excelPackage.</param>
        /// <param name="header">if set to
        /// <c> true </c>
        /// [header].</param>
        /// <returns></returns>
        public static DataSet ToDataSet( this ExcelPackage excelPackage, bool header = false )
        {
            try
            {
                var _row = header
                    ? 1
                    : 0;

                return excelPackage.ToDataSet( _row );
            }
            catch( Exception ex )
            {
                ExcelExtensions.Fail( ex );
                return default( DataSet );
            }
        }

        /// <summary>
        /// Converts to data set.
        /// </summary>
        /// <param name="excelPackage">The excelPackage.</param>
        /// <param name="header">The header.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">header - Must be 0 or greater.</exception>
        /// <exception cref="ArgumentOutOfRangeException">header - Must be 0 or greater.</exception>
        private static DataSet ToDataSet( this ExcelPackage excelPackage, int header = 0 )
        {
            try
            {
                ThrowIf.NegativeOrZero( header, nameof( header ) );
                var _result = new DataSet( );
                foreach( var _worksheet in excelPackage.Workbook.Worksheets )
                {
                    var _table = new DataTable( );
                    if( _worksheet?.Name != null )
                    {
                        _table.TableName = _worksheet?.Name;
                    }

                    var _start = 1;
                    if( header > 0 )
                    {
                        _start = header;
                    }

                    var _columns =
                        from _cell in _worksheet?.Cells[ _start, 1, _start,
                            _worksheet.Dimension.End.Column ] select new DataColumn( header > 0
                            ? _cell?.Value?.ToString( )
                            : $"Column {_cell?.Start?.Column}" );

                    _table.Columns.AddRange( _columns?.ToArray( ) );
                    var _i = header > 0
                        ? _start + 1
                        : _start;

                    for( var _index = _i; _index <= _worksheet?.Dimension.End.Row; _index++ )
                    {
                        var _range = _worksheet.Cells[ _index, 1, _index,
                            _worksheet.Dimension.End.Column ];

                        var _row = _table.Rows?.Add( );
                        foreach( var _cell in _range )
                        {
                            _row[ _cell.Start.Column - 1 ] = _cell.Value;
                        }
                    }

                    _result.Tables?.Add( _table );
                }

                return _result;
            }
            catch( Exception ex )
            {
                ExcelExtensions.Fail( ex );
                return default( DataSet );
            }
        }

        /// <summary>
        /// Trims the last empty rows.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        public static void TrimLastEmptyRows( this ExcelWorksheet worksheet )
        {
            try
            {
                while( worksheet.IsLastRowEmpty( ) )
                {
                    worksheet.DeleteRow( worksheet.Dimension.End.Row, 1 );
                }
            }
            catch( Exception ex )
            {
                ExcelExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Determines whether [is last row empty].
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <returns>
        /// <c> true </c>
        /// if [is last row empty] [the specified worksheet]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool IsLastRowEmpty( this ExcelWorksheet worksheet )
        {
            try
            {
                var _empties = new List<bool>( );
                for( var _index = 1; _index <= worksheet.Dimension.End.Column; _index++ )
                {
                    var _value = worksheet.Cells[ worksheet.Dimension.End.Row, _index ].Value;
                    _empties.Add( string.IsNullOrEmpty( _value?.ToString( ) ) );
                }

                return _empties.All( e => e );
            }
            catch( Exception ex )
            {
                ExcelExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Sets the width.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="width">The width.</param>
        public static void SetColumnWidth( this ExcelColumn column, double width )
        {
            if( width > 0 )
            {
                try
                {
                    var _first = width >= 1.0
                        ? Math.Round( ( Math.Round( 7.0 * ( width - 0.0 ), 0 ) - 5.0 ) / 7.0, 2 )
                        : Math.Round(
                            ( Math.Round( 12.0 * ( width - 0.0 ), 0 )
                                - Math.Round( 5.0 * width, 0 ) ) / 12.0, 2 );

                    var _second = width - _first;
                    var _third = width >= 1.0
                        ? Math.Round( 7.0 * _second - 0.0, 0 ) / 7.0
                        : Math.Round( 12.0 * _second - 0.0, 0 ) / 12.0 + 0.0;

                    column.Width = _first > 0.0
                        ? width + _third
                        : 0.0;
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                }
            }
        }

        /// <summary>
        /// Sets the height.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="height">The height.</param>
        public static void SetRowHeight( this ExcelRow row, double height )
        {
            if( height > 0 )
            {
                try
                {
                    row.Height = height;
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                }
            }
        }

        /// <summary>
        /// Expands the column.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int[ ] ExpandColumn( this int[ ] index, int offset )
        {
            if( offset > 0 )
            {
                try
                {
                    var _column = index;
                    _column[ 3 ] += offset;
                    return _column;
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                    return default( int[ ] );
                }
            }

            return default( int[ ] );
        }

        /// <summary>
        /// Expands the row.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int[ ] ExpandRow( this int[ ] index, int offset )
        {
            if( offset > 0 )
            {
                try
                {
                    var _row = index;
                    _row[ 2 ] += offset;
                    return _row;
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                    return default( int[ ] );
                }
            }

            return default( int[ ] );
        }

        /// <summary>
        /// Moves the column.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int[ ] MoveColumn( this int[ ] index, int offset )
        {
            if( offset > 0 )
            {
                try
                {
                    var _column = index;
                    _column[ 1 ] += offset;
                    _column[ 3 ] += offset;
                    return _column;
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                    return default( int[ ] );
                }
            }

            return default( int[ ] );
        }

        /// <summary>
        /// Moves the row.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static int[ ] MoveRow( this int[ ] index, int offset )
        {
            if( offset > 0 )
            {
                try
                {
                    var _row = index;
                    _row[ 0 ] += offset;
                    _row[ 2 ] += offset;
                    return _row;
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                    return default( int[ ] );
                }
            }

            return default( int[ ] );
        }

        /// <summary>
        /// All the borders.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="borderStyle">The border style.</param>
        public static void AllBorder( this ExcelRange range,
            ExcelBorderStyle borderStyle = ExcelBorderStyle.Thin )
        {
            try
            {
                range.ForEach( r => r.Style.Border.BorderAround( borderStyle ) );
            }
            catch( Exception ex )
            {
                ExcelExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Backgrounds the color.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="color">The color.</param>
        /// <param name="fillStyle">The fill style.</param>
        public static void BackgroundColor( this ExcelRange range, Color color,
            ExcelFillStyle fillStyle = ExcelFillStyle.Solid )
        {
            if( color != Color.Empty )
            {
                try
                {
                    range.Style.Fill.PatternType = fillStyle;
                    range.Style.Fill.BackgroundColor.SetColor( color );
                }
                catch( Exception ex )
                {
                    ExcelExtensions.Fail( ex );
                }
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }

        /// <summary>
        /// 
        /// </summary>
        public enum InsertMode
        {
            /// <summary>
            /// The row before
            /// </summary>
            RowBefore,

            /// <summary>
            /// The row after
            /// </summary>
            RowAfter,

            /// <summary>
            /// The column right
            /// </summary>
            ColumnRight,

            /// <summary>
            /// The column left
            /// </summary>
            ColumnLeft
        }
    }
}