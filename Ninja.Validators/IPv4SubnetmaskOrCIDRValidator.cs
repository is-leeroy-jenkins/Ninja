using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators;

using Utilities;

public class IPv4SubnetmaskOrCIDRValidator : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var subnetmaskOrCidr = (value as string)?.Trim();

        if (string.IsNullOrEmpty(subnetmaskOrCidr))
            return new ValidationResult(false, Strings.EnterValidSubnetmaskOrCIDR);


        if (Regex.IsMatch(subnetmaskOrCidr, RegexHelper.SubnetmaskRegex))
            return ValidationResult.ValidResult;

        if (int.TryParse(subnetmaskOrCidr.TrimStart('/'), out var cidr))
            if (cidr >= 0 && cidr < 33)
                return ValidationResult.ValidResult;

        return new ValidationResult(false, Strings.EnterValidSubnetmaskOrCIDR);
    }
}