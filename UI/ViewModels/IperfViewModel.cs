// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
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
        private CancellationToken _cancelPlotToken;

        /// <summary>
        /// The m plot model data
        /// </summary>
        private PlotModel _plotModelData = new PlotModel( );

        /// <summary>
        /// The plot token source
        /// </summary>
        private CancellationTokenSource _plotTokenSource;

        /// <summary>
        /// The iperf process
        /// </summary>
        public ProcessInterface IperfProcess;

        /// <summary>
        /// The line series current value
        /// </summary>
        public LineSeries LineSeriesCurrentVal;

        /// <summary>
        /// The x time axis
        /// </summary>
        public LinearAxis XTimeAxis;

        /// <summary>
        /// The y throughput value
        /// </summary>
        public LinearAxis YThroughputVal;

        /// <summary>
        /// The y zoom factor
        /// </summary>
        public int YZoomFactor = 1;

        /// <summary>
        /// Initializes the plot.
        /// </summary>
        public void InitPlot( )
        {
            PlotModelData = new PlotModel( );
            XTimeAxis = new LinearAxis( )
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

            YThroughputVal = new LinearAxis( )
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

            PlotModelData.Axes.Add( XTimeAxis );
            PlotModelData.Axes.Add( YThroughputVal );
            LineSeriesCurrentVal = new LineSeries( )
            {
                MarkerType = MarkerType.Circle,
                StrokeThickness = 1,
                YAxisKey = "Throughput",
                Title = "Throughput",
                Color = OxyColors.Red
            };

            PlotModelData.Series.Add( LineSeriesCurrentVal );
        }

        /// <summary>
        /// ies the zoom out.
        /// </summary>
        public void YZoomOut( )
        {
            YThroughputVal.Maximum *= 2;
            YThroughputVal.MajorStep =
                ( YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum ) / 10;
        }

        /// <summary>
        /// ies the zoom in.
        /// </summary>
        public void YZoomIn( )
        {
            YThroughputVal.Maximum /= 2;
            YThroughputVal.MajorStep =
                ( YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum ) / 10;
        }

        /// <summary>
        /// Clears the plot.
        /// </summary>
        public void ClearPlot( )
        {
            LineSeriesCurrentVal.Points.Clear( );
            PlotModelData.InvalidatePlot( true );
        }

        /// <summary>
        /// Stops the update plot task.
        /// </summary>
        internal void StopUpdatePlotTask( )
        {
            _plotTokenSource.Cancel( );
        }

        public void UpdatePlotTask( )
        {
            double _val = 300;
            _plotTokenSource = new CancellationTokenSource( );
            _cancelPlotToken = _plotTokenSource.Token;
            Task.Run( ( ) =>
            {
                while( true )
                {
                    if( _plotTokenSource.IsCancellationRequested )
                    {
                        break;
                    }

                    _val = Convert.ToDouble( IperfModel.Throughput );
                    var _date = DateTime.Now;
                    LineSeriesCurrentVal.Points.Add( DateTimeAxis.CreateDataPoint( _date, _val ) );
                    PlotModelData.InvalidatePlot( true );
                    if( _date.ToOADate( ) > XTimeAxis.ActualMaximum )
                    {
                        var _xPan = ( XTimeAxis.ActualMaximum - XTimeAxis.DataMaximum )
                            * XTimeAxis.Scale;

                        XTimeAxis.Pan( _xPan );
                    }

                    Thread.Sleep( 1000 );
                }
            }, _cancelPlotToken );
        }

        /// <summary>
        /// Runs the iperf.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void RunIperf( object parameter )
        {
            var _iperfPath = @"Libraries\iperf\" + IperfModel.Version;
            IperfProcess.StartProcess( _iperfPath, ( string )parameter, ProcessOutputDataReceived );
            LineSeriesCurrentVal.Points.Clear( );
            PlotModelData.InvalidatePlot( true );
        }

        /// <summary>
        /// Plots the data.
        /// </summary>
        /// <param name="str">The string.</param>
        public void PlotData( string str )
        {
            string _pattern;
            if( IperfModel.Parallel > 1 )
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
                IperfModel.Throughput = Convert.ToDouble( _bandwidth );
                var _val = Convert.ToDouble( IperfModel.Throughput );
                var _time = Convert.ToDouble( _timeB );
                YThroughputVal.MajorStep =
                    ( YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum ) / 10;

                LineSeriesCurrentVal.Points.Add( new DataPoint( _time, _val ) );
                PlotModelData.InvalidatePlot( true );
                if( _val > YThroughputVal.ActualMaximum )
                {
                    var _xPan = ( YThroughputVal.ActualMaximum - YThroughputVal.DataMaximum - 50 )
                        * YThroughputVal.Scale;

                    YThroughputVal.Pan( _xPan );
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
        /// <param name="e">The <see cref="DataReceivedEventArgs"/> instance containing the event data.</param>
        private void ProcessOutputDataReceived( object sender, DataReceivedEventArgs e )
        {
            IperfModel.Output += e.Data;
            IperfModel.Output += "\n";
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
            IperfProcess.StopProcess( );
        }

        /// <summary>
        /// Clears the output.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClearOutput( object parameter )
        {
            IperfModel.Output = "";
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
                await _defaultReceDataPath.WriteAsync( IperfModel.Output ).ConfigureAwait( false );
            }
        }

        /// <summary>
        /// Iperfs the help.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void IperfHelp( object parameter )
        {
            var _iperfPath = @"Libraries\iperf\" + IperfModel.Version;
            IperfProcess.StartProcess( _iperfPath, "--help", ProcessOutputDataReceived );
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="IperfViewModel"/> class.
        /// </summary>
        public IperfViewModel( )
        {
            IperfProcess = new ProcessInterface( );
            IperfModel = new IperfModel( );
            InitPlot( );
        }

        /// <summary>
        /// Gets or sets the iperf model.
        /// </summary>
        /// <value>
        /// The iperf model.
        /// </value>
        public IperfModel IperfModel { get; set; }

        /// <summary>
        /// Gets or sets the plot model data.
        /// </summary>
        /// <value>
        /// The plot model data.
        /// </value>
        public PlotModel PlotModelData
        {
            get
            {
                return _plotModelData;
            }
            set
            {
                _plotModelData = value;
                OnPropertyChanged( nameof( PlotModelData ) );
            }
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