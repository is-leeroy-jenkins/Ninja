// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="OutlookMail.cs" company="Terry D. Eppler">
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
//   OutlookMail.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Microsoft.Office.Interop.Outlook;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.InteropServices;
    using System.Text;
    using Attachment = System.Net.Mail.Attachment;
    using Exception = System.Exception;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
    [ SuppressMessage( "ReSharper", "ArrangeModifiersOrder" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantAssignment" ) ]
    [ SuppressMessage( "ReSharper", "MergeConditionalExpression" ) ]
    [ SuppressMessage( "ReSharper", "ConvertToAutoPropertyWhenPossible" ) ]
    public class OutlookMail : EmailManager
    {
        /// <summary>
        /// The email configuration
        /// </summary>
        private EmailConfig _emailConfig;

        /// <summary>
        /// The email content
        /// </summary>
        private EmailContent _emailContent;

        /// <summary>
        /// The email credential
        /// </summary>
        private EmailCredential _emailCredential;

        /// <summary>
        /// The host name
        /// </summary>
        private string _hostName;

        /// <summary>
        /// Deconstructs the specified email credentials.
        /// </summary>
        /// <param name="emailCredentials">The email credentials.</param>
        /// <param name="emailConfig">The email configuration.</param>
        /// <param name="emailContent">Content of the email.</param>
        /// <param name="hostName">Name of the host.</param>
        public void Deconstruct( out EmailCredential emailCredentials, out EmailConfig emailConfig,
            out EmailContent emailContent, out string hostName )
        {
            hostName = _hostName;
            emailCredentials = _emailCredential;
            emailConfig = _emailConfig;
            emailContent = _emailContent;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="message">The message.</param>
        private protected void Send( MailMessage message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _client = new SmtpClient( );
                var _userName = _emailCredential.UserName;
                var _passWord = _emailCredential.Password;
                _client.UseDefaultCredentials = false;
                _client.Credentials = new NetworkCredential( _userName, _passWord );
                _client.Host = HostName;
                _client.Port = 25;
                _client.EnableSsl = true;
                _client.Send( message );
            }
            catch( Exception ex )
            {
                message?.Dispose( );
                Fail( ex );
            }
        }

        /// <summary>
        /// Reads the email inbox items.
        /// </summary>
        public void ReadInboxItems( )
        {
            Application _outlook = null;
            NameSpace _namespace = null;
            MAPIFolder _inbox = null;
            Items _items = null;
            try
            {
                _outlook = new Application( );
                _namespace = _outlook.GetNamespace( "MAPI" );
                _inbox = _namespace.GetDefaultFolder( OlDefaultFolders.olFolderInbox );
                _items = _inbox.Items;
                foreach( MailItem _item in _items )
                {
                    var _builder = new StringBuilder( );
                    _builder.AppendLine( "From: " + _item.SenderEmailAddress );
                    _builder.AppendLine( "To: " + _item.To );
                    _builder.AppendLine( "CC: " + _item.CC );
                    _builder.AppendLine( "" );
                    _builder.AppendLine( "Subject: " + _item.Subject );
                    _builder.AppendLine( _item.Body );
                    Marshal.ReleaseComObject( _item );
                }
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
            finally
            {
                ReleaseComObject( _items );
                ReleaseComObject( _inbox );
                ReleaseComObject( _namespace );
                ReleaseComObject( _outlook );
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        public void SendEmail( )
        {
            try
            {
                var _message = CreateMessage( );
                Send( _message );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <returns></returns>
        public MailMessage CreateMessage( )
        {
            try
            {
                var _message = new MailMessage( );
                for( var _i = 0; _i < _emailConfig.Recipients.Count; _i++ )
                {
                    var _to = _emailConfig.Recipients[ _i ];
                    if( !string.IsNullOrEmpty( _to ) )
                    {
                        _message.To.Add( _to );
                    }
                }

                for( var _k = 0; _k < _emailConfig.Copies.Count; _k++ )
                {
                    var _cc = _emailConfig.Copies[ _k ];
                    if( !string.IsNullOrEmpty( _cc ) )
                    {
                        _message.CC.Add( _cc );
                    }
                }

                _message.From = new MailAddress( _emailConfig.Sender, _emailConfig.DisplayName,
                    Encoding.UTF8 );

                _message.IsBodyHtml = _emailContent.IsHtml;
                _message.Body = _emailContent.Message;
                _message.Priority = _emailConfig.Priority;
                _message.Subject = _emailContent.Subject;
                _message.BodyEncoding = Encoding.UTF8;
                _message.SubjectEncoding = Encoding.UTF8;
                if( _emailContent.Attachments != null )
                {
                    var _attachment = new Attachment( _emailContent.Attachments[ 0 ] );
                    _message.Attachments.Add( _attachment );
                }

                return _message != null
                    ? _message
                    : default( MailMessage );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( MailMessage );
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
                _emailConfig.Recipients.Add( address );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Adds the copy.
        /// </summary>
        /// <param name="address">The address.</param>
        public void AddCopy( string address )
        {
            try
            {
                ThrowIf.Null( address, nameof( address ) );
                _emailConfig.Copies.Add( address );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Adds the attachment.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void AddAttachment( string filePath )
        {
            try
            {
                ThrowIf.Null( filePath, nameof( filePath ) );
                var _attachment = new Attachment( filePath );
                var _message = new MailMessage( );
                _message.Attachments.Add( _attachment );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
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
                return !string.IsNullOrEmpty( _emailContent?.Message )
                    ? _emailContent.Message
                    : string.Empty;
            }
            catch( Exception ex )
            {
                Fail( ex );
                return string.Empty;
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="OutlookMail"/> class.
        /// </summary>
        public OutlookMail( )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailCredentials"></param>
        /// <param name="emailConfig"></param>
        /// <param name="emailContent"></param>
        public OutlookMail( EmailCredential emailCredentials, EmailConfig emailConfig,
            EmailContent emailContent )
        {
            _hostName = emailConfig.HostName;
            _emailCredential = emailCredentials;
            _emailConfig = emailConfig;
            _emailContent = emailContent;
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="OutlookMail"/> class.
        /// </summary>
        /// <param name="outlook">The outlook.</param>
        public OutlookMail( OutlookMail outlook )
        {
            _emailCredential = outlook.Credentials;
            _emailConfig = outlook.Configuration;
            _emailContent = outlook.Content;
            _hostName = outlook.HostName;
        }

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
        /// Gets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public EmailCredential Credentials
        {
            get
            {
                return _emailCredential;
            }
            private set
            {
                _emailCredential = value;
            }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public EmailConfig Configuration
        {
            get
            {
                return _emailConfig;
            }
            private set
            {
                _emailConfig = value;
            }
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public EmailContent Content
        {
            get
            {
                return _emailContent;
            }
            private set
            {
                _emailContent = value;
            }
        }
    }
}