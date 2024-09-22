using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Interfaces
{
    public class IpInterface
    {
        public PingReply PingSweep(string ip)
        {
            PingReply _reply = null;
            Ping _pingSender = null;
            try
            {
                _pingSender = new Ping();

                var _options = new PingOptions();
                _options.DontFragment = true;

                var _data = "hello";
                var _buffer = Encoding.ASCII.GetBytes(_data);
                var _timeout = 1000;

                var _ipa = IPAddress.Parse(ip);
                var _replyPing = _pingSender.Send(_ipa, _timeout, _buffer, _options);// .Send(ip, timeout, buffer, options);
                _reply = _replyPing;
            }
            catch (Exception ex)
            {
                _reply = null;
            }
            finally
            {
                _pingSender.Dispose();
            }
            return _reply;
        }

        public string GetHostName(string ip)
        {
            string _host = null;
            try
            {
                _host = Dns.GetHostEntry(ip).HostName;
                //host = Dns.GetHostEntryAsync(ip).HostName;
            }
            catch (Exception ex)
            {
                _host = null;
            }
            return _host;
        }
        public void GetIp()
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces();
            var _len = _interfaces.Length;

            for (var _i = 0; _i < _len; _i++)
            {
                var _ni = _interfaces[_i];

                if (_ni.OperationalStatus == OperationalStatus.Up)
                {
                    var _property = _ni.GetIPProperties();
                    foreach (var _ip in _property.UnicastAddresses)
                    {
                        if (_ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            var _address = _ip.Address.ToString();
                            var _niname = _ni.Name.ToString();
                            Console.WriteLine("【" + _niname + "】：" + _address);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Converts a uint representation of an Ip address to a
        /// string.
        /// </summary>
        /// <param name="ipAddr">The IP address to convert</param>
        /// <returns>A string representation of the IP address.</returns>
        public string LongToIpAddress(uint ipAddr)
        {
            //return new System.Net.IPAddress(IPAddr).ToString();
            var _a = (byte)((ipAddr & 0xFF000000) >> 24);
            var _b = (byte)((ipAddr & 0x00FF0000) >> 16);
            var _c = (byte)((ipAddr & 0x0000FF00) >> 8);
            var _d = (byte)(ipAddr & 0x000000FF);
            var _ipStr = String.Format("{0}.{1}.{2}.{3}", _a, _b, _c, _d);
            return _ipStr;
        }
        //public string Int2IP(uint IPAddr)
        //{
        //    byte a = (byte)((IPAddr & 0xFF000000) >> 24);
        //    byte b = (byte)((IPAddr & 0x00FF0000) >> 16);
        //    byte c = (byte)((IPAddr & 0x0000FF00) >> 8);
        //    byte d = (byte)(IPAddr & 0x000000FF);
        //    string ipStr = String.Format(" {0}.{1}.{2}.{3} ", a, b, c, d);
        //    return ipStr;
        //}
        /// <summary>
        /// Converts a string representation of an IP address to a
        /// uint. This encoding is proper and can be used with other
        /// networking functions such
        /// as the System.Net.IPAddress class.
        /// </summary>
        /// <param name="ipAddr">The Ip address to convert.</param>
        /// <returns>Returns a uint representation of the IP
        /// address.</returns>
        public uint IpAddressToLong(string ipAddr)
        {
            var _oIp = System.Net.IPAddress.Parse(ipAddr);
            var _byteIp = _oIp.GetAddressBytes();


            var _ip = (uint)_byteIp[3] << 24;
            _ip += (uint)_byteIp[2] << 16;
            _ip += (uint)_byteIp[1] << 8;
            _ip += (uint)_byteIp[0];

            return _ip;
        }
        /// <summary>
        /// This encodes the string representation of an IP address
        /// to a uint, but backwards so that it can be used to
        /// compare addresses. This function is used internally
        /// for comparison and is not valid for valid encoding of
        /// IP address information.
        /// </summary>
        /// <param name="ipAddr">A string representation of the IP
        /// address to convert</param>
        /// <returns>Returns a backwards uint representation of the
        /// string.</returns>
        public uint IpAddressToLongBackwards(string ipAddr)
        {
            var _oIp = System.Net.IPAddress.Parse(ipAddr);
            var _byteIp = _oIp.GetAddressBytes();


            var _ip = (uint)_byteIp[0] << 24;
            _ip += (uint)_byteIp[1] << 16;
            _ip += (uint)_byteIp[2] << 8;
            _ip += (uint)_byteIp[3];

            return _ip;
        }
    }
}
