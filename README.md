![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/GitHubImages/ProjectTemplate.png)

<div align="left">
  <p>
    <a href="#-download">Download</a> •  <a href="#--documentation">Documentation</a> •<a href="#-build">Build</a> • <a href="#-license">License</a>
  </p>
  <p>
    <b>A powerful open source tool for managing networks and troubleshooting network problems!</b>
  </p>
  <p align="left">
    Connect and manage remote systems with Remote Desktop, PowerShell, PuTTY, TigerVNC or AWS (Systems Manager) Session Manager. Analyze and troubleshoot your network and systems with features such as the WiFi Analyzer,IP Scanner, Port Scanner, Ping Monitor, Traceroute, DNS lookup or LLDP/CDP capture in a unfied interface. Hosts (or networks) can be saved in (encrypted) profiles and used across all features.  

</div>


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/GitHubImages/csharp.png)  Code

- Ninja supports 'AnyCPU' as well as x86/x64 specific builds
- [Controls](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Controls) - controls associated main ui layer and related functionality.
- [Converters](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Converters) - various converter classes.
- [Documentation](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Documentation)- app docs.
- [Localization](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Localization) - internationalization
- [Profiles](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Profiles) - profile classes
- [Models](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Models) - models used in network analysis
- [Settings](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Settings) - settings used in Ninja.
- [Utility](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Utilities) - static types used by Ninja.
- [Utilities.Wpf](https://github.com/is-leeroy-jenkins/Ninja/tree/master/Ninja.Utilities.WPF) - wpf utility classes.
- [Validators](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/Views) - Views
- [Ninja](https://github.com/is-leeroy-jenkins/Ninja/tree/master/UI/ViewModels) - models used by the ui


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/GitHubImages/documentation.png)  Documentation

- [User Guide](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/Github/Users.md) - how to use Ninja.
- [Compilation Guide](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/Github/Compilation.md) - instructions on how to compile Ninja.
- [Configuration Guide](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/Github/Configuration.md) - information for the Ninja configuration file. 
- [Distribution Guide](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/Github/Distribution.md) -  distributing Ninja.


## 📦 Download

Pre-built and binaries (setup, portable and archive) are available on the with install instructions (e.g. silent install). 




## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/GitHubImages/tools.png) Build

- [x] VisualStudio 2022
- [x] Based on .NET8 and WPF


```bash
$ git clone https://github.com/is-leeroy-jenkins/Ninja.git
$ cd Ninja
```
Run `Ninja.sln`


You can build the application like any other .NET / WPF application on Windows.

1. Make sure that the following requirements are installed:

   - [.NET 8.x - SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - Visual Studio 2022 with `.NET desktop development` and `Universal Windows Platform development`

2. Clone the repository with all submodules:

   ```PowerShell
   # Clone the repository
   git clone https://github.com/is-leeroy-jenkins/Ninja

   # Navigate to the repository
   cd Ninja

   # Clone the submodules
   git submodule update --init
   ```

3. Open the project file `.\Source\Ninja.sln` with Visual Studio or JetBrains Rider to build (or debug)
   the solution.

   > **ALTERNATIVE**
   >
   > With the following commands you can directly build the binaries from the command line:
   >
   > ```PowerShell
   > dotnet restore .\Source\Ninja.sln
   >
   > dotnet build .\Source\Ninja.sln --configuration Release --no-restore
   > ```



## 🙏 Acknoledgements

Thanks to everyone helping to improve Ninja by contributing code, translations, bug reports, feature requests, documentation, and more.
Ninja uses the following projects and libraries. Please consider supporting them as well (e.g., by starring their repositories):

|                                                                               |                                                                        |
| ----------------------------------------------------------------------------- | ---------------------------------------------------------------------- |
| [#SNMP Library](https://github.com/lextudio/sharpsnmplib)                     | SNMP library for .NET                                                  |
| [AirspaceFixer](https://github.com/chris84948/AirspaceFixer)                  | AirspacePanel fixes all Airspace issues with WPF-hosted Winforms.      |
| [ControlzEx](https://github.com/ControlzEx/ControlzEx)                        | Shared Controlz for WPF and more                                       |
| [DnsClient.NET](https://github.com/MichaCo/DnsClient.NET)                     | Powerful, high-performance open-source library for DNS lookups         |
| [Docusaurus](https://docusaurus.io/)                                          | Easy to maintain open source documentation websites.                   |
| [Dragablz](https://dragablz.net/)                                             | Tearable TabControl for WPF                                            |
| [GongSolutions.Wpf.DragDrop](https://github.com/punker76/gong-wpf-dragdrop)   | An easy to use drag'n'drop framework for WPF                           |
| [IPNetwork](https://github.com/lduchosal/ipnetwork)                           | .NET library for complex network, IP, and subnet calculations          |
| [LoadingIndicators.WPF](https://github.com/zeluisping/LoadingIndicators.WPF)  | A collection of loading indicators for WPF                             |
| [MahApps.Metro.IconPacks](https://github.com/MahApps/MahApps.Metro.IconPacks) | Awesome icon packs for WPF and UWP in one library                      |
| [MahApps.Metro](https://mahapps.com/)                                         | UI toolkit for WPF applications                                        |
| [NetBeauty2](https://github.com/nulastudio/NetBeauty2)                        | Move .NET app runtime components and dependencies into a sub-directory |
| [PSDiscoveryProtocol](https://github.com/lahell/PSDiscoveryProtocol)          | PowerShell module for LLDP/CDP discovery                               |

## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/GitHubImages/signature.png)  Code Signing 

Ninja uses free code signing provided by [SignPath.io](https://signpath.io/) and a free code signing certificate
from [SignPath Foundation](https://signpath.org/).

The binaries and installer are built on [AppVeyor](https://ci.appveyor.com/project/is-leeroy-jenkins/networkmanager) directly from the [GitHub repository](https://github.com/is-leeroy-jenkins/Ninja/blob/main/appveyor.yml).
Build artifacts are automatically sent to [SignPath.io](https://signpath.io/) via webhook, where they are signed after manual approval by the maintainer.
The signed binaries are then uploaded to the [GitHub releases](https://github.com/is-leeroy-jenkins/Ninja/releases) page.


## ![](https://github.com/is-leeroy-jenkins/Ninja/blob/master/Ninja/Resources/GitHubImages/training.png) Privacy Policy

This program will not transfer any information to other networked systems unless specifically requested by the user or the person installing or operating it.

Ninja has integrated the following services for additional functions, which can be enabled or disabled at the first start (in the welcome dialog) or at any time in the settings:

- [api.github.com](https://docs.github.com/en/site-policy/privacy-policies/github-general-privacy-statement) (Check for program updates)
- [ipify.org](https://www.ipify.org/) (Retrieve the public IP address used by the client)
- [ip-api.com](https://ip-api.com/docs/legal) (Retrieve network information such as geo location, ISP, DNS resolver used, etc. used by the client)

## 📝 License

Ninja is published under the [GNU General Public License v3](https://github.com/is-leeroy-jenkins/Ninja/blob/main/LICENSE).

The licenses of the libraries used can be found [here](https://github.com/is-leeroy-jenkins/Ninja/tree/main/Source/Ninja.Documentation/Licenses).
