using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Diagnostics;
using Ninja.Models;

namespace Ninja.Interfaces
{
    public class IperfCommandConverter : IMultiValueConverter
    {
        //Binding sequences start ----->

        // <Binding Path = "IperfModel.Role" />
        //< Binding Path="IperfModel.ServerIp"/>
        //<Binding Path = "IperfModel.Port" />
        //< Binding Path="IperfModel.Parallel"/>
        //<Binding Path = "IperfModel.Time" />
        //< Binding Path="IperfModel.Interval"/>
        //<Binding Path = "IperfModel.TcpFlag" />
        //< Binding Path="IperfModel.TcpWindowSize"/>
        //<Binding Path = "IperfModel.TcpWindowUnit" />
        //< Binding Path="IperfModel.UdpFlag"/>
        //<Binding Path = "IperfModel.BandWidth" />
        //< Binding Path="IperfModel.BandWidthUnit"/>
        //<Binding Path = "IperfModel.PacketLen" />
        //< Binding Path="IperfModel.Reverse"/>

        //Binding sequences end <-----
        //public string Command { get; set; }
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var _command = "";
                var _role = values[0].ToString();

                var _serverIp = values[1].ToString();
                var _port = (int)values[2];
                var _parallel = (int)values[3];
                var _time = (int)values[4];
                var _interval = (int)values[5];
                var _tcpFlag = (bool)values[6];
                var _tcpWindowSize = (int)values[7];
                var _tcpWindowUnit = values[8].ToString();
                var _udpFlag = (bool)values[9];
                var _bandwidth = (int)values[10];
                var _bandwidthUnit = values[11].ToString();
                var _packetLen = (int)values[12];
                var _reverse = (bool)values[13];
                if (_role == "-s")
                {
                    _command += _role;
                    _command += " -p ";
                    _command += _port;
                    _command += " -i ";
                    _command += _interval;

                }else if (_role == "-c")
                {
                    _command += _role;
                    _command += " ";
                    _command += _serverIp;
                    _command += " -p ";
                    _command += _port;
                    _command += " -P ";
                    _command += _parallel;
                    _command += " -t ";
                    _command += _time;
                    _command += " -i ";
                    _command += _interval;
                    if (_tcpFlag)
                    {
                        _command += " -w ";
                        _command += _tcpWindowSize;
                        _command += _tcpWindowUnit;
                    }
                    if (_udpFlag)
                    {
                        _command += " -b ";
                        _command += _bandwidth;
                        _command += _bandwidthUnit;
                        _command += " -u ";
                    }
                    if (_packetLen > 0)
                    {
                        _command += " -l ";
                        _command += _packetLen;
                    }
                    if (_reverse)
                    {
                        _command += " -R";
                    }
                }
                else
                {
                    _command = "command not support!";
                }
                return _command;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
