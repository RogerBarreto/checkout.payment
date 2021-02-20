using System.Text.Json.Serialization;

namespace Checkout.Payment.Command.Application.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CurrencyTypeModel
    {
        USD,
        EUR,
        GBP
    }
}
