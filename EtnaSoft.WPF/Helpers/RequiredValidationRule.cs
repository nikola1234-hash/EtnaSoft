using System;
using System.Globalization;
using System.Windows.Controls;

namespace EtnaSoft.WPF.Helpers
{
    public class RequiredValidationRule : ValidationRule
    {
        public static string GetErrorMessage(string fieldName, object fieldValue, object nullValue = null)
        {
            string errorMessage = String.Empty;
            if (nullValue != null && nullValue.Equals(fieldValue))
            {
                errorMessage = String.Format("Polje {0} ne sme biti prazno!", fieldName);
            }

            if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
            {
                errorMessage = String.Format("Polje {0} ne sme biti prazno!", fieldName);
            }

            return errorMessage;
        }


        public string FieldName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string error = GetErrorMessage(FieldName, value);
            if (!string.IsNullOrEmpty(FieldName))
            {
                return new ValidationResult(false, error);
            }
            return ValidationResult.ValidResult;
        }
    }
}
