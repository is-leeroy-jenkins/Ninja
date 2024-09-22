

namespace Ninja.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class Ipv4Result
    {
        /// <summary>
        /// Gets or sets the dest address.
        /// </summary>
        /// <value>
        /// The dest address.
        /// </value>
        public string DestAddress { get; set; }

        /// <summary>
        /// Gets or sets the mask.
        /// </summary>
        /// <value>
        /// The mask.
        /// </value>
        public string Mask { get; set; }

        /// <summary>
        /// Gets or sets the gateway.
        /// </summary>
        /// <value>
        /// The gateway.
        /// </value>
        public string Gateway { get; set; }

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
        /// Initializes a new instance of the
        /// <see cref="Ipv4Result"/> class.
        /// </summary>
        public Ipv4Result( )
        {
        }
    }
}
