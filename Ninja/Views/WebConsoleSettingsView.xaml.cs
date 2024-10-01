using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class WebConsoleSettingsView
    {
        private readonly WebConsoleSettingsViewModel _viewModel = new();

        public WebConsoleSettingsView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}