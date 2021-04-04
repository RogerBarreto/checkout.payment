using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Application.Common.Models.Payments;
using Checkout.Application.Common.Models.Payments.Commands;
using Checkout.Application.Common.Models.Shared;
using Checkout.Command.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Checkout.Command.Application.Payments.Commands
{
	public class PaymentCommandHandler : 
		IRequestHandler<CreatePaymentCommand, OneOf<CreatePaymentCommandResponse, PaymentError>>,
		IRequestHandler<UpdatePaymentCommand, OneOf<UpdatePaymentCommandResponse, PaymentNotFound, PaymentError>>
	{
		private readonly IPaymentRepository _repository;
		private readonly ILogger<PaymentCommandHandler> _logger;
		private readonly IPaymentNotifier _notifier;
		
		public PaymentCommandHandler(IPaymentRepository repository, IPaymentNotifier notifier,  ILogger<PaymentCommandHandler> logger)
		{
			_repository = repository;
			_notifier = notifier;
			_logger = logger;
		}

		public async Task<OneOf<CreatePaymentCommandResponse, PaymentError>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
		{
			var paymentResult =  (await _repository.CreatePaymentAsync(command)).Match<OneOf<CreatePaymentCommandResponse, PaymentError>>(
				paymentId => new CreatePaymentCommandResponse
				{
					PaymentId = paymentId
				}, 
				paymentError => paymentError);

			if (paymentResult.Value is PaymentError error)
			{
				return error;
			}

			var createdPayment = paymentResult.Value as CreatePaymentCommandResponse;
			
			return await NotifyCreatedPayment(command, createdPayment);
		}

		private async Task<OneOf<CreatePaymentCommandResponse, PaymentError>> NotifyCreatedPayment(CreatePaymentCommand command, CreatePaymentCommandResponse createdPayment)
		{
			var notificationResult = await _notifier.NotifyCreatePaymentAsync(command);
			if (notificationResult.Value is NotificationError)
			{
				_logger.LogError($"Failed to notify the payment [paymentId={createdPayment.PaymentId}]");

				await RollbackCreatedPayment(createdPayment);

				return new PaymentError("Failed to request the payment, Try again");
			}

			return createdPayment;
		}

		private async Task RollbackCreatedPayment(CreatePaymentCommandResponse createdPayment)
		{
			var deletePaymentResult = await _repository.DeletePaymentAsync(createdPayment.PaymentId);

			if (deletePaymentResult.Value is PaymentError deleteError)
			{
				_logger.LogError($"Failed to rollback payment [paymentId={createdPayment.PaymentId}, message={deleteError.Message}]");
			}
			else
			{
				_logger.LogInformation($"Payment rolled back [paymentId={createdPayment.PaymentId}]");
			}
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
