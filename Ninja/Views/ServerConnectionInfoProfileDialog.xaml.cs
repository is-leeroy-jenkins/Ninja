using System.Windows;
using System.Windows.Controls;
using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class ServerConnectionInfoProfileDialog
{
    public ServerConnectionInfoProfileDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        TextBoxName.Focus();
    }

    private void ContextMenu_Opened(object sender, RoutedEventArgs e)
    {
        if (sender is ContextMenu menu)
            menu.DataContext = (ServerConnectionInfoProfileViewModel)DataContext;
    }
}