using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Payment.Gateway.Seedwork.Extensions
{
    public interface ITryResult<T>
    {
        bool Success { get; }
        string Message { get; }
        public T Result { get; }
    }
}
