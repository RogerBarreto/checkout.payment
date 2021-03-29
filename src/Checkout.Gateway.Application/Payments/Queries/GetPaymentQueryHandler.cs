using Checkout.Gateway.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Domain.Entities;

namespace Checkout.Gateway.Application.Payments.Queries
{
	public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, GetPaymentQueryResponse>
	{
		private readonly IPaymentQueryClient _paymentClient;

		public GetPaymentQueryHandler(IPaymentQueryClient paymentClient)
		{
			_paymentClient = paymentClient;
		}

		public async Task<GetPaymentQueryResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
		{
			Payment paymentDetails = await _paymentClient.GetPaymentAsync(request.PaymentId);

			return GetPaymentQueryResponse.CreateFrom(paymentDetails);
		}
	}
}
