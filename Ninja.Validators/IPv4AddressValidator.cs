using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class IPv4AddressValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var ipAddress = (value as string)?.Trim();

            if (string.IsNullOrEmpty(ipAddress))
                return new ValidationResult(false, Strings.EnterValidIPv4Address);

            return Regex.IsMatch(ipAddress, RegexHelper.IPv4AddressRegex)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.EnterValidIPv4Address);
        }
    }
}