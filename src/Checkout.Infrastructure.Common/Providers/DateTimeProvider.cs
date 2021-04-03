using System;
using Checkout.Domain.Common;

namespace Checkout.Infrastructure.Common.Providers
{
    public class DateTimeProvider : IDateTime
    {
        public DateTime Value => DateTime.Now;
    }
}