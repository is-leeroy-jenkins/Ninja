using System.Globalization;
using System.Windows.Controls;
using Ninja.Localization.Resources;

namespace Ninja.Validators;

public class GroupNameValidator : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var groupName = value as string;

        if (groupName.StartsWith("~"))
            return new ValidationResult(false,
                string.Format(Strings.GroupNameCannotStartWithX, "~"));

        return ValidationResult.ValidResult;
    }
}