﻿using System.Windows;

namespace Ninja.Views
{
    public partial class IPAddressDialog
    {
        public IPAddressDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxIPAddress.Focus();
        }
    }
}