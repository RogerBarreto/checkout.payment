using Checkout.Payment.Command.Application.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Command.Application.Validations
{
    public class CurrencyTypeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Currency is required");
            }

            if (Enum.TryParse(value.ToString(), out CurrencyTypeModel result))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Currency {value} is invalid");
        }
    }
}
