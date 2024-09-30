using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class TracerouteSettingsView
{
    private readonly TracerouteSettingsViewModel _viewModel = new();

    public TracerouteSettingsView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}