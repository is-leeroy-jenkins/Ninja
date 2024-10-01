namespace Ninja
{
    using System.Windows;
    using ViewModels;

    // ReSharper disable once UnusedMember.Global, called from App.xaml.cs
    public partial class CommandLineWindow
    {
        private readonly CommandLineViewModel _viewModel = new();

        public CommandLineWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}