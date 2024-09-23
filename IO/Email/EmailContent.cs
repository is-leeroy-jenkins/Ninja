// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="EmailContent.cs" company="Terry D. Eppler">
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
//   EmailContent.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Exception = System.Exception;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
    public class EmailContent
    {
        /// <summary>
        /// The attachments
        /// </summary>
        private protected IList<string> _attachments;

        /// <summary>
        /// The is HTML
        /// </summary>
        private protected bool _isHtml;

        /// <summary>
        /// The message
        /// </summary>
        private protected string _message;

        /// <summary>
        /// The subject
        /// </summary>
        private protected string _subject;

        /// <summary>
        /// Deconstructs the specified is HTML.
        /// </summary>
        /// <param name="subject"> </param>
        /// <param name="message">
        /// The content.
        /// </param>
        /// <param name="attachments"></param>
        public void Deconstruct( out string subject, out string message,
            out IList<string> attachments )
        {
            subject = _subject;
            message = _message;
            attachments = _attachments;
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
                return !string.IsNullOrEmpty( _message )
                    ? _message
                    : string.Empty;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Adds the attachment.
        /// </summary>
        /// <param name="filePath">The attachment.</param>
        public void AddAttachment( string filePath )
        {
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                _attachments.Add( filePath );
            }
            catch( Exception ex )
            {
                Fail( ex );
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

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.EmailContent" />
        /// class.
        /// </summary>
        public EmailContent( )
        {
            _attachments = new List<string>( );
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public EmailContent( string subject, string message )
            : this( )
        {
            _message = message;
            _subject = subject;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ninja.EmailContent" /> class.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <param name="attachments">The attachments.</param>
        public EmailContent( string subject, string message, IList<string> attachments )
            : this( subject, message )
        {
            _message = message;
            _subject = subject;
            _attachments = attachments;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.EmailContent" /> class.
        /// </summary>
        /// <param name="content">The email.</param>
        public EmailContent( EmailContent content )
        {
            _message = content.Message;
            _subject = content.Subject;
            _attachments = content.Attachments;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is HTML.
        /// </summary>
        /// <value>
        /// <c> true </c>
        /// if this instance is HTML; otherwise,
        /// <c> false </c>
        /// .
        /// </value>
        public bool IsHtml
        {
            get
            {
                return _isHtml;
            }
            private protected set
            {
                _isHtml = value;
            }
        }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject
        {
            get
            {
                return _subject;
            }
            private protected set
            {
                _subject = value;
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get
            {
                return _message;
            }
            private protected set
            {
                _message = value;
            }
        }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public IList<string> Attachments
        {
            get
            {
                return _attachments;
            }
            set
            {
                _attachments = value;
            }
        }
    }
}