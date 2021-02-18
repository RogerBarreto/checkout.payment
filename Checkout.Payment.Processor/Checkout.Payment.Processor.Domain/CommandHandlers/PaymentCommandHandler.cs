using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.Domain.Models;
using Checkout.Payment.Processor.Seedwork.Extensions;
using Checkout.Payment.Processor.Seedwork.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Domain.CommandHandlers
{
    public class PaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ITryResult<CreatePaymentCommandResponse>>
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
            ITryResult<PaymentMessage> createPayment = await _paymentRepository.TryCreatePayment(command);
            if (!createPayment.Success)
            {
                _bus.PublishError("Failed to create a payment");
            }

            ITryResult<string> paymentNotification = await _paymentNotifier.TryNotifyPaymentAsync(createPayment.Result);
            if (!paymentNotification.Success)
            {
                _bus.PublishError("Failed to notify the payment");
                _logger.LogError($"Failed to notify the payment [message={paymentNotification.Message}]");

                var deletePaymentResult = await _paymentRepository.TryRemovePayment(createPayment.Result.PaymentId);
                if (!deletePaymentResult.Success)
				{
                    _logger.LogError($"Failed to rollback payment [paymentId={createPayment.Result.PaymentId}, message={deletePaymentResult.Message}]");
                }
            }

            _logger.LogInformation($"Published message [paymentId={createPayment.Result.PaymentId}, publishedMessageId={paymentNotification.Result}]");
            return TryResult<CreatePaymentCommandResponse>.CreateSuccessResult(new CreatePaymentCommandResponse(createPayment.Result.PaymentId));
        }
    }
}
