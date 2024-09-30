﻿using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SettingsSettingsView
{
    private readonly SettingsSettingsViewModel _viewModel = new(DialogCoordinator.Instance);

    public SettingsSettingsView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (_viewModel.CloseAction != null)
            return;

        var window = Window.GetWindow(this);

        if (window != null)
            _viewModel.CloseAction = window.Close;
    }
}