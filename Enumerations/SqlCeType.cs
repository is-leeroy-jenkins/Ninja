// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="SqlCeType.cs" company="Terry D. Eppler">
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
//   SqlCeType.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public enum SqlCeType
    {
        /// <summary>
        /// The smallint SQL CE data type
        /// </summary>
        SMALLINT,

        /// <summary>
        /// The int SQL CE data type
        /// </summary>
        INT,

        /// <summary>
        /// The tinyint SQL CE data type
        /// </summary>
        TINYINT,

        /// <summary>
        /// The bigint SQL CE data type
        /// </summary>
        BIGINT,

        /// <summary>
        /// The real SQL CE data type
        /// </summary>
        REAL,

        /// <summary>
        /// The float SQL CE data type
        /// </summary>
        FLOAT,

        /// <summary>
        /// The money SQL CE data type
        /// </summary>
        MONEY,

        /// <summary>
        /// The numeric SQL CE data type
        /// </summary>
        NUMERIC,

        /// <summary>
        /// The character SQL CE data type
        /// </summary>
        CHAR,

        /// <summary>
        /// The text SQL CE data type
        /// </summary>
        TEXT,

        /// <summary>
        /// The varchar SQL CE data type
        /// </summary>
        VARCHAR,

        /// <summary>
        /// The nchar SQL CE data type
        /// </summary>
        NCHAR,

        /// <summary>
        /// The ntext SQL CE data type
        /// </summary>
        NTEXT,

        /// <summary>
        /// The nvarchar SQL CE data type
        /// </summary>
        NVARCHAR,

        /// <summary>
        /// The datetime SQL CE data type
        /// </summary>
        DATETIME,

        /// <summary>
        /// The binary SQL CE data type
        /// </summary>
        BINARY,

        /// <summary>
        /// The image SQL CE data type
        /// </summary>
        IMAGE,

        /// <summary>
        /// The varbinary SQL CE data type
        /// </summary>
        VARBINARY,

        /// <summary>
        /// The cursor SQL CE data type
        /// </summary>
        CURSOR,

        /// <summary>
        /// The row version SQL CE data type
        /// </summary>
        ROWVERSION,

        /// <summary>
        /// The uniqueidentifier SQL CE data type
        /// </summary>
        UNIQUEIDENTIFIER,

        /// <summary>
        /// The timestamp SQL CE data type
        /// </summary>
        TIMESTAMP
    }
}