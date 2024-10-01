using System.Windows;

namespace Ninja.Views
{
    public partial class PortProfileDialog
    {
        public PortProfileDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxName.Focus();
        }
    }
}