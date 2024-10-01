using System.Globalization;
using System.IO;
using System.Windows.Controls;
using Ninja.Localization.Resources;

namespace Ninja.Validators
{
    public class EmptyOrFileExistsValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string))
                return ValidationResult.ValidResult;

            return File.Exists((string)value)
                ? ValidationResult.ValidResult
                : new ValidationResult(false, Strings.FileDoesNotExist);
        }
    }
}