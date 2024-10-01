using System.Windows;
using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class IPApiDNSResolverWidgetView
    {
        private readonly IPApiDNSResolverWidgetViewModel _viewModel = new();

        public IPApiDNSResolverWidgetView()
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