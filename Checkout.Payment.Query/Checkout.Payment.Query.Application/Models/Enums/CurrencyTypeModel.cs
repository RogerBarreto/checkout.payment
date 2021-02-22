using System.Text.Json.Serialization;

namespace Checkout.Payment.Query.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyTypeModel
    {
        USD,
        EUR,
        GBP
    }
}
