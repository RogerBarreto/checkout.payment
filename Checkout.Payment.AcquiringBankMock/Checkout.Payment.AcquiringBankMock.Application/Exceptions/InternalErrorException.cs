using System;

namespace Checkout.Payment.AcquiringBankMock.Application.Exceptions
{
	public class InternalErrorException : ApplicationException
	{
		public InternalErrorException(string message) : base(message) { }
	}
}
