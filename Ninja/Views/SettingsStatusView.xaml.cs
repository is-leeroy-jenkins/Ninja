using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsStatusView
{
    private readonly SettingsStatusViewModel _viewModel = new();

    public SettingsStatusView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}