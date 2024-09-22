// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="EmailManager.cs" company="Terry D. Eppler">
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
//   EmailManager.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using Office = Microsoft.Office.Interop.Outlook;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "LoopCanBePartlyConvertedToQuery" ) ]
    [ SuppressMessage( "ReSharper", "RedundantAssignment" ) ]
    public abstract class EmailManager
    {
        /// <summary>
        /// Enumerates the folders.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="search">The search.</param>
        public void EnumerateFolders( Office.Folder folder, string search )
        {
            try
            {
                var _folders = folder.Folders;
                if( _folders.Count > 0 )
                {
                    foreach( Office.Folder _child in _folders )
                    {
                        if( _child.FolderPath.Contains( "Inbox" )
                            || _child.FolderPath.Contains( "Deleted" ) )
                        {
                            EnumerateFolders( _child, search );
                        }
                    }
                }

                SearchMessages( folder, search );
            }
            catch( Exception ex )
            {
                Fail( ex );
            }
        }

        /// <summary>
        /// Iterates through all items ("messages") in a folder
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="search">The search.</param>
        public void SearchMessages( Office.Folder folder, string search )
        {
            var _folderItems = folder.Items;
            if( _folderItems != null )
            {
                try
                {
                    foreach( var item in _folderItems )
                    {
                        var mailitem = (Office.MailItem)item;
                        var body = mailitem.Body.ToLower( );
                        if( body.Contains( search.ToLower( ) ) )
                        {
                        }
                        else
                        {
                        }
                    }
                }
                catch( Exception ex )
                {
                    Fail( ex );
                }
            }
        }

        /// <summary>
        /// Releases the COM object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private protected void ReleaseComObject( object obj )
        {
            try
            {
                if( obj != null )
                {
                    Marshal.ReleaseComObject( obj );
                    obj = null;
                }
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
    }
}