using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsNetworkView
{
    private readonly SettingsNetworkViewModel _viewModel = new();

    public SettingsNetworkView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}