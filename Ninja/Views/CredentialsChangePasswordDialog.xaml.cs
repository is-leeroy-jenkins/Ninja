﻿using System.Windows;

namespace Ninja.Views
{
    public partial class CredentialsChangePasswordDialog
    {
        public CredentialsChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBoxPassword.Focus();
        }
    }
}