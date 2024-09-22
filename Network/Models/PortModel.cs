using Ninja.ViewModels;

namespace Ninja.Models
{
    using ViewModels;

    public class PortModel : MainWindowBase
    {
        public class NetInterfaceInfo
        {
            public string Name { get; set; }
            public string Ip { get; set; }
            public string Description { get; set; }
            public string Mask { get; set; }
        }

        private NetInterfaceInfo[] _netInfoItemSource;
        public NetInterfaceInfo[] NetInfoItemSource
        {
            get { return _netInfoItemSource; }
            set
            {
                if (_netInfoItemSource != value)
                {
                    _netInfoItemSource = value;
                    OnPropertyChanged(nameof(NetInfoItemSource));
                }
            }
        }

        private string _startIp;
        public string StartIp
        {
            get { return _startIp; }
            set
            {
                if (_startIp != value)
                {
                    _startIp = value;
                    OnPropertyChanged(nameof(StartIp));
                }
            }
        }
        private string _stopIp;
        public string StopIp
        {
            get { return _stopIp; }
            set
            {
                if (_stopIp != value)
                {
                    _stopIp = value;
                    OnPropertyChanged(nameof(StopIp));
                }
            }
        }
        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    OnPropertyChanged(nameof(Ip));
                }
            }
        }
        private int _onlineCnt;
        public int OnlineCnt
        {
            get { return _onlineCnt; }
            set
            {
                if (_onlineCnt != value)
                {
                    _onlineCnt = value;
                    OnPropertyChanged(nameof(OnlineCnt));
                }
            }
        }
        private int _offlineCnt;
        public int OfflineCnt
        {
            get { return _offlineCnt; }
            set
            {
                if (_offlineCnt != value)
                {
                    _offlineCnt = value;
                    OnPropertyChanged(nameof(OfflineCnt));
                }
            }
        }
        private string _scanButtonName;
        public string ScanButtonName
        {
            get { return _scanButtonName; }
            set
            {
                if (_scanButtonName != value)
                {
                    _scanButtonName = value;
                    OnPropertyChanged(nameof(ScanButtonName));
                }
            }
        }
        public PortModel()
        {
            StartIp = "192.168.1.1";
            StopIp = "192.168.1.255";
            ScanButtonName = "Start";
            OfflineCnt = 0;
            OnlineCnt = 0;
        }
    }
}
