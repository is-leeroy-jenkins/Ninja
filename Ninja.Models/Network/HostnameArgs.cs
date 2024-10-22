﻿using System;

namespace Ninja.Models.Network
{
    /// <summary>
    ///     Contains the information of a resolved hostname in a <see cref="Ping" />.
    /// </summary>
    public class HostnameArgs : EventArgs
    {
        /// <summary>
        ///     Creates a new instance of <see cref="HostnameArgs" /> with the given hostname.
        /// </summary>
        /// <param name="args"></param>
        public HostnameArgs(string hostname)
        {
            Hostname = hostname;
        }

        /// <summary>
        ///     Hostname.
        /// </summary>
        public string Hostname { get; }
    }
}