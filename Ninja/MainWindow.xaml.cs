// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 10-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        10-01-2024
// ******************************************************************************************
// <copyright file="MainWindow.xaml.cs" company="Terry D. Eppler">
//   An open source tool for managing networks and troubleshooting network problems.
// 
//   Connect and manage remote systems with Remote Desktop, PowerShell, PuTTY,
//   TigerVNC or AWS (Systems Manager) Session Manager.  Analyze and troubleshoot your
//   network and systems with features such as the WiFi Analyzer,IP Scanner,
//   Port Scanner, Ping Monitor, Traceroute, DNS lookup or LLDP/CDP capture in a unfied interface.
//   Hosts (or networks) can be saved in (encrypted) profiles and used across all features.
// 
//    Copyright ©  2019-2024 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   MainWindow.xaml.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using Controls;
    using Documentation;
    using Localization;
    using Models;
    using Models.AWS;
    using Models.EventSystem;
    using Models.Network;
    using Models.PowerShell;
    using Models.PuTTY;
    using Profiles;
    using Settings;
    using Update;
    using Utilities;
    using ViewModels;
    using Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Threading;
    using log4net;
    using MahApps.Metro.Controls.Dialogs;
    using Localization.Resources;
    using Application = System.Windows.Application;
    using ContextMenu = System.Windows.Controls.ContextMenu;
    using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="MahApps.Metro.Controls.MetroWindow" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public sealed partial class MainWindow : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Handles the PropertyChanged event of the SettingsManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void SettingsManager_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            switch( e.PropertyName )
            {
                case nameof( SettingsInfo.Localization_CultureCode ):
                    IsRestartRequired = true;
                    break;
                case nameof( SettingsInfo.TrayIcon_AlwaysShowIcon ):
                    if( SettingsManager.Current.TrayIcon_AlwaysShowIcon
                        && _notifyIcon == null )
                    {
                        InitNotifyIcon( );
                    }

                    if( _notifyIcon != null )
                    {
                        _notifyIcon.Visible = SettingsManager.Current.TrayIcon_AlwaysShowIcon;
                    }

                    break;
                case nameof( SettingsInfo.Network_UseCustomDNSServer ):
                case nameof( SettingsInfo.Network_CustomDNSServer ):
                    ConfigureDNS( );
                    break;
                case nameof( SettingsInfo.Appearance_PowerShellModifyGlobalProfile ):
                case nameof( SettingsInfo.Appearance_Theme ):
                case nameof( SettingsInfo.PowerShell_ApplicationFilePath ):
                case nameof( SettingsInfo.AWSSessionManager_ApplicationFilePath ):
                    if( SettingsManager.Current.WelcomeDialog_Show )
                    {
                        return;
                    }

                    WriteDefaultPowerShellProfileToRegistry( );
                    break;
            }
        }
        #endregion

        #region Bugfixes
        /// <summary>
        /// Handles the ManipulationBoundaryFeedback event of the ScrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ManipulationBoundaryFeedbackEventArgs" /> instance containing the event data.</param>
        private void ScrollViewer_ManipulationBoundaryFeedback( object sender,
            ManipulationBoundaryFeedbackEventArgs e )
        {
            e.Handled = true;
        }
        #endregion

        #region PropertyChangedEventHandler
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
        #endregion

        #region Variables
        /// <summary>
        /// The log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger( typeof( MainWindow ) );

        /// <summary>
        /// The notify icon
        /// </summary>
        private NotifyIcon _notifyIcon;

        /// <summary>
        /// The status window
        /// </summary>
        private StatusWindow _statusWindow;

        /// <summary>
        /// The is loading
        /// </summary>
        private readonly bool _isLoading;

        /// <summary>
        /// The is profile files loading
        /// </summary>
        private bool _isProfileFilesLoading;

        /// <summary>
        /// The is profile file updating
        /// </summary>
        private bool _isProfileFileUpdating;

        /// <summary>
        /// The is application view loading
        /// </summary>
        private bool _isApplicationViewLoading;

        /// <summary>
        /// The is network changing
        /// </summary>
        private bool _isNetworkChanging;

        /// <summary>
        /// The is in tray
        /// </summary>
        private bool _isInTray;

        /// <summary>
        /// The is closing
        /// </summary>
        private bool _isClosing;

        /// <summary>
        /// The application view is expanded
        /// </summary>
        private bool _applicationViewIsExpanded;

        /// <summary>
        /// Gets or sets a value indicating whether [application view is expanded].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [application view is expanded]; otherwise, <c>false</c>.
        /// </value>
        public bool ApplicationViewIsExpanded
        {
            get
            {
                return _applicationViewIsExpanded;
            }
            set
            {
                if( value == _applicationViewIsExpanded )
                {
                    return;
                }

                if( !_isLoading )
                {
                    SettingsManager.Current.ExpandApplicationView = value;
                }

                if( !value )
                {
                    ClearSearchOnApplicationListMinimize( );
                }

                _applicationViewIsExpanded = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The text box application search is focused
        /// </summary>
        private bool _textBoxApplicationSearchIsFocused;

        /// <summary>
        /// Gets or sets a value indicating whether [text box application search is focused].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [text box application search is focused]; otherwise, <c>false</c>.
        /// </value>
        public bool TextBoxApplicationSearchIsFocused
        {
            get
            {
                return _textBoxApplicationSearchIsFocused;
            }
            set
            {
                if( value == _textBoxApplicationSearchIsFocused )
                {
                    return;
                }

                if( !value )
                {
                    ClearSearchOnApplicationListMinimize( );
                }

                _textBoxApplicationSearchIsFocused = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The application view is open
        /// </summary>
        private bool _applicationViewIsOpen;

        /// <summary>
        /// Gets or sets a value indicating whether [application view is open].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [application view is open]; otherwise, <c>false</c>.
        /// </value>
        public bool ApplicationViewIsOpen
        {
            get
            {
                return _applicationViewIsOpen;
            }
            set
            {
                if( value == _applicationViewIsOpen )
                {
                    return;
                }

                if( !value )
                {
                    ClearSearchOnApplicationListMinimize( );
                }

                _applicationViewIsOpen = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The application view is mouse over
        /// </summary>
        private bool _applicationViewIsMouseOver;

        /// <summary>
        /// Gets or sets a value indicating whether [application view is mouse over].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [application view is mouse over]; otherwise, <c>false</c>.
        /// </value>
        public bool ApplicationViewIsMouseOver
        {
            get
            {
                return _applicationViewIsMouseOver;
            }
            set
            {
                if( value == _applicationViewIsMouseOver )
                {
                    return;
                }

                if( !value )
                {
                    ClearSearchOnApplicationListMinimize( );
                }

                _applicationViewIsMouseOver = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The applications
        /// </summary>
        private ICollectionView _applications;

        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <value>
        /// The applications.
        /// </value>
        public ICollectionView Applications
        {
            get
            {
                return _applications;
            }
            private set
            {
                if( value == _applications )
                {
                    return;
                }

                _applications = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The selected application
        /// </summary>
        private ApplicationInfo _selectedApplication;

        /// <summary>
        /// Gets or sets the selected application.
        /// </summary>
        /// <value>
        /// The selected application.
        /// </value>
        public ApplicationInfo SelectedApplication
        {
            get
            {
                return _selectedApplication;
            }
            set
            {
                if( _isApplicationViewLoading )
                {
                    return;
                }

                if( value == null
                    && !_applicationViewIsEmpty )
                {
                    return;
                }

                if( object.Equals( value, _selectedApplication ) )
                {
                    return;
                }

                if( value != null
                    && !_applicationViewIsEmpty )
                {
                    if( _selectedApplication != null )
                    {
                        OnApplicationViewHide( _selectedApplication.Name );
                    }

                    OnApplicationViewVisible( value.Name );
                    ConfigurationManager.Current.CurrentApplication = value.Name;
                }

                _selectedApplication = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The previous selected application
        /// </summary>
        private ApplicationInfo _previousSelectedApplication;

        /// <summary>
        /// The application view is empty
        /// </summary>
        private bool _applicationViewIsEmpty;

        /// <summary>
        /// The application search
        /// </summary>
        private string _applicationSearch = string.Empty;

        /// <summary>
        /// Gets or sets the application search.
        /// </summary>
        /// <value>
        /// The application search.
        /// </value>
        public string ApplicationSearch
        {
            get
            {
                return _applicationSearch;
            }
            set
            {
                if( value == _applicationSearch )
                {
                    return;
                }

                _applicationSearch = value;
                if( SelectedApplication != null )
                {
                    _previousSelectedApplication = SelectedApplication;
                }

                Applications.Refresh( );
                if( Applications.IsEmpty )
                {
                    _applicationViewIsEmpty = true;
                }
                else if( _applicationViewIsEmpty )
                {
                    SelectedApplication = null;
                    SelectedApplication = Applications.Cast<ApplicationInfo>( )
                            .FirstOrDefault( x => x.Name == _previousSelectedApplication.Name )
                        ?? Applications.Cast<ApplicationInfo>( ).FirstOrDefault( );

                    _applicationViewIsEmpty = false;
                }

                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The settings view
        /// </summary>
        private SettingsView _settingsView;

        /// <summary>
        /// The settings view is open
        /// </summary>
        private bool _settingsViewIsOpen;

        /// <summary>
        /// Gets or sets a value indicating whether [settings view is open].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [settings view is open]; otherwise, <c>false</c>.
        /// </value>
        public bool SettingsViewIsOpen
        {
            get
            {
                return _settingsViewIsOpen;
            }
            set
            {
                if( value == _settingsViewIsOpen )
                {
                    return;
                }

                _settingsViewIsOpen = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The flyout run command is open
        /// </summary>
        private bool _flyoutRunCommandIsOpen;

        /// <summary>
        /// Gets or sets a value indicating whether [flyout run command is open].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [flyout run command is open]; otherwise, <c>false</c>.
        /// </value>
        public bool FlyoutRunCommandIsOpen
        {
            get
            {
                return _flyoutRunCommandIsOpen;
            }
            set
            {
                if( value == _flyoutRunCommandIsOpen )
                {
                    return;
                }

                _flyoutRunCommandIsOpen = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The flyout run command are animations enabled
        /// </summary>
        private bool _flyoutRunCommandAreAnimationsEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether [flyout run command are animations enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [flyout run command are animations enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool FlyoutRunCommandAreAnimationsEnabled
        {
            get
            {
                return _flyoutRunCommandAreAnimationsEnabled;
            }
            set
            {
                if( value == _flyoutRunCommandAreAnimationsEnabled )
                {
                    return;
                }

                _flyoutRunCommandAreAnimationsEnabled = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The is restart required
        /// </summary>
        private bool _isRestartRequired;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is restart required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is restart required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRestartRequired
        {
            get
            {
                return _isRestartRequired;
            }
            set
            {
                if( value == _isRestartRequired )
                {
                    return;
                }

                _isRestartRequired = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The is update available
        /// </summary>
        private bool _isUpdateAvailable;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is update available.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is update available; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpdateAvailable
        {
            get
            {
                return _isUpdateAvailable;
            }
            set
            {
                if( value == _isUpdateAvailable )
                {
                    return;
                }

                _isUpdateAvailable = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The update release URL
        /// </summary>
        private string _updateReleaseUrl;

        /// <summary>
        /// Gets the update release URL.
        /// </summary>
        /// <value>
        /// The update release URL.
        /// </value>
        public string UpdateReleaseUrl
        {
            get
            {
                return _updateReleaseUrl;
            }
            private set
            {
                if( value == _updateReleaseUrl )
                {
                    return;
                }

                _updateReleaseUrl = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The profile files
        /// </summary>
        private ICollectionView _profileFiles;

        /// <summary>
        /// Gets the profile files.
        /// </summary>
        /// <value>
        /// The profile files.
        /// </value>
        public ICollectionView ProfileFiles
        {
            get
            {
                return _profileFiles;
            }
            private set
            {
                if( value == _profileFiles )
                {
                    return;
                }

                _profileFiles = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The selected profile file
        /// </summary>
        private ProfileFileInfo _selectedProfileFile;

        /// <summary>
        /// Gets or sets the selected profile file.
        /// </summary>
        /// <value>
        /// The selected profile file.
        /// </value>
        public ProfileFileInfo SelectedProfileFile
        {
            get
            {
                return _selectedProfileFile;
            }
            set
            {
                if( _isProfileFilesLoading )
                {
                    return;
                }

                if( value != null
                    && value.Equals( _selectedProfileFile ) )
                {
                    return;
                }

                _selectedProfileFile = value;
                if( value != null )
                {
                    if( !_isProfileFileUpdating )
                    {
                        LoadProfile( value );
                    }

                    ConfigurationManager.Current.ProfileManagerShowUnlock =
                        value.IsEncrypted && !value.IsPasswordValid;

                    SettingsManager.Current.Profiles_LastSelected = value.Name;
                }

                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The is profile file drop down opened
        /// </summary>
        private bool _isProfileFileDropDownOpened;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is profile file drop down opened.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is profile file drop down opened; otherwise, <c>false</c>.
        /// </value>
        public bool IsProfileFileDropDownOpened
        {
            get
            {
                return _isProfileFileDropDownOpened;
            }
            set
            {
                if( value == _isProfileFileDropDownOpened )
                {
                    return;
                }

                _isProfileFileDropDownOpened = value;
                OnPropertyChanged( );
            }
        }
        #endregion

        #region Constructor, window load and close events
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow( )
        {
            _isLoading = true;
            InitializeComponent( );
            DataContext = this;
            LanguageProperty.OverrideMetadata( typeof( FrameworkElement ),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage( LocalizationManager.GetInstance( ).Culture
                        .IetfLanguageTag ) ) );

            AppearanceManager.Load( );
            ConfigureDNS( );
            Title = $"NETworkManager {AssemblyManager.Current.Version}";
            SettingsManager.Current.PropertyChanged += SettingsManager_PropertyChanged;
            EventSystem.OnRedirectDataToApplicationEvent +=
                EventSystem_RedirectDataToApplicationEvent;

            EventSystem.OnRedirectToSettingsEvent += EventSystem_RedirectToSettingsEvent;
            _isLoading = false;
        }

        /// <summary>
        /// Handles the ContentRendered event of the MetroMainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MetroMainWindow_ContentRendered( object sender, EventArgs e )
        {
            WelcomeThenLoadAsync( );
        }

        /// <summary>
        /// Welcomes the then load asynchronous.
        /// </summary>
        private async void WelcomeThenLoadAsync( )
        {
            if( ConfigurationManager.Current.ShowSettingsResetNoteOnStartup )
            {
                var settings = AppearanceManager.MetroDialog;
                settings.AffirmativeButtonText = Strings.OK;
                await this.ShowMessageAsync( Strings.SettingsHaveBeenReset,
                    Strings.SettingsFileFoundWasCorruptOrNotCompatibleMessage,
                    MessageDialogStyle.Affirmative, settings );
            }

            if( SettingsManager.Current.WelcomeDialog_Show )
            {
                var customDialog = new CustomDialog
                {
                    Title = Strings.Welcome
                };

                var welcomeViewModel = new WelcomeViewModel( async instance =>
                {
                    await this.HideMetroDialogAsync( customDialog );
                    SettingsManager.Current.Update_CheckForUpdatesAtStartup =
                        instance.CheckForUpdatesAtStartup;

                    SettingsManager.Current.Dashboard_CheckPublicIPAddress =
                        instance.CheckPublicIPAddress;

                    SettingsManager.Current.Dashboard_CheckIPApiIPGeolocation =
                        instance.CheckIPApiIPGeolocation;

                    SettingsManager.Current.Dashboard_CheckIPApiDNSResolver =
                        instance.CheckIPApiDNSResolver;

                    SettingsManager.Current.Traceroute_CheckIPApiIPGeolocation =
                        instance.CheckIPApiIPGeolocation;

                    SettingsManager.Current.Appearance_PowerShellModifyGlobalProfile =
                        instance.PowerShellModifyGlobalProfile;

                    SettingsManager.Current.General_ApplicationList =
                        new ObservableSetCollection<ApplicationInfo>(
                            ApplicationManager.GetDefaultList( ) );

                    SettingsManager.Current.IPScanner_CustomCommands =
                        new ObservableCollection<CustomCommandInfo>(
                            IPScannerCustomCommand.GetDefaultList( ) );

                    SettingsManager.Current.PortScanner_PortProfiles =
                        new ObservableCollection<PortProfileInfo>( PortProfile.GetDefaultList( ) );

                    SettingsManager.Current.DNSLookup_DNSServers =
                        new ObservableCollection<DNSServerConnectionInfoProfile>(
                            DNSServer.GetDefaultList( ) );

                    SettingsManager.Current.AWSSessionManager_AWSProfiles =
                        new ObservableCollection<AWSProfileInfo>( AWSProfile.GetDefaultList( ) );

                    SettingsManager.Current.SNMP_OidProfiles =
                        new ObservableCollection<SNMPOIDProfileInfo>(
                            SNMPOIDProfile.GetDefaultList( ) );

                    SettingsManager.Current.SNTPLookup_SNTPServers =
                        new ObservableCollection<ServerConnectionInfoProfile>(
                            SNTPServer.GetDefaultList( ) );

                    foreach( var file in
                        PowerShell.GetDefaultInstallationPaths.Where( File.Exists ) )
                    {
                        SettingsManager.Current.PowerShell_ApplicationFilePath = file;
                        SettingsManager.Current.AWSSessionManager_ApplicationFilePath = file;
                        break;
                    }

                    foreach( var file in PuTTY.GetDefaultInstallationPaths.Where( File.Exists ) )
                    {
                        SettingsManager.Current.PuTTY_ApplicationFilePath = file;
                        break;
                    }

                    SettingsManager.Current.WelcomeDialog_Show = false;
                    SettingsManager.Save( );
                    Load( );
                } );

                customDialog.Content = new WelcomeDialog
                {
                    DataContext = welcomeViewModel
                };

                await this.ShowMetroDialogAsync( customDialog ).ConfigureAwait( true );
            }
            else
            {
                Load( );
            }
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        private void Load( )
        {
            LoadApplicationList( );
            SetRunCommandsView( );
            LoadProfiles( );
            if( SettingsManager.Current.TrayIcon_AlwaysShowIcon )
            {
                InitNotifyIcon( );
            }

            if( CommandLineManager.Current.Autostart
                && SettingsManager.Current.Autostart_StartMinimizedInTray )
            {
                HideWindowToTray( );
            }

            _statusWindow = new StatusWindow( this );
            NetworkChange.NetworkAvailabilityChanged += ( _, _ ) => OnNetworkHasChanged( );
            NetworkChange.NetworkAddressChanged += ( _, _ ) => OnNetworkHasChanged( );
            WriteDefaultPowerShellProfileToRegistry( );
            if( SettingsManager.Current.Update_CheckForUpdatesAtStartup )
            {
                CheckForUpdates( );
            }
        }

        /// <summary>
        /// Handles the Closing event of the MetroWindowMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        private async void MetroWindowMain_Closing( object sender, CancelEventArgs e )
        {
            if( !_isClosing )
            {
                if( SettingsManager.Current.Window_MinimizeInsteadOfTerminating
                    && WindowState != WindowState.Minimized )
                {
                    e.Cancel = true;
                    WindowState = WindowState.Minimized;
                    return;
                }

                if( SettingsManager.Current.Window_ConfirmClose )
                {
                    e.Cancel = true;
                    if( WindowState == WindowState.Minimized )
                    {
                        BringWindowToFront( );
                    }

                    var settings = AppearanceManager.MetroDialog;
                    settings.AffirmativeButtonText = Strings.Close;
                    settings.NegativeButtonText = Strings.Cancel;
                    settings.DefaultButtonFocus = MessageDialogResult.Affirmative;
                    ConfigurationManager.OnDialogOpen( );
                    var result = await this.ShowMessageAsync( Strings.ConfirmClose,
                        Strings.ConfirmCloseMessage, MessageDialogStyle.AffirmativeAndNegative,
                        settings );

                    ConfigurationManager.OnDialogClose( );
                    if( result != MessageDialogResult.Affirmative )
                    {
                        return;
                    }

                    _isClosing = true;
                    Close( );
                    return;
                }
            }

            if( _registeredHotKeys.Count > 0 )
            {
                UnregisterHotKeys( );
            }

            _notifyIcon?.Dispose( );
        }
        #endregion

        #region Application
        /// <summary>
        /// Loads the application list.
        /// </summary>
        private void LoadApplicationList( )
        {
            _isApplicationViewLoading = true;
            Applications = new CollectionViewSource
            {
                Source = SettingsManager.Current.General_ApplicationList
            }.View;

            Applications.Filter = o =>
            {
                if( o is not ApplicationInfo info )
                {
                    return false;
                }

                if( string.IsNullOrEmpty( ApplicationSearch ) )
                {
                    return info.IsVisible;
                }

                var regex = new Regex( @" |-" );
                var search = regex.Replace( ApplicationSearch, "" );
                return info.IsVisible
                    && ( regex
                        .Replace(
                            ResourceTranslator.Translate( ResourceIdentifier.ApplicationName,
                                info.Name ), "" )
                        .IndexOf( search, StringComparison.OrdinalIgnoreCase ) > -1 || regex
                        .Replace( info.Name.ToString( ), "" )
                        .Contains( search, StringComparison.OrdinalIgnoreCase ) );
            };

            SettingsManager.Current.General_ApplicationList.CollectionChanged +=
                ( _, _ ) => Applications.Refresh( );

            _isApplicationViewLoading = false;
            var applicationList = Applications.Cast<ApplicationInfo>( ).ToArray( );
            if( CommandLineManager.Current.Application != ApplicationName.None )
            {
                SelectedApplication = applicationList.FirstOrDefault( x =>
                    x.Name == CommandLineManager.Current.Application );
            }
            else
            {
                SelectedApplication = applicationList.FirstOrDefault( x => x.IsDefault )
                    ?? applicationList.FirstOrDefault( x => x.IsVisible );
            }

            if( SelectedApplication != null )
            {
                ListViewApplication.ScrollIntoView( SelectedApplication );
            }

            ApplicationViewIsExpanded = SettingsManager.Current.ExpandApplicationView;
        }

        /// <summary>
        /// The dashboard view
        /// </summary>
        private DashboardView _dashboardView;

        /// <summary>
        /// The network interface view
        /// </summary>
        private NetworkInterfaceView _networkInterfaceView;

        /// <summary>
        /// The wi fi view
        /// </summary>
        private WiFiView _wiFiView;

        /// <summary>
        /// The ip scanner host view
        /// </summary>
        private IPScannerHostView _ipScannerHostView;

        /// <summary>
        /// The port scanner host view
        /// </summary>
        private PortScannerHostView _portScannerHostView;

        /// <summary>
        /// The ping monitor host view
        /// </summary>
        private PingMonitorHostView _pingMonitorHostView;

        /// <summary>
        /// The traceroute host view
        /// </summary>
        private TracerouteHostView _tracerouteHostView;

        /// <summary>
        /// The DNS lookup host view
        /// </summary>
        private DNSLookupHostView _dnsLookupHostView;

        /// <summary>
        /// The remote desktop host view
        /// </summary>
        private RemoteDesktopHostView _remoteDesktopHostView;

        /// <summary>
        /// The power shell host view
        /// </summary>
        private PowerShellHostView _powerShellHostView;

        /// <summary>
        /// The putty host view
        /// </summary>
        private PuTTYHostView _puttyHostView;

        /// <summary>
        /// The aws session manager host view
        /// </summary>
        private AWSSessionManagerHostView _awsSessionManagerHostView;

        /// <summary>
        /// The tiger VNC host view
        /// </summary>
        private TigerVNCHostView _tigerVNCHostView;

        /// <summary>
        /// The web console host view
        /// </summary>
        private WebConsoleHostView _webConsoleHostView;

        /// <summary>
        /// The SNMP host view
        /// </summary>
        private SNMPHostView _snmpHostView;

        /// <summary>
        /// The SNTP lookup host view
        /// </summary>
        private SNTPLookupHostView _sntpLookupHostView;

        /// <summary>
        /// The discovery protocol view
        /// </summary>
        private DiscoveryProtocolView _discoveryProtocolView;

        /// <summary>
        /// The wake on lan view
        /// </summary>
        private WakeOnLANView _wakeOnLanView;

        /// <summary>
        /// The subnet calculator host view
        /// </summary>
        private SubnetCalculatorHostView _subnetCalculatorHostView;

        /// <summary>
        /// The bit calculator view
        /// </summary>
        private BitCalculatorView _bitCalculatorView;

        /// <summary>
        /// The lookup host view
        /// </summary>
        private LookupHostView _lookupHostView;

        /// <summary>
        /// The whois host view
        /// </summary>
        private WhoisHostView _whoisHostView;

        /// <summary>
        /// The ip geolocation host view
        /// </summary>
        private IPGeolocationHostView _ipGeolocationHostView;

        /// <summary>
        /// The connections view
        /// </summary>
        private ConnectionsView _connectionsView;

        /// <summary>
        /// The listeners view
        /// </summary>
        private ListenersView _listenersView;

        /// <summary>
        /// The arp table view
        /// </summary>
        private ARPTableView _arpTableView;

        /// <summary>
        /// Called when [application view visible].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="fromSettings">if set to <c>true</c> [from settings].</param>
        private void OnApplicationViewVisible( ApplicationName name, bool fromSettings = false )
        {
            switch( name )
            {
                case ApplicationName.Dashboard:
                    if( _dashboardView == null )
                    {
                        _dashboardView = new DashboardView( );
                    }
                    else
                    {
                        _dashboardView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _dashboardView;
                    break;
                case ApplicationName.NetworkInterface:
                    if( _networkInterfaceView == null )
                    {
                        _networkInterfaceView = new NetworkInterfaceView( );
                    }
                    else
                    {
                        _networkInterfaceView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _networkInterfaceView;
                    break;
                case ApplicationName.WiFi:
                    if( _wiFiView == null )
                    {
                        _wiFiView = new WiFiView( );
                    }
                    else
                    {
                        _wiFiView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _wiFiView;
                    break;
                case ApplicationName.IPScanner:
                    if( _ipScannerHostView == null )
                    {
                        _ipScannerHostView = new IPScannerHostView( );
                    }
                    else
                    {
                        _ipScannerHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _ipScannerHostView;
                    break;
                case ApplicationName.PortScanner:
                    if( _portScannerHostView == null )
                    {
                        _portScannerHostView = new PortScannerHostView( );
                    }
                    else
                    {
                        _portScannerHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _portScannerHostView;
                    break;
                case ApplicationName.PingMonitor:
                    if( _pingMonitorHostView == null )
                    {
                        _pingMonitorHostView = new PingMonitorHostView( );
                    }
                    else
                    {
                        _pingMonitorHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _pingMonitorHostView;
                    break;
                case ApplicationName.Traceroute:
                    if( _tracerouteHostView == null )
                    {
                        _tracerouteHostView = new TracerouteHostView( );
                    }
                    else
                    {
                        _tracerouteHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _tracerouteHostView;
                    break;
                case ApplicationName.DNSLookup:
                    if( _dnsLookupHostView == null )
                    {
                        _dnsLookupHostView = new DNSLookupHostView( );
                    }
                    else
                    {
                        _dnsLookupHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _dnsLookupHostView;
                    break;
                case ApplicationName.RemoteDesktop:
                    if( _remoteDesktopHostView == null )
                    {
                        _remoteDesktopHostView = new RemoteDesktopHostView( );
                    }
                    else
                    {
                        _remoteDesktopHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _remoteDesktopHostView;
                    break;
                case ApplicationName.PowerShell:
                    if( _powerShellHostView == null )
                    {
                        _powerShellHostView = new PowerShellHostView( );
                    }
                    else
                    {
                        _powerShellHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _powerShellHostView;
                    break;
                case ApplicationName.PuTTY:
                    if( _puttyHostView == null )
                    {
                        _puttyHostView = new PuTTYHostView( );
                    }
                    else
                    {
                        _puttyHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _puttyHostView;
                    break;
                case ApplicationName.AWSSessionManager:
                    if( _awsSessionManagerHostView == null )
                    {
                        _awsSessionManagerHostView = new AWSSessionManagerHostView( );
                    }
                    else
                    {
                        _awsSessionManagerHostView.OnViewVisible( fromSettings );
                    }

                    ContentControlApplication.Content = _awsSessionManagerHostView;
                    break;
                case ApplicationName.TigerVNC:
                    if( _tigerVNCHostView == null )
                    {
                        _tigerVNCHostView = new TigerVNCHostView( );
                    }
                    else
                    {
                        _tigerVNCHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _tigerVNCHostView;
                    break;
                case ApplicationName.WebConsole:
                    if( _webConsoleHostView == null )
                    {
                        _webConsoleHostView = new WebConsoleHostView( );
                    }
                    else
                    {
                        _webConsoleHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _webConsoleHostView;
                    break;
                case ApplicationName.SNMP:
                    if( _snmpHostView == null )
                    {
                        _snmpHostView = new SNMPHostView( );
                    }
                    else
                    {
                        _snmpHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _snmpHostView;
                    break;
                case ApplicationName.SNTPLookup:
                    if( _sntpLookupHostView == null )
                    {
                        _sntpLookupHostView = new SNTPLookupHostView( );
                    }
                    else
                    {
                        _sntpLookupHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _sntpLookupHostView;
                    break;
                case ApplicationName.DiscoveryProtocol:
                    if( _discoveryProtocolView == null )
                    {
                        _discoveryProtocolView = new DiscoveryProtocolView( );
                    }
                    else
                    {
                        _discoveryProtocolView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _discoveryProtocolView;
                    break;
                case ApplicationName.WakeOnLAN:
                    if( _wakeOnLanView == null )
                    {
                        _wakeOnLanView = new WakeOnLANView( );
                    }
                    else
                    {
                        _wakeOnLanView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _wakeOnLanView;
                    break;
                case ApplicationName.Whois:
                    if( _whoisHostView == null )
                    {
                        _whoisHostView = new WhoisHostView( );
                    }
                    else
                    {
                        _whoisHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _whoisHostView;
                    break;
                case ApplicationName.IPGeolocation:
                    if( _ipGeolocationHostView == null )
                    {
                        _ipGeolocationHostView = new IPGeolocationHostView( );
                    }
                    else
                    {
                        _ipGeolocationHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _ipGeolocationHostView;
                    break;
                case ApplicationName.SubnetCalculator:
                    if( _subnetCalculatorHostView == null )
                    {
                        _subnetCalculatorHostView = new SubnetCalculatorHostView( );
                    }
                    else
                    {
                        _subnetCalculatorHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _subnetCalculatorHostView;
                    break;
                case ApplicationName.BitCalculator:
                    if( _bitCalculatorView == null )
                    {
                        _bitCalculatorView = new BitCalculatorView( );
                    }
                    else
                    {
                        _bitCalculatorView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _bitCalculatorView;
                    break;
                case ApplicationName.Lookup:
                    if( _lookupHostView == null )
                    {
                        _lookupHostView = new LookupHostView( );
                    }
                    else
                    {
                        _lookupHostView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _lookupHostView;
                    break;
                case ApplicationName.Connections:
                    if( _connectionsView == null )
                    {
                        _connectionsView = new ConnectionsView( );
                    }
                    else
                    {
                        _connectionsView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _connectionsView;
                    break;
                case ApplicationName.Listeners:
                    if( _listenersView == null )
                    {
                        _listenersView = new ListenersView( );
                    }
                    else
                    {
                        _listenersView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _listenersView;
                    break;
                case ApplicationName.ARPTable:
                    if( _arpTableView == null )
                    {
                        _arpTableView = new ARPTableView( );
                    }
                    else
                    {
                        _arpTableView.OnViewVisible( );
                    }

                    ContentControlApplication.Content = _arpTableView;
                    break;
            }
        }

        /// <summary>
        /// Called when [application view hide].
        /// </summary>
        /// <param name="name">The name.</param>
        private void OnApplicationViewHide( ApplicationName name )
        {
            switch( name )
            {
                case ApplicationName.Dashboard:
                    _dashboardView?.OnViewHide( );
                    break;
                case ApplicationName.NetworkInterface:
                    _networkInterfaceView?.OnViewHide( );
                    break;
                case ApplicationName.WiFi:
                    _wiFiView?.OnViewHide( );
                    break;
                case ApplicationName.IPScanner:
                    _ipScannerHostView?.OnViewHide( );
                    break;
                case ApplicationName.PortScanner:
                    _portScannerHostView?.OnViewHide( );
                    break;
                case ApplicationName.PingMonitor:
                    _pingMonitorHostView?.OnViewHide( );
                    break;
                case ApplicationName.Traceroute:
                    _tracerouteHostView?.OnViewHide( );
                    break;
                case ApplicationName.DNSLookup:
                    _dnsLookupHostView?.OnViewHide( );
                    break;
                case ApplicationName.RemoteDesktop:
                    _remoteDesktopHostView?.OnViewHide( );
                    break;
                case ApplicationName.PowerShell:
                    _powerShellHostView?.OnViewHide( );
                    break;
                case ApplicationName.PuTTY:
                    _puttyHostView?.OnViewHide( );
                    break;
                case ApplicationName.AWSSessionManager:
                    _awsSessionManagerHostView?.OnViewHide( );
                    break;
                case ApplicationName.TigerVNC:
                    _tigerVNCHostView?.OnViewHide( );
                    break;
                case ApplicationName.WebConsole:
                    _webConsoleHostView?.OnViewHide( );
                    break;
                case ApplicationName.SNMP:
                    _snmpHostView?.OnViewHide( );
                    break;
                case ApplicationName.SNTPLookup:
                    _sntpLookupHostView?.OnViewHide( );
                    break;
                case ApplicationName.DiscoveryProtocol:
                    _discoveryProtocolView?.OnViewHide( );
                    break;
                case ApplicationName.WakeOnLAN:
                    _wakeOnLanView?.OnViewHide( );
                    break;
                case ApplicationName.Whois:
                    _whoisHostView?.OnViewHide( );
                    break;
                case ApplicationName.IPGeolocation:
                    _ipGeolocationHostView?.OnViewHide( );
                    break;
                case ApplicationName.Lookup:
                    _lookupHostView?.OnViewHide( );
                    break;
                case ApplicationName.SubnetCalculator:
                    _subnetCalculatorHostView?.OnViewHide( );
                    break;
                case ApplicationName.BitCalculator:
                    _bitCalculatorView?.OnViewHide( );
                    break;
                case ApplicationName.Connections:
                    _connectionsView?.OnViewHide( );
                    break;
                case ApplicationName.Listeners:
                    _listenersView?.OnViewHide( );
                    break;
                case ApplicationName.ARPTable:
                    _arpTableView?.OnViewHide( );
                    break;
            }
        }

        /// <summary>
        /// Clears the search on application list minimize.
        /// </summary>
        private void ClearSearchOnApplicationListMinimize( )
        {
            if( ApplicationViewIsExpanded )
            {
                return;
            }

            if( ApplicationViewIsOpen && TextBoxApplicationSearchIsFocused )
            {
                return;
            }

            if( ApplicationViewIsOpen && ApplicationViewIsMouseOver )
            {
                return;
            }

            ApplicationSearch = string.Empty;
            ListViewApplication.ScrollIntoView( SelectedApplication );
        }

        /// <summary>
        /// Handles the RedirectDataToApplicationEvent event of the EventSystem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private async void EventSystem_RedirectDataToApplicationEvent( object sender, EventArgs e )
        {
            if( e is not EventSystemRedirectArgs data )
            {
                return;
            }

            var application = Applications.Cast<ApplicationInfo>( )
                .FirstOrDefault( x => x.Name == data.Application );

            if( application == null )
            {
                var settings = AppearanceManager.MetroDialog;
                settings.AffirmativeButtonText = Strings.OK;
                await this.ShowMessageAsync( Strings.Error,
                    string.Format( Strings.CouldNotFindApplicationXXXMessage,
                        data.Application.ToString( ) ) );

                return;
            }

            SelectedApplication = application;
            if( string.IsNullOrEmpty( data.Args ) )
            {
                return;
            }

            switch( data.Application )
            {
                case ApplicationName.Dashboard:
                    break;
                case ApplicationName.NetworkInterface:
                    break;
                case ApplicationName.WiFi:
                    break;
                case ApplicationName.IPScanner:
                    _ipScannerHostView.AddTab( data.Args );
                    break;
                case ApplicationName.PortScanner:
                    _portScannerHostView.AddTab( data.Args );
                    break;
                case ApplicationName.PingMonitor:
                    _pingMonitorHostView.AddHost( data.Args );
                    break;
                case ApplicationName.Traceroute:
                    _tracerouteHostView.AddTab( data.Args );
                    break;
                case ApplicationName.DNSLookup:
                    _dnsLookupHostView.AddTab( data.Args );
                    break;
                case ApplicationName.RemoteDesktop:
                    _remoteDesktopHostView.AddTab( data.Args );
                    break;
                case ApplicationName.PowerShell:
                    _powerShellHostView.AddTab( data.Args );
                    break;
                case ApplicationName.PuTTY:
                    _puttyHostView.AddTab( data.Args );
                    break;
                case ApplicationName.AWSSessionManager:
                    break;
                case ApplicationName.TigerVNC:
                    _tigerVNCHostView.AddTab( data.Args );
                    break;
                case ApplicationName.WebConsole:
                    break;
                case ApplicationName.SNMP:
                    _snmpHostView.AddTab( data.Args );
                    break;
                case ApplicationName.SNTPLookup:
                    break;
                case ApplicationName.DiscoveryProtocol:
                    break;
                case ApplicationName.WakeOnLAN:
                    break;
                case ApplicationName.Whois:
                    break;
                case ApplicationName.IPGeolocation:
                    break;
                case ApplicationName.SubnetCalculator:
                    break;
                case ApplicationName.BitCalculator:
                    break;
                case ApplicationName.Lookup:
                    break;
                case ApplicationName.Connections:
                    break;
                case ApplicationName.Listeners:
                    break;
                case ApplicationName.ARPTable:
                    break;
                case ApplicationName.None:
                default:
                    Log.Error( $"Cannot redirect data to unknown application: {data.Application}" );
                    break;
            }
        }

        /// <summary>
        /// Handles the OnPreviewExecuted event of the TextBoxApplicationSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs" /> instance containing the event data.</param>
        private void TextBoxApplicationSearch_OnPreviewExecuted( object sender,
            ExecutedRoutedEventArgs e )
        {
            if( e.Command == ApplicationCommands.Copy
                || e.Command == ApplicationCommands.Cut
                || e.Command == ApplicationCommands.Paste )
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Run Command
        #region Variables
        /// <summary>
        /// The run commands
        /// </summary>
        private ICollectionView _runCommands;

        /// <summary>
        /// Gets the run commands.
        /// </summary>
        /// <value>
        /// The run commands.
        /// </value>
        public ICollectionView RunCommands
        {
            get
            {
                return _runCommands;
            }
            private set
            {
                if( value == _runCommands )
                {
                    return;
                }

                _runCommands = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The selected run command
        /// </summary>
        private RunCommandInfo _selectedRunCommand;

        /// <summary>
        /// Gets or sets the selected run command.
        /// </summary>
        /// <value>
        /// The selected run command.
        /// </value>
        public RunCommandInfo SelectedRunCommand
        {
            get
            {
                return _selectedRunCommand;
            }
            set
            {
                if( value == _selectedRunCommand )
                {
                    return;
                }

                _selectedRunCommand = value;
                OnPropertyChanged( );
            }
        }

        /// <summary>
        /// The run command search
        /// </summary>
        private string _runCommandSearch;

        /// <summary>
        /// Gets or sets the run command search.
        /// </summary>
        /// <value>
        /// The run command search.
        /// </value>
        public string RunCommandSearch
        {
            get
            {
                return _runCommandSearch;
            }
            set
            {
                if( value == _runCommandSearch )
                {
                    return;
                }

                _runCommandSearch = value;
                RefreshRunCommandsView( );
                OnPropertyChanged( );
            }
        }
        #endregion

        #region ICommands & Actions
        /// <summary>
        /// Gets the open run command.
        /// </summary>
        /// <value>
        /// The open run command.
        /// </value>
        public ICommand OpenRunCommand
        {
            get
            {
                return new RelayCommand( _ => OpenRunAction( ) );
            }
        }

        /// <summary>
        /// Opens the run action.
        /// </summary>
        private void OpenRunAction( )
        {
            ConfigurationManager.OnDialogOpen( );
            FlyoutRunCommandAreAnimationsEnabled = true;
            FlyoutRunCommandIsOpen = true;
        }

        /// <summary>
        /// Gets the run command do command.
        /// </summary>
        /// <value>
        /// The run command do command.
        /// </value>
        public ICommand RunCommandDoCommand
        {
            get
            {
                return new RelayCommand( _ => RunCommandDoAction( ) );
            }
        }

        /// <summary>
        /// Runs the command do action.
        /// </summary>
        private void RunCommandDoAction( )
        {
            RunCommandDo( );
        }

        /// <summary>
        /// Gets the run command close command.
        /// </summary>
        /// <value>
        /// The run command close command.
        /// </value>
        public ICommand RunCommandCloseCommand
        {
            get
            {
                return new RelayCommand( _ => RunCommandCloseAction( ) );
            }
        }

        /// <summary>
        /// Runs the command close action.
        /// </summary>
        private void RunCommandCloseAction( )
        {
            RunCommandFlyoutClose( ).ConfigureAwait( false );
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the run commands view.
        /// </summary>
        /// <param name="selectedRunCommand">The selected run command.</param>
        private void SetRunCommandsView( RunCommandInfo selectedRunCommand = null )
        {
            RunCommands = new CollectionViewSource
            {
                Source = RunCommandManager.GetList( )
            }.View;

            RunCommands.Filter = o =>
            {
                if( o is not RunCommandInfo info )
                {
                    return false;
                }

                if( string.IsNullOrEmpty( RunCommandSearch ) )
                {
                    return true;
                }

                return info.TranslatedName.IndexOf( RunCommandSearch,
                        StringComparison.OrdinalIgnoreCase ) > -1
                    || info.Name.IndexOf( RunCommandSearch, StringComparison.OrdinalIgnoreCase )
                    > -1;
            };

            if( selectedRunCommand != null )
            {
                SelectedRunCommand = RunCommands.Cast<RunCommandInfo>( )
                    .FirstOrDefault( x => x.Name == selectedRunCommand.Name );
            }

            SelectedRunCommand ??= RunCommands.Cast<RunCommandInfo>( ).FirstOrDefault( );
        }

        /// <summary>
        /// Refreshes the run commands view.
        /// </summary>
        private void RefreshRunCommandsView( )
        {
            SetRunCommandsView( SelectedRunCommand );
        }

        /// <summary>
        /// Runs the command do.
        /// </summary>
        private void RunCommandDo( )
        {
            if( SelectedRunCommand == null )
            {
                return;
            }

            switch( SelectedRunCommand.Type )
            {
                case RunCommandType.Application:
                    if( SettingsViewIsOpen )
                    {
                        CloseSettings( );
                    }

                    var applicationName = ( ApplicationName )Enum.Parse( typeof( ApplicationName ),
                        SelectedRunCommand.Name );

                    EventSystem.RedirectToApplication( applicationName, string.Empty );
                    break;
                case RunCommandType.Setting:
                    EventSystem.RedirectToSettings( );
                    break;
            }

            RunCommandFlyoutClose( true ).ConfigureAwait( false );
        }

        /// <summary>
        /// Runs the command flyout close.
        /// </summary>
        /// <param name="clearSearch">if set to <c>true</c> [clear search].</param>
        private async Task RunCommandFlyoutClose( bool clearSearch = false )
        {
            if( !FlyoutRunCommandIsOpen )
            {
                return;
            }

            FlyoutRunCommandAreAnimationsEnabled = false;
            FlyoutRunCommandIsOpen = false;
            ConfigurationManager.OnDialogClose( );
            if( clearSearch )
            {
                await Task.Delay( 500 );
                RunCommandSearch = string.Empty;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the OnPreviewMouseLeftButtonUp event of the ListViewRunCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void ListViewRunCommand_OnPreviewMouseLeftButtonUp( object sender,
            MouseButtonEventArgs e )
        {
            RunCommandDo( );
        }

        /// <summary>
        /// Handles the IsKeyboardFocusWithinChanged event of the FlyoutRunCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private void FlyoutRunCommand_IsKeyboardFocusWithinChanged( object sender,
            DependencyPropertyChangedEventArgs e )
        {
            if( e.NewValue is not false )
            {
                return;
            }

            RunCommandFlyoutClose( ).ConfigureAwait( false );
        }
        #endregion
        #endregion

        #region Settings
        /// <summary>
        /// Opens the settings.
        /// </summary>
        private void OpenSettings( )
        {
            OnApplicationViewHide( SelectedApplication.Name );
            if( _settingsView == null )
            {
                _settingsView = new SettingsView( );
                ContentControlSettings.Content = _settingsView;
            }
            else
            {
                _settingsView.OnViewVisible( );
            }

            _settingsView.ChangeSettingsView( SelectedApplication.Name );
            SettingsViewIsOpen = true;
        }

        /// <summary>
        /// Handles the RedirectToSettingsEvent event of the EventSystem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void EventSystem_RedirectToSettingsEvent( object sender, EventArgs e )
        {
            OpenSettings( );
        }

        /// <summary>
        /// Closes the settings.
        /// </summary>
        private void CloseSettings( )
        {
            SettingsViewIsOpen = false;
            _settingsView.OnViewHide( );
            if( SettingsManager.HotKeysChanged )
            {
                UnregisterHotKeys( );
                RegisterHotKeys( );
                SettingsManager.HotKeysChanged = false;
            }

            OnApplicationViewVisible( SelectedApplication.Name, true );
        }
        #endregion

        #region Profiles
        /// <summary>
        /// Loads the profiles.
        /// </summary>
        private void LoadProfiles( )
        {
            _isProfileFilesLoading = true;
            ProfileFiles = new CollectionViewSource
            {
                Source = ProfileManager.ProfileFiles
            }.View;

            ProfileFiles.SortDescriptions.Add( new SortDescription( nameof( ProfileFileInfo.Name ),
                ListSortDirection.Ascending ) );

            _isProfileFilesLoading = false;
            ProfileManager.OnLoadedProfileFileChangedEvent +=
                ProfileManager_OnLoadedProfileFileChangedEvent;

            SelectedProfileFile = ProfileFiles.SourceCollection.Cast<ProfileFileInfo>( )
                .FirstOrDefault( x => x.Name == SettingsManager.Current.Profiles_LastSelected );

            SelectedProfileFile ??= ProfileFiles.SourceCollection.Cast<ProfileFileInfo>( )
                .FirstOrDefault( );
        }

        /// <summary>
        /// Loads the profile.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="showWrongPassword">if set to <c>true</c> [show wrong password].</param>
        private async void LoadProfile( ProfileFileInfo info, bool showWrongPassword = false )
        {
            ConfigurationManager.Current.ProfileManagerIsEnabled = false;
            ConfigurationManager.Current.ProfileManagerErrorMessage = string.Empty;
            if( info.IsEncrypted
                && !info.IsPasswordValid )
            {
                var customDialog = new CustomDialog
                {
                    Title = Strings.UnlockProfileFile
                };

                var viewModel = new CredentialsPasswordProfileFileViewModel( async instance =>
                {
                    await this.HideMetroDialogAsync( customDialog );
                    ConfigurationManager.OnDialogClose( );
                    info.Password = instance.Password;
                    SwitchProfile( info );
                }, async _ =>
                {
                    await this.HideMetroDialogAsync( customDialog );
                    ConfigurationManager.OnDialogClose( );
                    ProfileManager.Unload( );
                }, info.Name, showWrongPassword );

                customDialog.Content = new CredentialsPasswordProfileFileDialog
                {
                    DataContext = viewModel
                };

                ConfigurationManager.OnDialogOpen( );
                await this.ShowMetroDialogAsync( customDialog );
            }
            else
            {
                SwitchProfile( info );
            }
        }

        /// <summary>
        /// Switches the profile.
        /// </summary>
        /// <param name="info">The information.</param>
        private async void SwitchProfile( ProfileFileInfo info )
        {
            try
            {
                ProfileManager.Switch( info );
                ConfigurationManager.Current.ProfileManagerShowUnlock = false;
                ConfigurationManager.Current.ProfileManagerIsEnabled = true;
                OnProfilesLoaded( SelectedApplication.Name );
            }
            catch( CryptographicException )
            {
                LoadProfile( info, true );
            }
            catch( Exception ex )
            {
                ConfigurationManager.Current.ProfileManagerErrorMessage =
                    Strings.ProfileFileCouldNotBeLoaded;

                var settings = AppearanceManager.MetroDialog;
                settings.AffirmativeButtonText = Strings.OK;
                settings.DefaultButtonFocus = MessageDialogResult.Affirmative;
                ConfigurationManager.OnDialogOpen( );
                await this.ShowMessageAsync( Strings.ProfileFileCouldNotBeLoaded,
                    string.Format( Strings.ProfileFileCouldNotBeLoadedMessage, ex.Message ),
                    MessageDialogStyle.Affirmative, settings );

                ConfigurationManager.OnDialogClose( );
            }
        }

        /// <summary>
        /// Called when [profiles loaded].
        /// </summary>
        /// <param name="name">The name.</param>
        private void OnProfilesLoaded( ApplicationName name )
        {
            switch( name )
            {
                case ApplicationName.AWSSessionManager:
                    _awsSessionManagerHostView?.OnProfileLoaded( );
                    break;
            }
        }

        /// <summary>
        /// Profiles the manager on loaded profile file changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ProfileManager_OnLoadedProfileFileChangedEvent( object sender,
            ProfileFileInfoArgs e )
        {
            _isProfileFileUpdating = e.ProfileFileUpdating;
            SelectedProfileFile = ProfileFiles.SourceCollection.Cast<ProfileFileInfo>( )
                .FirstOrDefault( x => x.Equals( e.ProfileFileInfo ) );

            _isProfileFileUpdating = false;
        }
        #endregion

        #region Update check
        /// <summary>
        /// Checks for updates.
        /// </summary>
        private void CheckForUpdates( )
        {
            var updater = new Updater( );
            updater.UpdateAvailable += Updater_UpdateAvailable;
            updater.CheckOnGitHub( Properties.Resources.Ninja_GitHub_User,
                Properties.Resources.Ninja_GitHub_Repo, AssemblyManager.Current.Version,
                SettingsManager.Current.Update_CheckForPreReleases );
        }

        /// <summary>
        /// Updaters the update available.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Updater_UpdateAvailable( object sender, UpdateAvailableArgs e )
        {
            UpdateReleaseUrl = e.Release.Prerelease
                ? e.Release.HtmlUrl
                : Properties.Resources.Ninja_LatestReleaseUrl;

            IsUpdateAvailable = true;
        }
        #endregion

        #region Handle WndProc messages (Single instance, handle HotKeys)
        /// <summary>
        /// The HWND source
        /// </summary>
        private HwndSource _hwndSource;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.SourceInitialized" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSourceInitialized( EventArgs e )
        {
            base.OnSourceInitialized( e );
            _hwndSource = HwndSource.FromHwnd( new WindowInteropHelper( this ).Handle );
            _hwndSource?.AddHook( HwndHook );
            RegisterHotKeys( );
        }

        /// <summary>
        /// HWNDs the hook.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns></returns>
        [ DebuggerStepThrough ]
        private IntPtr HwndHook( IntPtr hwnd, int msg, IntPtr wParam,
            IntPtr lParam, ref bool handled )
        {
            if( msg == SingleInstance.WM_SHOWME
                || ( msg == WmHotkey && wParam.ToInt32( ) == 1 ) )
            {
                ShowWindow( );
                handled = true;
            }

            return IntPtr.Zero;
        }
        #endregion

        #region Global HotKeys
        /// <summary>
        /// Registers the hot key.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="fsModifiers">The fs modifiers.</param>
        /// <param name="vk">The vk.</param>
        /// <returns></returns>
        [ DllImport( "user32.dll" ) ]
        private static extern bool RegisterHotKey( IntPtr hWnd, int id, int fsModifiers,
            int vk );

        /// <summary>
        /// Unregisters the hot key.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ DllImport( "user32.dll" ) ]
        private static extern bool UnregisterHotKey( IntPtr hWnd, int id );

        /// <summary>
        /// The wm hotkey
        /// </summary>
        private const int WmHotkey = 0x0312;

        /// <summary>
        /// The registered hot keys
        /// </summary>
        private readonly List<int> _registeredHotKeys = new List<int>( );

        /// <summary>
        /// Registers the hot keys.
        /// </summary>
        private void RegisterHotKeys( )
        {
            if( SettingsManager.Current.HotKey_ShowWindowEnabled )
            {
                MainWindow.RegisterHotKey( new WindowInteropHelper( this ).Handle, 1,
                    SettingsManager.Current.HotKey_ShowWindowModifier,
                    SettingsManager.Current.HotKey_ShowWindowKey );

                _registeredHotKeys.Add( 1 );
            }
        }

        /// <summary>
        /// Unregisters the hot keys.
        /// </summary>
        private void UnregisterHotKeys( )
        {
            foreach( var i in _registeredHotKeys )
            {
                MainWindow.UnregisterHotKey( new WindowInteropHelper( this ).Handle, i );
            }

            _registeredHotKeys.Clear( );
        }
        #endregion

        #region NotifyIcon
        /// <summary>
        /// Initializes the notify icon.
        /// </summary>
        private void InitNotifyIcon( )
        {
            _notifyIcon = new NotifyIcon( );
            using( var iconStream = Application
                .GetResourceStream( new Uri( "pack://application:,,,/Ninja.ico" ) )?.Stream )
            {
                if( iconStream != null )
                {
                    _notifyIcon.Icon = new Icon( iconStream );
                }
            }

            _notifyIcon.Text = Title;
            _notifyIcon.Click += NotifyIcon_Click;
            _notifyIcon.MouseDown += NotifyIcon_MouseDown;
            _notifyIcon.Visible = true;
        }

        /// <summary>
        /// Handles the MouseDown event of the NotifyIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void NotifyIcon_MouseDown( object sender, MouseEventArgs e )
        {
            if( e.Button != MouseButtons.Right )
            {
                return;
            }

            var trayMenu = ( ContextMenu )FindResource( "ContextMenuNotifyIcon" );
            trayMenu.IsOpen = true;
        }

        /// <summary>
        /// Handles the Click event of the NotifyIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void NotifyIcon_Click( object sender, EventArgs e )
        {
            var mouse = ( MouseEventArgs )e;
            if( mouse.Button != MouseButtons.Left )
            {
                return;
            }

            if( OpenStatusWindowCommand.CanExecute( null ) )
            {
                OpenStatusWindowCommand.Execute( null );
            }
        }

        /// <summary>
        /// Handles the StateChanged event of the MetroWindowMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MetroWindowMain_StateChanged( object sender, EventArgs e )
        {
            if( WindowState != WindowState.Minimized )
            {
                return;
            }

            if( SettingsManager.Current.Window_MinimizeToTrayInsteadOfTaskbar )
            {
                HideWindowToTray( );
            }
        }

        /// <summary>
        /// Handles the Opened event of the ContextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ContextMenu_Opened( object sender, RoutedEventArgs e )
        {
            if( sender is ContextMenu menu )
            {
                menu.DataContext = this;
            }
        }
        #endregion

        #region ICommands & Actions
        /// <summary>
        /// Gets the open status window command.
        /// </summary>
        /// <value>
        /// The open status window command.
        /// </value>
        public ICommand OpenStatusWindowCommand
        {
            get
            {
                return new RelayCommand( _ => OpenStatusWindowAction( ) );
            }
        }

        /// <summary>
        /// Opens the status window action.
        /// </summary>
        private void OpenStatusWindowAction( )
        {
            OpenStatusWindow( );
        }

        /// <summary>
        /// Gets the restart application command.
        /// </summary>
        /// <value>
        /// The restart application command.
        /// </value>
        public ICommand RestartApplicationCommand
        {
            get
            {
                return new RelayCommand( _ => RestartApplicationAction( ) );
            }
        }

        /// <summary>
        /// Restarts the application action.
        /// </summary>
        private void RestartApplicationAction( )
        {
            RestartApplication( );
        }

        /// <summary>
        /// Gets the open website command.
        /// </summary>
        /// <value>
        /// The open website command.
        /// </value>
        public ICommand OpenWebsiteCommand
        {
            get
            {
                return new RelayCommand( MainWindow.OpenWebsiteAction );
            }
        }

        /// <summary>
        /// Opens the website action.
        /// </summary>
        /// <param name="url">The URL.</param>
        private static void OpenWebsiteAction( object url )
        {
            ExternalProcessStarter.OpenUrl( ( string )url );
        }

        /// <summary>
        /// Gets the open documentation command.
        /// </summary>
        /// <value>
        /// The open documentation command.
        /// </value>
        public ICommand OpenDocumentationCommand
        {
            get
            {
                return new RelayCommand( _ => OpenDocumentationAction( ) );
            }
        }

        /// <summary>
        /// Opens the documentation action.
        /// </summary>
        private void OpenDocumentationAction( )
        {
            DocumentationManager.OpenDocumentation( SettingsViewIsOpen
                ? _settingsView.GetDocumentationIdentifier( )
                : DocumentationManager.GetIdentifierByApplicationName( SelectedApplication.Name ) );
        }

        /// <summary>
        /// Gets the open application list command.
        /// </summary>
        /// <value>
        /// The open application list command.
        /// </value>
        public ICommand OpenApplicationListCommand
        {
            get
            {
                return new RelayCommand( _ => OpenApplicationListAction( ) );
            }
        }

        /// <summary>
        /// Opens the application list action.
        /// </summary>
        private void OpenApplicationListAction( )
        {
            ApplicationViewIsOpen = true;
            TextBoxApplicationSearch.Focus( );
        }

        /// <summary>
        /// Gets the unlock profile command.
        /// </summary>
        /// <value>
        /// The unlock profile command.
        /// </value>
        public ICommand UnlockProfileCommand
        {
            get
            {
                return new RelayCommand( _ => UnlockProfileAction( ) );
            }
        }

        /// <summary>
        /// Unlocks the profile action.
        /// </summary>
        private void UnlockProfileAction( )
        {
            LoadProfile( SelectedProfileFile );
        }

        /// <summary>
        /// Gets the open settings command.
        /// </summary>
        /// <value>
        /// The open settings command.
        /// </value>
        public ICommand OpenSettingsCommand
        {
            get
            {
                return new RelayCommand( _ => OpenSettingsAction( ) );
            }
        }

        /// <summary>
        /// Opens the settings action.
        /// </summary>
        private void OpenSettingsAction( )
        {
            OpenSettings( );
        }

        /// <summary>
        /// Gets the open settings from tray command.
        /// </summary>
        /// <value>
        /// The open settings from tray command.
        /// </value>
        public ICommand OpenSettingsFromTrayCommand
        {
            get
            {
                return new RelayCommand( _ => OpenSettingsFromTrayAction( ) );
            }
        }

        /// <summary>
        /// Opens the settings from tray action.
        /// </summary>
        private void OpenSettingsFromTrayAction( )
        {
            ShowWindow( );
            OpenSettings( );
        }

        /// <summary>
        /// Gets the close settings command.
        /// </summary>
        /// <value>
        /// The close settings command.
        /// </value>
        public ICommand CloseSettingsCommand
        {
            get
            {
                return new RelayCommand( _ => CloseSettingsAction( ) );
            }
        }

        /// <summary>
        /// Closes the settings action.
        /// </summary>
        private void CloseSettingsAction( )
        {
            CloseSettings( );
        }

        /// <summary>
        /// Gets the show window command.
        /// </summary>
        /// <value>
        /// The show window command.
        /// </value>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new RelayCommand( _ => ShowWindowAction( ) );
            }
        }

        /// <summary>
        /// Shows the window action.
        /// </summary>
        private void ShowWindowAction( )
        {
            ShowWindow( );
        }

        /// <summary>
        /// Gets the close application command.
        /// </summary>
        /// <value>
        /// The close application command.
        /// </value>
        public ICommand CloseApplicationCommand
        {
            get
            {
                return new RelayCommand( _ => CloseApplicationAction( ) );
            }
        }

        /// <summary>
        /// Closes the application action.
        /// </summary>
        private void CloseApplicationAction( )
        {
            CloseApplication( );
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        private void CloseApplication( )
        {
            _isClosing = true;
            Application.Current.Dispatcher.BeginInvoke( DispatcherPriority.Normal,
                new Action( Close ) );
        }

        /// <summary>
        /// Restarts the application.
        /// </summary>
        /// <param name="asAdmin">if set to <c>true</c> [as admin].</param>
        public void RestartApplication( bool asAdmin = false )
        {
            ExternalProcessStarter.RunProcess( ConfigurationManager.Current.ApplicationFullName,
                $"{CommandLineManager.GetParameterWithSplitIdentifier( CommandLineManager.ParameterRestartPid )}{Environment.ProcessId} {CommandLineManager.GetParameterWithSplitIdentifier( CommandLineManager.ParameterApplication )}{SelectedApplication.Name}",
                asAdmin );

            CloseApplication( );
        }

        /// <summary>
        /// Gets the application list mouse enter command.
        /// </summary>
        /// <value>
        /// The application list mouse enter command.
        /// </value>
        public ICommand ApplicationListMouseEnterCommand
        {
            get
            {
                return new RelayCommand( _ => ApplicationListMouseEnterAction( ) );
            }
        }

        /// <summary>
        /// Applications the list mouse enter action.
        /// </summary>
        private void ApplicationListMouseEnterAction( )
        {
            ApplicationViewIsMouseOver = true;
        }

        /// <summary>
        /// Gets the application list mouse leave command.
        /// </summary>
        /// <value>
        /// The application list mouse leave command.
        /// </value>
        public ICommand ApplicationListMouseLeaveCommand
        {
            get
            {
                return new RelayCommand( _ => ApplicationListMouseLeaveAction( ) );
            }
        }

        /// <summary>
        /// Applications the list mouse leave action.
        /// </summary>
        private void ApplicationListMouseLeaveAction( )
        {
            if( !TextBoxApplicationSearchIsFocused )
            {
                ApplicationViewIsOpen = false;
            }

            ApplicationViewIsMouseOver = false;
        }

        /// <summary>
        /// Gets the text box search got focus command.
        /// </summary>
        /// <value>
        /// The text box search got focus command.
        /// </value>
        public ICommand TextBoxSearchGotFocusCommand
        {
            get
            {
                return new RelayCommand( _ => TextBoxSearchGotFocusAction( ) );
            }
        }

        /// <summary>
        /// Texts the box search got focus action.
        /// </summary>
        private void TextBoxSearchGotFocusAction( )
        {
            TextBoxApplicationSearchIsFocused = true;
        }

        /// <summary>
        /// Gets the text box search lost focus command.
        /// </summary>
        /// <value>
        /// The text box search lost focus command.
        /// </value>
        public ICommand TextBoxSearchLostFocusCommand
        {
            get
            {
                return new RelayCommand( _ => TextBoxSearchLostFocusAction( ) );
            }
        }

        /// <summary>
        /// Texts the box search lost focus action.
        /// </summary>
        private void TextBoxSearchLostFocusAction( )
        {
            if( !ApplicationViewIsMouseOver )
            {
                ApplicationViewIsOpen = false;
            }

            TextBoxApplicationSearchIsFocused = false;
        }

        /// <summary>
        /// Gets the clear search command.
        /// </summary>
        /// <value>
        /// The clear search command.
        /// </value>
        public ICommand ClearSearchCommand
        {
            get
            {
                return new RelayCommand( _ => ClearSearchAction( ) );
            }
        }

        /// <summary>
        /// Clears the search action.
        /// </summary>
        private void ClearSearchAction( )
        {
            ApplicationSearch = string.Empty;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Shows the window.
        /// </summary>
        private void ShowWindow( )
        {
            if( _isInTray )
            {
                ShowWindowFromTray( );
            }

            if( !IsActive )
            {
                BringWindowToFront( );
            }
        }

        /// <summary>
        /// Hides the window to tray.
        /// </summary>
        private void HideWindowToTray( )
        {
            if( _notifyIcon == null )
            {
                InitNotifyIcon( );
            }

            _isInTray = true;
            if( _notifyIcon != null )
            {
                _notifyIcon.Visible = true;
            }

            Hide( );
        }

        /// <summary>
        /// Shows the window from tray.
        /// </summary>
        private void ShowWindowFromTray( )
        {
            _isInTray = false;
            Show( );
            _notifyIcon.Visible = SettingsManager.Current.TrayIcon_AlwaysShowIcon;
        }

        /// <summary>
        /// Brings the window to front.
        /// </summary>
        private void BringWindowToFront( )
        {
            if( WindowState == WindowState.Minimized )
            {
                WindowState = WindowState.Normal;
            }

            Activate( );
        }

        /// <summary>
        /// Configures the DNS.
        /// </summary>
        private void ConfigureDNS( )
        {
            Log.Info( "Configure application DNS..." );
            DNSClientSettings dnsSettings = new DNSClientSettings( );
            if( SettingsManager.Current.Network_UseCustomDNSServer )
            {
                if( !string.IsNullOrEmpty( SettingsManager.Current.Network_CustomDNSServer ) )
                {
                    Log.Info(
                        $"Use custom DNS servers ({SettingsManager.Current.Network_CustomDNSServer})..." );

                    List<(string Server, int Port)> dnsServers =
                        new List<(string Server, int Port)>( );

                    foreach( var dnsServer in SettingsManager.Current.Network_CustomDNSServer.Split(
                        ";" ) )
                    {
                        dnsServers.Add( ( dnsServer, 53 ) );
                    }

                    dnsSettings.UseCustomDNSServers = true;
                    dnsSettings.DNSServers = dnsServers;
                }
                else
                {
                    Log.Info(
                        $"Custom DNS servers could not be set (Setting \"{nameof( SettingsManager.Current.Network_CustomDNSServer )}\" has value \"{SettingsManager.Current.Network_CustomDNSServer}\")! Fallback to Windows DNS servers..." );
                }
            }
            else
            {
                Log.Info( "Use Windows DNS servers..." );
            }

            SingletonBase<DNSClient>.GetInstance( ).Configure( dnsSettings );
        }

        /// <summary>
        /// Updates the DNS.
        /// </summary>
        private void UpdateDNS( )
        {
            Log.Info( "Update Windows DNS servers..." );
            SingletonBase<DNSClient>.GetInstance( ).UpdateFromWindows( );
        }

        /// <summary>
        /// Writes the default power shell profile to registry.
        /// </summary>
        private void WriteDefaultPowerShellProfileToRegistry( )
        {
            if( !SettingsManager.Current.Appearance_PowerShellModifyGlobalProfile )
            {
                return;
            }

            HashSet<string> paths = new HashSet<string>( );
            if( !string.IsNullOrEmpty( SettingsManager.Current.PowerShell_ApplicationFilePath )
                && File.Exists( SettingsManager.Current.PowerShell_ApplicationFilePath ) )
            {
                paths.Add( SettingsManager.Current.PowerShell_ApplicationFilePath );
            }

            if( !string.IsNullOrEmpty(
                    SettingsManager.Current.AWSSessionManager_ApplicationFilePath )
                && File.Exists( SettingsManager.Current.AWSSessionManager_ApplicationFilePath ) )
            {
                paths.Add( SettingsManager.Current.AWSSessionManager_ApplicationFilePath );
            }

            foreach( var path in paths )
            {
                PowerShell.WriteDefaultProfileToRegistry( SettingsManager.Current.Appearance_Theme,
                    path );
            }
        }
        #endregion

        #region Status window
        /// <summary>
        /// Opens the status window.
        /// </summary>
        /// <param name="enableCloseTimer">if set to <c>true</c> [enable close timer].</param>
        private void OpenStatusWindow( bool enableCloseTimer = false )
        {
            _statusWindow.ShowWindow( enableCloseTimer );
        }

        /// <summary>
        /// Called when [network has changed].
        /// </summary>
        private async void OnNetworkHasChanged( )
        {
            if( _isNetworkChanging )
            {
                return;
            }

            _isNetworkChanging = true;
            await Task.Delay( GlobalStaticConfiguration.StatusWindowDelayBeforeOpen );
            Log.Info( "Network availability or address has changed!" );
            if( !SettingsManager.Current.Network_UseCustomDNSServer )
            {
                UpdateDNS( );
            }

            if( SettingsManager.Current.Status_ShowWindowOnNetworkChange )
            {
                await Dispatcher.BeginInvoke( DispatcherPriority.Normal,
                    new Action( delegate { OpenStatusWindow( true ); } ) );
            }

            _isNetworkChanging = false;
        }
        #endregion

        #region Focus embedded window
        /// <summary>
        /// Handles the Activated event of the MetroMainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MetroMainWindow_Activated( object sender, EventArgs e )
        {
            FocusEmbeddedWindow( );
        }

        /// <summary>
        /// Focuses the embedded window.
        /// </summary>
        private async void FocusEmbeddedWindow( )
        {
            do
            {
                await Task.Delay( 250 );
            }
            while( Control.MouseButtons == MouseButtons.Left );

            if( SelectedApplication == null
                || SettingsViewIsOpen
                || IsProfileFileDropDownOpened
                || TextBoxApplicationSearchIsFocused
                || ConfigurationManager.Current.FixAirspace )
            {
                return;
            }

            switch( SelectedApplication.Name )
            {
                case ApplicationName.PowerShell:
                    _powerShellHostView?.FocusEmbeddedWindow( );
                    break;
                case ApplicationName.PuTTY:
                    _puttyHostView?.FocusEmbeddedWindow( );
                    break;
                case ApplicationName.AWSSessionManager:
                    _awsSessionManagerHostView?.FocusEmbeddedWindow( );
                    break;
            }
        }
        #endregion
    }
}