// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="LinqExtensions.cs" company="Terry D. Eppler">
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
//   LinqExtensions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public static class LinqExtensions
    {
        /// <summary>
        /// The specified predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static bool None<TSource>( this IEnumerable<TSource> source,
            Func<TSource, bool> predicate )
        {
            try
            {
                return !source.Any( predicate );
            }
            catch( Exception ex )
            {
                LinqExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has at least] [the specified minimum count].
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="minCount">The minimum count.</param>
        /// <returns>
        /// <c> true </c>
        /// if [has at least] [the specified minimum count]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool HasAtLeast<TSource>( this IEnumerable<TSource> source, int minCount )
        {
            try
            {
                return source.HasAtLeast( minCount, _ => true );
            }
            catch( Exception ex )
            {
                LinqExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has at least] [the specified minimum count].
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="minCount">The minimum count.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// <c> true </c>
        /// if [has at least] [the specified minimum count]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool HasAtLeast<TSource>( this IEnumerable<TSource> source, int minCount,
            Func<TSource, bool> predicate )
        {
            if( minCount == 0 )
            {
                return true;
            }

            if( source is ICollection _sequence
                && _sequence?.Count < minCount )
            {
                return false;
            }

            var _matches = 0;
            foreach( var _unused in source.Where( predicate ) )
            {
                _matches++;
                if( _matches >= minCount )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified count has exactly.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// <c> true </c>
        /// if the specified count has exactly; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool HasExactly<TSource>( this IEnumerable<TSource> source, int count )
        {
            try
            {
                return source is ICollection _sequence
                    ? _sequence.Count == count
                    : source.HasExactly( count, _ => true );
            }
            catch( Exception ex )
            {
                LinqExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified count has exactly.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// <c> true </c>
        /// if the specified count has exactly; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool HasExactly<TSource>( this IEnumerable<TSource> source, int count,
            Func<TSource, bool> predicate )
        {
            try
            {
                ThrowIf.NegativeOrZero( count, nameof( count ) );
                if( source is ICollection _sequence
                    && _sequence.Count < count )
                {
                    return false;
                }

                var _matches = 0;
                foreach( var _unused in source.Where( predicate ) )
                {
                    ++_matches;
                    if( _matches > count )
                    {
                        return false;
                    }
                }

                return _matches == count;
            }
            catch( Exception ex )
            {
                LinqExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has at most] [the specified limit].
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>
        /// <c> true </c>
        /// if [has at most] [the specified limit]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool HasAtMost<TSource>( this IEnumerable<TSource> source, int limit )
        {
            try
            {
                return source.HasAtMost( limit, _ => true );
            }
            catch( Exception ex )
            {
                LinqExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Determines whether [has at most] [the specified limit].
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// <c> true </c>
        /// if [has at most] [the specified limit]; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool HasAtMost<TSource>( this IEnumerable<TSource> source, int limit,
            Func<TSource, bool> predicate )
        {
            try
            {
                if( source is ICollection _sequence
                    && _sequence.Count <= limit )
                {
                    return true;
                }

                var _matches = 0;
                foreach( var _unused in source.Where( predicate ) )
                {
                    _matches++;
                    if( _matches > limit )
                    {
                        return false;
                    }
                }

                return true;
            }
            catch( Exception ex )
            {
                LinqExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}