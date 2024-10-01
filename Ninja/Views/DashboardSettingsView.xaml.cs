using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class DashboardSettingsView
    {
        private readonly DashboardSettingsViewModel _viewModel = new();

        public DashboardSettingsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}