// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="SqlServerType.cs" company="Terry D. Eppler">
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
//   SqlServerType.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The SqlServer enum provides the data types used in SQL Server databases
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public enum SqlServerType
    {
        /// <summary>
        /// The tinyint
        /// </summary>
        TINYINT,

        /// <summary>
        /// The smallint
        /// </summary>
        SMALLINT,

        /// <summary>
        /// The int
        /// </summary>
        INT,

        /// <summary>
        /// The bigint
        /// </summary>
        BIGINT,

        /// <summary>
        /// The numeric
        /// </summary>
        NUMERIC,

        /// <summary>
        /// The decimal
        /// </summary>
        DECIMAL,

        /// <summary>
        /// The smallmoney
        /// </summary>
        SMALLMONEY,

        /// <summary>
        /// The money
        /// </summary>
        MONEY,

        /// <summary>
        /// The float
        /// </summary>
        FLOAT,

        /// <summary>
        /// The real
        /// </summary>
        REAL,

        /// <summary>
        /// The character
        /// </summary>
        CHAR,

        /// <summary>
        /// The text
        /// </summary>
        TEXT,

        /// <summary>
        /// The varchar
        /// </summary>
        VARCHAR,

        /// <summary>
        /// The nchar
        /// </summary>
        NCHAR,

        /// <summary>
        /// The ntext
        /// </summary>
        NTEXT,

        /// <summary>
        /// The nvarchar
        /// </summary>
        NVARCHAR,

        /// <summary>
        /// The time
        /// </summary>
        TIME,

        /// <summary>
        /// The date
        /// </summary>
        DATE,

        /// <summary>
        /// The datetime
        /// </summary>
        DATETIME,

        /// <summary>
        /// The smalldatetime
        /// </summary>
        SMALLDATETIME,

        /// <summary>
        /// The datetim e2
        /// </summary>
        DATETIME2,

        /// <summary>
        /// The datetimeoffset
        /// </summary>
        DATETIMEOFFSET,

        /// <summary>
        /// The binary
        /// </summary>
        BINARY,

        /// <summary>
        /// The image
        /// </summary>
        IMAGE,

        /// <summary>
        /// The varbinary
        /// </summary>
        VARBINARY,

        /// <summary>
        /// The cursor
        /// </summary>
        CURSOR,

        /// <summary>
        /// The rowversion
        /// </summary>
        ROWVERSION,

        /// <summary>
        /// The hierarchyid
        /// </summary>
        HIERARCHYID,

        /// <summary>
        /// The uniqueidentifier
        /// </summary>
        UNIQUEIDENTIFIER,

        /// <summary>
        /// The SQL variant
        /// </summary>
        SQL_VARIANT,

        /// <summary>
        /// The XML
        /// </summary>
        XML,

        /// <summary>
        /// The table
        /// </summary>
        TABLE
    }
}