using System.Windows;

namespace Ninja.Views
{
    public partial class IPAddressAndSubnetmaskDialog
    {
        public IPAddressAndSubnetmaskDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxIPAddress.Focus();
        }
    }
}