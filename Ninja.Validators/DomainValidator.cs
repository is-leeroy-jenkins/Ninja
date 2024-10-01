using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class DomainValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is not string domain)
                return new ValidationResult(false, Strings.EnterValidDomain);

            return Regex.IsMatch(domain, RegexHelper.DomainRegex)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.EnterValidDomain);
        }
    }
}