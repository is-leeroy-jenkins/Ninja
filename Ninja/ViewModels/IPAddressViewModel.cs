using System;
using System.Windows.Input;
using Ninja.Utilities;

namespace Ninja.ViewModels;

using Utilities;

public abstract class IPAddressViewModel : ViewModelBase
{
    private string _ipAddress;

    protected IPAddressViewModel(Action<IPAddressViewModel> okCommand, Action<IPAddressViewModel> cancelHandler)
    {
        OKCommand = new RelayCommand(_ => okCommand(this));
        CancelCommand = new RelayCommand(_ => cancelHandler(this));
    }

    public ICommand OKCommand { get; }

    public ICommand CancelCommand { get; }

    public string IPAddress
    {
        get => _ipAddress;
        set
        {
            if (value == _ipAddress)
                return;

            _ipAddress = value;
            OnPropertyChanged();
        }
    }
}