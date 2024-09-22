// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="SheetConfig.cs" company="Terry D. Eppler">
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
//   SheetConfig.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "VirtualMemberNeverOverridden.Global" ) ]
    [ SuppressMessage( "ReSharper", "LoopCanBePartlyConvertedToQuery" ) ]
    [ SuppressMessage( "ReSharper", "ConvertSwitchStatementToSwitchExpression" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MergeIntoPattern" ) ]
    [ SuppressMessage( "ReSharper", "RedundantCheckBeforeAssignment" ) ]
    [ SuppressMessage( "ReSharper", "PossibleNullReferenceException" ) ]
    [ SuppressMessage( "ReSharper", "ParameterTypeCanBeEnumerable.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantBaseConstructorCall" ) ]
    public abstract class SheetConfig : ReportBase
    {
        /// <summary>
        /// Gets or sets the data connection.
        /// </summary>
        /// <value>
        /// The data connection.
        /// </value>
        public OleDbConnection DataConnection
        {
            get
            {
                return _dataConnection;
            }

            private protected set
            {
                _dataConnection = value;
            }
        }

        /// <summary>
        /// Gets or sets the data command.
        /// </summary>
        /// <value>
        /// The data command.
        /// </value>
        public OleDbCommand DataCommand
        {
            get
            {
                return _dataCommand;
            }

            private protected set
            {
                _dataCommand = value;
            }
        }

        /// <summary>
        /// Gets or sets the data adapter.
        /// </summary>
        /// <value>
        /// The data adapter.
        /// </value>
        public OleDbDataAdapter DataAdapter
        {
            get
            {
                return _dataAdapter;
            }

            private protected set
            {
                _dataAdapter = value;
            }
        }

        /// <summary>
        /// Gets or sets the file information.
        /// </summary>
        /// <value>
        /// The file information.
        /// </value>
        public FileInfo FileInfo
        {
            get
            {
                return _fileInfo;
            }

            private protected set
            {
                _fileInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>
        /// The application.
        /// </value>
        public ExcelPackage Application
        {
            get
            {
                return _excelPackage;
            }

            private protected set
            {
                _excelPackage = value;
            }
        }

        /// <summary>
        /// Gets or sets the workbook.
        /// </summary>
        /// <value>
        /// The workbook.
        /// </value>
        public ExcelWorkbook ExcelWorkbook
        {
            get
            {
                return _excelWorkbook;
            }

            private protected set
            {
                _excelWorkbook = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelRange PivotRange
        {
            get
            {
                return _pivotRange;
            }

            private protected set
            {
                _pivotRange = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelRange ChartRange
        {
            get
            {
                return _chartRange;
            }

            private protected set
            {
                _chartRange = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelRange DataRange
        {
            get
            {
                return _dataRange;
            }

            private protected set
            {
                _dataRange = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelRange HeaderRange
        {
            get
            {
                return _headerRange;
            }

            private protected set
            {
                _headerRange = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelRange CommentRange
        {
            get
            {
                return _commentRange;
            }

            private protected set
            {
                _commentRange = value;
            }
        }

        /// <summary>
        /// Gets or sets the worksheet.
        /// </summary>
        /// <value>
        /// The worksheet.
        /// </value>
        public ExcelWorksheet DataWorksheet
        {
            get
            {
                return _dataWorksheet;
            }

            private protected set
            {
                _dataWorksheet = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelWorksheet ChartWorksheet
        {
            get
            {
                return _chartWorksheet;
            }

            private protected set
            {
                _chartWorksheet = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ExcelWorksheet PivotWorksheet
        {
            get
            {
                return _pivotWorksheet;
            }

            private protected set
            {
                _pivotWorksheet = value;
            }
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public IList<ExcelComment> Comments
        {
            get
            {
                return _comments;
            }

            private protected set
            {
                _comments = value;
            }
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
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
        ///  <summary>
        ///  </summary>
        protected SheetConfig( )
            : base( )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public string CreateConnectionString( string extension, string filePath )
        {
            try
            {
                ThrowIf.Null( extension, nameof( extension ) );
                ThrowIf.Null( filePath, nameof( filePath ) );
                switch( extension?.ToUpper( ) )
                {
                    case ".XLS":
                    {
                        return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                            + filePath
                            + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
                    }
                    case ".XLSX":
                    {
                        return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + filePath
                            + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    }
                    default:
                    {
                        return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                            + filePath
                            + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    }
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelRange"></param>
        private protected void FormatHeader( ExcelRange excelRange )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                var _header = _startRow - 1;
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endColumn = excelRange.End.Column;
                _headerRange = _dataWorksheet.Cells[ _header, _startColumn, _header, _endColumn ];
                _headerRange.Style.Font.Name = "Segoe UI";
                _headerRange.Style.Font.Size = 8;
                _headerRange.Style.Font.Bold = false;
                _headerRange.Style.Font.Italic = false;
                _headerRange.Style.Font.Color.SetColor( _fontColor );
                _headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                _headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                _headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                _headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                _headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                _headerRange.Style.Fill.BackgroundColor.SetColor( _secondaryBackColor );
            }
            catch( Exception ex )
            {
                Fail( ex );
                Dispose( );
            }
        }

        /// <summary>
        /// Sets the header row format.
        /// </summary>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="names"> </param>
        private protected void SetHeaderValues( ExcelRange excelRange, IList<string> names )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( names, nameof( names ) );
                var _header = _startRow - 1;
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endColumn = excelRange.End.Column;
                _dataRange = _dataWorksheet.Cells[ _header, _startColumn, _header, _endColumn ];
                _dataRange.Style.Font.Name = "Segoe UI";
                _dataRange.Style.Font.Size = 9;
                _dataRange.Style.Font.Bold = false;
                _dataRange.Style.Font.Italic = false;
                for( var _i = _startColumn; _i < _endColumn; _i++ )
                {
                    _dataRange[ _header, _i ].Value = names[ _i ];
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                Dispose( );
            }
        }

        /// <summary>
        /// Sets the footer row format.
        /// </summary>
        /// <param name="excelRange">The excel range.</param>
        private protected void FormatFooter( ExcelRange excelRange )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                var _footer = excelRange.End.Row + 1;
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endColumn = excelRange.End.Column;
                _footerRange = _dataWorksheet.Cells[ _footer, _startColumn, _footer, _endColumn ];
                _footerRange.Style.Font.Name = "Segoe UI";
                _footerRange.Style.Font.Size = 8;
                _footerRange.Style.Font.Bold = true;
                _footerRange.Style.Font.Italic = false;
                _footerRange.Style.Font.Color.SetColor( _fontColor );
                _footerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                _footerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                _footerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                _footerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                _footerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                _footerRange.Style.Fill.BackgroundColor.SetColor( _primaryBackColor );
            }
            catch( Exception ex )
            {
                Fail( ex );
                Dispose( );
            }
        }
    }
}