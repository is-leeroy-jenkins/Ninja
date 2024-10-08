﻿using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Ninja.Localization.Resources;

namespace Ninja.Validators
{
    public class NoSpacesValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string) || !((string)value).Any(char.IsWhiteSpace))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, Strings.SpacesAreNotAllowed);
        }
    }
}