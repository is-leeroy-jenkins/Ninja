// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="CollectionExtensions.cs" company="Terry D. Eppler">
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
//   CollectionExtensions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ParameterTypeCanBeEnumerable.Global" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeRedundantParentheses" ) ]
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool AddIf<T>( this ICollection<T> collection, Func<T, bool> predicate,
            T value )
        {
            if( predicate( value ) )
            {
                try
                {
                    collection.Add( value );
                    return true;
                }
                catch( Exception ex )
                {
                    CollectionExtensions.Fail( ex );
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        public static void AddRange<T>( this ICollection<T> collection, params T[ ] values )
        {
            if( ( values?.Any( ) == true ) )
            {
                try
                {
                    for( var _i = 0; _i < values.Length; _i++ )
                    {
                        var _value = values[ _i ];
                        collection.Add( _value );
                    }
                }
                catch( Exception ex )
                {
                    CollectionExtensions.Fail( ex );
                }
            }
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>
        /// <c> true </c>
        /// if the specified collection is empty; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool IsEmpty<T>( this ICollection<T> collection )
        {
            try
            {
                return !( collection?.Count > 0 );
            }
            catch( Exception ex )
            {
                CollectionExtensions.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// Removes if contains.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="value">The value.</param>
        public static void RemoveIfContains<T>( this ICollection<T> collection, T value )
        {
            if( collection?.Contains( value ) == true )
            {
                try
                {
                    collection.Remove( value );
                }
                catch( Exception ex )
                {
                    CollectionExtensions.Fail( ex );
                }
            }
        }

        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="values">The values.</param>
        public static void RemoveRange<T>( this ICollection<T> collection, params T[ ] values )
        {
            if( ( values?.Any( ) == true ) )
            {
                try
                {
                    foreach( var _item in values )
                    {
                        collection.Remove( _item );
                    }
                }
                catch( Exception ex )
                {
                    CollectionExtensions.Fail( ex );
                }
            }
        }

        /// <summary>
        /// Removes the where.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        public static void RemoveWhere<T>( this ICollection<T> collection, Predicate<T> predicate )
        {
            try
            {
                var _list = collection?.Where( child => predicate( child ) )?.ToList( );
                if( _list?.Any( ) == true )
                {
                    _list.ForEach( t => collection.Remove( t ) );
                }
            }
            catch( Exception ex )
            {
                CollectionExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>
        /// <c> true </c>
        /// if the specified collection is empty; otherwise,
        /// <c> false </c>
        /// .
        /// </returns>
        public static bool IsEmpty( this ICollection collection )
        {
            try
            {
                return !( collection?.Count > 0 );
            }
            catch( Exception ex )
            {
                CollectionExtensions.Fail( ex );
                return true;
            }
        }

        /// <summary>
        /// Converts to bindinglist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static BindingList<T> ToBindingList<T>( this ICollection<T> collection )
        {
            try
            {
                var _list = new BindingList<T>( );
                foreach( var _item in collection )
                {
                    _list.Add( _item );
                }

                return _list?.Any( ) == true
                    ? _list
                    : default( BindingList<T> );
            }
            catch( Exception ex )
            {
                CollectionExtensions.Fail( ex );
                return default( BindingList<T> );
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