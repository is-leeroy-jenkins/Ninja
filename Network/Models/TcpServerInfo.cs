
namespace Ninja.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class TcpServerInfo
    {
        /// <summary>
        /// Gets or sets the remote ip.
        /// </summary>
        /// <value>
        /// The remote ip.
        /// </value>
        public string RemoteIp { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public DateTime Time { get; set; }
    }
}
