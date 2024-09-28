# Network

![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/ProjectTemplate.png)

Ninja is a network toolkit, support iperf, tcp, udp, websocket, 
mqtt, sniffer, pcap, port scan, listen, ip scan .etc.
Can be used  for network study by WPF MVVM.


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/features.png)  Functionality

- [x] Iperf2/Iperf3 Server and Client(Realtime plot and result save)
- [x] Network Scan(IP multi-thread scan)
- [x] Port Scan(Port status multi-thread scan)
- [x] Route tables(IPv4 & IPv6)
- [x] Local Port listen(TCP & UDP)
- [x] TCP Server & Client
- [x] UDP Server & Client
- [x] WebSocket Server & Client
- [x] MQTT
- [x] Sniffer(Using pcap, packets capture, save, filter, statistics, plot. etc.)
- [ ] ...


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/csharp.png)  Code

- Ninja supports 'AnyCPU' as well as x86/x64 specific builds
- [Controls](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/Controls) - controls associated main ui layer and related functionality.
- [Enumerations](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Enumerations) - various enumerations used for budgetary accounting.
- [Extensions](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Extensions)- useful extension methods for budget analysis by type.
- [Sockets](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Sockets) - tcp/udp/websockets classses
- [Interfaces](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Sockets) - network interface classes
- [Converters](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Converters) - type converters 
- [Stats](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Stats) - statistic classes 
- [Clients](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Clients) - other tools used and available.
- [Models](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Models) - models used in network analysis
- [Services](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Services) - networking service classes used in Ninja.
- [Static](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Static) - static types used by Ninja.
- [Interfaces](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Network/Interfaces) - abstractions used in network analysis.
- [Views](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/Views) - Views
- [ViewModels](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/ViewModels) - models used by the ui
- [Themes](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/Themes) - themes used in the ui
- [Windows](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/Windows) - window classes

## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/tools.png)  Build

- [x] VisualStudio 2022
- [x] Based on .NET8 and WPF

```bash
$ git clone https://github.com/is-leeroy-jenkins/Ninja.git
$ cd Ninja
```
Run `Ninja.sln`


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/documentation.png)  Documentation

- [User Guide](Resources/Github/Users.md) - how to use Ninja.
- [Compilation Guide](Resources/Github/Compilation.md) - instructions on how to compile Ninja.
- [Configuration Guide](Resources/Github/Configuration.md) - information for the Ninja configuration file. 
- [Distribution Guide](Resources/Github/Distribution.md) -  distributing Ninja.



## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/GitHubImages/library.png)  Libraries
Third-party librarirs used y Ninja.
* [Iperf](https://github.com/esnet/iperf)
* [oxyplot](https://github.com/oxyplot/oxyplot)

## Iperf
- ![iperf](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/DemoImages/iperf.png)

## Sniffer
- ![sniffer](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/DemoImages/sniffer.png)

## Stats
- ![stats](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/DemoImages/snifferstats.png)

## Websocket
- ![websocket](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/DemoImages/websocket.png)

## Port
- ![portlisten](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/DemoImages/portlisten.png)

## Routing
- ![routetable](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Resources/Assets/DemoImages/routetable.png)

## License

MIT License（[License MIT](./LICENSE)）


## Packages
```
<?xml version="1.0" encoding="utf-8"?>

<ItemGroup>    
    <PackageReference Include="EPPlus" Version="7.3.2" />
    <PackageReference Include="EPPlus.Interfaces" Version="6.1.1" />
    <PackageReference Include="EPPlus.System.Drawing" Version="6.1.1" />
    <PackageReference Include="Google.Apis.CustomSearchAPI.v1" Version="1.68.0.3520" />
    <PackageReference Include="MahApps.Metro" Version="2.4.10" />
    <PackageReference Include="Microsoft.CSharp" Version="4.7.0" />
    <PackageReference Include="Microsoft.Office.Interop.Excel" Version="15.0.4795.1001" />
    <PackageReference Include="Microsoft.Office.Interop.Outlook" Version="15.0.4797.1004" />
    <PackageReference Include="Microsoft.Office.Interop.Word" Version="15.0.4797.1004" />
    <PackageReference Include="ModernWpfUI" Version="0.9.6" />
    <PackageReference Include="SkiaSharp" Version="2.88.8" />
    <PackageReference Include="Syncfusion.Licensing" Version="24.1.41" />
    <PackageReference Include="Syncfusion.SfSkinManager.WPF" Version="24.1.41" />
    <PackageReference Include="Syncfusion.Shared.Base" Version="24.1.41" />
    <PackageReference Include="Syncfusion.Shared.WPF" Version="24.1.41" />
    <PackageReference Include="Syncfusion.Themes.FluentDark.WPF" Version="24.1.41" />
    <PackageReference Include="Syncfusion.Tools.WPF" Version="24.1.41" />
    <PackageReference Include="Syncfusion.UI.WPF.NET" Version="27.1.48" />
    <PackageReference Include="System.Data.Common" Version="4.3.0" />
    <PackageReference Include="System.Data.OleDb" Version="8.0.0" />
    <PackageReference Include="System.Data.SqlClient" Version="4.8.6" />
    <PackageReference Include="ToastNotifications.Messages.Net6" Version="1.0.4" />
    <PackageReference Include="ToastNotifications.Net6" Version="1.0.4" />
</ItemGroup>  
<ItemGroup>
    <Reference Include="LinqStatistics">
      <HintPath>Libraries\LinqStatistics\LinqStatistics.dll</HintPath>
    </Reference>
    <Reference Include="OxyPlot">
      <HintPath>Libraries\OxyPlot\OxyPlot.dll</HintPath>
    </Reference>
    <Reference Include="OxyPlot.Wpf">
      <HintPath>Libraries\OxyPlot\OxyPlot.Wpf.dll</HintPath>
    </Reference>
    <Reference Include="OxyPlot.Wpf.Shared">
      <HintPath>Libraries\OxyPlot\OxyPlot.Wpf.Shared.dll</HintPath>
    </Reference>
    <Reference Include="PacketDotNet">
      <HintPath>Libraries\PacketDotNet\PacketDotNet.dll</HintPath>
    </Reference>
    <Reference Include="PcapDotNet.Base">
      <HintPath>Libraries\PcapDotNet\PcapDotNet.Base.dll</HintPath>
    </Reference>
    <Reference Include="PcapDotNet.Core">
      <HintPath>Libraries\PcapDotNet\PcapDotNet.Core.dll</HintPath>
    </Reference>
    <Reference Include="PcapDotNet.Core.Extensions">
      <HintPath>Libraries\PcapDotNet\PcapDotNet.Core.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="PcapDotNet.Packets">
      <HintPath>Libraries\PcapDotNet\PcapDotNet.Packets.dll</HintPath>
    </Reference>
    <Reference Include="System.Data.SQLite">
      <HintPath>Libraries\System.Data\System.Data.SQLite.dll</HintPath>
    </Reference>
    <Reference Include="System.Data.SQLite.EF6">
      <HintPath>Libraries\System.Data\System.Data.SQLite.EF6.dll</HintPath>
    </Reference>
    <Reference Include="System.Data.SQLite.Linq">
      <HintPath>Libraries\System.Data\System.Data.SQLite.Linq.dll</HintPath>
    </Reference>
    <Reference Include="System.Data.SqlServerCe">
      <HintPath>Libraries\System.Data\System.Data.SqlServerCe.dll</HintPath>
    </Reference>
    <Reference Include="websocket-sharp">
      <HintPath>Libraries\WebSocketSharp\websocket-sharp.dll</HintPath>
    </Reference>
 </ItemGroup>
```