using MediatR;
using System;
using Checkout.Domain.Errors;
using OneOf;

namespace Checkout.Query.Application.Payments.Queries
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
