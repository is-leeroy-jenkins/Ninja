using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsLanguageView
{
    private readonly SettingsLanguageViewModel _viewModel = new();

    public SettingsLanguageView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}