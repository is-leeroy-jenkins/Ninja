using System.Windows;

namespace Ninja.Views
{
    public partial class ProfileFileDialog
    {
        public ProfileFileDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxName.Focus();
        }
    }
}