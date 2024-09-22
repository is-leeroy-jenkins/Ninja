// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 08-25-2020
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-25-2024
// ******************************************************************************************
// <copyright file="IDataMeasure.cs" company="Terry D. Eppler">
//    Badger is budget execution and data analysis tool for EPA Analysts
//    based on WPF, NET6.0, and is written in C-Sharp.
// 
//     Copyright ©  2020, 2022, 2204 Terry D. Eppler
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
//   IDataMeasure.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public interface IDataMeasure
    {
        /// <summary>
        /// Calculates the maximum.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns></returns>
        double CalculateMaximum( string numeric );

        /// <summary>
        /// Calculates the minimum.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns></returns>
        double CalculateMinimum( string numeric );

        /// <summary>
        /// Counts the values.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns>
        /// An int
        /// </returns>
        int CountValues( string numeric );

        /// <summary>
        /// Counts the values.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns>
        /// An int
        /// </returns>
        int CountValues( string numeric, IDictionary<string, object> where );

        /// <summary>
        /// Calculates the total.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns>
        /// A double
        /// </returns>
        double CalculateTotal( string numeric );

        /// <summary>
        /// Calculates the total.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        double CalculateTotal( string numeric, IDictionary<string, object> where );

        /// <summary>
        /// Calculates the average.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns></returns>
        double CalculateAverage( string numeric );

        /// <summary>
        /// Calculates the average.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        double CalculateAverage( string numeric, IDictionary<string, object> where );

        /// <summary>
        /// Calculates the percentage.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        double CalculatePercentage( string numeric, IDictionary<string, object> where );

        /// <summary>
        /// Calculates the deviation.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <returns></returns>
        double CalculateDeviation( string numeric );

        /// <summary>
        /// Calculates the deviation.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        /// <param name="where">The where.</param>
        /// <returns>
        /// A double
        /// </returns>
        double CalculateDeviation( string numeric, IDictionary<string, object> where );

        /// <summary>
        /// Calculates the variance.
        /// </summary>
        /// <param name="numeric">
        /// The numeric.
        /// </param>
        /// <returns>
        /// A double
        /// </returns>
        double CalculateVariance( string numeric );

        /// <summary>
        /// Calculates the variance.
        /// </summary>
        /// <param name="numeric">
        /// The numeric.
        /// </param>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <returns>
        /// A double
        /// </returns>
        double CalculateVariance( IDictionary<string, object> where, string numeric );
    }
}