using Checkout.Payment.Gateway.Domain.Models.Enums;
using System;

namespace Checkout.Payment.Gateway.Domain.Models
{
    public class GetPayment
    {
        public int MerchantId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
