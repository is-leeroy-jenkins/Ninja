// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="GoogleSearch.cs" company="Terry D. Eppler">
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
//   GoogleSearch.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Google.Apis.CustomSearchAPI.v1;
    using Google.Apis.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "PossibleNullReferenceException" ) ]
    [ SuppressMessage( "ReSharper", "UseObjectOrCollectionInitializer" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantBaseConstructorCall" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public class GoogleSearch : WebSearch
    {
        /// <summary>
        /// The engine
        /// </summary>
        private readonly string _engine;

        /// <summary>
        /// The key
        /// </summary>
        private readonly string _key;

        /// <summary>
        /// The configuration
        /// </summary>
        private protected NameValueCollection _config;

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <returns>
        /// List(SearchResult)
        /// </returns>
        public IList<SearchResult> GetResults( )
        {
            try
            {
                var _count = 0;
                var _results = new List<SearchResult>( );
                var _initializer = new BaseClientService.Initializer( );
                _initializer.ApiKey = _key;
                var _customSearch = new CustomSearchAPIService( _initializer );
                var _searchRequest = _customSearch?.Cse?.List( );
                if( _searchRequest != null )
                {
                    _searchRequest.Q = _query;
                    _searchRequest.Cx = _engine;
                    _searchRequest.Start = _count;
                    var _list = _searchRequest.Execute( )?.Items?.ToList( );
                    if( _list?.Any( ) == true )
                    {
                        for( var _i = 0; _i < _list.Count; _i++ )
                        {
                            var _snippet = _list[ _i ].Snippet ?? string.Empty;
                            var _lines = _list[ _i ].Link ?? string.Empty;
                            var _titles = _list[ _i ].Title ?? string.Empty;
                            var _htmlTitle = _list[ _i ].HtmlTitle ?? string.Empty;
                            var _searchResults = new SearchResult( _snippet, _lines,
                                _titles, _htmlTitle );

                            _results.Add( _searchResults );
                        }

                        return _results?.Any( ) == true
                            ? _results
                            : default( IList<SearchResult> );
                    }
                    else
                    {
                        return default( IList<SearchResult> );
                    }
                }
                else
                {
                    return default( IList<SearchResult> );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IList<SearchResult> );
            }
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <returns>
        /// Task(IList(SearchResult))
        /// </returns>
        public Task<IList<SearchResult>> GetResultsAsync( )
        {
            var _async = new TaskCompletionSource<IList<SearchResult>>( );
            try
            {
                var _count = 0;
                var _results = new List<SearchResult>( );
                var _initializer = new BaseClientService.Initializer( );
                _initializer.ApiKey = _key;
                var _customSearch = new CustomSearchAPIService( _initializer );
                var _searchRequest = _customSearch?.Cse?.List( );
                if( _searchRequest != null )
                {
                    _searchRequest.Q = _query;
                    _searchRequest.Cx = _engine;
                    _searchRequest.Start = _count;
                    var _list = _searchRequest.Execute( )?.Items?.ToList( );
                    if( _list?.Any( ) == true )
                    {
                        for( var _i = 0; _i < _list.Count; _i++ )
                        {
                            var _snippet = _list[ _i ].Snippet ?? string.Empty;
                            var _line = _list[ _i ].Link ?? string.Empty;
                            var _titles = _list[ _i ].Title ?? string.Empty;
                            var _htmlTitle = _list[ _i ].HtmlTitle ?? string.Empty;
                            var _searchResults =
                                new SearchResult( _snippet, _line, _titles, _htmlTitle );

                            _results.Add( _searchResults );
                        }

                        _async.SetResult( _results );
                        return _results?.Any( ) == true
                            ? _async.Task
                            : default( Task<IList<SearchResult>> );
                    }
                    else
                    {
                        return default( Task<IList<SearchResult>> );
                    }
                }
                else
                {
                    return default( Task<IList<SearchResult>> );
                }
            }
            catch( Exception ex )
            {
                _async.SetException( ex );
                Fail( ex );
                return default( Task<IList<SearchResult>> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.GoogleSearch" /> class.
        /// </summary>
        public GoogleSearch( )
            : base( )
        {
            _key = ConfigurationManager.AppSettings[ "ApiKey" ];
            _engine = ConfigurationManager.AppSettings[ "SearchEngineId" ];
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.GoogleSearch" /> class.
        /// </summary>
        /// <param name="keywords">
        /// The keywords.
        /// </param>
        public GoogleSearch( string keywords )
            : this( )
        {
            _query = keywords;
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public string Query
        {
            get
            {
                return _query;
            }
            private set
            {
                _query = value;
            }
        }
    }
}