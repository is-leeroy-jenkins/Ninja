using System.Windows;

namespace Ninja.Views
{
    public partial class TigerVNCConnectDialog
    {
        public TigerVNCConnectDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxHost.Focus();
        }
    }
}