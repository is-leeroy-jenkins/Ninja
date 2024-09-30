using System.Windows;

namespace Ninja.Views;

public partial class CredentialsSetPasswordDialog
{
    public CredentialsSetPasswordDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        PasswordBoxPassword.Focus();
    }
}