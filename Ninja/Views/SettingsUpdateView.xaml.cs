using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class SettingsUpdateView
    {
        private readonly SettingsUpdateViewModel _viewModel = new();

        public SettingsUpdateView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}