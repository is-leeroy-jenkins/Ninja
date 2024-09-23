// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="EmailValidator.cs" company="Terry D. Eppler">
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
//   EmailValidator.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ConvertIfStatementToSwitchStatement" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "MergeIntoPattern" ) ]
    public class EmailValidator : Validation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <param name="allowTopLevelDomains"></param>
        /// <param name="allowInternational"></param>
        /// <returns></returns>
        private static bool SkipDomain( string text, ref int index, bool allowTopLevelDomains,
            bool allowInternational )
        {
            try
            {
                ThrowIf.Null( text, nameof( text ) );
                ThrowIf.NegativeOrZero( index, nameof( index ) );
                if( !Validation.SkipSubDomain( text, ref index, allowInternational,
                    out var _type ) )
                {
                    return false;
                }

                if( index < text.Length
                    && text[ index ] == '.' )
                {
                    do
                    {
                        index++;
                        if( index == text.Length )
                        {
                            return false;
                        }

                        if( !Validation.SkipSubDomain( text, ref index, allowInternational,
                            out _type ) )
                        {
                            return false;
                        }
                    }
                    while( index < text.Length
                        && text[ index ] == '.' );
                }
                else if( !allowTopLevelDomains )
                {
                    return false;
                }

                return _type != SubDomainType.Numeric;
            }
            catch( Exception ex )
            {
                Validation.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <param name="allowInternational"></param>
        /// <returns></returns>
        private static bool SkipQuoted( string text, ref int index, bool allowInternational )
        {
            try
            {
                ThrowIf.Null( text, nameof( text ) );
                ThrowIf.NegativeOrZero( index, nameof( index ) );
                var _escaped = false;
                index++;
                while( index < text.Length )
                {
                    if( Validation.IsControl( text[ index ] )
                        || ( text[ index ] >= 128 && !allowInternational ) )
                    {
                        return false;
                    }

                    if( text[ index ] == '\\' )
                    {
                        _escaped = !_escaped;
                    }
                    else if( !_escaped )
                    {
                        if( text[ index ] == '"' )
                        {
                            break;
                        }
                    }
                    else
                    {
                        _escaped = false;
                    }

                    index++;
                }

                if( index >= text.Length
                    || text[ index ] != '"' )
                {
                    return false;
                }

                index++;
                return true;
            }
            catch( Exception ex )
            {
                Validation.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static bool SkipIPv4Literal( string text, ref int index )
        {
            try
            {
                ThrowIf.Null( text, nameof( text ) );
                ThrowIf.NegativeOrZero( index, nameof( index ) );
                var _groups = 0;
                while( index < text.Length
                    && _groups < 4 )
                {
                    var _startIndex = index;
                    var _value = 0;
                    while( index < text.Length
                        && Validation.IsDigit( text[ index ] ) )
                    {
                        _value = _value * 10 + ( text[ index ] - '0' );
                        index++;
                    }

                    if( index == _startIndex
                        || index - _startIndex > 3
                        || _value > 255 )
                    {
                        return false;
                    }

                    _groups++;
                    if( _groups < 4
                        && index < text.Length
                        && text[ index ] == '.' )
                    {
                        index++;
                    }
                }

                return _groups == 4;
            }
            catch( Exception ex )
            {
                Validation.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsHexDigit( char c )
        {
            try
            {
                var _test = c.ToString( );
                ThrowIf.Null( _test, nameof( c ) );
                return ( c >= 'A' && c <= 'F' ) || ( c >= 'a' && c <= 'f' )
                    || ( c >= '0' && c <= '9' );
            }
            catch( Exception ex )
            {
                Validation.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static bool SkipIPv6Literal( string text, ref int index )
        {
            try
            {
                ThrowIf.Null( text, nameof( text ) );
                ThrowIf.NegativeOrZero( index, nameof( index ) );
                var _needGroup = false;
                var _compact = false;
                var _groups = 0;
                while( index < text.Length )
                {
                    var _startIndex = index;
                    while( index < text.Length
                        && EmailValidator.IsHexDigit( text[ index ] ) )
                    {
                        index++;
                    }

                    if( index >= text.Length )
                    {
                        break;
                    }

                    if( index > _startIndex
                        && text[ index ] == '.'
                        && ( _compact || _groups == 6 ) )
                    {
                        index = _startIndex;
                        if( !EmailValidator.SkipIPv4Literal( text, ref index ) )
                        {
                            return false;
                        }

                        return _compact
                            ? _groups <= 4
                            : _groups == 6;
                    }

                    var _count = index - _startIndex;
                    if( _count > 4 )
                    {
                        return false;
                    }

                    bool _comp;
                    if( _count > 0 )
                    {
                        _needGroup = false;
                        _comp = false;
                        _groups++;
                        if( text[ index ] != ':' )
                        {
                            break;
                        }
                    }
                    else if( text[ index ] == ':' )
                    {
                        _comp = true;
                    }
                    else
                    {
                        break;
                    }

                    _startIndex = index;
                    while( index < text.Length
                        && text[ index ] == ':' )
                    {
                        index++;
                    }

                    _count = index - _startIndex;
                    if( _count > 2 )
                    {
                        return false;
                    }

                    if( _count == 2 )
                    {
                        if( _compact )
                        {
                            return false;
                        }

                        _compact = true;
                    }
                    else if( _comp )
                    {
                        return false;
                    }
                    else
                    {
                        _needGroup = true;
                    }
                }

                return !_needGroup && ( _compact
                    ? _groups <= 6
                    : _groups == 8 );
            }
            catch( Exception ex )
            {
                Validation.Fail( ex );
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="allowTopLevelDomains"></param>
        /// <param name="allowInternational"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool Validate( string email, bool allowTopLevelDomains = false,
            bool allowInternational = false )
        {
            try
            {
                ThrowIf.Null( email, nameof( email ) );
                var _index = 0;
                if( email == null )
                {
                    throw new ArgumentNullException( nameof( email ) );
                }

                if( email.Length == 0
                    || Validation.Measure( email, 0, email.Length, allowInternational )
                    > MaxEmailAddressLength )
                {
                    return false;
                }

                if( email[ _index ] == '"' )
                {
                    if( !EmailValidator.SkipQuoted( email, ref _index, allowInternational )
                        || _index >= email.Length )
                    {
                        return false;
                    }
                }
                else
                {
                    if( !Validation.SkipAtom( email, ref _index, allowInternational )
                        || _index >= email.Length )
                    {
                        return false;
                    }

                    while( email[ _index ] == '.' )
                    {
                        _index++;
                        if( _index >= email.Length )
                        {
                            return false;
                        }

                        if( !Validation.SkipAtom( email, ref _index, allowInternational ) )
                        {
                            return false;
                        }

                        if( _index >= email.Length )
                        {
                            return false;
                        }
                    }
                }

                var _localPartLength = Validation.Measure( email, 0, _index, allowInternational );
                if( _index + 1 >= email.Length
                    || _localPartLength > MaxLocalPartLength
                    || email[ _index++ ] != '@' )
                {
                    return false;
                }

                if( email[ _index ] != '[' )
                {
                    if( !EmailValidator.SkipDomain( email, ref _index, allowTopLevelDomains,
                        allowInternational ) )
                    {
                        return false;
                    }

                    return _index == email.Length;
                }

                _index++;
                if( _index + 7 >= email.Length )
                {
                    return false;
                }

                if( string.Compare( email, _index, "IPv6:", 0,
                    5, StringComparison.OrdinalIgnoreCase ) == 0 )
                {
                    _index += "IPv6:".Length;
                    if( !EmailValidator.SkipIPv6Literal( email, ref _index ) )
                    {
                        return false;
                    }
                }
                else
                {
                    if( !EmailValidator.SkipIPv4Literal( email, ref _index ) )
                    {
                        return false;
                    }
                }

                if( _index >= email.Length
                    || email[ _index++ ] != ']' )
                {
                    return false;
                }

                return _index == email.Length;
            }
            catch( Exception ex )
            {
                Validation.Fail( ex );
                return false;
            }
        }
    }
}