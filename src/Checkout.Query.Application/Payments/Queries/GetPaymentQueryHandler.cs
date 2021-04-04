using Checkout.Query.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Queries;
using OneOf;

namespace Checkout.Query.Application.Payments.Queries
{
	public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>>
	{
		private readonly IPaymentRepository _repository;

		public GetPaymentQueryHandler(IPaymentRepository repository)
		{
			_repository = repository;
		}

		public async Task<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
		{
			return (await _repository.GetPaymentAsync(request))
				.Match<OneOf<GetPaymentQueryResponse, PaymentNotFound, PaymentError>>(
					payment => GetPaymentQueryResponse.CreateFrom(payment), 
					notFound => notFound,
					error => error);
		}
	}
}
