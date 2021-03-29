using MediatR;
using System;

namespace Checkout.Gateway.Application.Payments.Queries
{
	public class GetPaymentQuery : IRequest<GetPaymentQueryResponse>
	{
		public Guid PaymentId { get; set; }
	}
}
