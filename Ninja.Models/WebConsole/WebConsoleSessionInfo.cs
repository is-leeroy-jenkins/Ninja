﻿namespace Ninja.Models.WebConsole
{
    /// <summary>
    ///     Class contains information's about a WebConsole session.
    /// </summary>
    public class WebConsoleSessionInfo
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="WebConsoleSessionInfo" /> class.
        /// </summary>
        public WebConsoleSessionInfo()
        {
        }

        /// <summary>
        ///     URL to connect to.
        /// </summary>
        public string Url { get; set; }
    }
}