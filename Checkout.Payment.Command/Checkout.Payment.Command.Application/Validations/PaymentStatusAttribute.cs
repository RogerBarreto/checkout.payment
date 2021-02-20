using Checkout.Payment.Command.Application.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.Command.Application.Validations
{
    public class PaymentStatusAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Enum.TryParse(value.ToString(), out CurrencyTypeModel result);
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = "Invalid Payment [currency={0}{1}]";
            }

            return base.FormatErrorMessage(name);
        }
    }
}
