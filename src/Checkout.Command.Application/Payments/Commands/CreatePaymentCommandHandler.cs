using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Application.Common.Payments.Commands;
using Checkout.Command.Application.Common.Interfaces;
using OneOf;
using Checkout.Domain.Errors;

namespace Checkout.Command.Application.Payments.Commands
{
	public class CreatePaymentCommandHandler : 
		IRequestHandler<CreatePaymentCommand, OneOf<CreatePaymentCommandResponse, PaymentError>>,
		IRequestHandler<UpdatePaymentCommand, OneOf<UpdatePaymentCommandResponse, PaymentNotFound, PaymentError>>
	{
		private readonly IPaymentRepository _repository;

		public CreatePaymentCommandHandler(IPaymentRepository repository)
		{
			_repository = repository;
		}

		public async Task<OneOf<CreatePaymentCommandResponse, PaymentError>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
		{
			return (await _repository.CreatePaymentAsync(command)).Match<OneOf<CreatePaymentCommandResponse, PaymentError>>(
				paymentId => new CreatePaymentCommandResponse
				{
					PaymentId = paymentId
				}, 
				error => error);
		}

		public async Task<OneOf<UpdatePaymentCommandResponse, PaymentNotFound, PaymentError>> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
		{
			return (await _repository.UpdatePaymentAsync(command))
				.Match<OneOf<UpdatePaymentCommandResponse, PaymentNotFound, PaymentError>>(
					paymentId => new UpdatePaymentCommandResponse(command.PaymentId),
					notFound => notFound,
					error => error);
		}
	}
}
