# Ninja

![logo](/Resources/favicon.png)

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
<img src="https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/iperf.png" width="700">
<img src="https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/sniffer.png" width="700">
<img src="https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/snifferstats.png" width="700">
<img src="https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/websocket.png" width="700">
<img src="https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/portlisten.png" width="700">
<img src="https://github.com/is-leeroy-jenkins/Ninja/tree/master/Resources/Assets/demo/routetable.png" width="700">


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/main/Resources/Assets/GitHubImages/csharp.png) Code

- Ninja supports AnyCPU as well as x86/x64 specific builds
- [Controls](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Controls) - main UI layer and associated controls and related functionality.
- [Enumerations](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Enumerations) - various enumerations used for budgetary accounting.
- [Extensions](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Extensions)- useful extension methods for budget analysis by type.
- [Clients](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Clients) - other tools used and available.
- [Ninja](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Ninja) - models used in EPA budget data analysis.
- [IO](https://github.com/is-leeroy-jenkins/Ninja/tree/main/IO) - input output classes used for networking and the file systemm.
- [Static](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Static) - static types used by Ninja.
- [Interfaces](https://github.com/is-leeroy-jenkins/Ninja/tree/Interfaces) - abstractions used in network analysis.
- `bin` - Binaries are included in the `bin` folder due to the complex Baby setup required. Don't empty this folder.
- `bin/storage` - HTML and JS required for downloads manager and custom error pages

## Build

- [x] VisualStudio 2022（Based on .NET8 and WPF）

```bash
$ git clone https://github.com/is-leeroy-jenkins/Ninja.git
$ cd Ninja
```
Run `Ninja.sln`


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/main/Resources/Assets/GitHubImages/documentation.png) Documentation

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

## License

MIT License（[License MIT](./LICENSE)）
