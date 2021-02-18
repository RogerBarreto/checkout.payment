namespace Checkout.Payment.Command.Seedwork.Extensions
{
    public class TryResult<T> : ITryResult<T>
    {
        public bool Success { get; }

        public string Message { get; }

        public T Result { get; private set; }
        public static TryResult<T> CreateSuccessResult(T result)
        {
            return new TryResult<T>(result, true);
        }
        public static TryResult<T> CreateFailResult(T result)
        {
            return new TryResult<T>(result, false);
        }
        public static TryResult<T> CreateFailResult(string failMessage)
        {
            return new TryResult<T>(false, failMessage);
        }
        public static TryResult<T> CreateFailResult()
        {
            return new TryResult<T>(false);
        }

        private TryResult(T result, bool success, string message = null)
        {
            Result = result;
            Success = success;

            Message = message;
        }
        private TryResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }
    }

    public class TryResult : ITryResult
    {
        public bool Success { get; }
        public string Message { get; }

        private TryResult(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }
        public static TryResult CreateSuccessResult()
        {
            return new TryResult(true);
        }
        public static TryResult CreateFailResult(string failMessage = null)
        {
            return new TryResult(false, failMessage);
        }
    }
}
