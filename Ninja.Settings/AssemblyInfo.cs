﻿using System;

namespace Ninja.Settings
{
    /// <summary>
    ///     Class contains information about the current executing assembly.
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="AssemblyInfo" /> class.
        /// </summary>
        public AssemblyInfo()
        {
        }

        /// <summary>
        ///     Version of the current executing assembly.
        ///     Like "2022.12.24.0".
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        ///     Location of the current executing assembly.
        ///     Like "C:\Program Files\Ninja\Ninja.exe".
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     Name of the current executing assembly.
        ///     Like "Ninja".
        /// </summary>
        public string Name { get; set; }
    }
}