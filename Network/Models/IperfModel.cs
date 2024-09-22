using Ninja.ViewModels;


namespace Ninja.Models
{
    using ViewModels;

    public class IperfModel : MainWindowBase
    {
        private string _version;
        public string Version
        {
            get{ return _version; }
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(nameof(Version));
                }
            }
        }
        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged(nameof(Role));
                }
            }
        }
        private string _serverIp;
        public string ServerIp
        {
            get { return _serverIp; }
            set
            {
                if (_serverIp != value)
                {
                    _serverIp = value;
                    OnPropertyChanged(nameof(ServerIp));
                }
            }
        }
        private int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    OnPropertyChanged(nameof(Port));
                }
            }
        }

        private int _time;
        public int Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }
        private int _parallel;
        public int Parallel
        {
            get { return _parallel; }
            set
            {
                if (_parallel != value)
                {
                    _parallel = value;
                    OnPropertyChanged(nameof(Parallel));
                }
            }
        }
        private int _interval;
        public int Interval
        {
            get { return _interval; }
            set
            {
                if (_interval != value)
                {
                    _interval = value;
                    OnPropertyChanged(nameof(Interval));
                }
            }
        }
        private int _tcpWindowSize;
        public int TcpWindowSize
        {
            get { return _tcpWindowSize; }
            set
            {
                if (_tcpWindowSize != value)
                {
                    _tcpWindowSize = value;
                    OnPropertyChanged(nameof(TcpWindowSize));
                }
            }
        }
        private string _tcpWindowUnit;
        public string TcpWindowUnit
        {
            get { return _tcpWindowUnit; }
            set
            {
                if (_tcpWindowUnit != value)
                {
                    _tcpWindowUnit = value;
                    OnPropertyChanged(nameof(TcpWindowUnit));
                }
            }
        }
        private int _bandWidth;
        public int BandWidth
        {
            get { return _bandWidth; }
            set
            {
                if (_bandWidth != value)
                {
                    _bandWidth = value;
                    OnPropertyChanged(nameof(BandWidth));
                }
            }
        }
        private string _bandWidthUnit;
        public string BandWidthUnit
        {
            get { return _bandWidthUnit; }
            set
            {
                if (_bandWidthUnit != value)
                {
                    _bandWidthUnit = value;
                    OnPropertyChanged(nameof(BandWidthUnit));
                }
            }
        }
        private int _packetLen;
        public int PacketLen
        {
            get { return _packetLen; }
            set
            {
                if (_packetLen != value)
                {
                    _packetLen = value;
                    OnPropertyChanged(nameof(PacketLen));
                }
            }
        }
        private bool _tcpFlag;
        public bool TcpFlag
        {
            get { return _tcpFlag; }
            set
            {
                if (_tcpFlag != value)
                {
                    _tcpFlag = value;
                    OnPropertyChanged(nameof(TcpFlag));
                }
            }
        }
        private bool _udpFlag;
        public bool UdpFlag
        {
            get { return _udpFlag; }
            set
            {
                if (_udpFlag != value)
                {
                    _udpFlag = value;
                    OnPropertyChanged(nameof(UdpFlag));
                }
            }
        }
        private bool _reverse;
        public bool Reverse
        {
            get { return _reverse; }
            set
            {
                if (_reverse != value)
                {
                    _reverse = value;
                    OnPropertyChanged(nameof(Reverse));
                }
            }
        }
        private double _throughput;
        public double Throughput
        {
            get { return _throughput; }
            set
            {
                if (_throughput != value)
                {
                    _throughput = value;
                    OnPropertyChanged(nameof(Throughput));
                }
            }
        }
        private string _command;
        public string Command
        {
            get { return _command; }
            set
            {
                if (_command != value)
                {
                    _command = value;
                    OnPropertyChanged(nameof(Command));
                }
            }
        }
        private string _output;
        public string Output
        {
            get { return _output; }
            set
            {
                if (_output != value)
                {
                    _output = value;
                    OnPropertyChanged(nameof(Output));
                }
            }
        }

        public IperfModel()
        {
            Version = "iperf.exe";
            Role = "-c";
            ServerIp = "10.21.68.29";
            Port = 5001;
            Parallel = 4;
            Time = 60;
            Interval = 1;
            TcpFlag = true;
            TcpWindowSize = 2;
            TcpWindowUnit = "M";
            UdpFlag = false;
            BandWidth = 100;
            BandWidthUnit = "M";
            PacketLen = 0;
            Reverse = false;
        }
    }
}
