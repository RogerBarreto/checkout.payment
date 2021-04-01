namespace Checkout.Domain.Errors
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