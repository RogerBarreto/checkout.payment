namespace Checkout.Payment.Command.Domain.Models.Enums
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
