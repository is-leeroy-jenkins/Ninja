using System.Windows;

namespace Ninja.Views;

public partial class CustomCommandDialog
{
    public CustomCommandDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        TextBoxName.Focus();
    }
}