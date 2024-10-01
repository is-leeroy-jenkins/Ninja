﻿using System;
using System.Security;
using System.Windows.Input;
using Ninja.Utilities;

namespace Ninja.ViewModels
{
    using Utilities;

    public class CredentialsPasswordViewModel : ViewModelBase
    {
        /// <summary>
        ///     Private variable for <see cref="IsPasswordEmpty" />.
        /// </summary>
        private bool _isPasswordEmpty = true;

        /// <summary>
        ///     Private variable for <see cref="Password" />.
        /// </summary>
        private SecureString _password = new();

        /// <summary>
        ///     Initialize a new class <see cref="CredentialsPasswordViewModel" /> with <see cref="OKCommand" /> and
        ///     <see cref="CancelCommand" />.
        /// </summary>
        /// <param name="okCommand"><see cref="OKCommand" /> which is executed on OK click.</param>
        /// <param name="cancelHandler"><see cref="CancelCommand" /> which is executed on cancel click.</param>
        public CredentialsPasswordViewModel(Action<CredentialsPasswordViewModel> okCommand,
            Action<CredentialsPasswordViewModel> cancelHandler)
        {
            OKCommand = new RelayCommand(_ => okCommand(this));
            CancelCommand = new RelayCommand(_ => cancelHandler(this));
        }

        /// <summary>
        ///     Command which is called when the OK button is clicked.
        /// </summary>
        public ICommand OKCommand { get; }

        /// <summary>
        ///     Command which is called when the cancel button is clicked.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        ///     Password as <see cref="SecureString" />.
        /// </summary>
        public SecureString Password
        {
            get => _password;
            set
            {
                if (value == _password)
                    return;

                _password = value;

                ValidatePassword();

                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Indicate if one of the password fields are empty.
        /// </summary>
        public bool IsPasswordEmpty
        {
            get => _isPasswordEmpty;
            set
            {
                if (value == _isPasswordEmpty)
                    return;

                _isPasswordEmpty = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Check if the passwords are valid.
        /// </summary>
        private void ValidatePassword()
        {
            IsPasswordEmpty = Password == null || Password.Length == 0;
        }
    }
}