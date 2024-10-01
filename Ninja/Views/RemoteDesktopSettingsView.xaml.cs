using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class RemoteDesktopSettingsView
    {
        private readonly RemoteDesktopSettingsViewModel _viewModel = new();

        public RemoteDesktopSettingsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}