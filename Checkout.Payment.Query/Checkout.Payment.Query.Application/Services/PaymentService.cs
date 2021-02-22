using Checkout.Payment.Query.Application.Interfaces;
using Checkout.Payment.Query.Application.Models;
using Checkout.Payment.Query.Domain;
using Checkout.Payment.Query.Seedwork.Extensions;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Checkout.Payment.Query.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMediator _mediator;

        public PaymentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ITryResult<GetPaymentResponseModel>> TryGetPaymentAsync(int merchantId, Guid paymentId)
        {
            var getPaymentQuery = new GetPaymentQuery()
            {
                MerchantId = merchantId,
                PaymentId = paymentId
            };

            var querytResponse = await _mediator.Send(getPaymentQuery);
            if (!querytResponse.Success)
            {
                return TryResult<GetPaymentResponseModel>.CreateFailResult();
            }

            return TryResult<GetPaymentResponseModel>.CreateSuccessResult(new GetPaymentResponseModel(querytResponse.Result));
        }
	}
}
