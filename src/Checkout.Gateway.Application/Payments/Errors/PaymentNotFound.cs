namespace Checkout.Gateway.Application.Payments.Errors
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