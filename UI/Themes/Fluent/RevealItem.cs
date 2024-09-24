// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 09-08-2020
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-08-2024
// ******************************************************************************************
// <copyright file="RevealItem.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts
//    that is based on WPF, NET6.0, and written in C-Sharp.
// 
//     Copyright �  2020, 2022, 2204 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the �Software�),
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
//    THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
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
//   RevealItem.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.ComponentModel;
    using Syncfusion.SfSkinManager;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Helper class to achieve reveal hover and pressed animation 
    /// </summary>
    /// <exclude/>
    [ EditorBrowsable( EditorBrowsableState.Never ) ]
    [ Browsable( false ) ]
    public class RevealItem : ContentControl
    {
        private Color RevealBackground = Colors.White;

        private double RevealBackgroundSize = 100;

        private double RevealBackgroundOpacity = 0.2;

        private Color RevealBorder = Colors.White;

        private double RevealBorderSize = 60;

        private double RevealBorderOpacity = 0.8;

        private Border revealBackground;

        private Border revealBorder;

        private RadialGradientBrush revealPressedRectBrush;

        private MetroDataGrid revealGrid;

        private Storyboard revealPressedStoryboard;

        /// <inheritdoc />
        /// <summary>
        /// Static constructor to handle reveal animations.
        /// </summary>
        static RevealItem( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( RevealItem ),
                new FrameworkPropertyMetadata( typeof( RevealItem ) ) );
        }

        /// <inheritdoc />
        /// <summary>
        /// Default constructor to handle reveal animations.
        /// </summary>
        public RevealItem( )
        {
        }

        /// <summary>
        /// Helper method to handle template change operations for <see cref="RevealItem"/>
        /// </summary>
        public override void OnApplyTemplate( )
        {
            base.OnApplyTemplate( );
            revealBackground = GetTemplateChild( "backgroundMouseOver" ) as Border;
            revealBorder = GetTemplateChild( "borderOpacityMask" ) as Border;
            revealPressedRectBrush = GetTemplateChild( "pressedRectBrush" ) as RadialGradientBrush;
            revealGrid = GetTemplateChild( "rootGrid" ) as MetroDataGrid;
            UpdateAnimationDetails( );
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.HoverEffectMode"/> attached property value that denotes the hover animation to be applied on UIElement.
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
        public HoverEffect HoverEffectMode
        {
            get { return ( HoverEffect )GetValue( HoverEffectModeProperty ); }
            set { SetValue( HoverEffectModeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.PressedEffectMode"/>
        /// attached property value that denotes the pressed animation to be applied on UIElement.
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
        public PressedEffect PressedEffectMode
        {
            get { return ( PressedEffect )GetValue( PressedEffectModeProperty ); }
            set { SetValue( PressedEffectModeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.HoverBorder"/>
        /// property value that denotes the hover border to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="Brush"/> value. The default value is <see cref="Brushes.Transparent"/>.
        /// </value>
        /// <exclude/>
        public Brush HoverBorder
        {
            get { return ( Brush )GetValue( HoverBorderProperty ); }
            set { SetValue( HoverBorderProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.HoverBackground"/> property value that
        /// denotes the hover background to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="Brush"/> value. The default value is <see cref="Brushes.Transparent"/>.
        /// </value>
        /// <exclude/>
        public Brush HoverBackground
        {
            get { return ( Brush )GetValue( HoverBackgroundProperty ); }
            set { SetValue( HoverBackgroundProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.PressedBackground"/>
        /// property value that denotes the pressed background to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="Brush"/> value. The default value is <see cref="Brushes.Transparent"/>.
        /// </value>
        /// <exclude/>
        public Brush PressedBackground
        {
            get { return ( Brush )GetValue( PressedBackgroundProperty ); }
            set { SetValue( PressedBackgroundProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.CornerRadius"/>
        /// property value that denotes the corner radius of hover and pressed border to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="CornerRadius"/> value. The default value is <b>0</b>.
        /// </value>
        /// <exclude/>
        public CornerRadius CornerRadius
        {
            get { return ( CornerRadius )GetValue( CornerRadiusProperty ); }
            set { SetValue( CornerRadiusProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.PressedBorderOpacity"/>
        /// property value that denotes the pressed border opacity to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="double"/> value. The default value is <b>0.2</b>.
        /// </value>
        /// <exclude/>
        public double PressedBorderOpacity
        {
            get { return ( double )GetValue( PressedBorderOpacityProperty ); }
            set { SetValue( PressedBorderOpacityProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.HoverBorderOpacity"/>
        /// property value that denotes the hover border opacity to be applied on UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="double"/> value. The default value is <b>0.4</b>.
        /// </value>
        /// <exclude/>
        public double HoverBorderOpacity
        {
            get { return ( double )GetValue( HoverBorderOpacityProperty ); }
            set { SetValue( HoverBorderOpacityProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.Position"/>
        /// attached property value that denotes the hover cursor position of the given UIElement.
        /// </summary>
        /// <value>
        /// The <see cref="Point"/> value. 
        /// </value>
        /// <exclude/>
        public Point Position
        {
            get { return ( Point )GetValue( PositionProperty ); }
            set { SetValue( PositionProperty, value ); }
        }

        /// <inheritdoc/>
        public new bool IsMouseOver
        {
            get { return ( bool )GetValue( IsMouseOverProperty ); }
            set { SetValue( IsMouseOverProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="RevealItem.IsPressed"/>
        /// attached property value that denotes whether the control is pressed or not.
        /// </summary>
        /// <value>
        /// The <see cref="bool"/> value. The default value is <b>false</b>.
        /// </value>
        /// <exclude/>
        public bool IsPressed
        {
            get { return ( bool )GetValue( IsPressedProperty ); }
            set { SetValue( IsPressedProperty, value ); }
        }

        /// <summary>
        /// Identifies the <see cref="RevealItem.Position" />
        /// dependency property to get or set this property to decide on the hover position.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.PositionProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register( nameof( Position ), typeof( Point ), typeof( RevealItem ),
                new PropertyMetadata( new Point( 0, 0 ), RevealItem.OnPositionChanged ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.HoverEffectMode" />
        /// dependency property to get or set this property to decide on the hover animation to be applied.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.HoverEffectModeProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty HoverEffectModeProperty =
            DependencyProperty.Register( nameof( HoverEffectMode ), typeof( HoverEffect ),
                typeof( RevealItem ), new PropertyMetadata( HoverEffect.BackgroundAndBorder ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.PressedEffectMode" />
        /// dependency property to get or set this property to decide on the pressed animation to be applied.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.PressedEffectModeProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty PressedEffectModeProperty =
            DependencyProperty.Register( nameof( PressedEffectMode ), typeof( PressedEffect ),
                typeof( RevealItem ), new PropertyMetadata( PressedEffect.Reveal ) );

        /// <inheritdoc/>
        public static readonly new DependencyProperty IsMouseOverProperty =
            DependencyProperty.Register( nameof( IsMouseOver ), typeof( bool ), typeof( RevealItem ),
                new FrameworkPropertyMetadata( false ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.IsPressed" />
        /// dependency property to get or set this property to decide on whether the control is pressed or not.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.IsPressedProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register( nameof( IsPressed ), typeof( bool ), typeof( RevealItem ),
                new FrameworkPropertyMetadata( false ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.HoverBackground" />
        /// dependency property to get or set this property to decide on the hover background to be applied for hover animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.HoverBackgroundProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register( nameof( HoverBackground ), typeof( Brush ), typeof( RevealItem ),
                new FrameworkPropertyMetadata( Brushes.Transparent ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.PressedBackground" />
        /// dependency property to get or set this property to decide on the pressed background to be applied for pressed animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.PressedBackgroundProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register( nameof( PressedBackground ), typeof( Brush ), typeof( RevealItem ),
                new FrameworkPropertyMetadata( Brushes.Transparent ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.HoverBorder" />
        /// dependency property to get or set this property to decide on the hover border to be applied for hover animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.HoverBorderProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty HoverBorderProperty =
            DependencyProperty.Register( nameof( HoverBorder ), typeof( Brush ), typeof( RevealItem ),
                new FrameworkPropertyMetadata( Brushes.Transparent ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.CornerRadius" />
        /// dependency property to get or set this property to decide on the corner radius to be applied for hover animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.CornerRadiusProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register( nameof( CornerRadius ), typeof( CornerRadius ),
                typeof( RevealItem ), new FrameworkPropertyMetadata( new CornerRadius( 0 ) ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.PressedBorderOpacity" />
        /// dependency property to get or set this property to decide on the pressed border opacity to be applied for pressed animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.PressedBorderOpacityProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty PressedBorderOpacityProperty =
            DependencyProperty.Register( nameof( PressedBorderOpacity ), typeof( double ),
                typeof( RevealItem ), new PropertyMetadata( 0.2 ) );

        /// <summary>
        /// Identifies the <see cref="RevealItem.HoverBorderOpacity" />
        /// dependency property to get or set this property to decide on the hover border opacity to be applied for hover animation.
        /// </summary>
        /// <remarks>
        /// The identifier for the <see cref="RevealItem.HoverBorderOpacityProperty" /> dependency property.
        /// </remarks>
        public static readonly DependencyProperty HoverBorderOpacityProperty =
            DependencyProperty.Register( nameof( HoverBorderOpacity ), typeof( double ),
                typeof( RevealItem ), new PropertyMetadata( 0.4 ) );

        /// <summary>
        /// Helper method to handle hover position changed actions
        /// </summary>
        /// <param name="d">Dependency Object</param>
        /// <param name="e">Dependency EventArgs</param>
        private static void OnPositionChanged( DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
            var revealItem = d as RevealItem;
            revealItem.UpdateAnimationDetails( );
        }

        /// <summary>
        /// Method to update reveal animation border width
        /// </summary>
        /// <param name="width">Control width</param>
        public void UpdateRevealBorderSize( double width )
        {
            RevealBackgroundSize = width * 50 / 100;
            RevealBorderSize = width * 30 / 100;
        }

        /// <summary>
        /// Method to update animation settings to apply hover and pressed animation
        /// </summary>
        private void UpdateAnimationDetails( )
        {
            if( revealBackground != null )
            {
                revealBackground.Background = GetRevealBrushValue( RevealBackground,
                    RevealBackgroundSize, RevealBackgroundOpacity, Position,
                    revealBackground.Background );
            }

            if( revealBorder != null )
            {
                revealBorder.OpacityMask = GetRevealBrushValue( RevealBorder, RevealBorderSize,
                    RevealBorderOpacity, Position, revealBorder.OpacityMask );
            }

            if( revealPressedRectBrush != null )
            {
                revealPressedRectBrush.Center = Position;
                revealPressedRectBrush.GradientOrigin = Position;
                revealPressedRectBrush.GradientStops[ 1 ].Color =
                    ( PressedBackground as SolidColorBrush ).Color;
            }
        }

        /// <summary>
        /// Helper method to get reveal gradient brush value based on position, color and size details
        /// </summary>
        /// <param name="color">Reveal color</param>
        /// <param name="size">Reveal Size</param>
        /// <param name="opacity">Reveal opacity</param>
        /// <param name="position">Reveal position</param>
        /// <param name="brush">Reveal brush to be changer</param>
        /// <returns>The <see cref="Brush"/> value</returns>
        private Brush GetRevealBrushValue( Color color, double size, double opacity,
            Point position, Brush brush )
        {
            RadialGradientBrush radialGradientBrush;
            var revealColor = Color.FromArgb( 0, color.R, color.G, color.B );
            if( !( brush is RadialGradientBrush ) )
            {
                radialGradientBrush = new RadialGradientBrush( color, revealColor );
            }
            else
            {
                radialGradientBrush = brush as RadialGradientBrush;
            }

            radialGradientBrush.MappingMode = BrushMappingMode.Absolute;
            radialGradientBrush.RadiusX = size;
            radialGradientBrush.RadiusY = size;
            radialGradientBrush.Opacity = opacity;
            radialGradientBrush.Center = position;
            radialGradientBrush.GradientOrigin = position;
            return radialGradientBrush;
        }

        /// <summary>
        /// Method to start pressed reveal animation
        /// </summary>
        internal void StartPressedRevealAnimation( )
        {
            if( revealPressedStoryboard == null
                && revealGrid != null )
            {
                revealPressedStoryboard = PressedEffectMode == PressedEffect.Reveal
                    ? revealGrid.Resources[ "PressedRevealStoryboard" ] as Storyboard
                    : PressedEffectMode == PressedEffect.Glow
                        ? revealGrid.Resources[ "PressedGlowStoryboard" ] as Storyboard
                        : null;
            }

            if( revealPressedStoryboard != null )
            {
                revealPressedStoryboard.Completed += RevealPressedStoryboard_Completed;
                revealPressedStoryboard.Begin( );
            }
        }

        /// <summary>
        /// Helper method to handle storyboard completion action
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void RevealPressedStoryboard_Completed( object sender, EventArgs e )
        {
            if( revealPressedStoryboard != null )
            {
                revealPressedStoryboard.Stop( );
                revealPressedStoryboard.Completed -= RevealPressedStoryboard_Completed;
            }
        }
    }
}