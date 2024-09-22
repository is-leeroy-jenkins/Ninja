

namespace Ninja.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class Ipv6Result
    {
        /// <summary>
        /// Gets or sets the interface.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the metric.
        /// </summary>
        /// <value>
        /// The metric.
        /// </value>
        public int Metric { get; set; }

        /// <summary>
        /// Gets or sets the dest address.
        /// </summary>
        /// <value>
        /// The dest address.
        /// </value>
        public string DestAddress { get; set; }

        /// <summary>
        /// Gets or sets the gateway.
        /// </summary>
        /// <value>
        /// The gateway.
        /// </value>
        public string Gateway { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ipv6Result"/> class.
        /// </summary>
        public Ipv6Result( )
        {
        }
    }
}
