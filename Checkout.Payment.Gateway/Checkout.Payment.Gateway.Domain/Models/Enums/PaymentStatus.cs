using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payment.Gateway.Domain.Models.Enums
{
    public enum PaymentStatus
    {
        Failed,
        Processing,
        RejectedInsuficientFunds,
        RejectedBlocked,
        RejectedCustom,
        Succeded
    }
}
