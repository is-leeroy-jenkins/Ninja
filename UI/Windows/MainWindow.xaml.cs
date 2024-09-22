

namespace Ninja.Views
{
    using ViewModels;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <summary>
    /// MainWindow.xaml 
    /// </summary>
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowViewModel = null;
        public MainWindow( )
        {
            InitializeComponent();
            Height = 750;
            Width = Height / 0.7;
            MainWindowViewModel = new MainWindowViewModel();
            DataContext = MainWindowViewModel;
        }
    }
}
