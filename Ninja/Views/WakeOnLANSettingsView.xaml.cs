using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class WakeOnLANSettingsView
    {
        private readonly WakeOnLANSettingsViewModel _viewModel = new();

        public WakeOnLANSettingsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}