using System.Windows;

namespace Ninja.Validators;

public class ServerDependencyObjectWrapper : DependencyObject
{
    public static readonly DependencyProperty AllowOnlyIPAddressProperty = DependencyProperty.Register(
        "AllowOnlyIPAddress",
        typeof(bool),
        typeof(ServerDependencyObjectWrapper));

    public bool AllowOnlyIPAddress
    {
        get => (bool)GetValue(AllowOnlyIPAddressProperty);
        set => SetValue(AllowOnlyIPAddressProperty, value);
    }
}