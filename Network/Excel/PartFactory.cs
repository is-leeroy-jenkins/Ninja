// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="PartFactory.cs" company="Terry D. Eppler">
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
//   PartFactory.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using OfficeOpenXml.Drawing.Chart;
    using OfficeOpenXml.Drawing.Chart.Style;
    using OfficeOpenXml.Export.ToDataTable;
    using OfficeOpenXml.Style;
    using OfficeOpenXml.Table;
    using OfficeOpenXml.Table.PivotTable;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UseObjectOrCollectionInitializer" ) ]
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "HeapView.ObjectAllocation.Evident" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "PossibleNullReferenceException" ) ]
    [ SuppressMessage( "ReSharper", "RedundantCheckBeforeAssignment" ) ]
    [ SuppressMessage( "ReSharper", "RedundantAssignment" ) ]
    [ SuppressMessage( "ReSharper", "ConvertSwitchStatementToSwitchExpression" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantBaseConstructorCall" ) ]
    public abstract class PartFactory : Workbook
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath
        {
            get
            {
                return _filePath;
            }

            private protected set
            {
                _filePath = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName
        {
            get
            {
                return _fileName;
            }

            private protected set
            {
                _fileName = value;
            }
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        protected PartFactory( )
            : base( )
        {
        }

        /// <summary>
        /// Creates the excel table.
        /// </summary>
        private protected ExcelTable CreateExcelTable( DataTable dataTable )
        {
            try
            {
                ThrowIf.Null( dataTable, nameof( dataTable ) );
                _dataRange = (ExcelRange)_dataWorksheet.Cells[ "A2" ]
                    ?.LoadFromDataTable( dataTable, true, TableStyles.Light1 );

                var _title = _dataTable.TableName.SplitPascal( ) ?? "Badger";
                _dataWorksheet.HeaderFooter.OddHeader.CenteredText = _title;
                _dataRange.Style.Font.Name = "Segoe UI";
                _dataRange.Style.Font.Size = 8;
                _dataRange.Style.Font.Bold = false;
                _dataRange.Style.Font.Italic = false;
                _dataRange.EntireRow.CustomHeight = true;
                _dataRange.Style.Font.Color.SetColor( _fontColor );
                _dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                _dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                _excelTable = _dataWorksheet.Tables.GetFromRange( _dataRange );
                _excelTable.TableStyle = TableStyles.Light1;
                _excelTable.ShowHeader = true;
                _excelTable.Columns.Delete( 0, 1 );
                FormatFooter( _dataRange );
                return _excelTable;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ExcelTable );
            }
        }

        /// <summary>
        /// Creates the data table.
        /// </summary>
        /// <param name="startRow">The start row.</param>
        /// <param name="startColumn"> </param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endColumn"> </param>
        /// <returns>
        /// DataTable
        /// </returns>
        private protected DataTable CreateDataTable( int startRow, int startColumn,
            int endRow, int endColumn )
        {
            try
            {
                var _table = new DataTable( );
                _startRow = startRow;
                _startColumn = startColumn;
                _dataRange = _dataWorksheet.Cells[ startRow, startColumn, endRow, endColumn ];
                var _options = ToDataTableOptions.Create( );
                _options.DataTableName = _fileName ?? string.Empty;
                _options.AlwaysAllowNull = true;
                _options.ColumnNameParsingStrategy = NameParsingStrategy.RemoveSpace;
                _options.ExcelErrorParsingStrategy =
                    ExcelErrorParsingStrategy.HandleExcelErrorsAsBlankCells;

                _table = _dataRange?.ToDataTable( _options );
                return _table?.Rows.Count > 0
                    ? _table
                    : default( DataTable );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( DataTable );
            }
        }

        /// <summary>
        /// Creates the pivot table.
        /// </summary>
        /// <param name="excelRange">The data range.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="rows"> </param>
        /// <param name="data"> </param>
        /// <returns>
        /// ExcelPivotTable
        /// </returns>
        private protected ExcelPivotTable CreatePivotTable( ExcelRange excelRange,
            string tableName, IList<string> rows, IList<string> data )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( tableName, nameof( tableName ) );
                ThrowIf.Empty( rows, nameof( rows ) );
                ThrowIf.Empty( data, nameof( data ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                _pivotWorksheet = _excelWorkbook.Worksheets.Add( "Pivot" );
                _pivotRange = _pivotWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                _pivotRange.Style.Font.Name = "Segoe UI";
                _pivotRange.Style.Font.Size = 8;
                _pivotRange.Style.Font.Bold = false;
                _pivotRange.Style.Font.Italic = false;
                _pivotRange.EntireRow.CustomHeight = true;
                _pivotRange.Style.Font.Color.SetColor( _fontColor );
                _pivotRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                _pivotRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                _pivotTable = _pivotWorksheet.PivotTables.Add( _pivotWorksheet.Cells[ "C2" ],
                    _excelTable, tableName );

                for( var _i = 0; _i < rows.Count; _i++ )
                {
                    var _field = rows[ _i ];
                    if( !string.IsNullOrEmpty( _field ) )
                    {
                        var _row = _pivotTable.Fields[ _field ];
                        if( _row != null )
                        {
                            _pivotTable.RowFields.Add( _row );
                        }
                    }
                }

                for( var _i = 0; _i < data.Count; _i++ )
                {
                    var _item = data[ _i ];
                    if( !string.IsNullOrEmpty( _item ) )
                    {
                        var _row = _pivotTable.Fields[ _item ];
                        if( _row != null )
                        {
                            var _dataField = _pivotTable.DataFields.Add( _row );
                            _dataField.Format = "#,##0";
                        }
                    }
                }

                _pivotTable.DataOnRows = true;
                var _title = _dataTable.TableName.SplitPascal( ) ?? "Badger";
                _pivotWorksheet.HeaderFooter.OddHeader.CenteredText = _title;
                _pivotTable.EnableDrill = true;
                _pivotTable.ShowDrill = true;
                _pivotTable.PivotTableStyle = PivotTableStyles.Light15;
                return _pivotTable;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ExcelPivotTable );
            }
        }

        /// <summary>
        /// Creates the pie chart.
        /// </summary>
        /// <param name="excelRange">The data range.</param>
        /// <param name="chartName">Name of the table.</param>
        /// <param name="row"> </param>
        /// <param name="column"> </param>
        /// <returns>
        /// ExcelPieChart
        /// </returns>
        private protected ExcelPieChart CreatePieChart( ExcelRange excelRange, string chartName,
            string row, string column )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( chartName, nameof( chartName ) );
                ThrowIf.Null( row, nameof( row ) );
                ThrowIf.Null( column, nameof( column ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                var _anchor = _pivotWorksheet.Cells[ _startRow, _startColumn ];
                _chartWorksheet = _excelWorkbook.Worksheets.Add( "Chart" );
                _chartRange = _chartWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                _pivotTable = _chartWorksheet.PivotTables.Add( _anchor, _chartRange, chartName );
                _pivotTable.RowFields.Add( _pivotTable.Fields[ row ] );
                var _dataField = _pivotTable.DataFields.Add( _pivotTable.Fields[ column ] );
                _dataField.Format = "#,##0";
                _pivotTable.DataOnRows = true;
                _pieChart = _chartWorksheet.Drawings
                    ?.AddPieChart( "Chart", ePieChartType.PieExploded3D, _pivotTable );

                _pieChart.SetPosition( 1, 0, 4, 0 );
                _pieChart.SetSize( 800, 600 );
                _pieChart.Legend.Remove( );
                _pieChart.Series[ 0 ].DataLabel.ShowCategory = true;
                _pieChart.Series[ 0 ].DataLabel.Position = eLabelPosition.OutEnd;
                _pieChart.StyleManager.SetChartStyle( ePresetChartStyle.Pie3dChartStyle6 );
                return _pieChart;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ExcelPieChart );
            }
        }

        /// <summary>
        /// Creates the bar chart.
        /// </summary>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="chartName">Name of the chart.</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private protected ExcelBarChart CreateBarChart( ExcelRange excelRange, string chartName,
            string row, string column )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( chartName, nameof( chartName ) );
                ThrowIf.Null( row, nameof( row ) );
                ThrowIf.Null( column, nameof( column ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                var _anchor = _pivotWorksheet.Cells[ _startRow, _startColumn ];
                _chartWorksheet = _excelWorkbook.Worksheets.Add( "Chart" );
                _chartRange = _chartWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                _pivotTable = _chartWorksheet.PivotTables.Add( _anchor, _chartRange, chartName );
                _pivotTable.RowFields.Add( _pivotTable.Fields[ row ] );
                var _dataField = _pivotTable.DataFields.Add( _pivotTable.Fields[ column ] );
                _dataField.Format = "#,##0";
                _pivotTable.DataOnRows = true;
                _barChart = _chartWorksheet.Drawings
                    ?.AddBarChart( "Chart", eBarChartType.BarClustered3D, _pivotTable );

                _barChart.SetPosition( 1, 0, 4, 0 );
                _barChart.SetSize( 800, 600 );
                _barChart.Legend.Remove( );
                _barChart.Series[ 0 ].DataLabel.ShowCategory = true;
                _barChart.Series[ 0 ].DataLabel.Position = eLabelPosition.OutEnd;
                _barChart.StyleManager.SetChartStyle( ePresetChartStyle.BarChartStyle1 );
                return _barChart;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ExcelBarChart );
            }
        }

        /// <summary>
        /// Creates the area chart.
        /// </summary>
        /// <param name="excelRange">The excel range.</param>
        /// <param name="chartName">Name of the chart.</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        private protected ExcelAreaChart CreateAreaChart( ExcelRange excelRange, string chartName,
            string row, string column )
        {
            try
            {
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                ThrowIf.Null( chartName, nameof( chartName ) );
                ThrowIf.Null( row, nameof( row ) );
                ThrowIf.Null( column, nameof( column ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.End.Row;
                var _endColumn = excelRange.End.Column;
                var _anchor = _pivotWorksheet.Cells[ _startRow, _startColumn ];
                _chartWorksheet = _excelWorkbook.Worksheets.Add( "Chart" );
                _chartRange = _chartWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];
                _pivotTable = _chartWorksheet.PivotTables.Add( _anchor, _chartRange, chartName );
                _pivotTable.RowFields.Add( _pivotTable.Fields[ row ] );
                var _dataField = _pivotTable.DataFields.Add( _pivotTable.Fields[ column ] );
                _dataField.Format = "#,##0";
                _pivotTable.DataOnRows = true;
                _areaChart = _chartWorksheet.Drawings
                    ?.AddAreaChart( "Chart", eAreaChartType.AreaStacked3D, _pivotTable );

                _areaChart.SetPosition( 1, 0, 4, 0 );
                _areaChart.SetSize( 800, 600 );
                _areaChart.Legend.Remove( );
                _areaChart.Series[ 0 ].DataLabel.ShowCategory = true;
                _areaChart.Series[ 0 ].DataLabel.Position = eLabelPosition.OutEnd;
                _areaChart.StyleManager.SetChartStyle( ePresetChartStyle.Area3dChartStyle1 );
                return _areaChart;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( ExcelAreaChart );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="excelRange"> </param>
        /// <param name="text">The text.</param>
        public void CreateComment( ExcelRange excelRange, string text )
        {
            try
            {
                ThrowIf.Null( text, nameof( text ) );
                ThrowIf.Null( excelRange, nameof( excelRange ) );
                _startRow = excelRange.Start.Row;
                _startColumn = excelRange.Start.Column;
                var _endRow = excelRange.Start.Row;
                var _endColumn = excelRange.End.Column;
                _commentRange =
                    _dataWorksheet.Cells[ _startRow, _startColumn, _endRow, _endColumn ];

                var _comment = _commentRange.AddComment( text, "Budget" );
                _comment.From.Row = _commentRange.Start.Row;
                _comment.From.Column = _commentRange.Start.Column;
                _comment.To.Row = _commentRange.End.Row;
                _comment.To.Column = _commentRange.End.Column;
                _comment.BackgroundColor = Color.FromArgb( 40, 40, 40 );
                _comment.Font.FontName = "Segoe UI";
                _comment.Font.Size = 8;
                _comment.Font.Color = Color.FromArgb( 106, 189, 252 );
                _comment.Text = text;
            }
            catch( Exception ex )
            {
                Dispose( );
                Fail( ex );
            }
        }
    }
}