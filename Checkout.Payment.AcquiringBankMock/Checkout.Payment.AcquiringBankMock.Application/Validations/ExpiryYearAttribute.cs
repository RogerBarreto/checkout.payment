using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.Payment.AcquiringBankMock.Application.Validations
{
	public class ExpiryYearAttribute : RangeAttribute
	{
		public ExpiryYearAttribute(int yearsToExpire = 20) : base(DateTime.Now.Year, DateTime.Now.Year + yearsToExpire) {}
	}
}
