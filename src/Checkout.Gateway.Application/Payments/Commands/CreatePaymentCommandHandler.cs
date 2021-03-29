using Checkout.Gateway.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Gateway.Application.Payments.Commands
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>
	{
		private readonly IPaymentCommandClient _paymentClient;

		public CreatePaymentCommandHandler(IPaymentCommandClient paymentClient)
		{
			_paymentClient = paymentClient;
		}

		public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
		{
			var paymentId = await _paymentClient.CreatePaymentAsync(command);

			return new CreatePaymentCommandResponse
			{
				PaymentId = paymentId
			};
		}
	}
}
