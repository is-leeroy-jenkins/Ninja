// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-25-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-25-2024
// ******************************************************************************************
// <copyright file="TextBoxEx.cs" company="Terry D. Eppler">
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
//   TextBoxEx.cs
// </summary>
// ******************************************************************************************



namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class TextBoxExConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter,
            CultureInfo culture )
        {
            return ( bool )value
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack( object value, Type targetType, object parameter,
            CultureInfo culture )
        {
            return DependencyProperty.UnsetValue;
        }
    }

    public class TextBoxEx : TextBox
    {
        static TextBoxEx( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( TextBoxEx ),
                new FrameworkPropertyMetadata( typeof( TextBoxEx ) ) );
        }

        public double Step
        {
            get { return ( double )GetValue( StepProperty ); }
            set { SetValue( StepProperty, value ); }
        }

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register( "Step", typeof( double ), typeof( TextBoxEx ),
                new PropertyMetadata( 1.0 ) );

        public int Minimum
        {
            get { return ( int )GetValue( MinimumProperty ); }
            set { SetValue( MinimumProperty, value ); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register( "Minimum", typeof( int ), typeof( TextBoxEx ),
                new PropertyMetadata( 0 ) );

        public int Maximum
        {
            get { return ( int )GetValue( MaximumProperty ); }
            set { SetValue( MaximumProperty, value ); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register( "Maximum", typeof( int ), typeof( TextBoxEx ),
                new PropertyMetadata( 0 ) );

        #region CornerRadius
        public CornerRadius GetCornerRadius( DependencyObject obj )
        {
            return ( CornerRadius )obj.GetValue( CornerRadiusProperty );
        }

        public static void SetCornerRadius( DependencyObject obj, CornerRadius value )
        {
            obj.SetValue( CornerRadiusProperty, value );
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached( "CornerRadius", typeof( CornerRadius ),
                typeof( TextBoxEx ) );
        #endregion

        #region IsClearButtonVisible
        public static bool GetIsClearBtnVisible( DependencyObject obj )
        {
            return ( bool )obj.GetValue( IsClearBtnVisibleProperty );
        }

        public static void SetIsClearBtnVisible( DependencyObject obj, bool value )
        {
            obj.SetValue( IsClearBtnVisibleProperty, value );
        }

        public static readonly DependencyProperty IsClearBtnVisibleProperty =
            DependencyProperty.RegisterAttached( "IsClearBtnVisible", typeof( bool ),
                typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextBoxHookChanged ) );

        private static void OnTextBoxHookChanged( DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
            var _textbox = d as TextBox;
            _textbox.RemoveHandler( ButtonBase.ClickEvent,
                new RoutedEventHandler( TextBoxEx.ClearBtnClicked ) );

            _textbox.AddHandler( ButtonBase.ClickEvent,
                new RoutedEventHandler( TextBoxEx.ClearBtnClicked ) );
        }

        private static void ClearBtnClicked( object sender, RoutedEventArgs e )
        {
            var _button = e.OriginalSource as Button;
            if( _button == null
                || _button.Name != "PART_BtnClear" )
            {
                return;
            }

            var _textbox = sender as TextBox;
            if( _textbox == null )
            {
                return;
            }

            _textbox.Text = "";
        }
        #endregion

        public static bool GetIsAddBtnVisible( DependencyObject obj )
        {
            return ( bool )obj.GetValue( IsAddBtnVisibleProperty );
        }

        public static void SetIsAddBtnVisible( DependencyObject obj, bool value )
        {
            obj.SetValue( IsAddBtnVisibleProperty, value );
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAddBtnVisibleProperty =
            DependencyProperty.RegisterAttached( "IsAddBtnVisible", typeof( bool ),
                typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextChanged ) );

        public static bool GetIsRemoveBtnVisible( DependencyObject obj )
        {
            return ( bool )obj.GetValue( IsRemoveBtnVisibleProperty );
        }

        public static void SetIsRemoveBtnVisible( DependencyObject obj, bool value )
        {
            obj.SetValue( IsRemoveBtnVisibleProperty, value );
        }

        public static readonly DependencyProperty IsRemoveBtnVisibleProperty =
            DependencyProperty.RegisterAttached( "IsRemoveBtnVisible", typeof( bool ),
                typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextChanged ) );

        private static void OnTextChanged( DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
            var _textbox = d as TextBox;
            if( ( bool )e.NewValue )
            {
                _textbox.MouseWheel += TextBoxEx.Textbox_MouseWheel;
            }
            else
            {
                _textbox.MouseWheel -= TextBoxEx.Textbox_MouseWheel;
            }

            _textbox.RemoveHandler( ButtonBase.ClickEvent,
                new RoutedEventHandler( TextBoxEx.ButtonClicked ) );

            _textbox.AddHandler( ButtonBase.ClickEvent,
                new RoutedEventHandler( TextBoxEx.ButtonClicked ) );
        }

        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Textbox_MouseWheel( object sender, MouseWheelEventArgs e )
        {
            var _textboxex = e.Source as TextBoxEx;
            double _step = 1;
            var _min = 0;
            if( _textboxex != null )
            {
                _step = _textboxex.Step;
                _min = _textboxex.Minimum;
            }

            var _textbox = sender as TextBox;
            double _temp;
            var _result = double.TryParse( _textbox.Text, out _temp );
            if( _result )
            {
                if( e.Delta > 0 )
                {
                    _textbox.Text = ( _temp + _step ).ToString( );
                }
                else
                {
                    _textbox.Text = ( _temp - _step ).ToString( );
                }

                if( double.Parse( _textbox.Text ) < _min )
                {
                    _textbox.Text = _min.ToString( );
                }

                _textbox.Select( _textbox.Text.Length, 0 );//光标设置到文本尾部
            }
        }

        private static void ButtonClicked( object sender, RoutedEventArgs e )
        {
            var _button = e.OriginalSource as RepeatButton;
            if( _button == null )
            {
                return;
            }

            var _textboxex = e.Source as TextBoxEx;
            double _step = 1;
            var _min = 0;
            if( _textboxex != null )
            {
                _step = _textboxex.Step;
                _min = _textboxex.Minimum;
            }

            var _textbox = sender as TextBox;
            if( _textbox == null )
            {
                return;
            }

            double _temp;

            //int Step = step.Step;
            var _result = double.TryParse( _textbox.Text, out _temp );
            if( _result )
            {
                if( _button.Name == "PART_BtnAdd" )
                {
                    _textbox.Text = ( _temp + _step ).ToString( );
                }
                else
                {
                    _textbox.Text = ( _temp - _step ).ToString( );
                }

                if( double.Parse( _textbox.Text ) < _min )
                {
                    _textbox.Text = _min.ToString( );
                }

                _textbox.Focus( );
                _textbox.Select( _textbox.Text.Length, 0 );
            }
        }
    }
}