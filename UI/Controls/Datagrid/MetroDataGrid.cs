// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="MetroDataGrid.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts
//    based on WPF, NET6.0, and written in C-Sharp.
// 
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
//   MetroDataGrid.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Syncfusion.UI.Xaml.Grid;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Input;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Syncfusion.UI.Xaml.Grid.SfDataGrid" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public class MetroDataGrid : SfDataGrid
    {
        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Badger.DataGrid" /> class.
        /// </summary>
        public MetroDataGrid( )
            : base( )
        {
            // Control Properties
            SetResourceReference( StyleProperty, typeof( SfDataGrid ) );
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            BorderBrush = _theme.BorderBrush;
            CurrentCellBorderBrush = _theme.LightBlueBrush;
            GroupRowSelectionBrush = _theme.SteelBlueBrush;
            RowSelectionBrush = _theme.SteelBlueBrush;
            RowHoverHighlightingBrush = _theme.DarkBlueBrush;
            AllowEditing = false;
            AllowSorting = true;
            AllowFiltering = true;
            FilterRowPosition = FilterRowPosition.Top;
            AllowDraggingColumns = true;
            AllowResizingColumns = true;
            AllowDeleting = true;
            AllowRowHoverHighlighting = true;
            AllowResizingColumns = true;
            AllowDrop = true;
            AllowDraggingRows = true;
            AllowCollectionView = true;
            AutoGenerateColumnsMode = AutoGenerateColumnsMode.SmartReset;
            AutoGenerateColumns = true;
            AutoExpandGroups = true;
            AllowGrouping = true;
            ShowGroupDropArea = true;
            IsGroupDropAreaExpanded = true;
            ShowToolTip = true;
            SelectionUnit = GridSelectionUnit.Row;
            SelectionMode = GridSelectionMode.Single;
            ShowColumnWhenGrouped = false;
        }

        /// <summary>
        /// Called when [column right clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private protected void OnMouseRightClick( object sender, MouseButtonEventArgs e )
        {
            try
            {
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [cell left clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private protected void OnMouseLeftClick( object sender, RoutedEventArgs e )
        {
            try
            {
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [automatic generating column].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private protected void OnAutoGeneratingColumn( object sender, AutoGeneratingColumnArgs e )
        {
            if( e.Column.MappingName.EndsWith( "Id" ) )
            {
                e.Column.AllowEditing = false;
                e.Column.AllowSorting = true;
                e.Column.AllowFiltering = true;
                e.Column.AllowGrouping = false;
                e.Column.AllowFocus = true;
                e.Column.AllowResizing = false;
                e.Column.ColumnSizer = GridLengthUnitType.Auto;
                e.Column.AllowDragging = true;
                e.Column.ColumnMemberType = typeof( int );
            }
        }

        /// <summary>
        /// Fails the specified _ex.
        /// </summary>
        /// <param name="_ex">The _ex.</param>
        private protected void Fail( Exception _ex )
        {
            var _error = new ErrorWindow( _ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}