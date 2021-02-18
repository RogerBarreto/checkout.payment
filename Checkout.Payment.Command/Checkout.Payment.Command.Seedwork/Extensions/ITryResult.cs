namespace Checkout.Payment.Command.Seedwork.Extensions
{
    public interface ITryResult<T> : ITryResult
    {
        public T Result { get; }
    }

    public interface ITryResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
