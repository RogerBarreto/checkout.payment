using Checkout.Payment.Processor.Domain.Commands;
using Checkout.Payment.Processor.Domain.Interfaces;
using Checkout.Payment.Processor.Domain.Models.AcquiringBank;
using Checkout.Payment.Processor.Domain.Models.Notification;
using Checkout.Payment.Processor.Domain.Models.PaymentCommand;
using Checkout.Payment.Processor.Seedwork.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Domain.CommandHandlers
{
    public class PaymentCommandHandler : 
        IRequestHandler<SendBankPaymentCommand, ITryResult<SendBankPaymentCommandResponse>>,
        IRequestHandler<UpdatePaymentCommand, ITryResult>,
        IRequestHandler<ReprocessPaymentCommand, ITryResult>
    {
        private readonly IAcquiringBankHttpClientAdapter _paymentBankClient;
        private readonly IPaymentCommandHttpClientAdapter _paymentCommandClient;
        private readonly IPaymentNotifier _paymentNotifier;
        private readonly ILogger<PaymentCommandHandler> _logger;

		public PaymentCommandHandler(IPaymentCommandHttpClientAdapter paymentCommandClient, IAcquiringBankHttpClientAdapter paymentBankClient, IPaymentNotifier paymentNotifier, ILogger<PaymentCommandHandler> logger)
        {
			_paymentCommandClient = paymentCommandClient;
			_paymentBankClient = paymentBankClient;
			_paymentNotifier = paymentNotifier;
            _logger = logger;
		}

		public async Task<ITryResult<SendBankPaymentCommandResponse>> Handle(SendBankPaymentCommand command, CancellationToken cancellationToken)
		{
			var bankRequest = new BankPaymentRequest(command);
			var bankRequestResult = await _paymentBankClient.TrySendPayment(bankRequest);
			if (!bankRequestResult.Success)
			{
				return TryResult<SendBankPaymentCommandResponse>.CreateFailResult();
			}

			return TryResult<SendBankPaymentCommandResponse>.CreateSuccessResult(new SendBankPaymentCommandResponse(bankRequestResult.Result));
		}

		public async Task<ITryResult> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
		{
			var updateRequest = new UpdatePaymentRequest(command);
			var updateResult = await _paymentCommandClient.TryUpdatePayment(updateRequest);

			return TryResult.CreateFromResult(updateResult);
		}

		public async Task<ITryResult> Handle(ReprocessPaymentCommand command, CancellationToken cancellationToken)
		{
			var paymentMessage = new ReprocessPaymentMessage(command);
			
			ITryResult<string> reprocessResult = await _paymentNotifier.TryReprocessPaymentAsync(paymentMessage);
			if (reprocessResult.Success)
			{
				_logger.LogInformation($"Payment message sent for Reprocess [paymentId={paymentMessage.PaymentId}, publishedMessageId={reprocessResult.Result}]");
			}

			return TryResult.CreateFromResult(reprocessResult);
		}
	}
}
