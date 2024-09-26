// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-26-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-26-2024
// ******************************************************************************************
// <copyright file="IperfViewModel.cs" company="Terry D. Eppler">
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
//   IperfViewModel.cs
// </summary>
// ******************************************************************************************

namespace Ninja.ViewModels
{
    using Interfaces;
    using Models;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using Microsoft.Win32;
    using System.IO;
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Ninja.ViewModels.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class IperfViewModel : MainWindowBase
    {
        /// <summary>
        /// The cancel plot token
        /// </summary>
        private CancellationToken _cancelToken;

        /// <summary>
        /// Gets or sets the iperf model.
        /// </summary>
        /// <value>
        /// The iperf model.
        /// </value>
        private protected IperfModel _iperfModel;

        /// <summary>
        /// The m plot model data
        /// </summary>
        private protected PlotModel _plotModelData;

        /// <summary>
        /// The plot token source
        /// </summary>
        private protected CancellationTokenSource _tokenSource;

        /// <summary>
        /// The iperf process
        /// </summary>
        private protected ProcessInterface _iperfProcess;

        /// <summary>
        /// The line series current value
        /// </summary>
        private protected LineSeries _lineSeriesCurrentVal;

        /// <summary>
        /// The x time axis
        /// </summary>
        private protected LinearAxis _xTimeAxis;

        /// <summary>
        /// The y throughput value
        /// </summary>
        private protected LinearAxis _yThroughputVal;

        /// <summary>
        /// The y zoom factor
        /// </summary>
        private protected int _yZoomFactor = 1;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="IperfViewModel"/> class.
        /// </summary>
        public IperfViewModel( )
        {
            _iperfProcess = new ProcessInterface( );
            _iperfModel = new IperfModel( );
            InitPlot( );
        }

        /// <summary>
        /// Gets or sets the iperf model.
        /// </summary>
        /// <value>
        /// The iperf model.
        /// </value>
        public IperfModel IperfModel
        {
            get
            {
                return _iperfModel;
            }
            private protected set
            {
                if( _iperfModel != value )
                {
                    _iperfModel = value;
                    OnPropertyChanged( nameof( IperfModel ) );
                }
            }
        }

        /// <summary>
        /// The m plot model data
        /// </summary>
        public PlotModel PlotModelData
        {
            get
            {
                return _plotModelData;
            }
            private protected set
            {
                if( _plotModelData != value )
                {
                    _plotModelData = value;
                    OnPropertyChanged( nameof( PlotModelData ) );
                }
            }
        }

        /// <summary>
        /// The plot token source
        /// </summary>
        public CancellationTokenSource TokenSource
        {
            get
            {
                return _tokenSource;
            }
            private protected set
            {
                if( _tokenSource != value )
                {
                    _tokenSource = value;
                    OnPropertyChanged( nameof( TokenSource ) );
                }
            }
        }

        /// <summary>
        /// The iperf process
        /// </summary>
        public ProcessInterface IperfProcess
        {
            get
            {
                return _iperfProcess;
            }
            private protected set
            {
                if( _iperfProcess != value )
                {
                    _iperfProcess = value;
                    OnPropertyChanged( nameof( IperfProcess ) );
                }
            }
        }

        /// <summary>
        /// The line series current value
        /// </summary>
        public LineSeries LineSeriesCurrentVal
        {
            get
            {
                return _lineSeriesCurrentVal;
            }
            private protected set
            {
                if( _lineSeriesCurrentVal != value )
                {
                    _lineSeriesCurrentVal = value;
                    OnPropertyChanged( nameof( LineSeriesCurrentVal ) );
                }
            }
        }

        /// <summary>
        /// The x time axis
        /// </summary>
        public LinearAxis XTimeAxis
        {
            get
            {
                return _xTimeAxis;
            }
            private protected set
            {
                if( _xTimeAxis != value )
                {
                    _xTimeAxis = value;
                    OnPropertyChanged( nameof( XTimeAxis ) );
                }
            }
        }

        /// <summary>
        /// The y throughput value
        /// </summary>
        public LinearAxis YThroughputVal
        {
            get
            {
                return _yThroughputVal;
            }
            private protected set
            {
                if( _yThroughputVal != value )
                {
                    _yThroughputVal = value;
                    OnPropertyChanged( nameof( YThroughputVal ) );
                }
            }
        }

        /// <summary>
        /// The y zoom factor
        /// </summary>
        public int YZoomFactor
        {
            get
            {
                return 1;
            }
            private protected set
            {
                if( _yZoomFactor != value )
                {
                    _yZoomFactor = value;
                    OnPropertyChanged( nameof( YZoomFactor ) );
                }
            }
        }
        
        /// <summary>
        /// Initializes the plot.
        /// </summary>
        public void InitPlot( )
        {
            _plotModelData = new PlotModel( );
            _xTimeAxis = new LinearAxis( )
            {
                Title = "Time(s)",
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.None,
                Minimum = 0,
                AbsoluteMinimum = 0,
                Maximum = 100,

                //MajorStep = 100,
                FontSize = 13,

                //PositionTier = 6,
                Key = "Time",
                MinorGridlineStyle = LineStyle.Solid,
                IsPanEnabled = true,
                IsZoomEnabled = true
            };

            _yThroughputVal = new LinearAxis( )
            {
                Title = "Throughput(Mbps)",
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.None,
                Minimum = 0,
                AbsoluteMinimum = 0,
                Maximum = 500.0,
                MajorStep = 50,
                FontSize = 13,
                PositionTier = 6,
                Key = "Throughput",
                MinorGridlineStyle = LineStyle.Solid,
                IsPanEnabled = true,
                IsZoomEnabled = true
            };

            _plotModelData.Axes.Add( _xTimeAxis );
            _plotModelData.Axes.Add( _yThroughputVal );
            _lineSeriesCurrentVal = new LineSeries( )
            {
                MarkerType = MarkerType.Circle,
                StrokeThickness = 1,
                YAxisKey = "Throughput",
                Title = "Throughput",
                Color = OxyColors.Red
            };

            _plotModelData.Series.Add( LineSeriesCurrentVal );
        }

        /// <summary>
        /// ies the zoom out.
        /// </summary>
        public void YZoomOut( )
        {
            _yThroughputVal.Maximum *= 2;
            _yThroughputVal.MajorStep =
                ( _yThroughputVal.ActualMaximum - _yThroughputVal.ActualMinimum ) / 10;
        }

        /// <summary>
        /// ies the zoom in.
        /// </summary>
        public void YZoomIn( )
        {
            _yThroughputVal.Maximum /= 2;
            _yThroughputVal.MajorStep =
                ( _yThroughputVal.ActualMaximum - _yThroughputVal.ActualMinimum ) / 10;
        }

        /// <summary>
        /// Clears the plot.
        /// </summary>
        public void ClearPlot( )
        {
            _lineSeriesCurrentVal.Points.Clear( );
            _plotModelData.InvalidatePlot( true );
        }

        /// <summary>
        /// Stops the update plot task.
        /// </summary>
        internal void StopUpdatePlotTask( )
        {
            _tokenSource.Cancel( );
        }

        /// <summary>
        /// Updates the plot task.
        /// </summary>
        public void UpdatePlotTask( )
        {
            double _val = 300;
            _tokenSource = new CancellationTokenSource( );
            _cancelToken = _tokenSource.Token;
            Task.Run( ( ) =>
            {
                while( true )
                {
                    if( _tokenSource.IsCancellationRequested )
                    {
                        break;
                    }

                    _val = Convert.ToDouble( _iperfModel.Throughput );
                    var _date = DateTime.Now;
                    _lineSeriesCurrentVal.Points.Add( DateTimeAxis.CreateDataPoint( _date, _val ) );
                    _plotModelData.InvalidatePlot( true );
                    if( _date.ToOADate( ) > _xTimeAxis.ActualMaximum )
                    {
                        var _xPan = ( _xTimeAxis.ActualMaximum - _xTimeAxis.DataMaximum )
                            * _xTimeAxis.Scale;

                        _xTimeAxis.Pan( _xPan );
                    }

                    Thread.Sleep( 1000 );
                }
            }, _cancelToken );
        }

        /// <summary>
        /// Runs the iperf.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void RunIperf( object parameter )
        {
            var _iperfPath =  @"C:\Users\terry\source\repos\Ninja\Libraries\iperf\" 
                + _iperfModel.Version;

            _iperfProcess.StartProcess( _iperfPath, ( string )parameter,
                ProcessOutputDataReceived );

            _lineSeriesCurrentVal.Points.Clear( );
            _plotModelData.InvalidatePlot( true );
        }

        /// <summary>
        /// Plots the data.
        /// </summary>
        /// <param name="str">The string.</param>
        public void PlotData( string str )
        {
            string _pattern;
            if( _iperfModel.Parallel > 1 )
            {
                _pattern =
                    @"\[SUM\]*\s*(?<a>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*-\s*(?<b>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*sec\s*(?<c>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*.Bytes\s*(?<d>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*Mbits/sec";
            }
            else
            {
                _pattern =
                    @"(?<a>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*-\s*(?<b>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*sec\s*(?<c>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*.Bytes\s*(?<d>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*Mbits/sec";
            }

            var _m = Regex.Match( str, _pattern );
            str = "";
            var _r = new Regex( "/(\\d+)[^\\d]+Mbits/sec/" );
            if( _m.Success )
            {
                var _timeA = _m.Groups[ "a" ].Value;
                var _timeB = _m.Groups[ "b" ].Value;
                var _bytes = _m.Groups[ "c" ].Value;
                var _bandwidth = _m.Groups[ "d" ].Value;
                _iperfModel.Throughput = Convert.ToDouble( _bandwidth );
                var _val = Convert.ToDouble( _iperfModel.Throughput );
                var _time = Convert.ToDouble( _timeB );
                _yThroughputVal.MajorStep =
                    ( _yThroughputVal.ActualMaximum - _yThroughputVal.ActualMinimum ) / 10;

                _lineSeriesCurrentVal.Points.Add( new DataPoint( _time, _val ) );
                _plotModelData.InvalidatePlot( true );
                if( _val > _yThroughputVal.ActualMaximum )
                {
                    var _xPan = ( _yThroughputVal.ActualMaximum - _yThroughputVal.DataMaximum - 50 )
                        * _yThroughputVal.Scale;

                    _yThroughputVal.Pan( _xPan );
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// Processes the output data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs"/>
        /// instance containing the event data.</param>
        private void ProcessOutputDataReceived( object sender, DataReceivedEventArgs e )
        {
            _iperfModel.Output += e.Data;
            _iperfModel.Output += "\n";
            if( e.Data != null )
            {
                PlotData( e.Data );
            }
        }

        /// <summary>
        /// Stops the iperf.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void StopIperf( object parameter )
        {
            _iperfProcess.StopProcess( );
        }

        /// <summary>
        /// Clears the output.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClearOutput( object parameter )
        {
            _iperfModel.Output = "";
        }

        /// <summary>
        /// Saves the output.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async void SaveOutput( object parameter )
        {
            var _receDataSaveFileDialog = new SaveFileDialog
            {
                Title = "Save Iperf Result",
                FileName = DateTime.Now.ToString( "yyyyMMddmmss" ),
                DefaultExt = ".txt",
                Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*"
            };

            if( _receDataSaveFileDialog.ShowDialog( ) == true )
            {
                var _dataRecvPath = _receDataSaveFileDialog.FileName;
                await using var _defaultReceDataPath = new StreamWriter( _dataRecvPath, true );
                await _defaultReceDataPath.WriteAsync( _iperfModel.Output ).ConfigureAwait( false );
            }
        }

        /// <summary>
        /// Iperfs the help.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void IperfHelp( object parameter )
        {
            var _iperfPath = @"C:\Users\terry\source\repos\Ninja\Libraries\iperf\" + _iperfModel.Version;
            _iperfProcess.StartProcess( _iperfPath, "--help", ProcessOutputDataReceived );
        }

        /// <summary>
        /// Gets the run iperf command.
        /// </summary>
        /// <value>
        /// The run iperf command.
        /// </value>
        public ICommand RunIperfCommand
        {
            get
            {
                return new RelayCommand( param => RunIperf( param ) );
            }
        }

        /// <summary>
        /// Gets the stop iperf command.
        /// </summary>
        /// <value>
        /// The stop iperf command.
        /// </value>
        public ICommand StopIperfCommand
        {
            get
            {
                return new RelayCommand( param => StopIperf( param ) );
            }
        }

        /// <summary>
        /// Gets the clear output command.
        /// </summary>
        /// <value>
        /// The clear output command.
        /// </value>
        public ICommand ClearOutputCommand
        {
            get
            {
                return new RelayCommand( param => ClearOutput( param ) );
            }
        }

        /// <summary>
        /// Gets the save output command.
        /// </summary>
        /// <value>
        /// The save output command.
        /// </value>
        public ICommand SaveOutputCommand
        {
            get
            {
                return new RelayCommand( param => SaveOutput( param ) );
            }
        }

        /// <summary>
        /// Gets the iperf help command.
        /// </summary>
        /// <value>
        /// The iperf help command.
        /// </value>
        public ICommand IperfHelpCommand
        {
            get
            {
                return new RelayCommand( param => IperfHelp( param ) );
            }
        }
    }
}