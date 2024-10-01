using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class SettingsWindowView
    {
        private readonly SettingsWindowViewModel _viewModel = new();

        public SettingsWindowView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}