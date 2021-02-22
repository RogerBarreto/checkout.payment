using Checkout.Payment.Query.Seedwork.Extensions;
using MediatR;
using System;

namespace Checkout.Payment.Query.Domain
{
    public class GetPaymentQuery : IRequest<ITryResult<GetPaymentQueryResponse>>
    {
        public int MerchantId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
