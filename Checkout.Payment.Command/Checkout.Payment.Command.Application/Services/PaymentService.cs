using Checkout.Payment.Command.Application.Interfaces;
using Checkout.Payment.Command.Application.Models;
using Checkout.Payment.Command.Domain;
using Checkout.Payment.Command.Domain.Models.Enums;
using Checkout.Payment.Command.Seedwork.Extensions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Command.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMediator _mediator;

        public PaymentService(IMediator mediator)
        {
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
                CurrencyType = Enum.Parse<CurrencyType>(paymentRequestModel.CurrencyType.ToString())
            };

            var commandResponse = await _mediator.Send(createPayment);
            if (!commandResponse.Success)
            {
                return TryResult<CreatePaymentResponseModel>.CreateFailResult();
            }

            return TryResult<CreatePaymentResponseModel>.CreateSuccessResult(new CreatePaymentResponseModel(commandResponse.Result.PaymentId));
        }

		public async Task<ITryResult<bool>> TryUpdatePaymentAsync(Guid paymentId, UpdatePaymentRequestModel requestModel)
		{
            var updateCommand = new UpdatePaymentCommand()
            {
                PaymentId = paymentId,
                BankPaymentId = requestModel.BankPaymentId,
                PaymentStatus = Enum.Parse<PaymentStatus>(requestModel.PaymentStatus.ToString()),
                PaymentStatusDetails = requestModel.PaymentStatusDetails
            };

            var commandResponse = await _mediator.Send(updateCommand);
            if (!commandResponse.Success)
            {
                return TryResult<bool>.CreateFailResult();
            }

            return TryResult<bool>.CreateSuccessResult(commandResponse.Result.UpdateSucessful);
        }
	}
}
