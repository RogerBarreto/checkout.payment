using Checkout.Gateway.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Gateway.Application.Payments.Errors;
using OneOf;

namespace Checkout.Gateway.Application.Payments.Queries
{
	public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>>
	{
		private readonly IPaymentQueryClient _paymentClient;

		public GetPaymentQueryHandler(IPaymentQueryClient paymentClient)
		{
			_paymentClient = paymentClient;
		}

		public async Task<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
		{
			return (await _paymentClient.GetPaymentAsync(request))
				.Match<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>>(
					payment => payment, 
					notFound => notFound,
					error => error);
		}
	}
}
