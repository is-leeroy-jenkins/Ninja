using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators;

using Utilities;

public class EmptyOrIPv4AddressValidator : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrEmpty(value as string))
            return ValidationResult.ValidResult;

        return Regex.IsMatch((string)value, RegexHelper.IPv4AddressRegex)
            ? ValidationResult.ValidResult
            : new ValidationResult(false, Strings.EnterValidIPv4Address);
    }
}