using Checkout.Gateway.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Commands;
using Checkout.Domain.Errors;
using OneOf;

namespace Checkout.Gateway.Application.Payments.Commands
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, OneOf<CreatePaymentCommandResponse, PaymentError>>
	{
		private readonly IPaymentCommandClient _paymentClient;

		public CreatePaymentCommandHandler(IPaymentCommandClient paymentClient)
		{
			_paymentClient = paymentClient;
		}

		public async Task<OneOf<CreatePaymentCommandResponse, PaymentError>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
		{
			return (await _paymentClient.CreatePaymentAsync(command)).Match<OneOf<CreatePaymentCommandResponse, PaymentError>>(
				paymentId => new CreatePaymentCommandResponse
				{
					PaymentId = paymentId
				}, 
				error => error);
		}
	}
}
