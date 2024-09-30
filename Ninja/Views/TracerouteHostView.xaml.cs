using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Ninja.Profiles;
using Ninja.ViewModels;

namespace Ninja.Views;

using Profiles;
using ViewModels;

public partial class TracerouteHostView
{
    private readonly TracerouteHostViewModel _viewModel = new(DialogCoordinator.Instance);

    public TracerouteHostView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    private void ContextMenu_Opened(object sender, RoutedEventArgs e)
    {
        if (sender is ContextMenu menu)
            menu.DataContext = _viewModel;
    }

    private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            _viewModel.TraceProfileCommand.Execute(null);
    }

    public void AddTab(string host)
    {
        _viewModel.AddTab(host);
    }

    public void AddTab(ProfileInfo profile)
    {
        _viewModel.AddTab(profile);
    }

    public void OnViewHide()
    {
        _viewModel.OnViewHide();
    }

    public void OnViewVisible()
    {
        _viewModel.OnViewVisible();
    }
}