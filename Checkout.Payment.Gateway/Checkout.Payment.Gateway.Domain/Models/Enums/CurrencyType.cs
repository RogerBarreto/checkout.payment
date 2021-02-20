using System.Text.Json.Serialization;

namespace Checkout.Payment.Gateway.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyType
    {
        USD,
        EUR,
        GBP
    }
}
