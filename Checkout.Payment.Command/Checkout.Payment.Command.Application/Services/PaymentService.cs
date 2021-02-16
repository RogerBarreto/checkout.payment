using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Application.Models;
using Checkout.Payment.Gateway.Domain;
using Checkout.Payment.Gateway.Domain.Models.Enums;
using Checkout.Payment.Gateway.MicroServices.HttpClients;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IPaymentCommandHttpClientAdapter _paymentCommandClient;
        public PaymentService(ILogger<PaymentService> logger, IPaymentCommandHttpClientAdapter paymentCommandClient)
        {
            _paymentCommandClient = paymentCommandClient;
            _logger = logger;
        }

        public async Task<CreatePaymentResponseModel> CreatePaymentAsync(int merchantId, CreatePaymentRequestModel paymentRequestModel)
        {
            var createPayment = new CreatePayment()
            {
                MerchantId = merchantId,
                Amount = paymentRequestModel.Amount,
                CardNumber = paymentRequestModel.CardNumber,
                CardCVV = paymentRequestModel.CardCVV,
                ExpiryDate = paymentRequestModel.ExpiryDate,
                CurrencyType = Enum.Parse<CurrencyType>(paymentRequestModel.CurrencyType)
            };

            var paymentId = await _paymentCommandClient.CreatePayment(createPayment);

            return new CreatePaymentResponseModel(paymentId);
        }
    }
}
