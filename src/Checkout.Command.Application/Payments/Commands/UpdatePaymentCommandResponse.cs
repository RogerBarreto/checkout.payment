using System;

namespace Checkout.Command.Application.Payments.Commands
{
	public class UpdatePaymentCommandResponse
	{
		public Guid PaymentId { get; }
		public UpdatePaymentCommandResponse(Guid paymentId)
		{
			PaymentId = paymentId;
		}
	}
}
