

namespace Ninja.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class IpScanResult : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="IpScanResult"/> class.
        /// </summary>
        public IpScanResult()
        {
        }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the TTL.
        /// </summary>
        /// <value>
        /// The TTL.
        /// </value>
        public string Ttl { get; set; }

        /// <summary>
        /// Updates the specified field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        public void Update<T>(ref T field, T value,
            [ CallerMemberName ]
            string propertyName = null)

        {
            if(EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var _handler = PropertyChanged;
            _handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Occurs when a property value changes.</summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
