// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="None.cs" company="Terry D. Eppler">
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
//   None.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc/>
    /// <summary> </summary>
    /// <typeparam name="T"> </typeparam>
    /// <seealso cref="T:Ninja.IOption`1"/>
    /// <seealso cref="T:Ninja.IOption`1"/>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnassignedGetOnlyAutoProperty" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public class None<T> : Option<T>
    {
        /// <inheritdoc/>
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value> The value. </value>
        public override T Value { get; }

        /// <summary> Gets the default. </summary>
        /// <value> The default. </value>
        public static T Default { get; }

        /// <inheritdoc/>
        /// <summary>
        /// Gets a value indicating whether this instance is some.
        /// </summary>
        /// <value>
        /// <c> true </c>
        /// if this instance is some; otherwise,
        /// <c> false </c>
        /// .
        /// </value>
        public override bool IsSome
        {
            get { return false; }
        }

        /// <inheritdoc/>
        /// <summary>
        /// Gets a value indicating whether this instance is none.
        /// </summary>
        /// <value>
        /// <c> true </c>
        /// if this instance is none; otherwise,
        /// <c> false </c>
        /// .
        /// </value>
        public override bool IsNone
        {
            get { return true; }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="None{T}"/>
        /// class.
        /// </summary>
        public None( )
        {
        }

        /// <inheritdoc/>
        /// <summary>
        /// Maps the specified function.
        /// </summary>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="func"> The function. </param>
        /// <returns> </returns>
        public override Option<TResult> Map<TResult>( Func<T, TResult> func )
        {
            try
            {
                return new None<TResult>( );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( Option<TResult> );
            }
        }

        /// <inheritdoc/>
        /// <summary> Matches the specified some function. </summary>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="someFunc"> Some function. </param>
        /// <param name="noneFunc"> The none function. </param>
        /// <returns> </returns>
        public override TResult Match<TResult>( Func<T, TResult> someFunc, Func<TResult> noneFunc )
        {
            try
            {
                return noneFunc( );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( TResult );
            }
        }
    }
}