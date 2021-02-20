using Checkout.Payment.Command.Application.Models.Enums;
using System;

namespace Checkout.Payment.Command.Application.Models
{
    public class UpdatePaymentResponseModel
    {
		public Guid PaymentId { get; }
		public UpdatePaymentResponseModel(Guid paymentId)
		{
			PaymentId = paymentId;
		}
	}
}
