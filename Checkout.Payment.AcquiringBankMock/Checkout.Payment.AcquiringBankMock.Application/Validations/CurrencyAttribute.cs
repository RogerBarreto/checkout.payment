using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.AcquiringBankMock.Application.Validations
{
    public class CurrencyAttribute : ValidationAttribute
    {
        enum ValidCurrencies
		{
            USD,
            EUR,
            GBP
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Currency is required");
            }

            if (Enum.TryParse(value.ToString(), out ValidCurrencies result)) 
            {
                return ValidationResult.Success;
			}

            return new ValidationResult($"Currency {value} is invalid");
        }
    }
}
