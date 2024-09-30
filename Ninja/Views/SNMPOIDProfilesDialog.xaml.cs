using System.Windows;
using System.Windows.Input;
using Ninja.ViewModels;

namespace Ninja.Views;

using ViewModels;

public partial class SNMPOIDProfilesDialog
{
    public SNMPOIDProfilesDialog()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        TextBoxSearch.Focus();
    }

    private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var x = (SNMPOIDProfilesViewModel)DataContext;

        if (x.OKCommand.CanExecute(null))
            x.OKCommand.Execute(null);
    }
}