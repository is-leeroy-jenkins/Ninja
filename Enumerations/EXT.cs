// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="EXT.cs" company="Terry D. Eppler">
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
//   EXT.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public enum EXT
    {
        /// <summary>
        /// The database
        /// </summary>
        DB,

        /// <summary>
        /// The accdb
        /// </summary>
        ACCDB,

        /// <summary>
        /// The MDB
        /// </summary>
        MDB,

        /// <summary>
        /// The SDF
        /// </summary>
        SDF,

        /// <summary>
        /// The MDF
        /// </summary>
        MDF,

        /// <summary>
        /// The XLS
        /// </summary>
        XLS,

        /// <summary>
        /// The XLSX
        /// </summary>
        XLSX,

        /// <summary>
        /// The CSV
        /// </summary>
        CSV,

        /// <summary>
        /// The text
        /// </summary>
        TXT,

        /// <summary>
        /// The PDF
        /// </summary>
        PDF,

        /// <summary>
        /// The docx
        /// </summary>
        DOCX,

        /// <summary>
        /// The document
        /// </summary>
        DOC,

        /// <summary>
        /// The SQL
        /// </summary>
        SQL,

        /// <summary>
        /// The RESX
        /// </summary>
        RESX,

        /// <summary>
        /// The DLL
        /// </summary>
        DLL,

        /// <summary>
        /// The executable
        /// </summary>
        EXE,

        /// <summary>
        /// The icon
        /// </summary>
        ICO,

        /// <summary>
        /// The PNG
        /// </summary>
        PNG,

        /// <summary>
        /// The GIF
        /// </summary>
        GIF,

        /// <summary>
        /// The Power Point Extension
        /// </summary>
        PPT,

        /// <summary>
        /// The Power Point Extension
        /// </summary>
        PPTX
    }
}