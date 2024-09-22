// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="Holiday.cs" company="Terry D. Eppler">
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
//   Holiday.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;

    /// <summary>
    /// </summary>
    public enum Holiday
    {
        /// <summary>
        /// New Year’s Day, January 1.
        /// </summary>
        NewYears,

        /// <summary>
        /// Birthday of Martin Luther King, Jr., the third Monday in January.
        /// </summary>
        MartinLutherKing,

        /// <summary>
        /// Washington’s Birthday, the third Monday in February.
        /// </summary>
        Washington,

        /// <summary>
        /// Memorial Day, the last Monday in May.
        /// </summary>
        Memorial,

        /// <summary>
        /// Juneteenth National Independence Day, June 19.
        /// </summary>
        Juneteenth,

        /// <summary>
        /// Independence Day, July 4.
        /// </summary>
        Independence,

        /// <summary>
        /// Veterans Day, November 11.
        /// </summary>
        Veterans,

        /// <summary>
        /// Labor Day, the first Monday in September.
        /// </summary>
        Labor,

        /// <summary>
        /// Columbus Day, the second Monday in October.
        /// </summary>
        Columbus,

        /// <summary>
        /// Thanksgiving Day, the fourth Thursday in November.
        /// </summary>
        Thanksgiving,

        /// <summary>
        /// Christmas Day, December 25.
        /// </summary>
        Christmas
    }
}