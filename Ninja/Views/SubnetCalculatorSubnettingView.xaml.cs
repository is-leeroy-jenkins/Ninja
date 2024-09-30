using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SubnetCalculatorSubnettingView
{
    private readonly SubnetCalculatorSubnettingViewModel _viewModel = new(DialogCoordinator.Instance);

    public SubnetCalculatorSubnettingView()
    {
        InitializeComponent();
        DataContext = _viewModel;

        Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
    }

    private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
    {
        _viewModel.OnShutdown();
    }

    private void ContextMenu_Opened(object sender, RoutedEventArgs e)
    {
        if (sender is ContextMenu menu)
            menu.DataContext = _viewModel;
    }
}