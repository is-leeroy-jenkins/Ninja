using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Ninja.Models.Network;
using Ninja.Settings;
using Ninja.Utilities;

namespace Ninja.ViewModels;

using Models.Network;
using Settings;
using Utilities;

public class SNMPOIDProfilesViewModel : ViewModelBase
{
    private string _search;

    private SNMPOIDProfileInfo _selectedOIDProfile;

    public SNMPOIDProfilesViewModel(Action<SNMPOIDProfilesViewModel> okCommand,
        Action<SNMPOIDProfilesViewModel> cancelHandler)
    {
        OKCommand = new RelayCommand(_ => okCommand(this));
        CancelCommand = new RelayCommand(_ => cancelHandler(this));

        OIDProfiles = CollectionViewSource.GetDefaultView(SettingsManager.Current.SNMP_OidProfiles);
        OIDProfiles.SortDescriptions.Add(new SortDescription(nameof(SNMPOIDProfileInfo.Name),
            ListSortDirection.Ascending));
        OIDProfiles.Filter = o =>
        {
            if (string.IsNullOrEmpty(Search))
                return true;

            if (o is not SNMPOIDProfileInfo info)
                return false;

            var search = Search.Trim();

            // Search: Name, OID
            return info.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1 ||
                   info.OID.IndexOf(search, StringComparison.OrdinalIgnoreCase) > -1;
        };
    }

    public ICommand OKCommand { get; }
    public ICommand CancelCommand { get; }

    public string Search
    {
        get => _search;
        set
        {
            if (value == _search)
                return;

            _search = value;

            OIDProfiles.Refresh();

            OnPropertyChanged();
        }
    }

    public ICollectionView OIDProfiles { get; }

    public SNMPOIDProfileInfo SelectedOIDProfile
    {
        get => _selectedOIDProfile;
        set
        {
            if (Equals(value, _selectedOIDProfile))
                return;

            _selectedOIDProfile = value;
            OnPropertyChanged();
        }
    }
}