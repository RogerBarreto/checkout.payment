using Checkout.Payment.Gateway.Application.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Gateway.Application.Validations
{
    public class CurrencyTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Enum.TryParse(value.ToString(), out CurrencyTypeModel result);
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "Invalid Currency [currency={0}]";
            }

            return base.FormatErrorMessage(name);
        }
    }
}
