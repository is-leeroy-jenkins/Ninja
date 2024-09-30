using System.Windows;

namespace Ninja.Views;

public partial class AWSProfileDialog
{
    public AWSProfileDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        TextBoxProfile.Focus();
    }
}