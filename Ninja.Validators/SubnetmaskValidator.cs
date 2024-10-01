using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class SubnetmaskValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value != null && Regex.IsMatch((string)value, RegexHelper.SubnetmaskRegex)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.EnterValidSubnetmask);
        }
    }
}