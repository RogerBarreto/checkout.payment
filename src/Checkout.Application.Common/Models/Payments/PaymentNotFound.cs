namespace Checkout.Application.Common.Models.Payments
{
    public struct PaymentNotFound
    {
        public string Message { get; }

        public PaymentNotFound(string message)
        {
            Message = message;
        }
    }
}