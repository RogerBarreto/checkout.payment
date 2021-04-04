using MediatR;
using System;
using OneOf;

namespace Checkout.Application.Common.Models.Payments.Queries
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
