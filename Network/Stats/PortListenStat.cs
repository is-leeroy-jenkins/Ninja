

namespace Ninja 
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class PortListenStat
    {
        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the local address.
        /// </summary>
        /// <value>
        /// The local address.
        /// </value>
        public string LocalAddress { get; set; }

        /// <summary>
        /// Gets or sets the local port.
        /// </summary>
        /// <value>
        /// The local port.
        /// </value>
        public string LocalPort { get; set; }

        /// <summary>
        /// Gets or sets the remote address.
        /// </summary>
        /// <value>
        /// The remote address.
        /// </value>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// Gets or sets the remote port.
        /// </summary>
        /// <value>
        /// The remote port.
        /// </value>
        public string RemotePort { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the pid.
        /// </summary>
        /// <value>
        /// The pid.
        /// </value>
        public int Pid { get; set; }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; set; }

        public PortListenStat( )
        {
        }
    }
}
