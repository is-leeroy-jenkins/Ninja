using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class LookupPortLookupView
    {
        private readonly LookupPortLookupViewModel _viewModel = new(DialogCoordinator.Instance);

        public LookupPortLookupView()
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