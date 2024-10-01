// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="ToolType.cs" company="Terry D. Eppler">
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
//   ToolType.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public enum ToolType
    {
        /// <summary>
        /// The ns
        /// </summary>
        Ns = 0,

        /// <summary>
        /// The account button
        /// </summary>
        AccountButton,

        /// <summary>
        /// The access button
        /// </summary>
        AccessButton,

        /// <summary>
        /// The add button
        /// </summary>
        AddButton,

        /// <summary>
        /// The add column button
        /// </summary>
        AddColumnButton,

        /// <summary>
        /// The add record button
        /// </summary>
        AddRecordButton,

        /// <summary>
        /// The add table button
        /// </summary>
        AddTableButton,

        /// <summary>
        /// The add database button
        /// </summary>
        AddDatabaseButton,

        /// <summary>
        /// The back button
        /// </summary>
        BackButton,

        /// <summary>
        /// The blue tooth button
        /// </summary>
        BlueToothButton,

        /// <summary>
        /// The browse button
        /// </summary>
        BrowseButton,

        /// <summary>
        /// The chart button
        /// </summary>
        ChartButton,

        /// <summary>
        /// The cancel request button
        /// </summary>
        CancelRequestButton,

        /// <summary>
        /// The calculator button
        /// </summary>
        CalculatorButton,

        /// <summary>
        /// The calendar button
        /// </summary>
        CalendarButton,

        /// <summary>
        /// The close button
        /// </summary>
        CloseButton,

        /// <summary>
        /// The client button
        /// </summary>
        ClientButton,

        /// <summary>
        /// The CSV button
        /// </summary>
        CsvButton,

        /// <summary>
        /// The CSV import button
        /// </summary>
        CsvImportButton,

        /// <summary>
        /// The CSV export button
        /// </summary>
        CsvExportButton,

        /// <summary>
        /// The copy button
        /// </summary>
        CopyButton,

        /// <summary>
        /// The database button
        /// </summary>
        DatabaseButton,

        /// <summary>
        /// The database settings button
        /// </summary>
        DatabaseSettingsButton,

        /// <summary>
        /// The data configuration button
        /// </summary>
        DataConfigButton,

        /// <summary>
        /// The delete column button
        /// </summary>
        DeleteColumnButton,

        /// <summary>
        /// The delete record button
        /// </summary>
        DeleteRecordButton,

        /// <summary>
        /// The delete button
        /// </summary>
        DeleteButton,

        /// <summary>
        /// The data row button
        /// </summary>
        DataRowButton,

        /// <summary>
        /// The delete table button
        /// </summary>
        DeleteTableButton,

        /// <summary>
        /// The delete database button
        /// </summary>
        DeleteDatabaseButton,

        /// <summary>
        /// The download button
        /// </summary>
        DownloadButton,

        /// <summary>
        /// The download data button
        /// </summary>
        DownloadDataButton,

        /// <summary>
        /// The exit button
        /// </summary>
        ExitButton,

        /// <summary>
        /// The email button
        /// </summary>
        EmailButton,

        /// <summary>
        /// The export button
        /// </summary>
        ExportButton,

        /// <summary>
        /// The export database button
        /// </summary>
        ExportDatabaseButton,

        /// <summary>
        /// The excel button
        /// </summary>
        ExcelButton,

        /// <summary>
        /// The edit button
        /// </summary>
        EditButton,

        /// <summary>
        /// The edit record button
        /// </summary>
        EditRecordButton,

        /// <summary>
        /// The edit column button
        /// </summary>
        EditColumnButton,

        /// <summary>
        /// The edit text button
        /// </summary>
        EditTextButton,

        /// <summary>
        /// The encrypt data button
        /// </summary>
        EncryptDataButton,

        /// <summary>
        /// The excel import button
        /// </summary>
        ExcelImportButton,

        /// <summary>
        /// The excel export button
        /// </summary>
        ExcelExportButton,

        /// <summary>
        /// The filter data button
        /// </summary>
        FilterDataButton,

        /// <summary>
        /// The filter button
        /// </summary>
        FilterButton,

        /// <summary>
        /// The forward button
        /// </summary>
        ForwardButton,

        /// <summary>
        /// The first button
        /// </summary>
        FirstButton,

        /// <summary>
        /// The grid button
        /// </summary>
        GridButton,

        /// <summary>
        /// The group button
        /// </summary>
        GroupButton,

        /// <summary>
        /// The guidance button
        /// </summary>
        GuidanceButton,

        /// <summary>
        /// The google button
        /// </summary>
        GoogleButton,

        /// <summary>
        /// The go button
        /// </summary>
        GoButton,

        /// <summary>
        /// The home button
        /// </summary>
        HomeButton,

        /// <summary>
        /// The insert button
        /// </summary>
        InsertButton,

        /// <summary>
        /// The import button
        /// </summary>
        ImportButton,

        /// <summary>
        /// The import database button
        /// </summary>
        ImportDatabaseButton,

        /// <summary>
        /// The last button
        /// </summary>
        LastButton,

        /// <summary>
        /// The logout button
        /// </summary>
        LogoutButton,

        /// <summary>
        /// The lookup button
        /// </summary>
        LookupButton,

        /// <summary>
        /// The previous button
        /// </summary>
        PreviousButton,

        /// <summary>
        /// The menu button
        /// </summary>
        MenuButton,

        /// <summary>
        /// The metrics button
        /// </summary>
        MetricsButton,

        /// <summary>
        /// The next button
        /// </summary>
        NextButton,

        /// <summary>
        /// The navigation button
        /// </summary>
        NavigationButton,

        /// <summary>
        /// The pause button
        /// </summary>
        PauseButton,

        /// <summary>
        /// The play button
        /// </summary>
        PlayButton,

        /// <summary>
        /// The pivot button
        /// </summary>
        PivotButton,

        /// <summary>
        /// The power point button
        /// </summary>
        PowerPointButton,

        /// <summary>
        /// The print button
        /// </summary>
        PrintButton,

        /// <summary>
        /// The printer button
        /// </summary>
        PrinterButton,

        /// <summary>
        /// The PDF button
        /// </summary>
        PdfButton,

        /// <summary>
        /// The PDF import button
        /// </summary>
        PdfImportButton,

        /// <summary>
        /// The PDF export button
        /// </summary>
        PdfExportButton,

        /// <summary>
        /// The refresh button
        /// </summary>
        RefreshButton,

        /// <summary>
        /// The refresh data button
        /// </summary>
        RefreshDataButton,

        /// <summary>
        /// The redo button
        /// </summary>
        RedoButton,

        /// <summary>
        /// The remove button
        /// </summary>
        RemoveButton,

        /// <summary>
        /// The remove filters button
        /// </summary>
        RemoveFiltersButton,

        /// <summary>
        /// The rewind button
        /// </summary>
        RewindButton,

        /// <summary>
        /// The save button
        /// </summary>
        SaveButton,

        /// <summary>
        /// The save as button
        /// </summary>
        SaveAsButton,

        /// <summary>
        /// The search data button
        /// </summary>
        SearchDataButton,

        /// <summary>
        /// The settings button
        /// </summary>
        SettingsButton,

        /// <summary>
        /// The skip button
        /// </summary>
        SkipButton,

        /// <summary>
        /// The stop button
        /// </summary>
        StopButton,

        /// <summary>
        /// The edit SQL button
        /// </summary>
        EditSqlButton,

        /// <summary>
        /// The SQL server button
        /// </summary>
        SqlServerButton,

        /// <summary>
        /// The shutdown button
        /// </summary>
        ShutdownButton,

        /// <summary>
        /// The table button
        /// </summary>
        TableButton,

        /// <summary>
        /// The table settings button
        /// </summary>
        TableSettingsButton,

        /// <summary>
        /// The trash button
        /// </summary>
        TrashButton,

        /// <summary>
        /// The transfer button
        /// </summary>
        TransferButton,

        /// <summary>
        /// The transfer in button
        /// </summary>
        TransferInButton,

        /// <summary>
        /// The transfer out button
        /// </summary>
        TransferOutButton,

        /// <summary>
        /// The update button
        /// </summary>
        UpdateButton,

        /// <summary>
        /// The undo button
        /// </summary>
        UndoButton,

        /// <summary>
        /// The upload button
        /// </summary>
        UploadButton,

        /// <summary>
        /// The upload data button
        /// </summary>
        UploadDataButton,

        /// <summary>
        /// The verify button
        /// </summary>
        VerifyButton,

        /// <summary>
        /// The verify data button
        /// </summary>
        VerifyDataButton,

        /// <summary>
        /// The web button
        /// </summary>
        WebButton,

        /// <summary>
        /// The word button
        /// </summary>
        WordButton
    }
}