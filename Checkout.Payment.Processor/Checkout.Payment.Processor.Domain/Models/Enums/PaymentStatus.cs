namespace Checkout.Payment.Processor.Domain.Models.Enums
{
    public enum PaymentStatus
    {
        Processing,
        RejectedInsuficientFunds,
        RejectedBlocked,
        RejectedCustom,
        Succeded
    }
}
