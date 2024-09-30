using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class BitCalculatorSettingsView
{
    private readonly BitCalculatorSettingsViewModel _viewModel = new();

    public BitCalculatorSettingsView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}