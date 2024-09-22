// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="EmailCredential.cs" company="Terry D. Eppler">
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
//   EmailCredential.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "ConvertToAutoProperty" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoPropertyWhenPossible" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    public class EmailCredential
    {
        /// <summary>
        /// The user name
        /// </summary>
        private string _userName;

        /// <summary>
        /// The password
        /// </summary>
        private string _password;

        /// <summary>
        /// The first name
        /// </summary>
        private string _firstName;

        /// <summary>
        /// The last name
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            private protected set
            {
                _firstName = value;
            }
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            private protected set
            {
                _lastName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get
            {
                return _userName;
            }
            private protected set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return _password;
            }
            private protected set
            {
                _password = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EmailCredential"/> class.
        /// </summary>
        public EmailCredential( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EmailCredential"/> class.
        /// </summary>
        /// <param name="firstName"> </param>
        /// <param name="lastName"> </param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public EmailCredential( string firstName, string lastName,
            string userName, string password )
        {
            _firstName = firstName;
            _lastName = lastName;
            _userName = userName;
            _password = password;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EmailCredential"/> class.
        /// </summary>
        /// <param name="credential">The credential.</param>
        public EmailCredential( EmailCredential credential )
        {
            _firstName = credential.FirstName;
            _lastName = credential.LastName;
            _userName = credential.UserName;
            _password = credential.Password;
        }

        /// <summary>
        /// Deconstructs the specified first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public void Deconstruct( out string firstName, out string lastName,
            out string userName, out string password )
        {
            firstName = _firstName;
            lastName = _lastName;
            userName = _userName;
            password = _password;
        }

        /// <summary> Converts to string. </summary>
        /// <returns>
        /// A
        /// <see cref="System.String"/>
        /// that represents this instance.
        /// </returns>
        public override string ToString( )
        {
            try
            {
                return !string.IsNullOrEmpty( _userName )
                    ? _userName
                    : string.Empty;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private protected void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}