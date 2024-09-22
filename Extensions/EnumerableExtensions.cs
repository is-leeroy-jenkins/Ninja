// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="EnumerableExtensions.cs" company="Terry D. Eppler">
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
//   EnumerableExtensions.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using OfficeOpenXml;
    using OfficeOpenXml.Table;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
    [ SuppressMessage( "ReSharper", "LoopCanBePartlyConvertedToQuery" ) ]
    [ SuppressMessage( "ReSharper", "UseObjectOrCollectionInitializer" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "CompareNonConstrainedGenericWithNull" ) ]
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts to bindinglist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static BindingList<T> ToBindingList<T>( this IEnumerable<T> enumerable )
        {
            try
            {
                var _bindingList = new BindingList<T>( );
                foreach( var _item in enumerable )
                {
                    if( _item != null )
                    {
                        _bindingList.Add( _item );
                    }
                }

                return _bindingList?.Any( ) == true
                    ? _bindingList
                    : default( BindingList<T> );
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( BindingList<T> );
            }
        }

        /// <summary>
        /// Converts to observable.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        /// ObservableCollection
        /// </returns>
        public static ObservableCollection<DataRow> ToObservable(
            this IEnumerable<DataRow> enumerable )
        {
            try
            {
                if( enumerable.Any( ) )
                {
                    var _rows = new ObservableCollection<DataRow>( );
                    foreach( var _row in enumerable )
                    {
                        _rows.Add( _row );
                    }

                    return _rows?.Count > 0
                        ? _rows
                        : default( ObservableCollection<DataRow> );
                }

                return default( ObservableCollection<DataRow> );
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( ObservableCollection<DataRow> );
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a given predicate
        /// and returns those values that don't match
        /// the predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of
        /// <paramref name="source" />
        /// .</typeparam>
        /// <param name="source">An
        /// <see cref="IEnumerable{T}" />
        /// to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        /// Those values that don't match the given predicate.
        /// </returns>
        public static IEnumerable<T> WhereNot<T>( this IEnumerable<T> source,
            Func<T, bool> predicate )
        {
            try
            {
                return source.Where( element => !predicate( element ) );
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( IEnumerable<T> );
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate and returns those values that don't match the
        /// given predicate. Each element's index is used in the logic of predicate function.
        /// </summary>
        /// <typeparam name="T">The type of the elements of
        /// <paramref name="source" />
        /// .</typeparam>
        /// <param name="source">An
        /// <see cref="IEnumerable{T}" />
        /// to filter.</param>
        /// <param name="predicate">A function to test each element for a condition; the second parameter of the functions represents
        /// the index of the source element.</param>
        /// <returns>
        /// Those values that don't match the given predicate.
        /// </returns>
        public static IEnumerable<T> WhereNot<T>( this IEnumerable<T> source,
            Func<T, int, bool> predicate )
        {
            try
            {
                return source.Where( ( element, index ) => !predicate( element, index ) );
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( IEnumerable<T> );
            }
        }

        /// <summary>
        /// Filters the specified columnName.
        /// </summary>
        /// <param name="dataRow">The dataRow.</param>
        /// <param name="name">The columnName.</param>
        /// <param name="value">The filter.</param>
        /// <returns></returns>
        public static IEnumerable<DataRow> Filter( this IEnumerable<DataRow> dataRow, string name,
            string value )
        {
            try
            {
                ThrowIf.Null( name, nameof( name ) );
                ThrowIf.Null( value, nameof( value ) );
                var _row = dataRow?.First( );
                var _dictionary = _row.ToDictionary( );
                var _array = _dictionary.Keys.ToArray( );
                if( _array?.Contains( name ) == true )
                {
                    var _select = dataRow?.Where( p => p.Field<string>( name ) == value )
                        ?.Select( p => p );

                    return _select?.Any( ) == true
                        ? _select
                        : default( IEnumerable<DataRow> );
                }

                return default( IEnumerable<DataRow> );
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( IEnumerable<DataRow> );
            }
        }

        /// <summary>
        /// Filters the specified dictionary.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <param name="where">The dictionary.</param>
        /// <returns>
        /// IEnumerable{DataRow}
        /// </returns>
        public static IEnumerable<DataRow> Filter( this IEnumerable<DataRow> dataRow,
            IDictionary<string, object> where )
        {
            if( dataRow?.Any( ) == true
                && where?.Any( ) == true )
            {
                try
                {
                    var _table = dataRow.CopyToDataTable( );
                    var _rows = _table?.Select( where.ToCriteria( ) );
                    return _rows?.Any( ) == true
                        ? _rows
                        : default( IEnumerable<DataRow> );
                }
                catch( Exception ex )
                {
                    EnumerableExtensions.Fail( ex );
                    return default( IEnumerable<DataRow> );
                }
            }

            return default( IEnumerable<DataRow> );
        }

        /// <summary>
        /// Converts to excel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The dataRow.</param>
        /// <param name="path">The path.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Verify Path</exception>
        /// <exception cref="Exception">Invalid file path. or Invalid file path. or No dataRow to export.</exception>
        public static ExcelPackage ToExcel<T>( this IEnumerable<T> type, string path,
            TableStyles style = TableStyles.Light1 )
        {
            if( string.IsNullOrEmpty( path )
                && type?.Any( ) == true
                && Enum.IsDefined( typeof( TableStyles ), style ) )
            {
                throw new ArgumentException( "Verify Path" );
            }

            try
            {
                var _excel = new ExcelPackage( new FileInfo( path ) );
                var _workbook = _excel.Workbook;
                var _worksheet = _workbook.Worksheets[ 0 ];
                var _range = _worksheet.Cells;
                _range?.LoadFromCollection( type, true, style );
                return _excel;
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( ExcelPackage );
            }
        }

        /// <summary>
        /// Extracts a contiguous count of elements from a sequence at a particular zero-based starting index.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="sequence">The sequence from which to extract elements.</param>
        /// <param name="startIndex">The zero-based index at which to begin slicing.</param>
        /// <param name="count">The number of items to slice out of the index.</param>
        /// <returns>
        /// A new sequence containing any elements sliced out from the source sequence.
        /// </returns>
        /// <remarks>
        /// <para>
        /// If the starting position or count specified result in slice extending past the end of the sequence,
        /// it will return all elements up to that point. There is no guarantee that the resulting sequence
        /// will contain the number of elements requested - it may have anywhere from 0 to
        /// <paramref name="count" />
        /// .
        /// </para>
        /// <para>
        /// This method is implemented in an optimized manner for any sequence implementing
        /// <see cref="IList{T}" />
        /// .
        /// </para>
        /// <para>
        /// The result of
        /// <see cref="Slice{T}" />
        /// is identical to:
        /// <c> sequence.Skip(startIndex).Take(count) </c></para>
        /// </remarks>
        public static IEnumerable<T> Slice<T>( this IEnumerable<T> sequence, int startIndex,
            int count )
        {
            ThrowIf.NegativeOrZero( startIndex, nameof( startIndex ) );
            ThrowIf.NegativeOrZero( count, nameof( count ) );
            return sequence switch
            {
                IList<T> _list => SliceList( _list.Count, i => _list[ i ] ),
                IReadOnlyList<T> _list => SliceList( _list.Count, i => _list[ i ] ),
                var _seq => _seq.Skip( startIndex ).Take( count )
            };

            IEnumerable<T> SliceList( int listCount, Func<int, T> indexer )
            {
                var _countdown = count;
                var _index = startIndex;
                while( _index < listCount
                    && _countdown-- > 0 )
                {
                    yield return indexer( _index++ );
                }
            }
        }

        /// <summary>
        /// Slices the specified start.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The dataRow.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public static IEnumerable<T> LazySlice<T>( this IEnumerable<T> type, int start, int end )
        {
            ThrowIf.NegativeOrZero( start, nameof( start ) );
            ThrowIf.NegativeOrZero( end, nameof( end ) );
            var _index = 0;
            foreach( var _item in type )
            {
                if( _index >= end )
                {
                    yield break;
                }

                if( _index >= start )
                {
                    yield return _item;
                }

                ++_index;
            }
        }

        /// <summary>
        /// Turns a finite sequence into a circular one,
        /// or equivalently, repeats the original sequence
        /// indefinitely.
        /// </summary>
        /// <typeparam name="T">The type of the elements of
        /// <paramref name="source" />
        /// .</typeparam>
        /// <param name="source">A
        /// <see cref="IEnumerable{T}" />
        /// to cycle through.</param>
        /// <returns>
        /// An infinite sequence cycling through the given sequence.
        /// </returns>
        public static IEnumerable<T> Cycle<T>( this IEnumerable<T> source )
        {
            try
            {
                return EnumerableExtensions.CycleIterator( source );
            }
            catch( Exception ex )
            {
                EnumerableExtensions.Fail( ex );
                return default( IEnumerable<T> );
            }
        }

        /// <summary>
        /// Cycles the iterator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        /// IEnumerable{T}
        /// </returns>
        private static IEnumerable<T> CycleIterator<T>( IEnumerable<T> source )
        {
            var _elementBuffer = source is not ICollection<T> _collection
                ? new List<T>( )
                : new List<T>( _collection.Count );

            foreach( var _element in source )
            {
                yield return _element;
                _elementBuffer.Add( _element );
            }

            if( _elementBuffer.IsEmpty<T>( ) )
            {
                yield break;
            }

            var _index = 0;
            while( true )
            {
                yield return _elementBuffer[ _index ];
                _index = ( _index + 1 ) % _elementBuffer.Count;
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