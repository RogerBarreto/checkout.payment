using System.Text.Json.Serialization;

namespace Checkout.Payment.Command.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyType
    {
        USD,
        EUR,
        GBP
    }
}
