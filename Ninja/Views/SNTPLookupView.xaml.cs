using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ninja.Controls;
using Ninja.ViewModels;

namespace Ninja.Views;

using Controls;
using ViewModels;

public partial class SNTPLookupView : IDragablzTabItem
{
    private readonly SNTPLookupViewModel _viewModel;

    public SNTPLookupView(Guid tabId)
    {
        InitializeComponent();

        _viewModel = new SNTPLookupViewModel(DialogCoordinator.Instance, tabId);

        DataContext = _viewModel;

        Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
    }

    public void CloseTab()
    {
        _viewModel.OnClose();
    }

    private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
    {
        _viewModel.OnClose();
    }

    private void ContextMenu_Opened(object sender, RoutedEventArgs e)
    {
        if (sender is ContextMenu menu)
            menu.DataContext = _viewModel;
    }
}