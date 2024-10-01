using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Ninja.Localization.Resources;
using Ninja.Utilities;

namespace Ninja.Validators
{
    using Utilities;

    public class NotEndWithSemicolonValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return Regex.IsMatch(((string)value).Trim(), RegexHelper.StringNotEndWithSemicolonRegex)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.InputCannotEndWithSemicolon);
        }
    }
}