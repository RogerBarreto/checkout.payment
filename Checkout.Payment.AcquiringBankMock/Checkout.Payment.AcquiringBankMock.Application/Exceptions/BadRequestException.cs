using System;

namespace Checkout.Payment.AcquiringBankMock.Application.Exceptions
{
	public class BadRequestException : ApplicationException
	{
		public BadRequestException(string message) : base(message) { }
	}
}
