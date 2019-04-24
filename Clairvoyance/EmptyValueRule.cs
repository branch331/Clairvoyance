using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Clairvoyance
{
    /// <summary>
    /// Validation rule to check and deny empty/null values.
    /// </summary>
    class EmptyValueRule : ValidationRule
    {
        public EmptyValueRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "Please enter a value.");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
