﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using Ninja.Localization.Resources;

namespace Ninja.Validators
{
    public class IsNameUnique : ValidationRule
    {
        public IsNameUniqueDependencyObjectWrapper Wrapper { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return Wrapper.UsedNames.Any(x => x.Equals(value as string, StringComparison.OrdinalIgnoreCase))
                ? new ValidationResult(false, Strings.ErrorMessage_NameIsAlreadyUsed)
                : ValidationResult.ValidResult;
        }
    }
}