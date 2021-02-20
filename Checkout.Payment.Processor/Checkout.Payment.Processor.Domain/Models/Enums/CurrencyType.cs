using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Checkout.Payment.Processor.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyType
    {
        USD,
        EUR,
        GBP
    }
}
