// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 09-08-2020
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-08-2024
// ******************************************************************************************
// <copyright file="FluentHelper.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts
//    that is based on WPF, NET6.0, and written in C-Sharp.
// 
//     Copyright ©  2020, 2022, 2204 Terry D. Eppler
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
//   FluentHelper.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Syncfusion.SfSkinManager;
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Input;

    /// <summary>
    /// Helper class to achieve fluent theme hover and pressed animation 
    /// </summary>
    /// <example>
    /// <code language="XAML">
    /// <![CDATA[
    ///    <Border Width = "300" Height="200" HorizontalAlignment="Center" VerticalAlignment="Center">
    ///        <TextBlock Text = "Testing" HorizontalAlignment="Center" VerticalAlignment="Center"/>
    ///    </Border>
    /// ]]>
    /// </code>
    /// </example>
    /// <exclude/>
    [ EditorBrowsable( EditorBrowsableState.Never ) ]
    [ Browsable( false ) ]
    [ SuppressMessage( "ReSharper", "PossibleNullReferenceException" ) ]
    [ SuppressMessage( "ReSharper", "UsePatternMatching" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class FluentHelper
    {
        /// <summary>
        /// Initializes the <see cref="FluentHelper"/> class.
        /// </summary>
        static FluentHelper( )
        {
        }

        /// <summary>
        /// Gets the <see cref="FluentHelper.HoverEffectModeProperty"/>
        /// attached property value that denotes the hover animation to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The default value is <see cref="HoverEffect.BackgroundAndBorder"/>.
        /// <b>Fields:</b>
        /// <list type="table">
        /// <listheader>
        /// <term>Enumeration</term>
        /// <description>Description.</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="HoverEffect.BackgroundAndBorder"/></term>
        /// <description>The hover animation will be applied for both background and border</description>
        /// </item>  
        /// <item>
        /// <term><see cref="HoverEffect.Border"/></term>
        /// <description>The hover animation will be applied for border only</description>
        /// </item>  
        /// <item>
        /// <term><see cref="HoverEffect.Background"/></term>
        /// <description>The hover animation will be applied for background only</description>
        /// </item>  
        /// <item>
        /// <term><see cref="HoverEffect.None"/></term>
        /// <description>The hover animation will not be applied</description>
        /// </item>       
        /// </list>
        /// </value>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static HoverEffect GetHoverEffectMode( DependencyObject obj )
        {
            return ( HoverEffect )obj.GetValue( HoverEffectModeProperty );
        }

        /// <summary>
        /// Sets the <see cref="FluentHelper.HoverEffectModeProperty"/>
        /// attached property value that denotes the hover
        /// animation to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The default value is <see cref="HoverEffect.BackgroundAndBorder"/>.
        /// <b>Fields:</b>
        /// <list type="table">
        /// <listheader>
        /// <term>Enumeration</term>
        /// <description>Description.</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="HoverEffect.BackgroundAndBorder"/></term>
        /// <description>The hover animation will be applied for both background and border</description>
        /// </item>  
        /// <item>
        /// <term><see cref="HoverEffect.Border"/></term>
        /// <description>The hover animation will be applied for border only</description>
        /// </item>  
        /// <item>
        /// <term><see cref="HoverEffect.Background"/></term>
        /// <description>The hover animation will be applied for background only</description>
        /// </item>  
        /// <item>
        /// <term><see cref="HoverEffect.None"/></term>
        /// <description>The hover animation will not be applied</description>
        /// </item>       
        /// </list>
        /// </value>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static void SetHoverEffectMode( DependencyObject obj, HoverEffect value )
        {
            obj.SetValue( HoverEffectModeProperty, value );
        }

        /// <summary>
        /// Gets the <see cref="FluentHelper.PressedEffectModeProperty"/> attached property value that denotes the pressed animation to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The default value is <see cref="PressedEffect.Reveal"/>.
        /// <b>Fields:</b>
        /// <list type="table">
        /// <listheader>
        /// <term>Enumeration</term>
        /// <description>Description.</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="PressedEffect.Reveal"/></term>
        /// <description>The pressed reveal animation will be applied</description>
        /// </item>  
        /// <item>
        /// <term><see cref="PressedEffect.Glow"/></term>
        /// <description>The pressed glow animation will be applied</description>
        /// </item>  
        /// <item>
        /// <term><see cref="PressedEffect.None"/></term>
        /// <description>The pressed animation will not be applied</description>
        /// </item>     
        /// </list>
        /// </value>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static PressedEffect GetPressedEffectMode( DependencyObject obj )
        {
            return ( PressedEffect )obj.GetValue( PressedEffectModeProperty );
        }

        /// <summary>
        /// Sets the <see cref="FluentHelper.PressedEffectModeProperty"/> attached property value that denotes the pressed animation to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The default value is <see cref="PressedEffect.Reveal"/>.
        /// <b>Fields:</b>
        /// <list type="table">
        /// <listheader>
        /// <term>Enumeration</term>
        /// <description>Description.</description>
        /// </listheader>
        /// <item>
        /// <term><see cref="PressedEffect.Reveal"/></term>
        /// <description>The pressed reveal animation will be applied</description>
        /// </item>  
        /// <item>
        /// <term><see cref="PressedEffect.Glow"/></term>
        /// <description>The pressed glow animation will be applied</description>
        /// </item>  
        /// <item>
        /// <term><see cref="PressedEffect.None"/></term>
        /// <description>The pressed animation will not be applied</description>
        /// </item>     
        /// </list>
        /// </value>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static void SetPressedEffectMode( DependencyObject obj, PressedEffect value )
        {
            obj.SetValue( PressedEffectModeProperty, value );
        }

        /// <summary>
        /// Gets the <see cref="RevealItemProperty"/> attached property value that denotes the <see cref="RevealItem"/> instance to decide on animation settings to apply for hover and pressed animation.
        /// </summary>
        /// <value>
        /// The <see cref="RevealItem"/> value. The default value is <b>null</b>.
        /// </value>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static RevealItem GetRevealItem( DependencyObject obj )
        {
            return ( RevealItem )obj.GetValue( RevealItemProperty );
        }

        /// <summary>
        /// Sets the <see cref="RevealItemProperty"/> attached property value that denotes the <see cref="RevealItem"/> instance to decide on animation settings to apply for hover and pressed animation.
        /// </summary>
        /// <value>
        /// The <see cref="RevealItem"/> value. The default value is <b>null</b>.
        /// </value>
        /// <example>
        /// <code language="XAML">
        /// <![CDATA[
        /// 
        ///     <skinmanager:RevealItem x:Key="revealItem" HoverBackground="LightGray" HoverBorder="Black" PressedBackground="LightGray" CornerRadius="2"/>
        /// 
        ///     <Border Width = "300" Height="200" HorizontalAlignment="Center" VerticalAlignment="Center" syncfusion:FluentHelper.RevealItem="{StaticResource revealItem}">
        ///         <TextBlock Text = "Testing" HorizontalAlignment="Center" VerticalAlignment="Center"/>
        ///     </Border>
        ///    
        /// ]]>
        /// </code>
        /// </example>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static void SetRevealItem( DependencyObject obj, RevealItem value )
        {
            obj.SetValue( RevealItemProperty, value );
        }

        /// <summary>
        /// Identifies the <see cref="FluentHelper.RevealItemProperty" /> dependency attached property to get or set the Reveal item instance to decide on animation settings to apply for hover and pressed animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="FluentHelper.RevealItemProperty" /> dependency attached property.
        /// </remarks>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static readonly DependencyProperty RevealItemProperty =
            DependencyProperty.RegisterAttached( "RevealItem", typeof( RevealItem ),
                typeof( FluentHelper ),
                new PropertyMetadata( null, FluentHelper.OnRevealItemChanged ) );

        /// <summary>
        /// Identifies the <see cref="FluentHelper.HoverEffectModeProperty" /> dependency attached property to get or set this property to decide on the hover animation to be applied.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="FluentHelper.HoverEffectModeProperty" /> dependency attached property.
        /// </remarks>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static readonly DependencyProperty HoverEffectModeProperty =
            DependencyProperty.RegisterAttached( "HoverEffectMode", typeof( HoverEffect ),
                typeof( FluentHelper ),
                new FrameworkPropertyMetadata( HoverEffect.BackgroundAndBorder,
                    FrameworkPropertyMetadataOptions.Inherits ) );

        /// <summary>
        /// Identifies the <see cref="FluentHelper.PressedEffectModeProperty" /> dependency attached property to get or set this property to decide on the pressed animation to be applied.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="FluentHelper.PressedEffectModeProperty" /> dependency attached property.
        /// </remarks>
        /// <exclude/>
        [ EditorBrowsable( EditorBrowsableState.Never ) ]
        [ Browsable( false ) ]
        public static readonly DependencyProperty PressedEffectModeProperty =
            DependencyProperty.RegisterAttached( "PressedEffectMode", typeof( PressedEffect ),
                typeof( FluentHelper ),
                new FrameworkPropertyMetadata( PressedEffect.Reveal,
                    FrameworkPropertyMetadataOptions.Inherits ) );

        /// <summary>
        /// Helper method to handle <see cref="RevealItemProperty"/> property changed actions
        /// </summary>
        /// <param name="d">Dependency object</param>
        /// <param name="e">Dependency EventArgs</param>
        private static void OnRevealItemChanged( DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
            var revealElement = FluentHelper.GetRevealItem( d );
            if( revealElement != null )
            {
                if( FluentHelper.GetHoverEffectMode( d ) != HoverEffect.None
                    || FluentHelper.GetPressedEffectMode( d ) != PressedEffect.None )
                {
                    ( d as UIElement ).MouseEnter += FluentHelper.FluentControl_MouseEnter;
                }
            }
        }

        /// <summary>
        /// Helper method to handle mouse leave operation related to hover and pressed animation.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private static void FluentControl_MouseLeave( object sender, MouseEventArgs e )
        {
            var dependencyObject = sender as DependencyObject;
            var uIElement = sender as UIElement;
            if( uIElement != null )
            {
                var revealElement = FluentHelper.GetRevealItem( dependencyObject );
                if( revealElement != null )
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer( uIElement );
                    if( adornerLayer != null )
                    {
                        var toRemoveArray = adornerLayer.GetAdorners( sender as UIElement );
                        Adorner toRemove;
                        if( toRemoveArray != null )
                        {
                            toRemove = toRemoveArray[ 0 ];
                            adornerLayer.Remove( toRemove );
                        }
                    }

                    uIElement.MouseLeave -= FluentHelper.FluentControl_MouseLeave;
                }
            }
        }

        /// <summary>
        /// Helper method to handle mouse enter operation related to hover and pressed animation.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">MouseEventArgs</param>
        private static void FluentControl_MouseEnter( object sender, MouseEventArgs e )
        {
            var dependencyObject = sender as DependencyObject;
            var uIElement = sender as UIElement;
            if( uIElement != null )
            {
                var adorner = AdornerLayer.GetAdornerLayer( uIElement );
                var revealElement = FluentHelper.GetRevealItem( dependencyObject );
                if( revealElement != null
                    && adorner != null )
                {
                    var hoverEffect = FluentHelper.GetHoverEffectMode( dependencyObject );
                    var pressedEffect = FluentHelper.GetPressedEffectMode( dependencyObject );
                    var defaultHoverEffect = RevealItem.HoverEffectModeProperty.DefaultMetadata;
                    if( !hoverEffect.Equals( defaultHoverEffect.DefaultValue ) )
                    {
                        revealElement.HoverEffectMode = hoverEffect;
                    }

                    var defaultPressedEffect = RevealItem.HoverEffectModeProperty.DefaultMetadata;
                    if( !pressedEffect.Equals( defaultPressedEffect.DefaultValue ) )
                    {
                        revealElement.PressedEffectMode = pressedEffect;
                    }

                    adorner.Add( new RevealItemAdorner( uIElement, revealElement ) );
                    uIElement.MouseLeave += FluentHelper.FluentControl_MouseLeave;
                }
            }
        }
    }
}