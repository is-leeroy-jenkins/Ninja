using System.Windows;

namespace Ninja.Views;

public partial class CredentialsPasswordDialog
{
    public CredentialsPasswordDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        PasswordBoxPassword.Focus();
    }
}