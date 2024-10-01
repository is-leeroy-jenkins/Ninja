using System.Windows;
using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class IPApiIPGeolocationWidgetView
    {
        private readonly IPApiIPGeolocationWidgetViewModel _viewModel = new();

        public IPApiIPGeolocationWidgetView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Check();
        }
    }
}