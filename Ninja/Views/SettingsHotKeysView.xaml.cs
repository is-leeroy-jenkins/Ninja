using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsHotkeysView
{
    private readonly SettingsHotKeysViewModel _viewModel = new();

    public SettingsHotkeysView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}