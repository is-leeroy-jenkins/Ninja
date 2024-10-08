﻿using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class EmptyOrWindowsDomainValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var domain = value as string;

            if (string.IsNullOrEmpty(domain))
                return ValidationResult.ValidResult;

            // For local authentication "." is a valid domain
            if (domain.Equals("."))
                return ValidationResult.ValidResult;

            return Regex.IsMatch(domain, RegexHelper.HostnameOrDomainRegex)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.EnterValidDomain);
        }
    }
}