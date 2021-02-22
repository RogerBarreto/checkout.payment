using System.Text.Json.Serialization;

namespace Checkout.Payment.Query.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Processing,
        RejectedIncorrect,
        RejectedInsuficientFunds,
        RejectedCardBlocked,
        RejectedCustom,
        Succeeded,
        Unexpected
    }
}
