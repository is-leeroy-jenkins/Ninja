using System.Windows;
using Ninja.Models.Network;

namespace Ninja.Validators
{
    using Models.Network;

    public class SNMPOIDDependencyObjectWrapper : DependencyObject
    {
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode",
            typeof(SNMPMode),
            typeof(SNMPOIDDependencyObjectWrapper));

        public SNMPMode Mode
        {
            get => (SNMPMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }
}