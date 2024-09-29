

namespace Ninja
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class UdpClientInfo
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="UdpClientInfo"/> class.
        /// </summary>
        public UdpClientInfo( )
        {
        }

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
        /// Gets or sets the recv bytes.
        /// </summary>
        /// <value>
        /// The recv bytes.
        /// </value>
        public int RecvBytes { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public DateTime Time { get; set; }
    }
}
