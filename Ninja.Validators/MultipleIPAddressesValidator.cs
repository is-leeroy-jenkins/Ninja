using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class MultipleIPAddressesValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
                return ValidationResult.ValidResult;

            for (var index = 0; index < ((string)value).Split(';').Length; index++)
            {
                var ipAddress = ((string)value).Split(';')[index];

                if (!Regex.IsMatch(ipAddress.Trim(), RegexHelper.IPv4AddressRegex) &&
                    !Regex.IsMatch(ipAddress.Trim(), RegexHelper.IPv6AddressRegex))
                    return new ValidationResult(false, Strings.EnterOneOrMoreValidIPAddresses);
            }

            return ValidationResult.ValidResult;
        }
    }
}