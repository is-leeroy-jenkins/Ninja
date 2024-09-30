using System.Globalization;
using System.Windows.Controls;
using Ninja.Localization.Resources;

namespace Ninja.Validators;

public class EmptyValidator : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrEmpty((string)value)
            ? new ValidationResult(false, Strings.FieldCannotBeEmpty)
            : ValidationResult.ValidResult;
    }
}