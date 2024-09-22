// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="AccessType.cs" company="Terry D. Eppler">
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
//   AccessType.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The Access enum provides the data types used in MS Access databases
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public enum AccessType
    {
        /// <summary>
        /// The auto number Access data type
        /// </summary>
        AUTONUMBER,

        /// <summary>
        /// The number Access data type
        /// </summary>
        NUMBER,

        /// <summary>
        /// The large number Access data type
        /// </summary>
        LARGENUMBER,

        /// <summary>
        /// The short text Access data type
        /// </summary>
        SHORTTEXT,

        /// <summary>
        /// The text Access data type
        /// </summary>
        TEXT,

        /// <summary>
        /// The long text Access data type
        /// </summary>
        LONGTEXT,

        /// <summary>
        /// The date time Access data type
        /// </summary>
        DATETIME,

        /// <summary>
        /// The date Access data type
        /// </summary>
        DATE,

        /// <summary>
        /// The currency Access data type
        /// </summary>
        CURRENCY,

        /// <summary>
        /// The yes/no Access data type
        /// </summary>
        YESNO,

        /// <summary>
        /// The hyperlink Access data type
        /// </summary>
        HYPERLINK,

        /// <summary>
        /// The attachment Access data type
        /// </summary>
        ATTACHMENT
    }
}