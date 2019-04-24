using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace Clairvoyance
{
    public class TimeRangeRule : ValidationRule
    {
        public TimeRangeRule()
        {
        }

        public override ValidationResult Validate (object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Please enter a valid time.");
            }
            try
            {
                Convert.ToDateTime(value.ToString());
            }
            catch(System.FormatException)
            {

                return new ValidationResult(false, "Invalid data type.");
            }

            return new ValidationResult(true, null);
        }
    }
}
