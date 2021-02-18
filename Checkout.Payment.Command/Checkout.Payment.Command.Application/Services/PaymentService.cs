using Checkout.Payment.Command.Application.Interfaces;
using Checkout.Payment.Command.Application.Models;
using Checkout.Payment.Command.Domain;
using Checkout.Payment.Command.Domain.Models.Enums;
using Checkout.Payment.Command.Seedwork.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Application.Services
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

        public async Task<ITryResult<CreatePaymentResponseModel>> TryCreatePaymentAsync(int merchantId, CreatePaymentRequestModel paymentRequestModel)
        {
            var createPayment = new CreatePaymentCommand()
            {
                MerchantId = merchantId,
                Amount = paymentRequestModel.Amount,
                CardNumber = paymentRequestModel.CardNumber,
                CardCVV = paymentRequestModel.CardCVV,
                ExpiryDate = paymentRequestModel.ExpiryDate,
                CurrencyType = Enum.Parse<CurrencyType>(paymentRequestModel.CurrencyType)
            };

            var commandResponse = await _mediator.Send(createPayment);
            if (!commandResponse.Success)
            {
                TryResult<CreatePaymentResponseModel>.CreateFailResult();
            }

            return TryResult<CreatePaymentResponseModel>.CreateSuccessResult(new CreatePaymentResponseModel(commandResponse.Result.PaymentId));
        }
    }
}
