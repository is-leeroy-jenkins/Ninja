using System.Windows;
using Ninja.Models.Network;
using Ninja.ViewModels;

namespace Ninja.Views;

using Models.Network;
using ViewModels;

public partial class WiFiConnectDialog
{
    public WiFiConnectDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // Get data context from viewmodel
        var viewModel = (WiFiConnectViewModel)DataContext;

        switch (viewModel.ConnectMode)
        {
            // Pre-shared key
            case WiFiConnectMode.Psk:
                PasswordBoxPreSharedKey.Focus();
                break;
            // EAP
            case WiFiConnectMode.Eap:
                TextBoxUsername.Focus();
                break;
        }

        // Check if WPS is available for the network
        _ = viewModel.CheckWpsAsync();
    }
}