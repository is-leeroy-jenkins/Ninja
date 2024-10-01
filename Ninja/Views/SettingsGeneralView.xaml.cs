using System.Windows;
using System.Windows.Controls;
using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class SettingsGeneralView
    {
        private readonly SettingsGeneralViewModel _viewModel = new();

        public SettingsGeneralView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (sender is ContextMenu menu)
                menu.DataContext = _viewModel;
        }
    }
}