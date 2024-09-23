// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="Workbook.cs" company="Terry D. Eppler">
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
//   Workbook.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MergeIntoPattern" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantCheckBeforeAssignment" ) ]
    [ SuppressMessage( "ReSharper", "RedundantBaseConstructorCall" ) ]
    public abstract class Workbook : SheetConfig
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.Workbook" /> class.
        /// </summary>
        public void FormatDataWorksheet( )
        {
            try
            {
                if( _dataTable != null )
                {
                    _dataWorksheet.View.ShowGridLines = false;
                    _dataWorksheet.View.ZoomScale = _zoomLevel;
                    _dataWorksheet.View.PageLayoutView = true;
                    _dataWorksheet.View.ShowHeaders = true;
                    _dataWorksheet.DefaultRowHeight = _rowHeight;
                    _dataWorksheet.DefaultColWidth = _columnWidth;
                    _dataWorksheet.PrinterSettings.ShowHeaders = false;
                    _dataWorksheet.PrinterSettings.ShowGridLines = false;
                    _dataWorksheet.PrinterSettings.LeftMargin = _leftMargin;
                    _dataWorksheet.PrinterSettings.RightMargin = _rightMargin;
                    _dataWorksheet.PrinterSettings.TopMargin = _headerMargin;
                    _dataWorksheet.PrinterSettings.BottomMargin = _footerMargin;
                    _dataWorksheet.PrinterSettings.HorizontalCentered = true;
                    _dataWorksheet.PrinterSettings.VerticalCentered = true;
                    _dataWorksheet.PrinterSettings.FitToPage = true;
                    _dataWorksheet.HeaderFooter.AlignWithMargins = true;
                    _dataWorksheet.HeaderFooter.ScaleWithDocument = true;
                    _dataWorksheet.HeaderFooter.FirstHeader.CenteredText =
                        _dataTable.TableName.SplitPascal( );
                }
                else
                {
                    _dataWorksheet.View.ShowGridLines = false;
                    _dataWorksheet.View.ZoomScale = _zoomLevel;
                    _dataWorksheet.View.PageLayoutView = true;
                    _dataWorksheet.View.ShowHeaders = true;
                    _dataWorksheet.DefaultRowHeight = _rowHeight;
                    _dataWorksheet.DefaultColWidth = _columnWidth;
                    _dataWorksheet.PrinterSettings.ShowHeaders = false;
                    _dataWorksheet.PrinterSettings.ShowGridLines = false;
                    _dataWorksheet.PrinterSettings.LeftMargin = _leftMargin;
                    _dataWorksheet.PrinterSettings.RightMargin = _rightMargin;
                    _dataWorksheet.PrinterSettings.TopMargin = _headerMargin;
                    _dataWorksheet.PrinterSettings.BottomMargin = _footerMargin;
                    _dataWorksheet.PrinterSettings.HorizontalCentered = true;
                    _dataWorksheet.PrinterSettings.VerticalCentered = true;
                    _dataWorksheet.PrinterSettings.FitToPage = true;
                    _dataWorksheet.HeaderFooter.AlignWithMargins = true;
                    _dataWorksheet.HeaderFooter.ScaleWithDocument = true;
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                Dispose( );
            }
        }

        /// <summary>
        /// Sets the header text.
        /// </summary>
        /// <param name="excelRange">
        /// The grid.
        /// </param>
        /// <param name="labels">
        /// </param>
        public void SetHeaderRowText( ExcelRange excelRange, IList<string> labels )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( labels, nameof( labels ) );
                var _header = excelRange.Start.Row - 1;
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.Start.Row - 1;
                var _endColumn = excelRange.End.Column;
                _headerRange = _dataWorksheet.Cells[ _header, _startColumn, _endRow, _endColumn ];
                foreach( var _item in labels )
                {
                    if( _startColumn <= _endColumn )
                    {
                        _headerRange.SetCellValue( _header, _startColumn, _item );
                    }

                    _startColumn++;
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                Dispose( );
            }
        }

        /// <summary>
        /// Sets the footer text.
        /// </summary>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="labels">The labels.</param>
        public void SetFooterRowText( ExcelRange excelRange, IEnumerable<string> labels )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( labels, nameof( labels ) );
                _startRow = excelRange.Start.Row;
                var _footer = excelRange.Start.Row + 1;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row + 1;
                var _endColumn = excelRange.End.Column;
                _footerRange = _dataWorksheet.Cells[ _footer, _startColumn, _endRow, _endColumn ];
                foreach( var _item in labels )
                {
                    if( _startColumn <= _endColumn )
                    {
                        _footerRange.SetCellValue( _footer, _startColumn, _item );
                    }

                    _startColumn++;
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                Dispose( );
            }
        }

        /// <summary>
        /// Sets the color of the alternating row.
        /// </summary>
        /// <param name="excelRange">The range.</param>
        public void SetAlternatingRowColor( ExcelRange excelRange )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                _dataRange = _dataWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                for( var _i = _startRow; _i < _endRow; _i++ )
                {
                    if( _i % 2 == 0 )
                    {
                        var _row = _dataWorksheet.Cells[ _i, _startColumn, _i, _endColumn ];
                        _row.EntireRow.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                        _row.EntireRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        _row.EntireRow.Style.Fill.BackgroundColor.SetColor( _primaryBackColor );
                    }
                    else
                    {
                        var _row = _dataWorksheet.Cells[ _i, _startColumn, _i, _endColumn ];
                        _row.EntireRow.Style.Border.Bottom.Style = ExcelBorderStyle.Hair;
                        _row.EntireRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        _row.EntireRow.Style.Fill.BackgroundColor.SetColor( _secondaryBackColor );
                    }
                }
            }
            catch( Exception ex )
            {
                Dispose( );
                Fail( ex );
            }
        }

        /// <summary>
        /// Sets the numeric row format.
        /// </summary>
        /// <param name="excelRange">The range.</param>
        public void FormatNumericRow( ExcelRange excelRange )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                _dataRange = _dataWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                _dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                _dataRange.Style.Numberformat.Format = "#,###";
            }
            catch( Exception ex )
            {
                Dispose( );
                Fail( ex );
            }
        }

        /// <summary>
        /// Sets the total row format.
        /// </summary>
        /// <param name="excelRange">The range.</param>
        public void FormatTable( ExcelRange excelRange )
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
                _dataRange.Style.Fill.BackgroundColor.SetColor( _secondaryBackColor );
                _dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
            }
            catch( Exception ex )
            {
                Dispose( );
                Fail( ex );
            }
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        protected Workbook( )
            : base( )
        {
        }

        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>
        /// The color of the font.
        /// </value>
        public Color FontColor
        {
            get
            {
                return _fontColor;
            }

            private protected set
            {
                _fontColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
        public Font Font
        {
            get
            {
                return _font;
            }

            private protected set
            {
                _font = value;
            }
        }

        /// <summary>
        /// Gets or sets the title font.
        /// </summary>
        /// <value>
        /// The title font.
        /// </value>
        public Font TitleFont
        {
            get
            {
                return _titleFont;
            }

            private protected set
            {
                _titleFont = value;
            }
        }
    }
}