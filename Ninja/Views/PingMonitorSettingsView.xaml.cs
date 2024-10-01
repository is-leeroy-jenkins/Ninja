using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class PingMonitorSettingsView
    {
        private readonly PingMonitorSettingsViewModel _viewModel = new();

        public PingMonitorSettingsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}