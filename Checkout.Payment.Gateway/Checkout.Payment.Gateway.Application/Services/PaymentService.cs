using Checkout.Payment.Gateway.Application.Interfaces;
using Checkout.Payment.Gateway.Application.Models;
using Checkout.Payment.Gateway.Domain.Models;
using Checkout.Payment.Gateway.Domain.Models.Enums;
using Checkout.Payment.Gateway.MicroServices.HttpClients;
using Checkout.Payment.Gateway.Seedwork.Extensions;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Gateway.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCommandHttpClientAdapter _paymentCommandClient;
        public PaymentService(IPaymentCommandHttpClientAdapter paymentCommandClient)
        {
            _paymentCommandClient = paymentCommandClient;
        }

        public async Task<ITryResult<CreatePaymentResponseModel>> TryCreatePaymentAsync(int merchantId, CreatePaymentRequestModel paymentRequestModel)
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

            var paymentResult = await _paymentCommandClient.TryCreatePayment(createPayment);
            if (!paymentResult.Success)
            {
                return TryResult<CreatePaymentResponseModel>.CreateFailResult();
            }
            return TryResult<CreatePaymentResponseModel>.CreateSuccessResult(new CreatePaymentResponseModel(paymentResult.Result.PaymentId));
        }
    }
}
