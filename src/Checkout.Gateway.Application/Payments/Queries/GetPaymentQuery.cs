using MediatR;
using System;
using Checkout.Gateway.Application.Payments.Errors;
using OneOf;

namespace Checkout.Gateway.Application.Payments.Queries
{
	public class GetPaymentQuery : IRequest<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>>
	{
		public Guid PaymentId { get; }
		public int MerchantId { get; }

		public GetPaymentQuery(Guid paymentId, int merchantId)
		{
			PaymentId = paymentId;
			MerchantId = merchantId;
		}
	}
}
