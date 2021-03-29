namespace Checkout.Domain.Enums
{
	public enum PaymentStatus
    {
        Processing = 1000,
        RejectedCustom = 2000,
        RejectedIncorrect = 2001,
        RejectedInsuficientFunds = 2001,
        RejectedCardBlocked = 2002,
        Unexpected = 4000,
        Succeeded = 5000
    }
}
