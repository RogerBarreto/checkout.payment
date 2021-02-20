using Checkout.Payment.Processor.Application.Interfaces;
using Checkout.Payment.Processor.Application.Models;
using Checkout.Payment.Processor.Application.Models.Enums;
using Checkout.Payment.Processor.Domain;
using Checkout.Payment.Processor.Domain.Commands;
using Checkout.Payment.Processor.Domain.Models.Enums;
using Checkout.Payment.Processor.Seedwork.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Processor.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IMediator _mediator;

        public PaymentService(ILogger<PaymentService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private async Task<ITryResult> TryProcessingPaymentAsync(PaymentMessageRequestModel message)
		{
            var sendBankPaymentCommand = new SendBankPaymentCommand
            {
                PaymentId = message.PaymentId,
                Amount = message.Amount,
                BankPaymentId = message.BankPaymentId,
                CardCVV = message.CardCVV,
                CardNumber = message.CardNumber,
                CurrencyType = Enum.Parse<CurrencyType>(message.CurrencyType),
                ExpiryDate = message.ExpiryDate
            };

            var bankResult = await _mediator.Send(sendBankPaymentCommand);
            if (!bankResult.Success)
            {
                var reprocessCommand = new ReprocessPaymentCommand
                {
                    Amount = message.Amount,
                    CardCVV = message.CardCVV,
                    CardNumber = message.CardNumber,
                    CurrencyType = Enum.Parse<CurrencyType>(message.CurrencyType),
                    ExpiryDate = message.ExpiryDate,
                    PaymentId = message.PaymentId,
                    PaymentStatus = Enum.Parse<PaymentStatus>(message.PaymentStatus.ToString())
                };

                var reprocessResult = await _mediator.Send(reprocessCommand);
                if (!reprocessResult.Success)
                {
                    return TryResult.CreateFailResult();
                }

                return TryResult.CreateSuccessResult();
            }

            var updatePaymentCommand = new UpdatePaymentCommand
            {
                PaymentId = message.PaymentId,
                BankPaymentId = bankResult.Result.BankPaymentId,
                PaymentStatus = bankResult.Result.PaymentStatus,
                PaymentStatusDetails = bankResult.Result.PaymentStatusDetails
            };

            return await TryProcessedPaymentAsync(message, updatePaymentCommand);
        }

        private async Task<ITryResult> TryProcessedPaymentAsync(PaymentMessageRequestModel message, UpdatePaymentCommand updatePaymentCommand)
		{
            var updateResult = await _mediator.Send(updatePaymentCommand);
            if (!updateResult.Success)
            {
                var reprocessCommand = new ReprocessPaymentCommand
                {
                    Amount = message.Amount,
                    CardCVV = message.CardCVV,
                    CardNumber = message.CardNumber,
                    CurrencyType = Enum.Parse<CurrencyType>(message.CurrencyType),
                    ExpiryDate = message.ExpiryDate,
                    PaymentId = message.PaymentId,
                    PaymentStatus = updatePaymentCommand.PaymentStatus,
                    BankPaymentId = updatePaymentCommand.BankPaymentId,
                    PaymentStatusDetails = updatePaymentCommand.PaymentStatusDetails
                };

                var reprocessResult = await _mediator.Send(reprocessCommand);
                if (!reprocessResult.Success)
                {
                    return TryResult.CreateFailResult();
                }
            }
            return TryResult.CreateSuccessResult();
        }


        public async Task<ITryResult> TryProcessPaymentAsync(PaymentMessageRequestModel message)
		{

            if (message.PaymentStatus == PaymentStatusModel.Processing)
            {
                return await TryProcessingPaymentAsync(message);
            }

            var updatePaymentCommand = new UpdatePaymentCommand
            {
                PaymentId = message.PaymentId,
                BankPaymentId = message.BankPaymentId,
                PaymentStatus = Enum.Parse<PaymentStatus>(message.PaymentStatus.ToString()),
                PaymentStatusDetails = message.PaymentStatusDetails
            };

            return await TryProcessedPaymentAsync(message, updatePaymentCommand);
		}
	}
}
