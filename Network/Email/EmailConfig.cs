// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="EmailConfig.cs" company="Terry D. Eppler">
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
//   EmailConfig.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoProperty" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public class EmailConfig : EmailBase
    {
        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string HostName
        {
            get
            {
                return _hostName;
            }
            private protected set
            {
                _hostName = value;
            }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            private protected set
            {
                _displayName = value;
            }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public MailPriority Priority
        {
            get
            {
                return _priority;
            }
            private protected set
            {
                _priority = value;
            }
        }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender
        {
            get
            {
                return _sender;
            }
            private protected set
            {
                _sender = value;
            }
        }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        /// <value>
        /// The recipient.
        /// </value>
        public IList<string> Recipients
        {
            get
            {
                return _recipients;
            }
            private protected set
            {
                _recipients = value;
            }
        }

        /// <summary>
        /// Gets or sets the carbon copy.
        /// </summary>
        /// <value>
        /// The carbon copy.
        /// </value>
        public IList<string> Copies
        {
            get
            {
                return _copies;
            }
            private protected set
            {
                _copies = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="EmailConfig" /> class.
        /// </summary>
        /// <inheritdoc />
        public EmailConfig( )
        {
            _recipients = new List<string>( );
            _copies = new List<string>( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.EmailConfig" /> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="copies">The copies.</param>
        /// <param name="priority">The priority.</param>
        public EmailConfig( string sender, IList<string> recipients, IList<string> copies,
            MailPriority priority = MailPriority.Normal )
            : this( )
        {
            _sender = sender;
            _displayName = sender;
            _recipients = recipients;
            _copies = copies;
            _priority = priority;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Ninja.EmailConfig" /> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public EmailConfig( EmailConfig config )
        {
            _sender = config.Sender;
            _displayName = config.DisplayName;
            _priority = config.Priority;
            _recipients = config.Recipients;
            _copies = config.Copies;
        }

        /// <summary>
        /// Deconstructs the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="displayName"> </param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="copies">The copies.</param>
        /// <param name="priority"> </param>
        public void Deconstruct( out string sender, out string displayName,
            out IList<string> recipients, out IList<string> copies, out MailPriority priority )
        {
            sender = _sender;
            displayName = _displayName;
            recipients = _recipients;
            copies = _copies;
            priority = _priority;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" />
        /// that represents this instance.
        /// </returns>
        public override string ToString( )
        {
            try
            {
                return !string.IsNullOrEmpty( _displayName )
                    ? _displayName
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
        /// <param name="address">The attachment.</param>
        public void AddCopy( string address )
        {
            try
            {
                ThrowIf.Null( address, nameof( address ) );
                if( !_copies.Contains( address ) )
                {
                    _copies?.Add( address );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Adds the recipient.
        /// </summary>
        /// <param name="address">The address.</param>
        public void AddRecipient( string address )
        {
            try
            {
                ThrowIf.Null( address, nameof( address ) );
                if( !_recipients.Contains( address ) )
                {
                    _recipients?.Add( address );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }
    }
}