using System.Text.Json.Serialization;

namespace Checkout.Payment.Gateway.Domain.Models.Enums
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
