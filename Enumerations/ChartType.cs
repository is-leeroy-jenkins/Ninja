// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="ChartType.cs" company="Terry D. Eppler">
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
//   ChartType.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public enum ChartType
    {
        /// <summary>
        /// The ns
        /// </summary>
        Ns = 0,

        /// <summary>
        /// The column
        /// </summary>
        Column,

        /// <summary>
        /// The area
        /// </summary>
        Area,

        /// <summary>
        /// The bar
        /// </summary>
        Bar,

        /// <summary>
        /// The box and whisker
        /// </summary>
        BoxAndWhisker,

        /// <summary>
        /// The bubble
        /// </summary>
        Bubble,

        /// <summary>
        /// The candle
        /// </summary>
        Candle,

        /// <summary>
        /// The column range
        /// </summary>
        ColumnRange,

        /// <summary>
        /// The gannt
        /// </summary>
        Gannt,

        /// <summary>
        /// The rotated spline
        /// </summary>
        RotatedSpline,

        /// <summary>
        /// The funnel
        /// </summary>
        Funnel,

        /// <summary>
        /// The heat map
        /// </summary>
        HeatMap,

        /// <summary>
        /// The hi lo
        /// </summary>
        HiLo,

        /// <summary>
        /// The hi lo open close
        /// </summary>
        HiLoOpenClose,

        /// <summary>
        /// The histogram
        /// </summary>
        Histogram,

        /// <summary>
        /// The kagi
        /// </summary>
        Kagi,

        /// <summary>
        /// The line
        /// </summary>
        Line,

        /// <summary>
        /// The pie
        /// </summary>
        Pie,

        /// <summary>
        /// The point and figure
        /// </summary>
        PointAndFigure,

        /// <summary>
        /// The polar
        /// </summary>
        Polar,

        /// <summary>
        /// The pyramid
        /// </summary>
        Pyramid,

        /// <summary>
        /// The radar
        /// </summary>
        Radar,

        /// <summary>
        /// The range area
        /// </summary>
        RangeArea,

        /// <summary>
        /// The scatter
        /// </summary>
        Scatter,

        /// <summary>
        /// The spline
        /// </summary>
        Spline,

        /// <summary>
        /// The spline area
        /// </summary>
        SplineArea,

        /// <summary>
        /// The stacking area
        /// </summary>
        StackingArea,

        /// <summary>
        /// The stacking area100
        /// </summary>
        StackingArea100,

        /// <summary>
        /// The stacking bar
        /// </summary>
        StackingBar,

        /// <summary>
        /// The stacking bar100
        /// </summary>
        StackingBar100,

        /// <summary>
        /// The stacking column
        /// </summary>
        StackingColumn,

        /// <summary>
        /// The stacking column100
        /// </summary>
        StackingColumn100,

        /// <summary>
        /// The step area
        /// </summary>
        StepArea,

        /// <summary>
        /// The step line
        /// </summary>
        StepLine,

        /// <summary>
        /// The three line break
        /// </summary>
        ThreeLineBreak,

        /// <summary>
        /// The tornado
        /// </summary>
        Tornado
    }
}