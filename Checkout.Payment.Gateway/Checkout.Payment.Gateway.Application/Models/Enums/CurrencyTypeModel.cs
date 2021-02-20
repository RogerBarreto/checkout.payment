using System.Text.Json.Serialization;

namespace Checkout.Payment.Gateway.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyTypeModel
    {
        USD,
        EUR,
        GBP
    }
}
