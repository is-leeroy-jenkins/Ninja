using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsAppearanceView
{
    private readonly SettingsAppearanceViewModel _viewModel = new();

    public SettingsAppearanceView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}