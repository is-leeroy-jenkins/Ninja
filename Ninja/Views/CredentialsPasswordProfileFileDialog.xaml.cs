﻿using System.Windows;

namespace Ninja.Views
{
    public partial class CredentialsPasswordProfileFileDialog
    {
        public CredentialsPasswordProfileFileDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBoxPassword.Focus();
        }
    }
}