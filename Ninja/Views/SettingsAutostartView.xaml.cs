using MahApps.Metro.Controls.Dialogs;
using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsAutostartView
{
    private readonly SettingsAutostartViewModel _viewModel = new(DialogCoordinator.Instance);

    public SettingsAutostartView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}