// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-22-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-22-2024
// ******************************************************************************************
// <copyright file="SnifferCaptureViewModel.cs" company="Terry D. Eppler">
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
//   SnifferCaptureViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Input;
    using System.Windows;
    using PcapDotNet.Packets;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Win32;
    using PcapDotNet.Core;
    using PcapDotNet.Packets.Ethernet;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ConvertToAutoPropertyWhenPossible" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "AccessToModifiedClosure" ) ]
    public class SnifferCaptureViewModel : MainWindowBase
    {
        /// <summary>
        /// The capture token source
        /// </summary>
        private CancellationTokenSource _captureTokenSource;

        /// <summary>
        /// The communicator
        /// </summary>
        private PacketCommunicator _communicator;

        /// <summary>
        /// The network interface list
        /// </summary>
        private List<IPacketDevice> _networkInterfaceList;

        /// <summary>
        /// The packet filter
        /// </summary>
        private string _packetFilter;

        /// <summary>
        /// The packets
        /// </summary>
        private ObservableCollection<Packet> _packets;

        /// <summary>
        /// The save status
        /// </summary>
        private bool _saveStatus;

        /// <summary>
        /// The selected interface
        /// </summary>
        private IPacketDevice _selectedInterface;

        /// <summary>
        /// The selected packet
        /// </summary>
        private Packet _selectedPacket;

        /// <summary>
        /// The start capture BTN name
        /// </summary>
        private string _startCaptureBtnName;

        /// <summary>
        /// Starts the capture asynchronous.
        /// </summary>
        /// <param name="packetDevice">The packet device.</param>
        public async void StartCaptureAsync( IPacketDevice packetDevice )
        {
            _captureTokenSource = new CancellationTokenSource( );
            Packet _packet;
            try
            {
                await Task.Run( ( ) =>
                {
                    _communicator = packetDevice.Open( 65536,
                        PacketDeviceOpenAttributes.Promiscuous, 1000 );

                    if( !string.IsNullOrEmpty( PacketFilter ) )
                    {
                        _communicator.SetFilter( PacketFilter );
                    }

                    do
                    {
                        var _result = _communicator.ReceivePacket( out _packet );
                        switch( _result )
                        {
                            case PacketCommunicatorReceiveResult.Timeout:
                            {
                                // Timeout elapsed
                                continue;
                            }
                            case PacketCommunicatorReceiveResult.Ok:
                            {
                                if( _packet.Ethernet.EtherType == EthernetType.IpV4 )
                                {
                                    Application.Current.Dispatcher.Invoke( ( ) =>
                                    {
                                        _packets.Add( _packet );
                                        SnifferStatsProcess.UpdateStats( _packet );
                                    } );
                                }

                                break;
                            }
                            default:
                            {
                                var _msg = "PacketCommunicator InvalidOperationException";
                                throw new InvalidOperationException( _msg );
                            }
                        }
                    }
                    while( !_captureTokenSource.Token.IsCancellationRequested );
                } );
            }
            catch( Exception ex )
            {
            }
            finally
            {
                _communicator.Break( );
                _communicator.Dispose( );
                _communicator = null;
                _captureTokenSource.Dispose( );
                _captureTokenSource = null;
            }
        }

        /// <summary>
        /// Stops the capture.
        /// </summary>
        public void StopCapture( )
        {
            _captureTokenSource?.Cancel( );
        }

        /// <summary>
        /// Starts the capture.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void StartCapture( object parameter )
        {
            if( StartCaptureBtnName == "Start Capture" )
            {
                StartCaptureBtnName = "Stop Capture";
                StartCaptureAsync( SelectedInterface );
                SnifferStatsProcess.Start( );
                SaveStatus = false;
            }
            else
            {
                StartCaptureBtnName = "Start Capture";
                SnifferStatsProcess.Stop( );
                StopCapture( );
                SaveStatus = true;
            }
        }

        /// <summary>
        /// Filters the help.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void FilterHelp( object parameter )
        {
            var _destinationurl = "https://www.tcpdump.org/manpages/pcap-filter.7.html";
            var _sInfo = new ProcessStartInfo( _destinationurl )
            {
                UseShellExecute = true
            };

            Process.Start( _sInfo );
        }

        /// <summary>
        /// Saves the packets.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void SavePackets( object parameter )
        {
            SaveStatus = false;
            var _receDataSaveFileDialog = new SaveFileDialog
            {
                Title = "Save pcap packets",
                FileName = DateTime.Now.ToString( "yyyyMMddmmss" ),
                DefaultExt = ".pcap",
                Filter = "pcap files(*.pcap)|*.pcap|All files(*.*)|*.*"
            };

            if( _receDataSaveFileDialog.ShowDialog( ) == true )
            {
                var _dataRecvPath = _receDataSaveFileDialog.FileName;
                Task.Run( ( ) =>
                {
                    foreach( var _packet in _packets )
                    {
                        PacketDumpFile.Dump( _dataRecvPath, DataLinkKind.Ethernet,
                            ( int )SnifferStatsProcess.ByteCount, _packets );
                    }

                    SaveStatus = true;
                } );
            }
        }

        /// <summary>
        /// Gets the network interface list.
        /// </summary>
        private void GetNetworkInterfaceList( )
        {
            NetworkInterfaceList = new List<IPacketDevice>( );
            IList<LivePacketDevice> _allDevices = LivePacketDevice.AllLocalMachine;
            for( var _i = 0; _i < _allDevices.Count; _i++ )
            {
                NetworkInterfaceList.Add( _allDevices[ _i ] );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnifferCaptureViewModel"/> class.
        /// </summary>
        public SnifferCaptureViewModel( )
        {
            Packets = new ObservableCollection<Packet>( );
            StartCaptureBtnName = "Start Capture";
            GetNetworkInterfaceList( );
        }

        /// <summary>
        /// Gets the packets.
        /// </summary>
        /// <value>
        /// The packets.
        /// </value>
        public ObservableCollection<Packet> Packets
        {
            get
            {
                return _packets;
            }
            private set
            {
                _packets = value;
            }
        }

        /// <summary>
        /// Gets or sets the start name of the capture BTN.
        /// </summary>
        /// <value>
        /// The start name of the capture BTN.
        /// </value>
        public string StartCaptureBtnName
        {
            get
            {
                return _startCaptureBtnName;
            }
            set
            {
                if( _startCaptureBtnName != value )
                {
                    _startCaptureBtnName = value;
                    OnPropertyChanged( nameof( StartCaptureBtnName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save status].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [save status]; otherwise, <c>false</c>.
        /// </value>
        public bool SaveStatus
        {
            get { return _saveStatus; }
            set
            {
                if( _saveStatus != value )
                {
                    _saveStatus = value;
                    OnPropertyChanged( nameof( SaveStatus ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the network interface list.
        /// </summary>
        /// <value>
        /// The network interface list.
        /// </value>
        public List<IPacketDevice> NetworkInterfaceList
        {
            get { return _networkInterfaceList; }
            set
            {
                if( _networkInterfaceList != value )
                {
                    _networkInterfaceList = value;
                    OnPropertyChanged( nameof( NetworkInterfaceList ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected interface.
        /// </summary>
        /// <value>
        /// The selected interface.
        /// </value>
        public IPacketDevice SelectedInterface
        {
            get { return _selectedInterface; }
            set
            {
                if( _selectedInterface != value )
                {
                    _selectedInterface = value;
                    OnPropertyChanged( nameof( SelectedInterface ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected packet.
        /// </summary>
        /// <value>
        /// The selected packet.
        /// </value>
        public Packet SelectedPacket
        {
            get { return _selectedPacket; }
            set
            {
                if( _selectedPacket != value )
                {
                    _selectedPacket = value;
                    OnPropertyChanged( nameof( SelectedPacket ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the packet filter.
        /// </summary>
        /// <value>
        /// The packet filter.
        /// </value>
        public string PacketFilter
        {
            get { return _packetFilter; }
            set
            {
                if( _packetFilter != value )
                {
                    _packetFilter = value;
                    OnPropertyChanged( nameof( PacketFilter ) );
                }
            }
        }

        /// <summary>
        /// Gets the start capture command.
        /// </summary>
        /// <value>
        /// The start capture command.
        /// </value>
        public ICommand StartCaptureCommand
        {
            get { return new RelayCommand( param => StartCapture( param ) ); }
        }

        /// <summary>
        /// Gets the filter help command.
        /// </summary>
        /// <value>
        /// The filter help command.
        /// </value>
        public ICommand FilterHelpCommand
        {
            get { return new RelayCommand( param => FilterHelp( param ) ); }
        }

        /// <summary>
        /// Gets the save packets command.
        /// </summary>
        /// <value>
        /// The save packets command.
        /// </value>
        public ICommand SavePacketsCommand
        {
            get { return new RelayCommand( param => SavePackets( param ) ); }
        }
    }
}