using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class SNTPLookupSettingsView
    {
        private readonly SNTPLookupSettingsViewModel _viewModel = new(DialogCoordinator.Instance);

        public SNTPLookupSettingsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (sender is ContextMenu menu)
                menu.DataContext = _viewModel;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.EditServer().ConfigureAwait(false);
        }
    }
}