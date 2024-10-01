using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class RemoteDesktopHostnameAndPortValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var hostnameAndPort = (string)value;

            if (hostnameAndPort.Contains(':'))
            {
                var hostnameAndPortValues = hostnameAndPort.Split(':');

                if (Regex.IsMatch(hostnameAndPortValues[0], RegexHelper.HostnameOrDomainRegex) &&
                    !string.IsNullOrEmpty(hostnameAndPortValues[1]) &&
                    Regex.IsMatch(hostnameAndPortValues[1], RegexHelper.PortRegex))
                    return ValidationResult.ValidResult;

                return new ValidationResult(false, Strings.EnterValidHostnameAndPort);
            }

            return Regex.IsMatch((string)value, RegexHelper.HostnameOrDomainRegex)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.EnterValidHostname);
        }
    }
}