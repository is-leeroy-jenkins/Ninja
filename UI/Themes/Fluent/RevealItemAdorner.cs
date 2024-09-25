// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="RevealItemAdorner.cs" company="Terry D. Eppler">
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
//   RevealItemAdorner.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Documents.Adorner" />
    [ EditorBrowsable( EditorBrowsableState.Never ) ]
    [ Browsable( false ) ]
    [ SuppressMessage( "ReSharper", "ConvertIfStatementToConditionalTernaryExpression" ) ]
    public class RevealItemAdorner : Adorner
    {
        /// <summary>
        /// Gets or sets the reveal item.
        /// </summary>
        /// <value>
        /// The reveal item.
        /// </value>
        public RevealItem RevealItem
        {
            get;
            set;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Badger.RevealItemAdorner" /> class.
        /// </summary>
        /// <param name="adornedElement">The adorned element.</param>
        /// <param name="revealElement">The reveal element.</param>
        public RevealItemAdorner( UIElement adornedElement, RevealItem revealElement )
            : base( adornedElement )
        {
            RevealItem = revealElement;
            IsHitTestVisible = false;
            adornedElement.PreviewMouseMove += AdornedElement_PreviewMouseMove;
            adornedElement.PreviewMouseDown += AdornedElement_PreviewMouseDown;
            adornedElement.MouseLeave += AdornedElement_MouseLeave;
        }

        /// <summary>
        /// Handles the MouseLeave event of the AdornedElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void AdornedElement_MouseLeave( object sender, MouseEventArgs e )
        {
            RevealItem.IsMouseOver = false;
        }

        /// <summary>
        /// Handles the PreviewMouseDown event of the AdornedElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/>
        /// instance containing the event data.</param>
        private void AdornedElement_PreviewMouseDown( object sender, MouseEventArgs e )
        {
            RevealItem.Position = e.GetPosition( sender as IInputElement );
            RevealItem.StartPressedRevealAnimation( );
        }

        /// <summary>
        /// Handles the PreviewMouseMove event of the AdornedElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void AdornedElement_PreviewMouseMove( object sender, MouseEventArgs e )
        {
            if( e.LeftButton != MouseButtonState.Pressed )
            {
                RevealItem.IsPressed = false;
            }
            else
            {
                RevealItem.IsPressed = true;
            }

            RevealItem.IsMouseOver = true;
            RevealItem.Position = e.GetPosition( sender as IInputElement );
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, positions child elements and
        /// determines a size for a <see cref="T:System.Windows.FrameworkElement" /> derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this
        /// element should use to arrange itself and its children.</param>
        /// <returns>
        /// The actual size used.
        /// </returns>
        protected override Size ArrangeOverride( Size finalSize )
        {
            double x = 0;
            double y = 0;
            RevealItem.Arrange( new Rect( x, y, AdornedElement.RenderSize.Width,
                AdornedElement.RenderSize.Height ) );

            RevealItem.UpdateRevealBorderSize( AdornedElement.RenderSize.Width );
            return finalSize;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose( )
        {
            AdornedElement.PreviewMouseMove -= AdornedElement_PreviewMouseMove;
            AdornedElement.PreviewMouseDown -= AdornedElement_PreviewMouseDown;
            AdornedElement.MouseLeave -= AdornedElement_MouseLeave;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the number of visual child elements within this element.
        /// </summary>
        protected override int VisualChildrenCount { get { return 1; } }

        /// <inheritdoc />
        /// <summary>
        /// Overrides <see cref="M:System.Windows.Media.Visual.GetVisualChild(System.Int32)" />,
        /// and returns a child at the specified index from a collection of child elements.
        /// </summary>
        /// <param name="index">The zero-based index of the requested child element in the collection.</param>
        /// <returns>
        /// The requested child element. This should not return <see langword="null" />;
        /// if the provided index is out of range, an exception is thrown.
        /// </returns>
        protected override Visual GetVisualChild( int index ) { return RevealItem; }
    }
}