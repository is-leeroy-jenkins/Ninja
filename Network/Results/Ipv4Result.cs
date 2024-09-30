

namespace Ninja.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class Ipv4Result : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ipv4Result"/> class.
        /// </summary>
        public Ipv4Result()
        {
        }

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
