using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class IPv4SubnetValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var subnet = (value as string)?.Trim();

            if (string.IsNullOrEmpty(subnet))
                return new ValidationResult(false, Strings.EnterValidSubnet);

            if (Regex.IsMatch(subnet, RegexHelper.IPv4AddressCidrRegex))
                return ValidationResult.ValidResult;

            if (Regex.IsMatch(subnet, RegexHelper.IPv4AddressSubnetmaskRegex))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, Strings.EnterValidSubnet);
        }
    }
}