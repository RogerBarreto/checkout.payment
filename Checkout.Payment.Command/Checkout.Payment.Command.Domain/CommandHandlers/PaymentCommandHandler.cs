using Checkout.Payment.Command.Domain.Interfaces;
using Checkout.Payment.Command.Domain.Models;
using Checkout.Payment.Command.Seedwork.Extensions;
using Checkout.Payment.Command.Seedwork.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Domain.CommandHandlers
{
    public class PaymentCommandHandler : 
        IRequestHandler<CreatePaymentCommand, ITryResult<CreatePaymentCommandResponse>>,
        IRequestHandler<UpdatePaymentCommand, ITryResult<UpdatePaymentCommandResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentNotifier _paymentNotifier;
        private readonly ILogger<PaymentCommandHandler> _logger;
		private readonly IDomainNotification _bus;

		public PaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentNotifier paymentNotifier, ILogger<PaymentCommandHandler> logger, IDomainNotification bus)
        {
            _paymentRepository = paymentRepository;
            _paymentNotifier = paymentNotifier;
            _logger = logger;
			_bus = bus;
		}
        public async Task<ITryResult<CreatePaymentCommandResponse>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            ITryResult<PaymentRequest> createPayment = await _paymentRepository.TryCreatePayment(command);
            if (!createPayment.Success)
            {
                _bus.PublishError("Failed to create payment");
                return TryResult<CreatePaymentCommandResponse>.CreateFailResult();
            }

            ITryResult<string> paymentNotification = await _paymentNotifier.TryNotifyPaymentAsync(createPayment.Result);
            if (!paymentNotification.Success)
            {
                _bus.PublishError("Failed to notify the payment");

                var deletePaymentResult = await _paymentRepository.TryRemovePayment(createPayment.Result.PaymentId);
                if (!deletePaymentResult.Success)
				{
                    _logger.LogError($"Failed to rollback payment [paymentId={createPayment.Result.PaymentId}, message={deletePaymentResult.Message}]");
                }

                return TryResult<CreatePaymentCommandResponse>.CreateFailResult();
            }

            return TryResult<CreatePaymentCommandResponse>.CreateSuccessResult(new CreatePaymentCommandResponse(createPayment.Result.PaymentId));
        }

		public async Task<ITryResult<UpdatePaymentCommandResponse>> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
		{
            ITryResult<bool> updatePaymentResult = await _paymentRepository.TryUpdatePayment(command);
            if (!updatePaymentResult.Success)
            {
                _bus.PublishError("Failed to update payment");
            }

            return TryResult<UpdatePaymentCommandResponse>.CreateSuccessResult(new UpdatePaymentCommandResponse(command.PaymentId, updatePaymentResult.Result));
        }
	}
}
