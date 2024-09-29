// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-26-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-26-2024
// ******************************************************************************************
// <copyright file="MainWindowViewModel.cs" company="Terry D. Eppler">
// 
//    Ninja is a network toolkit, support iperf, tcp, udp, websocket, mqtt,
//    sniffer, pcap, port scan, listen, ip scan .etc.
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
//   MainWindowViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Views;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "RedundantEmptySwitchSection" ) ]
    public class MainWindowViewModel : MainWindowBase
    {
        /// <summary>
        /// The iperf view model
        /// </summary>
        private protected IperfViewModel _iperfViewModel;

        /// <summary>
        /// The network scan view model
        /// </summary>
        private protected NetworkScanViewModel _networkScanViewModel;

        /// <summary>
        /// The port scan view model
        /// </summary>
        private protected PortScanViewModel _portScanViewModel;

        /// <summary>
        /// The route view model
        /// </summary>
        private protected RouteViewModel _routeViewModel;

        /// <summary>
        /// The port view model
        /// </summary>
        private protected PortViewModel _portViewModel;

        /// <summary>
        /// The server view model
        /// </summary>
        private protected ServerViewModel _serverViewModel;

        /// <summary>
        /// The sniffer view model
        /// </summary>
        private protected SnifferViewModel _snifferViewModel;

        /// <summary>
        /// The selected view model
        /// </summary>
        private protected object _selectedViewModel;

        /// <summary>
        /// The filter text
        /// </summary>
        private string _filterText;

        /// <summary>
        /// The menu items collection
        /// CollectionViewSource enables XAML code to set the
        /// commonly used CollectionView properties,
        /// passing these settings to the underlying view.
        /// </summary>
        private CollectionViewSource _menuItemsCollection;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel( )
        {
            _selectedViewModel = new object( );
            var _menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems
                {
                    MenuName = "Iperf",
                    MenuImage = @"/Resources/Assets/ApplicationImages/Speed.png"
                },
                new MenuItems
                {
                    MenuName = "NetworkScan",
                    MenuImage = @"/Resources/Assets/ApplicationImages/IP.png"
                },
                new MenuItems
                {
                    MenuName = "PortScan",
                    MenuImage = @"/Resources/Assets/ApplicationImages/port.png"
                },
                new MenuItems
                {
                    MenuName = "RouteTable",
                    MenuImage = @"/Resources/Assets/ApplicationImages/route.png"
                },
                new MenuItems
                {
                    MenuName = "PortListen",
                    MenuImage = @"/Resources/Assets/ApplicationImages/portlisten.png"
                },
                new MenuItems
                {
                    MenuName = "Server",
                    MenuImage = @"/Resources/Assets/ApplicationImages/servers.png"
                },
                new MenuItems
                {
                    MenuName = "Sniffer",
                    MenuImage = @"/Resources/Assets/ApplicationImages/sniffer.png"
                }
            };

            _menuItemsCollection = new CollectionViewSource
            {
                Source = _menuItems
            };

            _menuItemsCollection.Filter += OnMenuItemsFilter;
            _iperfViewModel = new IperfViewModel( );
            _selectedViewModel = _iperfViewModel;
        }

        /// ICollectionView enables collections to have the
        /// functionalities of current record management,
        /// custom sorting, filtering, and grouping.
        /// <summary>
        /// Gets the source collection.
        /// </summary>
        /// <value>
        /// The source collection.
        /// </value>
        public ICollectionView SourceCollection
        {
            get
            {
                return _menuItemsCollection.View;
            }
        }

        /// <summary>
        /// Gets or sets the filter text.
        /// </summary>
        /// <value>
        /// The filter text.
        /// </value>
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                if( _filterText != value )
                {
                    _filterText = value;
                    _menuItemsCollection.View.Refresh( );
                    OnPropertyChanged( nameof( FilterText ) );
                }
            }
        }

        /// <summary>
        /// Runs the calculate.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void RunCalculator( object parameter )
        {
            Process.Start( "calc.exe" );
        }

        /// <summary>
        /// Gets the run calculate command.
        /// </summary>
        /// <value>
        /// The run calculate command.
        /// </value>
        public ICommand RunCalculatorCommand
        {
            get
            {
                return new RelayCommand( p => RunCalculator( p ) );
            }
        }

        /// <summary>
        /// Runs the menu command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void OpenMenu( object parameter )
        {
            switch( parameter )
            {
                case "Calc":
                {
                    Process.Start( "calc.exe" );
                    break;
                }
                case "About":
                {
                    var _aboutWindow = new AboutWindow( );
                    _aboutWindow.Show( );
                    Dispatcher.Run( );
                    break;
                }
                case "Iperf":
                {
                    var _iperfInfo = new ProcessStartInfo( "https://iperf.fr/" )
                    {
                        UseShellExecute = true
                    };

                    Process.Start( _iperfInfo );
                    break;
                }
                case "Pcap":
                {
                    var _pcapInfo =
                        new ProcessStartInfo( "https://www.tcpdump.org/manpages/pcap-filter.7.html" )
                        {
                            UseShellExecute = true
                        };

                    Process.Start( _pcapInfo );
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the run menu command.
        /// </summary>
        /// <value>
        /// The run menu command.
        /// </value>
        public ICommand RunMenuCommand
        {
            get
            {
                return new RelayCommand( p => OpenMenu( p ) );
            }
        }

        /// <summary>
        /// Gets or sets the selected view model.
        /// </summary>
        /// <value>
        /// The selected view model.
        /// </value>
        public object SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                if( _selectedViewModel != value )
                {
                    _selectedViewModel = value;
                    OnPropertyChanged( nameof( SelectedViewModel ) );
                }
            }
        }

        // Menu Button Command
        /// <summary>
        /// The menucommand
        /// </summary>
        private ICommand _menuCommand;

        /// <summary>
        /// Gets the menu command.
        /// </summary>
        /// <value>
        /// The menu command.
        /// </value>
        public ICommand MenuCommand
        {
            get
            {
                if( _menuCommand == null )
                {
                    _menuCommand = new RelayCommand( p => SwitchViews( p ) );
                }

                return _menuCommand;
            }
        }

        /// <summary>
        /// The iperf view model
        /// </summary>
        public IperfViewModel IperfViewModel
        {
            get
            {
                return _iperfViewModel;
            }
            private protected set
            {
                if( _iperfViewModel != value )
                {
                    _iperfViewModel = value;
                    OnPropertyChanged( nameof( IperfViewModel ) );
                }
            }
        }

        /// <summary>
        /// The network scan view model
        /// </summary>
        public NetworkScanViewModel NetworkScanViewModel
        {
            get
            {
                return _networkScanViewModel;
            }
            private protected set
            {
                if( _networkScanViewModel != value )
                {
                    _networkScanViewModel = value;
                    OnPropertyChanged( nameof( NetworkScanViewModel ) );
                }
            }
        }

        /// <summary>
        /// The port scan view model
        /// </summary>
        public PortScanViewModel PortScanViewModel
        {
            get
            {
                return _portScanViewModel;
            }
            private protected set
            {
                if( _portScanViewModel != value )
                {
                    _portScanViewModel = value;
                    OnPropertyChanged( nameof( PortScanViewModel ) );
                }
            }
        }

        /// <summary>
        /// The route view model
        /// </summary>
        public RouteViewModel RouteViewModel
        {
            get
            {
                return _routeViewModel;
            }
            private protected set
            {
                if( _routeViewModel != value )
                {
                    _routeViewModel = value;
                    OnPropertyChanged( nameof( RouteViewModel ) );
                }
            }
        }

        /// <summary>
        /// The port view model
        /// </summary>
        public PortViewModel PortViewModel
        {
            get
            {
                return _portViewModel;
            }
            private protected set
            {
                if( _portViewModel != value )
                {
                    _portViewModel = value;
                    OnPropertyChanged( nameof( PortScanViewModel ) );
                }
            }
        }

        /// <summary>
        /// The server view model
        /// </summary>
        public ServerViewModel ServerViewModel
        {
            get
            {
                return _serverViewModel;
            }
            private protected set
            {
                if( _serverViewModel != value )
                {
                    _serverViewModel = value;
                    OnPropertyChanged( nameof( ServerViewModel ) );
                }
            }
        }

        /// <summary>
        /// The sniffer view model
        /// </summary>
        public SnifferViewModel SnifferViewModel
        {
            get
            {
                return _snifferViewModel;
            }
            private protected set
            {
                if( _snifferViewModel != value )
                {
                    _snifferViewModel = value;
                    OnPropertyChanged( nameof( SnifferViewModel ) );
                }
            }
        }

        /// <summary>
        /// Switches the views.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void SwitchViews( object parameter )
        {
            var _view = parameter.ToString( );
            switch( _view )
            {
                case "Iperf":
                {
                    _iperfViewModel ??= new IperfViewModel( );
                    _selectedViewModel = _iperfViewModel;
                    break;
                }
                case "NetworkScan":
                {
                    _networkScanViewModel ??= new NetworkScanViewModel( );
                    _selectedViewModel = _networkScanViewModel;
                    break;
                }
                case "PortScan":
                {
                    _portScanViewModel ??= new PortScanViewModel( );
                    _selectedViewModel = _portScanViewModel;
                    break;
                }
                case "RouteTable":
                {
                    _routeViewModel ??= new RouteViewModel( );
                    _selectedViewModel = _routeViewModel;
                    break;
                }
                case "PortListen":
                {
                    _portViewModel ??= new PortViewModel( );
                    _selectedViewModel = _portViewModel;
                    break;
                }
                case "Server":
                {
                    _serverViewModel ??= new ServerViewModel( );
                    _selectedViewModel = _serverViewModel;
                    break;
                }
                case "Sniffer":
                {
                    _snifferViewModel ??= new SnifferViewModel( );
                    _selectedViewModel = _snifferViewModel;
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the Filter event of the MenuItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FilterEventArgs"/>
        /// instance containing the event data.</param>
        private void OnMenuItemsFilter( object sender, FilterEventArgs e )
        {
            if( string.IsNullOrEmpty( _filterText ) )
            {
                e.Accepted = true;
                return;
            }

            var _item = e.Item as MenuItems;
            if( _item.MenuName.ToUpper( ).Contains( _filterText.ToUpper( ) ) )
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
    }
}
