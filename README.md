# Ninja

![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/ApplicationImages/flavicon.png)

Ninja is a network toolkit, support iperf, tcp, udp, websocket, 
mqtt, sniffer, pcap, port scan, listen, ip scan .etc.

Can be used  for network study by WPF MVVM.

## Functionality

- [x] Iperf2/Iperf3 Server and Client(Realtime plot and result save)
- [x] Network Scan(IP multi-thread scan)
- [x] Port Scan(Port status multi-thread scan)
- [x] Route tables(IPv4 & IPv6)
- [x] Local Port listen(TCP & UDP)
- [x] TCP Server & Client
- [x] UDP Server & Client
- [x] WebSocket Server & Client
- [ ] MQTT
- [x] Sniffer(Using pcap, packets capture, save, filter, statistics, plot. etc.)
- [ ] ...


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/csharp.png) Code

- Ninja supports AnyCPU as well as x86/x64 specific builds
- [Controls](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/Controls) - main UI layer and associated controls and related functionality.
- [Enumerations](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Enumerations) - various enumerations used for budgetary accounting.
- [Extensions](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Extensions)- useful extension methods for budget analysis by type.
- [Clients](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Clients) - other tools used and available.
- [Models](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Models) - models used in network analysis
- [Services](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Services) - networking service classes used in Ninja.
- [Static](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Static) - static types used by Ninja.
- [Interfaces](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Interfaces) - abstractions used in network analysis.
- `bin` - Binaries are included in the `bin` folder due to the complex Baby setup required. Don't empty this folder.
- `bin/storage` - HTML and JS required for downloads manager and custom error pages

## Build

- [x] VisualStudio 2022（Based on .NET8 and WPF）

```bash
$ git clone https://github.com/is-leeroy-jenkins/Ninja.git
$ cd Ninja
```
Run `Ninja.sln`


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/documentation.png) Documentation

- [User Guide](Resources/Github/Users.md)
- [Compilation Guide](Resources/Github/Compilation.md)
- [Configuration Guide](Resources/Github/Configuration.md)
- [Distribution Guide](Resources/Github/Distribution.md)


## Packages
```
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="OxyPlot.Core" version="2.1.0" targetFramework="net48" />
  <package id="OxyPlot.Wpf" version="2.1.0" targetFramework="net48" />
  <package id="OxyPlot.Wpf.Shared" version="2.1.0" targetFramework="net48" />
  <package id="Pcap.Net.x64" version="1.0.4.1" targetFramework="net48" />
  <package id="Pcap.Net.x86" version="1.0.4.1" targetFramework="net48" />
  <package id="System.Globalization.Extensions" version="4.3.0" targetFramework="net48" />
  <package id="WebSocketSharp" version="1.0.3-rc11" targetFramework="net48" />
</packages>
```
## Contribute

* [Iperf](https://github.com/esnet/iperf)
* [oxyplot](https://github.com/oxyplot/oxyplot)

## Screenshots
- [iperf]!(https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/iperf.png)
- [sniffer]!(https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/sniffer.png)
- [stats]1(https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/snifferstats.png)
- [websocket]!(https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/websocket.png)
- [portlisten]!(https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/portlisten.png)
- [routetable]!(https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/routetable.png)
## License

MIT License（[License MIT](./LICENSE)）
