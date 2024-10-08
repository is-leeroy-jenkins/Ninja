﻿using Ninja.Models.Lookup;

namespace Ninja.Models.Network
{
    using Lookup;

    /// <summary>
    ///     Class representing a port info.
    /// </summary>
    public class PortInfo
    {
        /// <summary>
        ///     Create an instance of <see cref="PortInfo" /> with parameters.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="lookupInfo"></param>
        /// <param name="state"></param>
        public PortInfo(int port, PortLookupInfo lookupInfo, PortState state)
        {
            Port = port;
            LookupInfo = lookupInfo;
            State = state;
        }

        /// <summary>
        ///     Port number.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Port lookup info like service and description.
        /// </summary>
        public PortLookupInfo LookupInfo { get; set; }

        /// <summary>
        ///     State of the port.
        /// </summary>
        public PortState State { get; set; }
    }
}