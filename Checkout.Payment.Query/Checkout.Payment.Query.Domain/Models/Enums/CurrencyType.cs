using System.Text.Json.Serialization;

namespace Checkout.Payment.Query.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyType
    {
        USD,
        EUR,
        GBP
    }
}
