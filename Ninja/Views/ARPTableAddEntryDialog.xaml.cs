using System.Windows;

namespace Ninja.Views
{
    public partial class ARPTableAddEntryDialog
    {
        public ARPTableAddEntryDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxIPAddress.Focus();
        }
    }
}